#!/system/bin/sh

# 配置参数（根据实际路径修改）
A_DIR=$1  # 源目录a（需替换为你的路径）
B_DIR=$2  # 目标目录b（需替换为你的路径）

# 临时文件前缀（避免冲突）
TMP_PREFIX=".sync_tmp_"

# 清理临时文件（确保脚本退出时执行）
cleanup() {
    rm -f "${A_DIR}/${TMP_PREFIX}"* 2>/dev/null
    rm -f "${B_DIR}/${TMP_PREFIX}"* 2>/dev/null
}
trap cleanup EXIT

# 生成目录文件的MD5列表（相对路径|MD5），直接输出到标准输出（不依赖临时文件）
generate_md5() {
    local base_dir="$1"
    # 遍历所有文件（处理特殊字符），计算相对路径和MD5
    find "$base_dir" -type f -print0 2>/dev/null | while IFS= read -r -d '' file; do
        rel_path="${file#$base_dir}"  # 关键修正：直接截取base_dir后的路径（避免末尾斜杠问题）
        md5=$(md5sum "$file" 2>/dev/null | awk '{print $1}')  # 忽略无权限文件
        printf "%s|%s\n" "$rel_path" "$md5"
    done
}

# 主同步逻辑（a→b）
main() {
    # 检查目录是否存在
    [ ! -d "$A_DIR" ] && echo "错误：目录a不存在：$A_DIR" && exit 1
    [ ! -d "$B_DIR" ] && echo "错误：目录b不存在：$B_DIR" && exit 1

    echo "===== 开始同步（a→b） ====="
    echo "同步时间：$(date)"

    # 生成a和b的实时MD5列表（不依赖临时文件，直接通过管道处理）
    echo "生成a目录MD5列表..."
    a_md5=$(generate_md5 "$A_DIR")
    echo "生成b目录MD5列表..."
    b_md5=$(generate_md5 "$B_DIR")

    # 临时保存a的MD5列表（用于删除阶段对比）
    a_md5_tmp=$(mktemp -p "$A_DIR" "${TMP_PREFIX}a_md5.XXXXXX")
    echo "$a_md5" > "$a_md5_tmp"

    # 处理a→b的新增/修改
    echo "处理a→b同步（新增/修改）..."
    while IFS='|' read -r a_rel a_md5; do
        [ -z "$a_rel" ] && continue  # 跳过空行
        
        a_file="${A_DIR}${a_rel}"  # 完整路径（因rel_path已包含相对路径，无需拼接/）
        b_file="${B_DIR}${a_rel}"

        # 情况1：b中无此文件 → 新增
        if [ ! -f "$b_file" ]; then
            echo "新增：$b_file"
            mkdir -p "$(dirname "$b_file")" && cp -af "$a_file" "$b_file"
        # 情况2：b中有但MD5不同 → 修改
        else
            b_current_md5=$(grep -m1 "^${a_rel}|" "$b_md5" | cut -d '|' -f 2)
            if [ "$a_md5" != "$b_current_md5" ]; then
                echo "修改：$b_file"
                mkdir -p "$(dirname "$b_file")" && cp -af "$a_file" "$b_file"
            fi
        fi
    done <<< "$a_md5"

    # 处理a中已删除但b中仍存在的文件（反向同步删除）
    echo "处理a→b同步（删除冗余）..."
    while IFS='|' read -r b_rel b_md5; do
        [ -z "$b_rel" ] && continue
        
        # 检查a当前是否无此文件（通过a的MD5列表判断）
        if ! grep -q "^${b_rel}|" "$a_md5_tmp"; then
            b_file="${B_DIR}${b_rel}"
            echo "删除：$b_file（a中已不存在）"
            rm -f "$b_file"  # 忽略无权限文件
        fi
    done <<< "$b_md5"

    # 清理临时MD5文件
    rm -f "$a_md5_tmp"
    echo "===== 同步完成 ====="
}

# 执行主逻辑
main
