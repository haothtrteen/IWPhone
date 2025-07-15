#!/bin/bash
# log="/log"
iwphone="/data/local/tmp/iwphone"
#iwphone=$pwd"/1"

#set -eo pipefail
#shopt -s failglob  # 启用严格模式

if [ ! -d $iwphone ]; then 
  mkdir -p $iwphone; 
fi; 
cd $iwphone

work=$(pwd)

echo "<<-----------iwphone_start------------->>" >> log.txt
echo "<<-----------iwphone_start------------->>"
echo "启动目录："$work

cd 
# pwd

# 确保脚本持续运行
main() {
while true
do

if [ -f "end" ]; then
  echo "<<---------close iwphone_server----------->>" >> log.txt
  exit
else
  echo "<<---------iwphone_server----------->>" >> log.txt
    # 在每次循环中执行一些任务，例如运行一个程序
    echo "当前shell进程pid："$$ >> $work"/log/log.txt"
    echo "当前shell进程pid："$$
    
    #同步状态
    syncs

    # 使用 sleep 命令让脚本暂停一段时间（例如，60秒）
    sleep 60
    
fi        
        
done


}

syncs(){
    if [ -f "firststart" ]; then
    
       if [ -f "/sdcard/iwphone/reupdate" ]; then
         echo "开始同步目录"$iwphone" > /sdcard/iwphone" >> $work"/log/log.txt"
         echo "开始重新同步目录"$iwphone" > /sdcard/iwphone"
         sh sync.sh $iwphone "/sdcard/iwphone/"
         echo "您现在可以在 /sdcard/iwphone 下看到工作目录状态了"      
         rm /sdcard/iwphone/reupdate
           else
         echo "iwphone power"
        
       fi  
        
      if [ -f "/sdcard/iwphone/readbsh" ]; then      
        echo "即将执行来自sd目录新更新的shell"      
        echo "sh /sdcard/iwphone/adbsh/main.sh"
        iwsh
        rm /sdcard/iwphone/readbsh       
        else
        echo "iwphone power"
      fi
      
       if [ -f "/sdcard/iwphone/rebooti" ]; then      
        echo "注意！即将重启所有进程！"              
        rebooti
        rm /sdcard/iwphone/rebooti       
        else
        echo "iwphone power"
      fi
        
      else
         echo "开始同步目录"$iwphone" > /sdcard/iwphone" >> $work"/log/log.txt"
         echo "开始同步目录"$iwphone" > /sdcard/iwphone"
         sh sync.sh $iwphone "/sdcard/iwphone/"
         echo "您现在可以在 /sdcard/iwphone 下看到工作目录状态了"
         echo "success" > firststart   
     fi     
     
}


iwsh(){
echo "开始同步目录 /sdcard/iwphone/adbsh/ > "$iwphone"/adbsh" >> $work"/log/log.txt"
echo "同步目录 /sdcard/iwphone/adbsh/ > "$iwphone"/adbsh"
sh sync.sh "/sdcard/iwphone/adbsh/" $iwphone"/adbsh/"
chmod -R 777 $iwphone"/adbsh/"
sh $iwphone"/adbsh/main.sh"
}


rebooti(){
sh stop_runner.sh
sleep 5
sh iwphone.sh
}



main