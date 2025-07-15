using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace iwphone
{
    /// <summary>
    /// Windowtoast.xaml 的交互逻辑
    /// </summary>
    public partial class Windowtoast : Window
    {
        public Windowtoast()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(string), typeof(Windowtoast), new PropertyMetadata(""));

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(Windowtoast), new PropertyMetadata(""));

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(Windowtoast), new PropertyMetadata(null));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        private DispatcherTimer _timer;

        public void Show(int duration = 3000)
        {
            Visibility = Visibility.Visible;

            // 设置自动关闭计时器
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(duration) };
            _timer.Tick += (sender, args) => { Hide(); _timer.Stop(); };
            _timer.Start();
        }

        public void Hide()
        {
            var sb = new Storyboard();
            var animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            Storyboard.SetTarget(animation, this);
            Storyboard.SetTargetProperty(animation, new PropertyPath("Opacity"));
            sb.Children.Add(animation);
            sb.Completed += (sender, args) => Visibility = Visibility.Collapsed;
            sb.Begin();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            _timer?.Stop();


        }

        // ToastManager.cs
        public class ToastManager
        {
            private readonly Grid _container;
            private readonly StackPanel _toastPanel;

            public ToastManager(Grid container)
            {
                _container = container;

                // 创建 Toast 容器（右下角对齐）
                _toastPanel = new StackPanel { Orientation = Orientation.Vertical, HorizontalAlignment = HorizontalAlignment.Right, VerticalAlignment = VerticalAlignment.Bottom };
                _container.Children.Add(_toastPanel);
            }

            public StackPanel Get_toastPanel()
            {
                return _toastPanel;
            }

            public void ShowToast(string title, string message, StackPanel _toastPanel, ImageSource icon = null, int duration = 3000)
            {
                var toast = new Windowtoast
                {
                    Title = title,
                    Message = message,
                    Icon = icon
                };
                //_toastPanel.Children.Add(toast);
                toast.Show(duration);

                // 自动移除已隐藏的 Toast
                //toast.VisibilityChanged += (sender, args) =>
                //{
                //    if (toast.Visibility == Visibility.Collapsed)
                //    {
                //        _toastPanel.Children.Remove(toast);
                //    }
                //};
            }
        }

    }
}
