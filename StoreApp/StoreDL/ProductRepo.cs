using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using StoreModels;

namespace StoreDL
{
    public class ProductRepo
    {
        private const string filepath = "../StoreDL/products.json";
        private string jsonString;
        public List<Product> GetAllProducts()
        {
            try
            {
                jsonString = File.ReadAllText(filepath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Product>();
            }
            return JsonSerializer.Deserialize<List<Product>>(jsonString);
        }

        public Product GetOneProduct(string name)
        {
            return GetAllProducts().Where(loc => loc.Name == name).FirstOrDefault();
        }

        public Product AddNewProduct(Product customer)
        {
            List<Product> allProducts = GetAllProducts();
            allProducts.Add(customer);
            jsonString = JsonSerializer.Serialize(allProducts);
            File.WriteAllText(filepath, jsonString);
            return customer;
        }
        
    }
}
