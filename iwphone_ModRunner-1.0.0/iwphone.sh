#!/system/bin/sh

# ==================== 配置区 ====================
TMP_DIR="./tmp"              # 模块触发文件目录（如：/tmp/login 触发login模块）
WORK_DIR="./work"            # 模块解压根目录（需按模块名存放main.sh）
LOG_ROOT_DIR="/sdcard/iwphone/logs"  # 日志根目录（所有模块日志存放于此）
MAX_PARALLEL=0              # 最大并行数（0=无限制）
AUTO_CLEAN=1                # 是否自动清理触发文件（1=是）
VERBOSE=1                   # 是否输出详细进度（1=是）
LOCK_DIR="/sdcard/iwphone/locks" # 模块锁文件目录（存放各模块.lock文件）
PID_FILE="./module_runner.pid"  # 脚本自身PID记录文件
# ================================================

# ---------------------- 工具函数 ----------------------
log() {
    local msg="$1"
    local timestamp=$(date "+%Y-%m-%d %H:%M:%S")
    echo "[$timestamp] $msg" >> "${LOG_ROOT_DIR}/module_runner_$(date +%s).log"
    [ "$VERBOSE" -eq 1 ] && echo "$msg"
}

module_log() {
    local module="$1"
    local msg="$2"
    local timestamp=$(date "+%Y-%m-%d %H:%M:%S")
    # 模块日志路径调整为：LOG_ROOT_DIR/modules/{module}/{module}.log
    local module_log_file="${LOG_ROOT_DIR}/modules/${module}/${module}.log"
    # 自动创建模块日志目录（若不存在）
    mkdir -p "$(dirname "$module_log_file")" || { log "❌ 失败：无法创建模块日志目录 $module_log_file" && exit 1; }
    echo "[$timestamp] [$module] $msg" >> "$module_log_file"
}

validate_module() {
    local module="$1"
    echo "🔍 校验模块名：$module"  # 调试日志：确认模块名被正确接收
    if [ -z "$module" ]; then
        log "❌ 拒绝非法模块名：空模块名"
        return 1
    fi
    if echo "$module" | grep -q '[[:space:]]'; then
        log "❌ 拒绝非法模块名：'$module'（包含空格）"
        return 1
    fi
    if echo "$module" | grep -q '\.\./'; then
        log "❌ 拒绝非法模块名：'$module'（包含路径遍历../）"
        return 1
    fi
    if echo "$module" | grep -q '/'; then
        log "❌ 拒绝非法模块名：'$module'（包含子目录/）"
        return 1
    fi
    return 0
}

check_file() {
    local path="$1"
    if [ ! -e "$path" ]; then
        log "❌ 路径不存在：$path"
        return 1
    fi
    if [ ! -f "$path" ]; then
        log "❌ 不是普通文件：$path"
        return 1
    fi
    return 0
}

# 创建锁文件目录（兼容首次运行）
create_lock_dir() {
    mkdir -p "$LOCK_DIR" || { log "❌ 失败：无法创建锁目录 $LOCK_DIR" && exit 1; }
}

# 检查模块是否已加锁（存在锁文件且进程未退出）
is_module_locked() {
    local module="$1"
    local lock_file="${LOCK_DIR}/${module}.lock"
    
    # 锁文件不存在：未加锁
    if [ ! -f "$lock_file" ]; then
        return 1  # 未锁定
    fi

    # 锁文件存在，检查对应进程是否存活
    local pid=$(cat "$lock_file")
    if ps | grep -q "^$pid$"; then
        return 0  # 进程存活，已锁定
    else
        # 进程已退出，清理残留锁文件
        rm -f "$lock_file" && log "🧹 清理残留锁文件：$lock_file"
        return 1  # 未锁定
    fi
}

