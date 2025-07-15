using System.Windows.Controls;
using System.Diagnostics;
using System.Security.Policy;

namespace UIKitTutorials.Pages
{
    /// <summary>
    /// Lógica de interacción para PaymentPage.xaml
    /// </summary>
    public partial class PaymentPage : Page
    {
        public PaymentPage()
        {
            InitializeComponent();
        }

        private void yybutton(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/haothtrteen/IWPhone");
        }
    }
}
