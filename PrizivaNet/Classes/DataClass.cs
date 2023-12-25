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
using static System.Net.WebRequestMethods;

namespace PrizivaNet.Classes
{
    public class DataClass
    {
        public DataClass()
        {

        }


        public void AddCategory(string name, string file)
        {
            bool inCategory = false;
            List<Category> categories = new List<Category>();
            if (!System.IO.File.Exists(file))
            {
                FileStream fs1 = new FileStream(file, FileMode.Create);
                {
                    categories.Add(new Category(name));
                    JsonSerializer.Serialize(fs1, categories);
                    MessageBox.Show("Файл создан и категория добавлена!");
                    fs1.Close();
                }
            }
            else
            {
                categories = LoadCategories(categories, file);
                foreach (Category сategory in categories)
                {
                    if (сategory.Name == name)
                    {
                        MessageBox.Show("Категория с таким именем уже уже существует");
                        inCategory = true;
                        break;
                    }
                }
                if (!inCategory)
                {
                    FileStream fs2 = new FileStream(file, FileMode.Truncate);
                    categories.Add(new Category(name));
                    JsonSerializer.Serialize(fs2, categories);
                    MessageBox.Show("Категория добавлена!");
                    fs2.Close();
                }
            }
        }

        public void DeleteProduct(string name, string file)
        {
            bool inProduct = false;
            List<Product> products = new List<Product>();
            products = LoadProducts(products, file);
            foreach (Product product in products)
            {
                if (product.Name == name)
                {
                    inProduct = true;
                    products.Remove(product);
                    break;
                }
            }

            if (!inProduct)
            {
                MessageBox.Show("Товара с таким именем нет");
            }
            else
            {
                MessageBox.Show("Товар удален");
            }

            FileStream fs2 = new FileStream(file, FileMode.Truncate);
            {
                JsonSerializer.Serialize(fs2, products);
                fs2.Close();
            }
        }

        public void DeleteCategory(string name, string file)
        {
            bool inCategory = false;
            List<Category> categories = new List<Category>();
            categories = LoadCategories(categories, file);
            foreach (Category сategory in categories)
            {
                if (сategory.Name == name)
                {
                    inCategory = true;
                    categories.Remove(сategory);
                    break;
                }
            }

            if (!inCategory)
            {
                MessageBox.Show("Категории с таким именем нет");
            }
            else
            {
                MessageBox.Show("Категория удалена");
            }
            FileStream fs2 = new FileStream(file, FileMode.Truncate);
            {
                JsonSerializer.Serialize(fs2, categories);
                fs2.Close();
            }
        }

        public void ChangeProduct(string name, string category, string manufacturer, string image, double price, int count, string whatChange, string file)
        {
            bool inProduct = false;
            List<Product> products = new List<Product>();
            products = LoadProducts(products, file);
            foreach (Product product in products)
            {
                if (product.Name == whatChange)
                {
                    inProduct = true;
                    product.Name = name;
                    product.Category = category;
                    product.Manufacturer = manufacturer;
                    product.Image = image;
                    product.Price = price;
                    product.Count = count;
                    break;
                }
            }

            if (!inProduct)
            {
                MessageBox.Show("Товара с таким именем нет");
            }
            else
            {
                MessageBox.Show("Товар изменён");
            }
            FileStream fs2 = new FileStream(file, FileMode.Truncate);
            {
                JsonSerializer.Serialize(fs2, products);
                fs2.Close();
            }
        }

        public void AddProduct(string name, string category, string manufacturer, string image, double price, int count, string file)
        {
            bool inProducts = false;
            List<Product> products = new List<Product>();
            if (!System.IO.File.Exists(file))
            {
                FileStream fs1 = new FileStream(file, FileMode.Create);
                {
                    products.Add(new Product(name, category, manufacturer, image, price, count));
                    JsonSerializer.Serialize(fs1, products);
                    MessageBox.Show("Файл создан и товар добавлен!");
                    fs1.Close();
                }
            }
            else
            {
                products = LoadProducts(products, file);
                foreach (Product product in products)
                {
                    if (product.Name == name)
                    {
                        MessageBox.Show("Товар с таким именем уже уже существует");
                        inProducts = true;
                        break;
                    }
                }

                if (!inProducts)
                {
                    FileStream fs2 = new FileStream(file, FileMode.Truncate);
                    products.Add(new Product(name, category, manufacturer, image, price, count));
                    JsonSerializer.Serialize(fs2, products);
                    MessageBox.Show("Товар добавлен!");
                    fs2.Close();
                }
            }
        }

        public List<Product> LoadProducts(List<Product> products, string file)
        {
            FileStream fs1 = new FileStream(file, FileMode.Open);
            {
                products = (List<Product>)JsonSerializer.Deserialize(fs1, typeof(List<Product>));
                fs1.Close();
            }
            return products;
        }

        public List<User> LoadUsers(List<User> users)
        {
            FileStream fs1 = new FileStream("users.json", FileMode.Open);
            {
                users = (List<User>)JsonSerializer.Deserialize(fs1, typeof(List<User>));
                fs1.Close();
            }
            return users;
        }

        public List<Category> LoadCategories(List<Category> categories, string file)
        {
            FileStream fs1 = new FileStream(file, FileMode.Open);
            {
                categories = (List<Category>)JsonSerializer.Deserialize(fs1, typeof(List<Category>));
                fs1.Close();
            }
            return categories;
        }
    }
}