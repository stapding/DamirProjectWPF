using PrizivaNet;
using PrizivaNet.Classes;
using System.Windows.Documents;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PrizivaNetTest
{
    [TestClass]
    public class DataClassTest
    {
        [TestMethod]
        public void LoadProducts_LoadProductsFromJSONFile_ReturnsListProducts()
        {
            // Arrange
            DataClass data = new DataClass();
            List<Product> products = new List<Product>();

            // Act
            products = data.LoadProducts(products, "products.json");

            // Assert
            Assert.AreNotEqual(0, products.Count);
        }

        [TestMethod]
        public void LoadUsers_LoadUsersFromJSONFile_ReturnsListUsers()
        {
            // Arrange
            DataClass data = new DataClass();
            List<User> users = new List<User>();

            // Act
            users = data.LoadUsers(users);

            // Assert
            Assert.AreNotEqual(0, users.Count);
        }

        [TestMethod]
        public void LoadCategories_LoadCategoriesFromJSONFile_ReturnsListCategories()
        {
            // Arrange
            DataClass data = new DataClass();
            string file = "categories.json";
            List<Category> categories = new List<Category>();

            // Act
            categories = data.LoadCategories(categories, file);

            // Assert
            Assert.AreNotEqual(0, categories.Count);
        }

        [TestMethod]
        public void AddCategory_CreateNewJSONFileAndAddCategory_CreateJSONFile()
        {
            // Arrange
            DataClass data = new DataClass();
            List<Category> categories = new List<Category>();
            string file = "categoriesNEW.json";
            if (System.IO.File.Exists(file))
            {
                System.IO.File.Delete(file);
            }

            // Act
            data.AddCategory("Призыв", file);

            // Assert
            Assert.IsTrue(System.IO.File.Exists(file));
        }

        [TestMethod]
        public void AddCategory_AddCategoryIntoJSONFile_AddCategoryInFile()
        {
            // Arrange
            DataClass data = new DataClass();
            List<Category> categories = new List<Category>();
            string file = "categoriesNEW.json";


            // Act
            categories = data.LoadCategories(categories, file);
            if (categories.Count > 0) 
            { 
                foreach (Category category in categories)
                {
                    if (category.Name == "Призывок")
                    {
                        categories.Remove(category);
                        break;
                    }
                }
            }
            data.AddCategory("Призывок", file);
            categories = data.LoadCategories(categories, file);

            // Assert
            Assert.IsTrue(System.IO.File.Exists(file));
            Assert.AreNotEqual(0, categories.Count);
        }

        [TestMethod]
        public void AddCategory_NameCategoryInJSONFile_DontAddCategoryInFile()
        {
            // Arrange
            DataClass data = new DataClass();
            List<Category> categories = new List<Category>();
            string file = "categoriesNEW.json";


            // Act
            data.AddCategory("Призывок", file);
            categories = data.LoadCategories(categories, file);

            // Assert
            Assert.IsTrue(System.IO.File.Exists(file));
            Assert.AreNotEqual(0, categories.Count);
        }

        [TestMethod]
        public void DeleteProduct_DeleteCategoryFromJSONFile_DeleteCategoryInFile()
        {
            // Arrange
            DataClass data = new DataClass();
            List<Product> products = new List<Product>();
            bool inProducts = false;
            string file = "productsNEW.json";
            data.AddProduct("Последний шанс", "Призыв", "ПризываНет", "C:\\Users\\10a\\source\\repos\\PrizivaNet\\PrizivaNet\\9054404_bx_basket_icon.png", 1000, file);


            // Act

            data.DeleteProduct("Последний шанс", file);
            products = data.LoadProducts(products, file);

            // Assert
            Assert.IsTrue(System.IO.File.Exists(file));
            Assert.AreEqual(1, products.Count);
        }

        [TestMethod]
        public void DeleteProduct_DeleteProductFromJSONFileWithIncorrectName_DontDeleteProductInFile()
        {
            // Arrange
            DataClass data = new DataClass();
            List<Product> products = new List<Product>();
            string file = "productsNEW.json";


            // Act

            data.DeleteProduct("Последний шансfsdfsdfdsfdsfdsfds", file);
            products = data.LoadProducts(products, file);

            // Assert
            Assert.IsTrue(System.IO.File.Exists(file));
            Assert.AreEqual(1, products.Count);
        }

        [TestMethod]
        public void DeleteCategory_DeleteCategoryFromJSONFile_DeleteCaregoryInFile()
        {
            // Arrange
            DataClass data = new DataClass();
            List<Category> categories = new List<Category>();
            string file = "categoriesNEW.json";
            data.AddCategory("Последний шанс", file);

            // Act

            data.DeleteCategory("Последний шанс", file);
            categories = data.LoadCategories(categories, file);

            // Assert
            Assert.IsTrue(System.IO.File.Exists(file));
            Assert.AreEqual(2, categories.Count);
        }

        [TestMethod]
        public void DeleteCategory_DeleteCategoryFromJSONFileWithIncorrectName_DontDeleteCategoryInFile()
        {
            // Arrange
            DataClass data = new DataClass();
            List<Category> categories = new List<Category>();
            string file = "categoriesNEW.json";


            // Act

            data.DeleteCategory("Последний ш423423432432432анс", file);
            categories = data.LoadCategories(categories, file);


            // Assert
            Assert.IsTrue(System.IO.File.Exists(file));
            Assert.AreEqual(2, categories.Count);
        }

        [TestMethod]
        public void ChangeProduct_ChangeProductInJSONFile_ChangeProductInFile()
        {
            // Arrange
            DataClass data = new DataClass();
            List<Product> products = new List<Product>();
            string file = "productsNEWNEW.json";


            // Act

            data.ChangeProduct("TANK","Мобилизация","ПризываНет", "C:\\Users\\10a\\source\\repos\\PrizivaNet\\PrizivaNet\\9054404_bx_basket_icon.png",5000,"TANK", file);
            data.ChangeProduct("TANK", "Мобилизация", "ПризываНет", "C:\\Users\\10a\\source\\repos\\PrizivaNet\\PrizivaNet\\9054404_bx_basket_icon.png", 5000, "TANK441421", file);
            products = data.LoadProducts(products, file);


            // Assert
            Assert.IsTrue(System.IO.File.Exists(file));
            Assert.AreEqual(1, products.Count);
        }

        [TestMethod]
        public void AddProduct_CreateNewJSONFileAndAddProducts_CreateJSONFile()
        {
            // Arrange
            DataClass data = new DataClass();
            List<Product> products = new List<Product>();
            string file = "productsNEWNEWNEW.json";
            if (System.IO.File.Exists(file))
            {
                System.IO.File.Delete(file);
            }

            // Act
            data.AddProduct("TANK", "Мобилизация", "ПризываНет", "C:\\Users\\10a\\source\\repos\\PrizivaNet\\PrizivaNet\\9054404_bx_basket_icon.png", 5000, file);
            data.AddProduct("TANK", "Мобилизация", "ПризываНет", "C:\\Users\\10a\\source\\repos\\PrizivaNet\\PrizivaNet\\9054404_bx_basket_icon.png", 5000, file);
            products = data.LoadProducts(products, file);

            // Assert
            Assert.IsTrue(System.IO.File.Exists(file));
            Assert.AreEqual(1, products.Count);
        }
    }
}