# 为模块加锁（创建锁文件并记录PID）
lock_module() {
    local module="$1"
    local lock_file="${LOCK_DIR}/${module}.lock"
    local pid=$$
    
    # 写入当前PID到锁文件
    echo "$pid" > "$lock_file" || { log "❌ 失败：无法为模块 $module 加锁" && exit 1; }
    log "🔒 模块 $module 已加锁（PID：$pid）"
}

# 为模块解锁（删除锁文件）
unlock_module() {
    local module="$1"
    local lock_file="${LOCK_DIR}/${module}.lock"
    
    if [ -f "$lock_file" ]; then
        rm -f "$lock_file" && log "🔓 模块 $module 已解锁"
    fi
}

# 记录脚本自身PID到文件（用于外部停止）
record_script_pid() {
    echo $$ > "$PID_FILE" || { log "❌ 失败：无法记录脚本PID到 $PID_FILE" && exit 1; }
    log "📝 脚本PID已记录：$PID_FILE"
}

# 清理所有子进程（模块执行进程）
cleanup_subprocesses() {
    log "🔄 开始清理子进程..."
    # 遍历记录的子进程PID并终止
    for pid in $pids; do
        if ps | grep -q "^$pid$"; then
            log "⏹️ 终止子进程：$pid"
            kill "$pid" 2>/dev/null
            wait "$pid" 2>/dev/null  # 等待进程退出
        fi
    done
    pids=""  # 清空PID列表
    log "✅ 所有子进程已清理"
}

# 优雅退出（处理SIGINT/SIGTERM）
graceful_exit() {
    log "📢 收到终止信号，开始清理..."
    cleanup_subprocesses  # 清理子进程
    unlock_all_modules    # 解锁所有模块（可选）
    rm -f "$PID_FILE"     # 清理脚本PID文件
    log "👋 脚本已退出"
    exit 0
}

# 解锁所有模块（防止残留锁文件）
unlock_all_modules() {
    log "🔄 开始解锁所有模块..."
    # 查找所有模块锁文件并清理
    find "$LOCK_DIR" -maxdepth 1 -type f -name "*.lock" | while read -r lock_file; do
        module=$(basename "$lock_file" .lock)
        rm -f "$lock_file" && log "🔓 解锁模块：$module"
    done
    log "✅ 所有模块已解锁"
}
# ------------------------------------------------------

