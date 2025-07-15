#!/system/bin/sh

# 读取脚本PID文件
SCRIPT_PID=$(cat ./module_runner.pid 2>/dev/null)

if [ -z "$SCRIPT_PID" ]; then
    echo "❌ 未找到进程PID文件"
    exit 1
fi

# 终止脚本进程及其子进程
echo "⏹️ 正在终止进程（PID：$SCRIPT_PID）..."
kill "$SCRIPT_PID" 2>/dev/null
wait "$SCRIPT_PID" 2>/dev/null

# 清理残留PID文件
rm -f ./module_runner.pid
echo "✅ 脚本已停止"
