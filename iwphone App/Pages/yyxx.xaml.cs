using Microsoft.Win32;
using System;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace iwphone.Pages
{
    /// <summary>
    /// yyxx.xaml 的交互逻辑
    /// </summary>
    public partial class yyxx : Window
    {
        private string appNames;
        private string package;
        private string versionCode;
        private string versionName;
        private string apkPath;
      
        public yyxx(string appNames, string package, string versionCode, string versionName, string apkPath)
        {
            InitializeComponent();
            //MessageBox.Show(appNames, "应用程序信息");
            appname.Text = appNames;
            packages.Text = package;
            versioncodes.Text = versionCode;
            versionnames.Text = versionName;
            apkpaths.Text = apkPath;

            Sidebar.Visibility = Visibility.Collapsed;


         
            string destinationDirectory = Directory.GetCurrentDirectory(); // 获取当前目录
            string destinationFilePath = Path.Combine(destinationDirectory, "WallpaperImagee_copy.jpg"); // 目标文件路径

            if (File.Exists(destinationFilePath))
            {
                // 创建BitmapImage对象并设置图片路径
                BitmapImage bitmap = new BitmapImage(new Uri(destinationFilePath/*, UriKind.Relative*/));


                //Console.WriteLine("当前壁纸路径: " + bitmap);

                // 将BitmapImage对象赋值给Image控件的Source属性
                WallpaperImage.Source = bitmap;
            }
           
        }
        Point _pressedPosition;
        bool _isDragMoved = false;


        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void Window_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _pressedPosition = e.GetPosition(this);
        }
        void Window_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && _pressedPosition != e.GetPosition(this))
            {
                _isDragMoved = true;
                DragMove();
            }
        }
        void Window_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragMoved)
            {
                _isDragMoved = false;
                e.Handled = true;
            }
        }

        private void HideSidebar_Click(object sender, RoutedEventArgs e)
        {
            Sidebar.Visibility = Visibility.Collapsed;
          
        }

        private void ShowSidebar_Click(object sender, RoutedEventArgs e)
        {
            Sidebar.Visibility = Visibility.Visible;
        }

        private void yygl_Click(object sender, MouseButtonEventArgs e)
        {
            Sidebar.Visibility = Visibility.Visible;
            title.Text = "应用管理";
            gn1.Text = "保留数据安装不同签名的apk";
            gn2.Text = "保留数据降级安装apk";
            zgn.Text = "卸载";

        }

        private void gbbutton(object sender, RoutedEventArgs e)
        {
            Sidebar.Visibility = Visibility.Collapsed;
         
        }

        private void gnbutton(object sender, RoutedEventArgs e)
        {
            if (title.Text == "应用管理")
            {
                if (gnc1.IsChecked == true)
                {
                    if (gnc2.IsChecked == true)
                    {
                        MessageBox.Show("不能同时选择两种功能！");
                    }
                    else
                    {
                        string file = xzapk();
                        MessageBoxResult result = MessageBox.Show("即将卸载软件当前版本并安装目标软件apk，你确定吗？\n部分系统需要请先检查是否打开开发者选项中的“允许usb安装应用”，并且在一会的操作过程中可能在手机弹窗通知安装apk，请点击同意\n目标apk：" + file, "卸载应用", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                package = packages.Text;
                            
                           
                                break;
                            case MessageBoxResult.No:
                                MessageBox.Show("已取消操作!", "成功");
                                break;

                        }

                       
                    }
                }
                else
                {
                    if (gnc2.IsChecked == true)
                    {
                        string files = xzapk();
                        MessageBoxResult result = MessageBox.Show("即将卸载软件当前版本并安装目标软件apk，你确定吗？\n部分系统需要请先检查是否打开开发者选项中的“允许usb安装应用”，并且在一会的操作过程中可能在手机弹窗通知安装apk，请点击同意\n下方的“是”和“否”分别对应两种模式，只有“取消”按钮才是退出，不要乱选\n目标apk：" + files, "卸载应用", MessageBoxButton.YesNoCancel);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                MessageBoxResult resultA = MessageBox.Show("模式一\n谷歌官方降级方式，比较安全，但部分设备不可使用", "卸载", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                                switch (resultA)
                                {
                                    case MessageBoxResult.Yes:
                                       
                                        break;
                                    case MessageBoxResult.No:
                                        MessageBox.Show("已取消卸载!", "成功");
                                        break;
                                }
                                break;
                            case MessageBoxResult.No:
                                //string file = xzapk();
                                MessageBoxResult resultS = MessageBox.Show("模式二\n另类降级方式，中途会卸载原apk，可能会造成严重后果\n部分系统需要请先检查是否打开开发者选项中的“允许usb安装应用”，并且在一会的操作过程中可能在手机弹窗通知安装apk，请点击同意\n目标apk：" + files, "卸载应用", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                                switch (resultS)
                                {
                                    case MessageBoxResult.Yes:
                                        package = packages.Text;
                                       
                                      
                                        break;
                                    case MessageBoxResult.No:
                                        MessageBox.Show("已取消操作!", "成功");
                                        break;

                                }
                                break;
                            case MessageBoxResult.Cancel:
                                MessageBox.Show("已取消操作！", "卸载");
                                break;
                        }
                        //string file = xzapk();
                        //MessageBox.Show(file);
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("你没有选择任何扩展功能，默认将直接卸载该软件，你确定吗？", "卸载应用" ,MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                package = packages.Text;
                              
                                MessageBox.Show("卸载成功!", "成功");
                                break;
                            case MessageBoxResult.No:
                                MessageBox.Show("已取消卸载!", "成功");
                                break;
                          
                        }
                    }

                }
            }

            if (title.Text == "状态管理")
            {
                if (gnc1.IsChecked == true)
                {
                    if (gnc2.IsChecked == true)
                    {
                        MessageBox.Show("不能同时选择两种功能！");
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("即将冻结" + appNames + "，你确定吗？", "管理应用", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                package = packages.Text;
                               
                                MessageBox.Show("冻结成功!", "成功");
                                break;
                            case MessageBoxResult.No:
                                MessageBox.Show("已取消冻结!", "成功");
                                break;

                        }

                    }
                }
                else
                {
                    if (gnc2.IsChecked == true)
                    {
                        MessageBoxResult result = MessageBox.Show("即将解除冻结" + appNames + "，你确定吗？", "管理应用", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                package = packages.Text;
                                
                                MessageBox.Show("解除冻结成功!", "成功");
                                break;
                            case MessageBoxResult.No:
                                MessageBox.Show("已取消解除冻结!", "成功");
                                break;

                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("你还没有选择任何功能！");
            }
       }

        public static string xzapk()
        {
            MessageBox.Show("请选择你要安装的apk文件");
            // 实例化一个文件选择对象
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".apk";  // 设置默认类型
                                         // 设置可选格式
            dialog.Filter = @"apk文件(*.apk)|*.apk";
            // 打开选择框选择
            Nullable<bool> result = dialog.ShowDialog();
           
           string file = dialog.FileName; // 获取选择的文件名
               
           return file;
           
        }

        private void ztgl_Click(object sender, MouseButtonEventArgs e)
        {
            Sidebar.Visibility = Visibility.Visible;
            title.Text = "状态管理";
            gn1.Text = "冻结";
            gn2.Text = "解冻";
            zgn.Text = "开始";
        }

        private void gjgl_Click(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void root_Click(object sender, MouseButtonEventArgs e)
        {
            Sidebar.Visibility = Visibility.Visible;
            title.Text = "root提权";
            gn1.Text = "开发中";
            gn2.Text = "开发中";
            zgn.Text = "开始";
        }
    }

}
