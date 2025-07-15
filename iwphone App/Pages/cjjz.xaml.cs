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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace iwphone.Pages
{
    /// <summary>
    /// cjjz.xaml 的交互逻辑
    /// </summary>
    public partial class cjjz : Window
    {
        public cjjz()
        {
            InitializeComponent();
        }

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

        private void ztgl_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void yygl_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void root_Click(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
