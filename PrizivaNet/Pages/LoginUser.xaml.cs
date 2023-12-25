using PrizivaNet.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.Windows.Threading;

namespace PrizivaNet
{
    /// <summary>
    /// Логика взаимодействия для LoginUser.xaml
    /// </summary>
    public partial class LoginUser : Page
    {
        public Frame MainFrame { get; set; }
        public DataClass data = new DataClass();
        public LoginUser(Frame mainFrame)
        {
            InitializeComponent();
            MainFrame = mainFrame;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isAdmin = false;
            bool userFound = false;
            List<User> userss = new List<User>();

            userss = data.LoadUsers(userss);
               
            foreach (User user in userss)
            {
                if (user.Email == email.Text && user.Password == password.Text && user.Role == "admin")
                {
                    MessageBox.Show($"{user.Email} вошёл в систему как {user.Role}");
                    TextBlock userFI = (TextBlock)Application.Current.MainWindow.FindName("userFIO");
                    userFI.Text = user.Login;
                    userFound = true;
                    isAdmin = true;
                    break;
                }
                else if (user.Email == email.Text && user.Password == password.Text && user.Role == "runner")
                {
                    MessageBox.Show($"{user.Email} вошёл в систему как {user.Role}");
                    TextBlock userFI = (TextBlock)Application.Current.MainWindow.FindName("userFIO");
                    userFI.Text = user.Login;
                    userFound = true;
                    break;
                }
            }

            if (!userFound)
            {
                MessageBox.Show("Вы ввели неверный Email или Пароль.");
            }

            if (userFound)
            {
                if (MainFrame != null && isAdmin)
                {
                    MainFrame.Navigate(new AdminPanel(MainFrame));
                }
                else
                {
                    MainFrame.Navigate(new CatalogProducts(MainFrame));
                }
            }
            
           
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            email.Text = "";
            password.Text = "";
        }
    }
}
