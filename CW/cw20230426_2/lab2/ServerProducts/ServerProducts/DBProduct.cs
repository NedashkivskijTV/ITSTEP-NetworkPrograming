using ProductLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerProducts
{
    internal class DBProduct
    {
        public static List<Product> LoadProductsCollection()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product("Lemon", "yellow", 11));
            products.Add(new Product("Pineapple", "sweet", 55));
            products.Add(new Product("Orange", "juicy", 27));
            products.Add(new Product("Potato", "large", 22));
            products.Add(new Product("Tomato", "red", 25));

            return products;
        }
    }
}
