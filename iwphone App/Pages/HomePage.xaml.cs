using iwphone;
using iwphone.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIKitTutorials.Pages
{
    /// <summary>
    /// Lógica de interacción para HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        //private void yygl(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("123");
        //}

        private void yyglbutton(object sender, MouseButtonEventArgs e)
        {
          
        }

        private void adbbutton(object sender, MouseButtonEventArgs e)
        {
         



        }


        private async void awhlbutton(object sender, MouseButtonEventArgs e)
        {

            //MainWindow MainWindow = new MainWindow();
            //MainWindow.cjms();
            //string phone = " devices";
            //string outputNumber = MainWindow.Cmdadb(phone);
            //if (outputNumber.Split('\n').Any(line => line.Trim().EndsWith("device") && !line.Contains("List of devices attached")))
            //{
                //new ToastContentBuilder()
                //  .AddArgument("scrcpy action", "viewConversation")
                //  .AddArgument("conversationId", 9813)
                //  .AddText("爱玩互联")
                //  .AddText("爱玩互联是基于开源项目Scrcpy（https://github.com/Genymobile/scrcpy）的扩展程序，此应用程序镜像通过 USB 或通过 TCP/IP的方式连接到 Android 设备（视频和音频），并允许控制 带有计算机键盘和鼠标的设备。")
                //  ; // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater

                awhl awhl = new awhl();
                awhl.Show();

                //await Task.Run(() =>
                //{
                //    using (Process process = new Process())
                //    {
                //        string scrcpypatch = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);


                //        string scrcpyPath = scrcpypatch + "\\scrcpy\\scrcpy.exe";

                //        //执行CMD命令
                //        process.StartInfo.FileName = scrcpyPath;


                //        //string PatCh = scrcpypatch + "\\scrcpy\\scrcpy.exe";

                //        ////执行CMD命令
                //        //process.StartInfo.FileName = "cmd.exe";
                //        ////'/C'是启动参数不更改，devices可以改为其他命令
                //        //process.StartInfo.Arguments = "/C " + PatCh + " --push-target /sdcard";

                //        //IntPtr windowHandle = FindWindow(null, "scrcpy"); // 将该窗口设置为顶层窗口 
                //        //SetWindowPos(windowHandle, new IntPtr(-1), 0, 0, 0, 0, 0x0001); // SWP_NOOWNERZORDER | SWP_NOSIZE | SWP_NOMOVE


                //        //// 设置进程要执行的命令和参数
                //        //ProcessStartInfo startInfo = new ProcessStartInfo();
                //        //startInfo.FileName = adbPath; // adb工具的完整路径
                //        //startInfo.Arguments = "devices"; // adb命令参数
                //        //startInfo.RedirectStandardOutput = true; // 重定向标准输出
                //        //startInfo.UseShellExecute = false; // 不使用操作系统外壳程序启动进程
                //        process.StartInfo.CreateNoWindow = true; // 不创建窗口
                //                                                 //设置输出可写入变量
                //        process.StartInfo.UseShellExecute = false;
                //        process.StartInfo.RedirectStandardOutput = true;
                //        process.StartInfo.StandardOutputEncoding = Encoding.UTF8;

                //        process.Start();

                //        //传出变量output
                //        string output = process.StandardOutput.ReadToEnd();

                //    }
                //});
            //}
            //else
            //{
            //    //new ToastContentBuilder().AddArgument("action", "viewConversation").AddArgument("conversationId", 9813).AddText("注意").AddText("当前没有任何设备连接");

            //}

            //MessageBox.Show("?");
        }




        private void glqdbutton(object sender, MouseButtonEventArgs e)
        {



            // int conversationId = 384929;

            // // Construct the content
            // var builder = new ToastContentBuilder()
            //     .AddArgument("conversationId", conversationId)
            //     .AddArgument("Action", "viewConversation")
            //     .AddText("即将安装安卓设备驱动")
            //     .AddText("爱玩机需要在当前电脑设备中安装安卓设备驱动，驱动安装程序由爱玩机开发组提供，单击确认以继续安装程序。。。")
            //// Text box for replying
            ////.AddInputTextBox("我同意", placeHolderContent: "输入“我同意”以便向您的设备安装新功能拓展")
            //// Buttons
            //.AddButton(new ToastButton()
            //.SetContent("确认")
            //.AddArgument("adbqd action", "adbqd yes")
            //.SetBackgroundActivation())

            //  .AddButton(new ToastButton()
            //.SetContent("取消")
            //.AddArgument("adbqd action", "adbqd no")
            //.SetBackgroundActivation());


            //builder.Show();

            using (Process process = new Process())
            {

                string batpatch = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);


                string BATPath = batpatch + "\\driver\\main.bat";
                //Console.WriteLine(BATPath);

                //执行
                process.StartInfo.FileName = BATPath;



                process.StartInfo.CreateNoWindow = true; // 窗口
                                                         //process.StartInfo.StandardOutputEncoding = Encoding.UTF8;

                //设置输出可写入变量
                process.StartInfo.UseShellExecute = true;
                //process.StartInfo.RedirectStandardOutput = true;

                process.StartInfo.Verb = "runas";

                process.Start();

                //传出变量output
                //string output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();

                //process.Close();
            }



        }



    }
}
