using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
//using ToastNotifications;
//using ToastNotifications.Lifetime;
//using ToastNotifications.Messages;
//using ToastNotifications.Position;

namespace iwphone.Pages
{
    /// <summary>
    /// jzdh.xaml 的交互逻辑
    /// </summary>
    public partial class jzdh : Window
    {
        public jzdh()
        {
            InitializeComponent();
            //notifier.ShowInformation("hello");
            //string message = "hello";
            //notifier.ShowInformation(message);
            //notifier.ShowSuccess(message);
            //notifier.ShowWarning(message);
            //notifier.ShowError(message);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var animation = new DoubleAnimation();
            animation.From = -50;
            animation.To = 400;
            animation.Duration = new Duration(TimeSpan.FromSeconds(0));
            animation.RepeatBehavior = RepeatBehavior.Forever;
            ProgressRectangle.BeginAnimation(TranslateTransform.XProperty, animation);
        }

        private static jzdh instance;


        public static void ShowLoadingWindow()
        {
            if (instance == null)
            {
                instance = new jzdh();
            }

            instance.Show();
        }

        public static void CloseLoadingWindow()
        {
            if (instance != null)
            {
                instance.Close();
                instance = null;
            }
        }


///* * */
//Notifier notifier = new Notifier(cfg =>
//{
//    cfg.PositionProvider = new WindowPositionProvider(
//        parentWindow: Application.Current.MainWindow,
//        corner: Corner.TopRight,
//        offsetX: 10,
//        offsetY: 10);

//    cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
//        notificationLifetime: TimeSpan.FromSeconds(3),
//        maximumNotificationCount: MaximumNotificationCount.FromCount(5));

//    cfg.Dispatcher = Application.Current.Dispatcher;
//});


       



}
}
