using Microsoft.Win32;
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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Page
    {
        public Frame MainFrame { get; set; }
        public AdminPanel(Frame mainFrame)
        {
            InitializeComponent();
            MainFrame = mainFrame;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Control(MainFrame, txt1.Text));
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("categories.json"))
            {
                MainFrame.Navigate(new Control(MainFrame, txt3.Text));
            }
            else
            {
                MessageBox.Show("Файла с категориями нет! Добавьте их.");
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Control(MainFrame, txt2.Text));
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("products.json"))
            {
                MainFrame.Navigate(new Control(MainFrame, txt4.Text));
            }
            else
            {
                MessageBox.Show("Файла с товарами нет! Добавьте их.");
            }
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("products.json"))
            {
                MainFrame.Navigate(new Control(MainFrame, txt5.Text));
            }
            else
            {
                MessageBox.Show("Файла с товарами нет! Добавьте их.");
            }
        }
    }
}
