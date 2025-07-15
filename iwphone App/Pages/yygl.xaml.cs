//using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;



namespace iwphone.Pages
{
    /// <summary>
    /// yygl.xaml 的交互逻辑
    /// </summary>
    public partial class yygl : Window
    {
        private List<AppInfo> appList;
        private List<AppInfo> filteredAppList;
      
        public yygl()
        {
            InitializeComponent();
            appList = new List<AppInfo>();
            filteredAppList = new List<AppInfo>();
            LoadAppList();
            lstApps.ItemsSource = filteredAppList;
            txtSearch_TextChanged(null, null);
            notifier.ShowInformation("双击列表以查看应用状态(部分系统应用可能无法正常显示信息）");
        }

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(10),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        private void LoadAppList()
        {
         
        }

        private string ExtractApkPath(string line)
        {
            Match match = Regex.Match(line, "package:(.*?)==/base.apk");
            if (match.Success)
            {
                string path = match.Groups[1].Value;
                path = path.Trim();
                path += "==/base.apk"; // 添加"==/base.apk"部分
                return path;
            }
            else
            {
                return string.Empty;
            }
        }

        private string ExtractValue(string input, string pattern)
        {
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(input, pattern);
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return string.Empty;
            }
        }

        private void txtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //    string searchText = txtSearch.Text.ToLower();
            //    filteredAppList = appList.Where(app => app.AppName.ToLower().Contains(searchText) || app.PackageName.ToLower().Contains(searchText)).ToList();
            //    lstApps.ItemsSource = filteredAppList;
            string keyword = txtSearch.Text.ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                lstApps.ItemsSource = appList; // allApps 是包含所有应用程序的集合
            }
            else
            {
                var filteredApps = appList.Where(app => app.AppName.ToLower().Contains(keyword)).ToList();
                lstApps.ItemsSource = filteredApps;
            }
        }




        private object _lastSelectedItem;
        private int _lastSelectedIndex;


        private void lstApps_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            AppInfo selectedApp = lstApps.SelectedItem as AppInfo;
            // 获取当前选中的项

            if (selectedApp != null)
            {
                AppInfo app = (AppInfo)lstApps.SelectedItem;
                string appName = app.AppName;
                string apkPath = app.ApkPath;
                //MessageBox.Show(apkPath);

                string aapt = GetAPKInfo(apkPath);

                // 解析应用程序信息
                string package = ExtractValue(aapt.ToString(), "package: name='(.*?)'");
                string versionCode = ExtractValue(aapt.ToString(), "versionCode='(.*?)'");
                string versionName = ExtractValue(aapt.ToString(), "versionName='(.*?)'");
                string sdkVersion = ExtractValue(aapt.ToString(), "sdkVersion:'(.*?)'");
                string targetSdkVersion = ExtractValue(aapt.ToString(), "targetSdkVersion:'(.*?)'");
                string appNames = ExtractValue(aapt, "application-label:'(.*?)'");

                // 显示应用程序信息
                string message = $"应用程序名称（app name）：{appNames}\n" +
                                 $"包名（package name）：{package}\n" +
                                 $"版本号（version code）：{versionCode}\n" +
                                 $"版本名称（version name）：{versionName}\n" +
                                 $"SDK版本（sdk version）：{sdkVersion}\n" +
                                 $"目标SDK版本（target SDK version）：{targetSdkVersion}\n";




                var selectedItem = lstApps.SelectedItem;
                var selectedIndex = lstApps.SelectedIndex;

                if (selectedItem != null && selectedIndex != -1)
                {
                    if (selectedItem == _lastSelectedItem && selectedIndex == _lastSelectedIndex)
                    {
                        // 重新选中了同一个项目
                        // 执行某操作
                        // 双击事件处理  
                        yyxx yyxx = new yyxx(appNames, package, versionCode, versionName, apkPath);
                        yyxx.Show();
                    }
                    else
                    {
                        // 选中了不同的项目
                        // 更新上一次选中的项
                        // 单击事件处理
                        //new ToastContentBuilder()
                        //   .AddArgument("AAPT action", "viewConversation")
                        //   .AddArgument("conversationId", 9813)
                        //   .AddText("应用管理")
                        //  .AddText("双击以查看 " + appNames + " 的应用状态")
                        //   ;
                     
                        lstApps.SelectedIndex = 1;
                    }
                }
                else
                {
                    // 没有项目被选中
                    // ...
                    lstApps.SelectedIndex = 1;
                }

                _lastSelectedItem = selectedItem;
                _lastSelectedIndex = selectedIndex;
            
       
                  
             
               





                AddNewDataToItems(appNames);
                //appNameListBox.Items.Add(appNames);


                //MessageBox.Show(appName);
                //MessageBox.Show(aapt);
            }
        }

        //private void CheckInput(ToastNotificationActivatedEventArgsCompat toastArgs, string appNames, string package, string versionCode, string versionName, string apkPath)
        //{
        //    //MessageBox.Show(toastArgs.Argument);

        //    string toastContent = toastArgs.Argument;

        //    // 使用正则表达式匹配Toast通知的内容
        //    Match match = Regex.Match(toastContent, "action=([^;]+)");

        //    // 如果匹配成功，则执行相应的操作
        //    if (match.Success)
        //    {
        //        string action = match.Groups[1].Value;

        //        //MessageBox.Show(action);
        //        if (action == "yes")
        //        {
        //            yyxx yyxx = new yyxx(appNames, package, versionCode, versionName, apkPath);
        //            yyxx;

        //            // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
        //            //new ToastContentBuilder()
        //            //    .AddArgument("action", "viewConversation")
        //            //    .AddArgument("conversationId", 9813)
        //            //    .AddText("爱玩机扩展程序")
        //            //    .AddText("安装aapt程序成功！")
        //            //    ; // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater
        //            ////MessageBox.Show(outaapptp + outaapptc, "成功");
        //        }
        //        else if (action == "no")
        //        {
        //            // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
        //            //new ToastContentBuilder()
        //            //    .AddArgument("action", "viewConversation")
        //            //    .AddArgument("conversationId", 9813)
        //            //    .AddText("爱玩机扩展安装程序")
        //            //    .AddText("已取消安装aapt程序！")
        //            //    ; // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater
        //        }

        //    }
        //    else
        //    {
        //        // 如果匹配失败，则执行默认操作
        //        //defaultAction(0);
        //        //MessageBox.Show(toastContent);
        //    }
        //}

            private void AddNewDataToItems(string names)
        {
            txtNewData.Text = "应用名称：" + names;            
            
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null)
                        return childItem;
                }
            }
            return null;
        }

        private string GetAPKInfo(string apkPath)
        {
            Process process = new Process();
            string adbpatch = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);


            string AdbPath = adbpatch + "\\adb\\adb.exe shell \"/data/local/tmp/aapt ";

            //执行CMD命令
            process.StartInfo.FileName = "cmd.exe";
            //'/C'是启动参数不更改，devices可以改为其他命令
            process.StartInfo.Arguments = "/C " + AdbPath + "dump badging \"" + apkPath + "\"";
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            //MessageBox.Show("/C " + AdbPath + "dump badging " + apkPath + "\"");
            //Console.WriteLine("/C " + AdbPath + "dump badging " + apkPath + "\"");
            try
            {
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return output;
            }
            catch (Exception ex)
            {
                return "Error extracting APK info: " + ex.Message;
            }
        }


        //public static string ExtractIconPath(string apkPath)
        //{
        //    Process process = new Process();
        //    string adbpatch = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);


        //    string AdbPath = adbpatch + "\\adb\\adb.exe shell \"/data/local/tmp/aapt-arm-pie ";

        //    //执行CMD命令
        //    process.StartInfo.FileName = "cmd.exe";
        //    //'/C'是启动参数不更改，devices可以改为其他命令
        //    process.StartInfo.Arguments = "/C " + AdbPath + "dump badging \"" + apkPath + "\"  | grep \"application-icon\"";
        //    process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
        //    process.StartInfo.UseShellExecute = false;
        //    process.StartInfo.RedirectStandardOutput = true;
        //    process.StartInfo.CreateNoWindow = true;

        //    process.Start();
        //    string output = process.StandardOutput.ReadToEnd();
        //    process.WaitForExit();

        //    string iconPath = GetIconPath(output);

        //    //MessageBox.Show(apkPath);
        //    //MessageBox.Show(output);
        //    //MessageBox.Show(iconPath);

        //    return iconPath;
        //}



        //private static string GetIconPath(string aaptOutput)
        //{
        //    string[] lines = aaptOutput.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

        //    foreach (string line in lines)
        //    {
        //        if (line.StartsWith("application-icon") && line.Contains(":"))
        //        {
        //            string[] parts = line.Split(':');
        //            string iconPath = parts[1].Trim('\'');

        //            if (iconPath.Contains("640"))
        //            {
        //                return iconPath;
        //            }
        //            else if (iconPath.Contains("480"))
        //            {
        //                return iconPath;
        //            }
        //            // 可以根据需要添加其他分辨率的判断条件

        //            // 如果没有符合条件的分辨率图标，则返回默认图标路径
        //            return iconPath;
        //        }
        //    }

        //    return null;
        //}


        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnwall_Click(object sender, RoutedEventArgs e)
        {
            //new ToastContentBuilder()
            //    .AddArgument("wall action", "viewConversation")
            //    .AddArgument("conversationId", 9813)
            //    .AddText("爱玩机工具箱")
            //    .AddText("执行成功")
            //    .AddText("通过系统注册表获取本设备的壁纸以用来当做“应用详细”界面的背景图片，请重启本程序并且不要反复执行本功能")
            //    ; // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater

            

            string wallpaperPath = GetDesktopWallpaper();
            //Console.WriteLine("当前壁纸路径: " + wallpaperPath);
            notifier.ShowSuccess("通过系统注册表获取本设备的壁纸以用来当做“应用详细”界面的背景图片，请重启本程序并且不要反复执行本功能"+ "\n当前壁纸路径: " + wallpaperPath);

            string sourceFilePath = wallpaperPath; // 源文件路径
            string destinationDirectory = Directory.GetCurrentDirectory(); // 获取当前目录
            string destinationFilePath = Path.Combine(destinationDirectory, "WallpaperImagee_copy.jpg"); // 目标文件路径

            // 复制文件
            File.Copy(sourceFilePath, destinationFilePath, true); // overwrite 为 true 表示如果目标文件已存在，则覆盖它
        }


        static string GetDesktopWallpaper()
        {
            // 访问系统的注册表
            var registry = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop");
            if (registry != null)
            {
                var wallpaper = registry.GetValue("Wallpaper") as string;
                if (wallpaper != null && wallpaper.Length > 0)
                {
                    return wallpaper;
                }
            }
            return "未设置壁纸";
        }

    }

    public class AppInfo
    {
        public string PackageName { get; set; }
        public string AppName { get; set; }
        public string ApkPath { get; internal set; }
    }
}

 

