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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrizivaNet
{
    /// <summary>
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        public Frame MainFrame { get; set; }
        public StartPage(Frame mf)
        {
            InitializeComponent();
            MainFrame = mf;
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RegistrationUser(MainFrame));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LoginUser(MainFrame));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TextBlock userFI = (TextBlock)Application.Current.MainWindow.FindName("userFIO");
            userFI.Text = "Гость";
            MainFrame.Navigate(new CatalogProducts(MainFrame));
        }
    }
}
