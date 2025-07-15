#!/system/bin/sh

# 配置源目录和目标目录（可根据需要修改）
SRC_DIR=$1   # 替换为你的源目录路径
DST_DIR=$2    # 替换为你的目标目录路径

echo "<<---------iwphone_sync----------->>" >> log.txt

# 检查源目录是否存在
if [ ! -d "$SRC_DIR" ]; then
    echo "错误：源目录 $SRC_DIR 不存在" >> log.txt
    exit 1
fi

# 确保目标目录父级存在（自动创建）
mkdir -p "$(dirname "$DST_DIR")"

# 函数：同步文件（复制/更新）
sync_files() {
    # 遍历源目录所有文件
    find "$SRC_DIR" -type f | while read -r src_file; do
        # 转换为目标文件路径（保持目录结构）
        dst_file="${DST_DIR}/${src_file#$SRC_DIR/}"
        dst_dir=$(dirname "$dst_file")
        
        # 创建目标目录（如果不存在）
        if [ ! -d "$dst_dir" ]; then
            mkdir -p "$dst_dir"
            echo "创建目录：$dst_dir" >> log.txt
        fi
        
        # 获取时间戳（使用toybox date命令，兼容安卓）
        src_mtime=$(date -r "$src_file" +%s 2>/dev/null)
        dst_mtime=0
        
        # 检查目标文件是否存在并获取其时间戳
        if [ -e "$dst_file" ]; then
            dst_mtime=$(date -r "$dst_file" +%s 2>/dev/null)
        fi
        
        # 仅当源文件更新时复制
        if [ "$src_mtime" -gt "$dst_mtime" ]; then
            cp -f "$src_file" "$dst_file"
            echo "更新文件：$src_file -> $dst_file" >> log.txt
        fi
    done
}

# 函数：删除目标中多余的文件
clean_extra_files() {
    # 遍历目标目录所有文件
    find "$DST_DIR" -type f | while read -r dst_file; do
        # 转换为源文件对应路径
        src_file="${SRC_DIR}/${dst_file#$DST_DIR/}"
        
        # 源不存在则删除目标文件
        if [ ! -e "$src_file" ]; then
            rm "$dst_file"
            echo "删除多余文件：$dst_file" >> log.txt
        fi
    done
}

# 函数：删除目标中多余的空目录
clean_empty_dirs() {
    # 按深度从大到小排序目录（先删深层空目录）
    find "$DST_DIR" -type d | sort -r | while read -r dst_dir; do
        # 转换为源目录对应路径
        src_dir="${SRC_DIR}/${dst_dir#$DST_DIR/}"
        
        # 情况1：源目录不存在 → 彻底删除目标目录
        if [ ! -d "$src_dir" ]; then
            if rmdir "$dst_dir" 2>/dev/null; then
                echo "删除多余目录：$dst_dir" >> log.txt
            fi
        # 情况2：源目录存在但目标目录为空 → 删除目标空目录
        elif [ -z "$(ls -A "$dst_dir")" ]; then
            rmdir "$dst_dir"
            echo "删除空目录：$dst_dir" >> log.txt
        fi
    done
}

# 执行同步流程
echo "开始同步：$SRC_DIR → $DST_DIR" >> log.txt
sync_files
clean_extra_files
clean_empty_dirs
echo "同步完成" >> log.txt


echo "<<---------iwphone_sync----------->>" >> log.txt