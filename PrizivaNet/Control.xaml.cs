using Microsoft.Win32;
using PrizivaNet.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
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
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PrizivaNet
{
    /// <summary>
    /// Логика взаимодействия для Control.xaml
    /// </summary>
    public partial class Control : Page
    {
        public Frame MainFrame { get; set; }
        public DataClass data = new DataClass();
        public string WhatGet { get; set; }
        public Control(Frame mainFrame, string whatGet)
        {
            InitializeComponent();
            WhatGet = whatGet;
            text1.Text = whatGet;
            text2.Text = "";
            MainFrame = mainFrame;
            if (whatGet == "Добавить категорию")
            {
                txt1.Text = "Имя категории:  ";
                txt2.Visibility = Visibility.Hidden;
                txt3.Visibility = Visibility.Hidden;
                txt4.Visibility = Visibility.Hidden;
                txt5.Visibility = Visibility.Hidden;
                txt6.Visibility = Visibility.Hidden;
                txt7.Visibility = Visibility.Hidden;
                tb2.Visibility = Visibility.Hidden;
                tb3.Visibility = Visibility.Hidden;
                tb4.Visibility = Visibility.Hidden;
                tb5.Visibility = Visibility.Hidden;
                tb6.Visibility = Visibility.Hidden;
                tb7.Visibility = Visibility.Hidden;
            }
            else if (whatGet == "Удалить товар")
            {
                txt1.Text = "Имя товара:  ";

                txt2.Visibility = Visibility.Hidden;
                txt3.Visibility = Visibility.Hidden;
                txt4.Visibility = Visibility.Hidden;
                txt5.Visibility = Visibility.Hidden;
                txt6.Visibility = Visibility.Hidden;
                txt7.Visibility = Visibility.Hidden;
                tb2.Visibility = Visibility.Hidden;
                tb3.Visibility = Visibility.Hidden;
                tb4.Visibility = Visibility.Hidden;
                tb5.Visibility = Visibility.Hidden;
                tb6.Visibility = Visibility.Hidden;
                tb7.Visibility = Visibility.Hidden;
            }
            else if (whatGet == "Удалить категорию")
            {
                txt1.Text = "Имя категории:  ";
                txt2.Visibility = Visibility.Hidden;
                txt3.Visibility = Visibility.Hidden;
                txt4.Visibility = Visibility.Hidden;
                txt5.Visibility = Visibility.Hidden;
                txt6.Visibility = Visibility.Hidden;
                txt7.Visibility = Visibility.Hidden;
                tb2.Visibility = Visibility.Hidden;
                tb3.Visibility = Visibility.Hidden;
                tb4.Visibility = Visibility.Hidden;
                tb5.Visibility = Visibility.Hidden;
                tb6.Visibility = Visibility.Hidden;
                tb7.Visibility = Visibility.Hidden;
            }
            else if (whatGet == "Добавить товар")
            {
                txt1.Text = "Имя товара:  ";
                txt2.Text = "Производитель:  ";
                txt3.Text = "Цена:  ";
                txt4.Text = "Фото:  ";
                txt5.Text = "Категория:  ";
                txt7.Visibility = Visibility.Hidden;
                tb7.Visibility = Visibility.Hidden;
                List<Category> categories = new List<Category>();
                if (File.Exists("categories.json"))
                {
                    categories = data.LoadCategories(categories, "categories.json");
                    int i = 0;
                    foreach (Category сategory in categories)
                    {
                        tb5.Items.Insert(i, сategory.Name);
                        i++;
                    }
                }
            }
            else if (whatGet == "Изменить товар")
            {
                txt1.Text = "Имя товара:  ";
                txt2.Text = "Производитель:  ";
                txt3.Text = "Цена:  ";
                txt4.Text = "Фото:  ";
                txt5.Text = "Категория:  ";
                List<Category> categories = new List<Category>();
                if (File.Exists("categories.json"))
                {
                    FileStream fs1 = new FileStream("categories.json", FileMode.Open);
                    {
                        categories = (List<Category>)JsonSerializer.Deserialize(fs1, typeof(List<Category>));
                        int i = 0;
                        foreach (Category сategory in categories)
                        {
                            tb5.Items.Insert(i, сategory.Name);
                            i++;
                        }
                        fs1.Close();
                    }
                }
            }

        }

        private void readyButtin_Click(object sender, RoutedEventArgs e)
        {
            if (WhatGet == "Добавить категорию")
            {
                if (!string.IsNullOrWhiteSpace(tb1.Text))
                {
                    data.AddCategory(tb1.Text, "categories.json");
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
            else if (WhatGet == "Удалить категорию")
            {
                if (!string.IsNullOrWhiteSpace(tb1.Text))
                {
                    data.DeleteCategory(tb1.Text, "categories.json");
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
            else if (WhatGet == "Удалить товар")
            {
                if (!string.IsNullOrWhiteSpace(tb1.Text))
                {
                    data.DeleteProduct(tb1.Text, "products.json");
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
            else if (WhatGet == "Изменить товар")
            {
                if (!double.TryParse(tb3.Text, out double number) || Convert.ToInt32(tb6.Text) > 0 && Convert.ToDouble(tb3.Text) > 0)
                {
                    MessageBox.Show("Введите нормальное число в цене/кол-ве");
                    return;
                }
                if (!string.IsNullOrWhiteSpace(tb1.Text) && !string.IsNullOrWhiteSpace(tb2.Text) && !string.IsNullOrWhiteSpace(tb3.Text) && !string.IsNullOrWhiteSpace(filePath.Text) && !string.IsNullOrWhiteSpace(tb5.Text))
                {
                    data.ChangeProduct(tb1.Text, tb5.Text, tb2.Text, path, Convert.ToDouble(tb3.Text), Convert.ToInt32(tb6.Text), tb7.Text, "products.json");
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
            else if (WhatGet == "Добавить товар")
            {
                if (!double.TryParse(tb3.Text, out double number1) || Convert.ToInt32(tb6.Text) > 0 && Convert.ToDouble(tb3.Text) > 0)
                {
                    MessageBox.Show("Введите нормальное число в цене/кол-ве");
                    return;
                }
                if (!string.IsNullOrWhiteSpace(tb1.Text) && !string.IsNullOrWhiteSpace(tb2.Text) && !string.IsNullOrWhiteSpace(tb3.Text) && !string.IsNullOrWhiteSpace(filePath.Text) && !string.IsNullOrWhiteSpace(tb5.Text))
                {
                    data.AddProduct(tb1.Text, tb5.Text, tb2.Text, path, Convert.ToDouble(tb3.Text), Convert.ToInt32(tb6.Text),  "products.json");
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tb1.Text = "";
            tb2.Text = "";
            tb3.Text = "";
            filePath.Text = "";
            tb5.Text = "";
        }

        string path = "";
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            if (openFileDialog.ShowDialog() == true)
            {
                path = openFileDialog.FileName;
                filePath.Text = System.IO.Path.GetFileName(path);
            }
        }
    }
}
