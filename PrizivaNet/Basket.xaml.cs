using PrizivaNet.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing.IndexedProperties;
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

namespace PrizivaNet
{
    /// <summary>
    /// Логика взаимодействия для Basket.xaml
    /// </summary>
    public partial class Basket : Page
    {
        public List<Product> MyList = new List<Product>();
        public DataClass data = new DataClass();
        public Basket()
        {
            InitializeComponent();
            RefreshItems();

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            List<Product> products = new List<Product>();
            products = data.LoadProducts(products, "products.json");

            
            foreach(Product product1 in MyList)
            {
                foreach(Product product in products)
                {
                    if (product1.Name == product.Name)
                    {
                        product.Count += product.CountBasket;
                        product.CountBasket = 0;
                        break;
                    }
                }
            }
          
            FileStream fs2 = new FileStream("products.json", FileMode.Truncate);
            {
                JsonSerializer.Serialize(fs2, products);
                fs2.Close();
            }
            MyList.Clear();
            List<Product> MyListClear = new List<Product>();
            ListRunners.ItemsSource = MyListClear;
            totalPrice.Text = "Корзина пуста  |";
            totalProduct.Text = "Нет";
            ListRunners.Visibility = Visibility.Hidden;
        }

      

        private void ListRunners_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListRunners.SelectedItem != null)
            {
                Product selectedItem = (Product)ListRunners.SelectedItem;
                List<Product> products = new List<Product>();
                products = data.LoadProducts(products, "products.json");
                foreach(Product product in products)
                {
                    if (selectedItem.Name == product.Name)
                    {
                        if (product.CountBasket > 0)
                        {
                            product.CountBasket -= 1;
                            product.Count += 1;
                            FileStream fs2 = new FileStream("products.json", FileMode.Truncate);
                            {
                                JsonSerializer.Serialize(fs2, products);
                                fs2.Close();
                            }
                            RefreshItems();
                            ListRunners.Items.Refresh();
                            break;
                        }
                        else
                        {
                            product.CountBasket = 0;
                            product.Count += product.CountBasket;
                            FileStream fs2 = new FileStream("products.json", FileMode.Truncate);
                            {
                                JsonSerializer.Serialize(fs2, products);
                                fs2.Close();
                            }
                            RefreshItems();
                            ListRunners.Items.Refresh();
                            break;
                        }
                    }
                }
                

            }
        }

        public void RefreshItems()
        {
            MyList = new List<Product>();
            List<Product> products = new List<Product>();
            products = data.LoadProducts(products, "products.json");
            foreach (Product product in products)
            {
                if (product.CountBasket != 0)
                {
                    MyList.Add(product);
                }
            }

            if (MyList.Count == 0)
            {
                ListRunners.Visibility = Visibility.Hidden;
                totalPrice.Text = "Корзина пуста  |";
                totalProduct.Text = "Нет";
            }
            else
            {
                double tot = 0;
                int count = 0;
                foreach (Product product in MyList)
                {
                    tot += product.CountBasket * product.Price;
                    count += product.CountBasket;
                }
                totalPrice.Text = $"{tot.ToString()}  |";
                totalProduct.Text = count.ToString();
                ListRunners.ItemsSource = MyList;
            }
        }
    }
}
