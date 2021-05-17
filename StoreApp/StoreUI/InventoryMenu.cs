using System;
using System.Collections.Generic;
using Serilog;
using StoreBL;
using StoreModels;

namespace StoreUI
{
    public class InventoryMenu : IMenu
    {
        private Location _currentLocation;
        private LocationBL _locBL;
        private ProductBL _prodBL;

        public InventoryMenu(LocationBL locBL, ProductBL prodBL)
        {
            _locBL = locBL;
            _prodBL = prodBL;
        }
        public void Start(Customer customer)
        {
            bool repeat = true;
            string input;
            int parsedInput = 0;
            do
            {
                Console.WriteLine("This is Manage Inventory Menu.");
                if (_currentLocation is null)
                {
                    Console.WriteLine("Please select a store location to begin");
                    List<Location> allLocations = GetAllLocations();
                    for (int i = 0; i < allLocations.Count; i++)
                    {
                        Console.WriteLine($"[{i}] Name: {allLocations[i].Name}\n\tAddress: {allLocations[i].Address}");
                    }
                    input = Console.ReadLine();
                    parsedInput = Int32.Parse(input);
                    if(parsedInput >= 0 || parsedInput < allLocations.Count)
                    {
                        _currentLocation = allLocations[parsedInput];
                        Console.WriteLine($"You picked {_currentLocation.ToString()}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection");
                    }
                }
                List<Inventory> inventory = GetAllInventory(_currentLocation.Id);
                if(inventory.Count == 0)
                {
                    Console.WriteLine("There is nothing in the inventory...");
                    Console.WriteLine("[0] Go Back");
                    Console.WriteLine("[1] Add a New Product to the Inventory");
                    input = Console.ReadLine();
                    switch(input)
                    {
                        case "0":
                            repeat = false;
                        break;
                        case "1":
                            AddNewProductToInventory();
                        break;
                        default:
                            Console.WriteLine("I don't understand your input, please try again.");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine($"This is {_currentLocation.Name}'s Inventory");
                    Console.WriteLine("Select the product you want to manage");
                    Console.WriteLine("[0] Go Back");
                    for (int i = 0; i < inventory.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {inventory[i].ToString()}");
                    }
                    Console.WriteLine($"[{inventory.Count + 1}] Add a New Product to the Inventory");
                    input = Console.ReadLine();
                    parsedInput = Int32.Parse(input) - 1;
                    if(input == "0") repeat = false;
                    else if(parsedInput >= 0 && parsedInput < inventory.Count)
                    {
                        Console.WriteLine($"We got here {parsedInput}, {inventory[parsedInput].ToString()}");
                        ChangeInventory(inventory[parsedInput]);
                    }
                    else if(parsedInput == inventory.Count)
                    {
                        AddNewProductToInventory();
                    }
                }

            } while(repeat);
            
            
            
        }

        private void AddNewProductToInventory()
        {
            Console.WriteLine("Which product would you like to add?");
            List<Product> allProd = _prodBL.GetAllProducts();
            for(int i = 0; i < allProd.Count; i++)
            {
                Console.WriteLine($"[{i}] {allProd[i].ToString()}");
            }
            string input = Console.ReadLine();
            int parsedInput = Int32.Parse(input);
            Product selectedProd = allProd[parsedInput];
            
            Console.WriteLine("Enter the quantity:");
            input = Console.ReadLine();
            parsedInput = Int32.Parse(input);

            Inventory newInven = new Inventory (selectedProd, _currentLocation, parsedInput);

            try
            {
                _locBL.AddInventory(newInven);
                Console.WriteLine("Inventory added successfully!");
                Console.WriteLine(newInven.ToString());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private Inventory ChangeInventory(Inventory item)
        {
            Console.WriteLine("Enter new quantity: ");
            string input = Console.ReadLine();
            item.Quantity = Int32.Parse(input);
            return _locBL.UpdateInventoryItem(item);
        }

        private List<Location> GetAllLocations()
        {
            return _locBL.GetAllLocations();
        }
        private List<Inventory> GetAllInventory(int locId)
        {
            return _locBL.GetLocationInventory(locId);
        }
    }
}