# ---------------------- 主逻辑 ----------------------
main() {
    # 初始化目录（含日志根目录和模块日志子目录）
    mkdir -p "$LOG_ROOT_DIR" || { log "❌ 失败：无法创建日志根目录 $LOG_ROOT_DIR" && exit 1; }
    create_lock_dir       # 创建锁文件目录
    record_script_pid     # 记录脚本自身PID

    # 注册信号处理（捕获Ctrl+C或外部kill命令）
    trap 'graceful_exit' SIGINT SIGTERM

    log "===== 日志根目录初始化完成：$LOG_ROOT_DIR ====="

    log "===== 开始扫描模块触发文件 ====="
    local modules=""  # 存储模块名的字符串（如："login 1 2 3"）
    local tmp_file="./tmp_modules.list"  # 临时文件存储find结果

    # 替换进程替换为临时文件（兼容Ash/Dash）
    find "$TMP_DIR" -maxdepth 1 -type f -print0 > "$tmp_file"  # 将find结果写入临时文件

    # 读取临时文件中的模块名
    while IFS= read -r -d '' tmp_file_entry; do
        module_name=$(basename "$tmp_file_entry")
        if validate_module "$module_name"; then
            modules="$modules $module_name"  # 在父shell中拼接模块名
            [ "$VERBOSE" -eq 1 ] && log "🔍 发现有效模块：$module_name"
        else
            log "⏭️ 跳过非法模块：$module_name"
        fi
    done < "$tmp_file"  # 从临时文件读取内容

    # 清理临时文件
    rm -f "$tmp_file"

    log "🔍 收集到的模块名：$modules"  # 调试日志：确认模块名被正确收集

    if [ -z "$modules" ]; then
        log "ℹ️ 无有效模块需要执行"
        log "===== 模块执行结束 $(date) ====="
        exit 0
    fi

    log "🚀 开始并行执行模块（共$(echo "$modules" | wc -w)个）"
    local pids=""  # 存储后台进程PID的字符串

    # 关键修复：使用for循环分割模块名（兼容Ash/Dash）
    for module in $modules; do  # 注意：无引号，允许空格分割
        # 控制最大并行数
        while [ "$MAX_PARALLEL" -gt 0 ] && [ $(echo "$pids" | wc -w) -ge "$MAX_PARALLEL" ]; do
            wait -n
            local exit_code=$?
            [ "$VERBOSE" -eq 1 ] && log "🔄 进程槽位释放（剩余并行数：$(echo "$pids" | wc -w)）"
        done

        # 检查模块是否已加锁（避免重复执行）
        if is_module_locked "$module"; then
            log "⏭️ 模块 '$module' 正在执行，跳过本次触发"
            continue
        fi

        target_main="${WORK_DIR}/${module}/main.sh"  # 模块main.sh绝对路径
        if ! check_file "$target_main"; then
            log "⏭️ 跳过模块 '$module'（main.sh不存在或格式错误）"
            continue
        fi

        log "▶️ 准备执行模块 '$module'（目录：${WORK_DIR}/${module}）"
        
        # 新增：切换到模块目录并执行main.sh（子shell中完成）
        (
            # 切换到模块目录
            local target_dir="${WORK_DIR}/${module}"
            log "▶️ 尝试切换到模块目录：$target_dir"
            if ! cd "$target_dir" 2>/dev/null; then
                log "❌ 切换目录失败：模块目录不存在或无权限（$target_dir）"
                unlock_module "$module"  # 异常时解锁
                return 1  # 子shell中return会终止当前后台进程
            fi
            log "✅ 成功切换到模块目录：$(pwd)"

            # 执行模块前加锁
            lock_module "$module"

            # 执行模块main.sh（使用相对路径或绝对路径均可）
            module_log "$module" "===== 模块执行开始 $(date) ====="
            sh "./main.sh" >> "${LOG_ROOT_DIR}/modules/${module}/${module}.out" 2>> "${LOG_ROOT_DIR}/modules/${module}/${module}.err"  # 输出到模块日志目录
            local exit_code=$?
            if [ $exit_code -eq 0 ]; then
                module_log "$module" "✅ 模块执行成功（退出码：$exit_code）"
            else
                module_log "$module" "❌ 模块执行失败（退出码：$exit_code）"
            fi
            module_log "$module" "===== 模块执行结束 $(date) ====="

            # 执行完成后解锁
            unlock_module "$module"
        ) &
        pids="$pids $!"
        log "当前模块""$module""的pid：$!"
    done

    log "⏳ 等待所有模块执行完成..."
    wait
    log "✅ 所有模块执行完成"

    # 清理触发文件（可选）
    if [ "$AUTO_CLEAN" -eq 1 ]; then
        for module in $modules; do  # 无引号，允许空格分割
            tmp_file="/tmp/$module"
            [ -f "$tmp_file" ] && rm -f "$tmp_file" && log "🧹 清理触发文件：$tmp_file"
        done
    fi

    # 汇总结果
    local success=0
    local failed=0
    for module in $modules; do  # 无引号，允许空格分割
        target_main="${WORK_DIR}/${module}/main.sh"
        # 检查模块日志是否存在成功标记
        if [ -f "${LOG_ROOT_DIR}/modules/${module}/${module}.out" ] && grep -q "✅ 模块执行成功" "${LOG_ROOT_DIR}/modules/${module}/${module}.log"; then
            success=$((success+1))
        else
            failed=$((failed+1))
        fi
    done
    log "📊 执行结果：成功 $success 个，失败 $failed 个"

    log "===== 模块执行结束 $(date) ====="
}
# ------------------------------------------------------

main

exit 0
