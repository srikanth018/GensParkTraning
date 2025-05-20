using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeConsoleApp
{
    public class Product
    {
        private Dictionary<string, double> products = new Dictionary<string, double>();

        public void AddProduct()
        {
            
            products.Add("Laptop", 1200.99);
            products.Add("Smartphone", 799.49);
            products.Add("Tablet", 399.99);
            products.Add("Headphones", 149.95);
            products.Add("Monitor", 299.89);

            
            foreach (var item in products)
            {
                Console.WriteLine($"Product: {item.Key}, Price: {item.Value}");
            }
        }
    }
}
