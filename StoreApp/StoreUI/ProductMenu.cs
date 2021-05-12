using System;
using System.Collections.Generic;
using StoreBL;
using StoreModels;

namespace StoreUI
{
    public class ProductMenu : IMenu
    {
        private ProductBL _productBL;
        public ProductMenu(ProductBL productBL)
        {
            _productBL = productBL;
        }
        public void Start()
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
            try
            {
                Console.WriteLine("Enter details about the new product");
                Console.WriteLine("Name: ");
                string name = Console.ReadLine();
                
                Console.WriteLine("Description: ");
                string desc = Console.ReadLine();

                Console.WriteLine("Price: ");
                double price = Double.Parse(Console.ReadLine());

                Console.WriteLine("Category: ");
                string cat = Console.ReadLine();

                if(_productBL.FindProductByName(name) is null)
                {
                    Product newProd = new Product(name, desc, price, cat);
                    _productBL.AddNewProduct(newProd);
                    Console.WriteLine("Product added successfully");
                    Console.WriteLine(newProd.ToString());

                }
                else
                {
                    Console.WriteLine("There is already a product with the same name");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}