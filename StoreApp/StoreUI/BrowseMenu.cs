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
        private OrderBL _orderBL;
        private Location _currentLocation;
        private Customer _currentCustomer;
        private Order _openOrder;

        public BrowseMenu(LocationBL locationBL, ProductBL productBL, OrderBL orderBL)
        {
            _locationBL = locationBL;
            _productBL = productBL;
            _orderBL = orderBL;
        }
        public void Start(Customer customer)
        {
            _currentCustomer = customer;
            if(customer.Orders is null || customer.Orders.Count == 0)
            {
                Order newOrder = new Order {
                    Customer = _currentCustomer,
                    Location = _currentLocation
                };
                try
                {
                    _openOrder = _orderBL.CreateOrder(newOrder);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            } 
            else
            {
                _openOrder = _orderBL.GetOpenOrder(customer);
            }
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
                        Console.WriteLine($"You picked {_currentLocation.ToString()}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection");
                    }
                }
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[1] Browse all items");
                Console.WriteLine("[2] View my cart");
                Console.WriteLine("[3] Search items by category");
                Console.WriteLine("[0] Go Back");
                input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        repeat = false;
                    break;

                    case "1":
                        ViewInventory(_currentLocation.Id);
                    break;

                    case "2":
                        ViewCart();
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

        public void ViewInventory(int locId)
        {
            List<Inventory> allInventory =  _locationBL.GetLocationInventory(locId);
            if(allInventory.Count == 0)
            {
                Console.WriteLine("Looks like this store is empty :(");
            }
            else
            {
                Console.WriteLine("Select items to add to cart");
                for(int i = 0; i < allInventory.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {allInventory[i].ToString()}");
                }
                string input = Console.ReadLine();
                Product selectedProd = allInventory[Int32.Parse(input) - 1].Product;
                Console.WriteLine("How many do you want?");
                input = Console.ReadLine();

                LineItem item = new LineItem {
                    Product = selectedProd,
                    Quantity = Int32.Parse(input),
                    Order = _openOrder
                };
                try 
                {
                    _orderBL.AddItemToOrder(item);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }

        public void ViewCart()
        {
            Console.WriteLine(_openOrder.ToString());
        }
    }
}