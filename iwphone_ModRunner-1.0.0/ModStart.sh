modules=$1

workd="/data/local/tmp/iwphone"
cd $workd

work=$(pwd)
echo "当前工作目录为"$work

modfiles=$work"/module/"
modwork=$work"/work/"



echo "<<------iwphone_server mod-------->>" >> log.txt
echo "正在安装模块"$modules >> log.txt

#ls $modfiles


unzip $modfiles$modules -d $modwork$modules