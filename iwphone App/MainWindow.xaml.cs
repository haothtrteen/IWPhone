using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using iwphone.Pages;
using System.IO;
using System.Reflection;
using static iwphone.Windowtoast;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace iwphone
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
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


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            jzdh.ShowLoadingWindow();

         
            PagesNavigation.Navigate(new System.Uri("Pages/SoundsPage.xaml", UriKind.RelativeOrAbsolute));    

         


            jzdh.CloseLoadingWindow();

        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            

            System.Windows.Application.Current.Shutdown();
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

        private void rdHome_Click(object sender, RoutedEventArgs e)
        {
            // PagesNavigation.Navigate(new HomePage());

            PagesNavigation.Navigate(new System.Uri("Pages/SoundsPage.xaml", UriKind.RelativeOrAbsolute));



        }

        private void rdSounds_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Pages/HomePage.xaml", UriKind.RelativeOrAbsolute));

           
        }

        private void rdNotes_Click(object sender, RoutedEventArgs e)
        {


            PagesNavigation.Navigate(new System.Uri("Pages/cjgl.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdPayment_Click(object sender, RoutedEventArgs e)
        {
            PagesNavigation.Navigate(new System.Uri("Pages/PaymentPage.xaml", UriKind.RelativeOrAbsolute));

            //Window1 Window1 = new Window1();
            //Window1.Show();
        }



        public void cjms()
        {
            myBorder.Background = Brushes.Transparent;
            PagesNavigation.Navigate(new System.Uri("Pages/NotesPage.xaml", UriKind.RelativeOrAbsolute));
        }



        private void cqxx(object sender, RoutedEventArgs e)
        {
        
        }

        private void cqxr(object sender, RoutedEventArgs e)
        {
           
        }

        private void cqxf(object sender, RoutedEventArgs e)
        {
          
        }

        private void cqrx(object sender, RoutedEventArgs e)
        {
           
        }

        private void cqrr(object sender, RoutedEventArgs e)
        {
           
        }

        private void cqrf(object sender, RoutedEventArgs e)
        {
          
        }

        private void cqfx(object sender, RoutedEventArgs e)
        {
            
        }

        private void cqfr(object sender, RoutedEventArgs e)
        {
            
        }

        private void cqff(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
