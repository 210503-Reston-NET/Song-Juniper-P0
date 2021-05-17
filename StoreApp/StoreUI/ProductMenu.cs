using System;
using System.Collections.Generic;
using Serilog;
using StoreBL;
using StoreModels;

namespace StoreUI
{
    public class ProductMenu : IMenu
    {
        private ProductBL _productBL;
        private ValidationService _validationService;
        public ProductMenu(ProductBL productBL, ValidationService validationService)
        {
            _productBL = productBL;
            _validationService = validationService;
        }
        public void Start(Customer customer)
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Manage Products Menu");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[1] View all Products");
                Console.WriteLine("[2] Add a new Product");
                Console.WriteLine("[0] Go Back");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        repeat = false;
                    break;

                    case "1":
                        ViewAllProducts();
                    break;

                    case "2":
                        AddNewProduct();
                    break;

                    default:
                        Console.WriteLine("I don't understand your input, please try again.");
                    break;
                }
            } while(repeat);
        }

        public void ViewAllProducts()
        {
            List<Product> allProducts = _productBL.GetAllProducts();
            if(allProducts.Count == 0)
            {
                Console.WriteLine("No products to display!");
            }
            else
            {
                foreach(Product prod in allProducts)
                {
                    Console.WriteLine(prod.ToString() + "\n");
                }
            }
            
        }

        public void AddNewProduct()
        {
            Console.WriteLine("Enter details about the new product");
            string name = _validationService.ValidateString("Name: ");
            string desc = _validationService.ValidateString("Description: ");
            double price = _validationService.ValidateDouble("Price: ");
            string category = _validationService.ValidateString("Category: ");
            using (var log = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().WriteTo.File("../logs/logs.txt", rollingInterval: RollingInterval.Day).CreateLogger())
            {
                try
                {
                    Product newProd = new Product(name, desc, price, category);
                    Product createdProd = _productBL.AddNewProduct(newProd);
                    Console.WriteLine("Product added successfully");
                    Console.WriteLine(createdProd.ToString());
                }
                catch (Exception ex)
                {
                    log.Warning(ex, ex.Message);
                }
            }
        }
    }
}