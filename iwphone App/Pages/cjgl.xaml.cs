using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace iwphone.Pages
{
    /// <summary>
    /// cjgl.xaml 的交互逻辑
    /// </summary>
    public partial class cjgl : Page
    {
        public cjgl()
        {
            InitializeComponent();
            DataContext = this; // 绑定数据上下文
        }

        private void factoryfiles4(object sender, RoutedEventArgs e)
        {
            //new ToastContentBuilder()
            //  .AddArgument("scrcpy action", "viewConversation")
            //  .AddArgument("conversationId", 9813)
            //  .AddText("插件")
            //  .AddText("插件详细界面")

              ; // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater

            cjjz cjjz = new cjjz();
            cjjz.Show();
        }

        private void factoryfiles3(object sender, RoutedEventArgs e)
        {
            //new ToastContentBuilder()
            //  .AddArgument("scrcpy action", "viewConversation")
            //  .AddArgument("conversationId", 9813)
            //  .AddText("插件")
            //  .AddText("插件详细界面")

              ; // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater

            //cjjz cjjz = new cjjz();
            //cjjz;
        }

        private void factoryfiles2(object sender, RoutedEventArgs e)
        {
            //new ToastContentBuilder()
            //  .AddArgument("scrcpy action", "viewConversation")
            //  .AddArgument("conversationId", 9813)
            //  .AddText("插件")

            //  .AddText("插件详细界面")

              ; // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater

            //cjjz cjjz = new cjjz();
            //cjjz;
        }

        private void factoryfiles1(object sender, RoutedEventArgs e)
        {
            //new ToastContentBuilder()
            //     .AddArgument("scrcpy action", "viewConversation")
            //     .AddArgument("conversationId", 9813)
            //     .AddText("插件")
            //     .AddText("插件详细界面")

                 ; // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater

            //cjjz cjjz = new cjjz();
            //cjjz;

        }

        private void ymodbutton(object sender, MouseButtonEventArgs e)
        {

            cjsc page2Window = new cjsc(); // Window2 包含 Page2
            page2Window.Show(); // 非模态窗口
                                // 或 page2Window.ShowDialog(); // 模态窗口

        }


        
            // 定义一个可绑定的属性（用于存储开关状态）
            private bool _isNightMode;
            public bool IsNightMode
            {
                get { return _isNightMode; }
                set
                {
                    _isNightMode = value;
                    OnPropertyChanged(); // 通知UI更新
                    UpdateStatusLabel();   // 更新状态显示
                }
            }

         
             
            

            // 开关选中事件（夜间模式开启）
            private void NightModeToggle_Checked(object sender, RoutedEventArgs e)
            {
                // 这里可以添加实际逻辑，比如切换界面主题
                MessageBox.Show("夜间模式已开启！");
            }

            // 开关未选中事件（夜间模式关闭）
            private void NightModeToggle_Unchecked(object sender, RoutedEventArgs e)
            {
                // 这里可以添加实际逻辑，比如恢复默认主题
                MessageBox.Show("夜间模式已关闭！");
            }

            // 更新状态显示
            private void UpdateStatusLabel()
            {
                StatusLabel.Content = $"当前：{(IsNightMode ? "夜间" : "日间")}模式";
            }

            // 实现INotifyPropertyChanged接口（用于数据绑定）
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
       

 }
}
