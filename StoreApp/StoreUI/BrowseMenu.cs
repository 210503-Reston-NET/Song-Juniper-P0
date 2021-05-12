using System;
using System.Collections.Generic;
using StoreBL;
using StoreModels;

namespace StoreUI
{
    public class BrowseMenu : IMenu
    {
        private LocationBL _locationBL;
        private ProductBL _productBL;
        private Location _currentLocation;

        public BrowseMenu(LocationBL locationBL, ProductBL productBL)
        {
            _locationBL = locationBL;
            _productBL = productBL;
        }
        public void Start()
        {
            bool repeat = true;
            string input;
            do
            {
                Console.WriteLine("This is Browse Menu.");
                if (_currentLocation is null)
                {
                    Console.WriteLine("Please choose a store location");
                    List<Location> allLocations = GetAllLocations();
                    for (int i = 0; i < allLocations.Count; i++)
                    {
                        Console.WriteLine($"[{i}] Name: {allLocations[i].Name}\n\tAddress: {allLocations[i].Address}");
                    }
                    input = Console.ReadLine();
                    int parsedInput = Int32.Parse(input);
                    if(parsedInput >= 0 || parsedInput < allLocations.Count)
                    {
                        _currentLocation = allLocations[parsedInput];
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection");
                    }
                }
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[1] Browse all items");
                Console.WriteLine("[2] Search items by name");
                Console.WriteLine("[3] Search items by category");
                Console.WriteLine("[0] Go Back");
                input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        repeat = false;
                    break;

                    case "1":
                        ViewAllProducts();
                    break;

                    case "2":
                    break;

                    case "3":
                    break;

                    default:
                        Console.WriteLine("I don't understand your input, please try again.");
                    break;
                }
            } while(repeat);
        }

        public List<Location> GetAllLocations() 
        {
            return _locationBL.GetAllLocations();
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
    }


}