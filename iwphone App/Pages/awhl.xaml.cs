//using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace iwphone.Pages
{
    /// <summary>
    /// awhl.xaml 的交互逻辑
    /// </summary>
    public partial class awhl : Window
    {
        public awhl()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        Point _pressedPosition;
        bool _isDragMoved = false;
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


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Topmost = true;
            btnMenu.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, btnMenu));

            loadscrcpy(sender, e);
        }

          private void rdHome_Click(object sender, RoutedEventArgs e)
        {
            //string phone = " shell input keyevent KEYCODE_HOME";
            //string koutputNumber = MainWindow.Cmdadb(phone);
            //UpdateOutput(koutputNumber);

        }

        private void rdSounds_Click(object sender, RoutedEventArgs e)
        {
            //new ToastContentBuilder()
            //      .AddArgument("scrcpy2 action", "viewConversation")
            //      .AddArgument("conversationId", 9913)
            //      .AddText("传输文件")
            //      .AddText("只需要把要传输的文件拖到投屏窗口,即可把文件推送到 /sdcard/Download 下载目录")
            //       .GetToastContent(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater

            UpdateOutput("只需要把要传输的文件拖到投屏窗口,即可把文件推送到 /sdcard/Download 下载目录");
        }

        private void rdNotes_Click(object sender, RoutedEventArgs e)
        {
            //string phone = " shell dumpsys activity";
            //string koutputNumber = MainWindow.Cmdadb(phone);
            //UpdateOutput(koutputNumber);
        }

        private void rdPayment_Click(object sender, RoutedEventArgs e)
        {

        }

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
            KillProcessTree(); // 窗口关闭时终止进程
            Close();
        }


        private Process _runningProcess; // 存储进程引用
        private void loadscrcpy(object sender, RoutedEventArgs e)
        {


            string scrcpypatch = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);


            string scrcpyPath = scrcpypatch + "\\scrcpy\\scrcpy-console.bat";


            //string PatCh = scrcpypatch + "\\scrcpy\\scrcpy.exe";
            // ...原有进程启动代码...
          

            var scrcpyprocess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = scrcpyPath,
                    WorkingDirectory = Path.GetDirectoryName(scrcpyPath),
                    UseShellExecute = false,          // 必须设置为false才能重定向流
                    CreateNoWindow = true,            // 不创建新窗口
                    RedirectStandardOutput = true,    // 重定向标准输出
                    RedirectStandardError = true      // 重定向错误输出
                },
                EnableRaisingEvents = true
            };
            _runningProcess = scrcpyprocess; // 保存进程实例

            // 输出数据处理
            scrcpyprocess.OutputDataReceived += (s, args) => UpdateOutput(args.Data);
            scrcpyprocess.ErrorDataReceived += (s, args) => UpdateOutput($"ERROR: {args.Data}");

            scrcpyprocess.Exited += (s, args) =>
            {
                UpdateOutput($"\nscrcpyprocess exited with code {scrcpyprocess.ExitCode}");
                scrcpyprocess.Dispose();
            };

            try
            {
                scrcpyprocess.Start();

                // 开始异步读取输出
                scrcpyprocess.BeginOutputReadLine();
                scrcpyprocess.BeginErrorReadLine();
            }
            catch (Exception ex)
            {
                UpdateOutput($"Error: {ex.Message}");
            }
        }

        private void UpdateOutput(string message)
        {
            // 使用Dispatcher确保线程安全
            Dispatcher.Invoke(() =>
            {
                if (!string.IsNullOrEmpty(message))
                {
                    OutputTextBox.AppendText($"{message}\n");
                    OutputTextBox.ScrollToEnd();
                }
            });
        }

        private void KillProcessTree()
        {
            try
            {
                if (_runningProcess != null && !_runningProcess.HasExited)
                {
                    // 使用Windows命令终止整个进程树
                     var killer = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = "cmd.exe",
                            Arguments = $"/c taskkill /F /T /PID {_runningProcess.Id}",
                            CreateNoWindow = true,
                            UseShellExecute = false
                        }
                    };
                    killer.Start();
                    killer.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"终止进程失败: {ex.Message}");
            }
        }


    }
}
