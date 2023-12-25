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

namespace PrizivaNet
{
    /// <summary>
    /// Логика взаимодействия для CatalogProducts.xaml
    /// </summary>
    public partial class CatalogProducts : Page
    {
        public Frame MainFrame { get; set; }
        public DataClass data = new DataClass();
        public List<Product> products = new List<Product>();
        public List<Category> categories = new List<Category>();
        public TextBlock userFI = (TextBlock)Application.Current.MainWindow.FindName("userFIO");

        public CatalogProducts(Frame mf)
        {
            InitializeComponent();
            MainFrame = mf;
        }

        private void ListRunners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (userFI.Text != "Гость")
            {
                MainFrame.Navigate(new Basket());
            }
        }

        private void ListRunners_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListRunners.SelectedItem != null && userFI.Text != "Гость")
            {
                Product selectedItem = (Product)ListRunners.SelectedItem;
                if (selectedItem.Count > 0)
                {
                    selectedItem.CountBasket += 1;
                    selectedItem.Count -= 1;

                    List<Product> serealizedProducts = new List<Product>();
                    ListRunners.Items.Refresh();
                    foreach (Product product in ListRunners.Items)
                    {
                        serealizedProducts.Add(product);
                    }

                    FileStream fs2 = new FileStream("products.json", FileMode.Truncate);
                    {
                        JsonSerializer.Serialize(fs2, serealizedProducts);
                        fs2.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Товара нет в наличии");
                }
                
            }
        }

        private void loadSort()
        {
            if (ListRunners != null)
                ListRunners.ItemsSource = SortedProduct(products, filter, sort, search);
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            loadSort();
        }

        private void sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadSort();
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadSort();
        }

        public void LoadCategoriesIntoComboBox(List<Category> categories, ComboBox filter)
        {
            if (System.IO.File.Exists("categories.json"))
            {
                categories = data.LoadCategories(categories, "categories.json");
                int i = 1;
                foreach (Category сategory in categories)
                {
                    filter.Items.Insert(i, сategory.Name);
                    i++;
                }
            }
        }

        public List<Product> SortedProduct(List<Product> products, ComboBox filter, ComboBox sort, TextBox search)
        {
            List<Product> sortProducts = new List<Product>();
            sortProducts = products;
            if (filter != null && filter.SelectedIndex != 0)
            {
                sortProducts = sortProducts.Where(i => i.Category.Contains(filter.SelectedItem.ToString())).ToList();
            }
            if (sort != null && sort.SelectedIndex == 1)
            {
                sortProducts = sortProducts.OrderByDescending(i => i.Price).ToList();
            }
            if (sort != null && sort.SelectedIndex == 2)
            {
                sortProducts = sortProducts.OrderBy(i => i.Price).ToList();
            }
            if (search.Text != null && sortProducts != null)
            {
                sortProducts = sortProducts.Where(i => i.Name.Contains(search.Text.ToString())).ToList();
            }
            return sortProducts;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            products = data.LoadProducts(products, "products.json");
            categories = data.LoadCategories(categories, "categories.json");
            LoadCategoriesIntoComboBox(categories, filter);
            ListRunners.ItemsSource = products;
        }
    }
}
