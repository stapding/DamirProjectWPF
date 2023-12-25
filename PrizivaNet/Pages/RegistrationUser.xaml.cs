using Microsoft.Win32;
using PrizivaNet.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegistrationUser.xaml
    /// </summary>
    public partial class RegistrationUser : Page
    {
        public Frame MainFrame { get; set; }
        public DataClass data = new DataClass();
        public RegistrationUser(Frame mainFrame)
        {
            InitializeComponent();
            MainFrame = mainFrame;
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            if (passwordTB.Text != "" && againPassword.Text != "" && emailTB.Text != "" && loginTB.Text != "" && !String.IsNullOrWhiteSpace(loginTB.Text))
            {
                if (passwordTB.Text.Length < 6)
                {
                    MessageBox.Show("Пароль меньше 6 символов");
                    return;
                }
                if (!passwordTB.Text.Any(c => char.IsUpper(c)))
                {
                    MessageBox.Show("Пароль не содержит букв большого регистра");
                    return;
                }
                if (!passwordTB.Text.Any(c => char.IsDigit(c)))
                {
                    MessageBox.Show("Пароль не содержит цифры");
                    return;
                }
                if (!passwordTB.Text.Any(c => "!@#$%^".Contains(c)))
                {
                    MessageBox.Show("Пароль не содержит символы !@#$%^");
                    return;
                }
                if (passwordTB.Text != againPassword.Text)
                {
                    MessageBox.Show("Пароли не совпадают");
                    return;
                }
                string cond = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
            + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
            + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
                if (!Regex.IsMatch(emailTB.Text, cond))
                {
                    MessageBox.Show("эмейл плох");
                    return;
                }
                if (loginTB.Text == "Гость")
                {
                    MessageBox.Show("Нельзя добавить пользователя с логином «Гость»");
                    return;
                }

                bool inUser = false;
                List<User> userss = new List<User>();
                if (!File.Exists("users.json"))
                {
                    FileStream fs1 = new FileStream("users.json", FileMode.Create);
                    {
                        string password = passwordTB.Text;
                        string email = emailTB.Text;
                        string role = "runner";
                        string login = loginTB.Text;
                        userss.Add(new User(email, password, role, login));

                        JsonSerializer.Serialize(fs1, userss);

                        MessageBox.Show("Файл создан и пользователь добавлен!");
                        fs1.Close();

                        MainFrame.Navigate(new LoginUser(MainFrame));
                    }
                }
                else
                {

                    userss = data.LoadUsers(userss);

                    foreach (User user in userss)
                    {
                        if (user.Email == emailTB.Text)
                        {
                            MessageBox.Show("Пользователь с таким Email'ом уже существует");
                            inUser = true;
                            break;
                        }
                    }
                        

                    if (!inUser)
                    {
                        FileStream fs2 = new FileStream("users.json", FileMode.Truncate);

                        string password = passwordTB.Text;
                        string email = emailTB.Text;
                        string role = "runner";
                        string login = loginTB.Text;

                        userss.Add(new User(email, password, role, login));

                        JsonSerializer.Serialize(fs2, userss);

                        MessageBox.Show("Пользователь добавлен!");

                        fs2.Close();

                        MainFrame.Navigate(new LoginUser(MainFrame));
                    }
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполнены!");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            loginTB.Text = "";
            emailTB.Text = "";
            passwordTB.Text = "";
            againPassword.Text = "";
        }
    }
}
