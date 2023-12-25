using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizivaNet
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public int CountBasket { get; set; }

        public Product(string name, string category, string manufacturer, string image, double price, int count)
        {
            Name = name;
            Category = category;
            Manufacturer = manufacturer;
            Image = image;
            Price = price;
            Count = count;
            CountBasket = 0;
        }
    }
}
