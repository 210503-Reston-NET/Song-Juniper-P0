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
                if(_openOrder is null)
                {
                    _openOrder = new Order {
                        
                        CustomerId = _currentCustomer.Id,
                        LocationId = _currentLocation.Id,
                        LineItems = new List<LineItem>()
                    };
                }
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[1] Browse all items");
                Console.WriteLine("[2] View current order");
                Console.WriteLine("[3] Place Order");
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
                        PlaceOrder();
                    break;

                    default:
                        Console.WriteLine("I don't understand your input, please try again.");
                    break;
                }
            } while(repeat);
        }

        private void PlaceOrder()
        {
            Console.WriteLine("This is your current order");
            Console.WriteLine(_currentLocation.ToString());
            Console.WriteLine(_openOrder.ToString());
            bool repeat = true;
            do
            {
                Console.WriteLine("Would you like to place the order? [y/n]");
                string input = Console.ReadLine().ToLower();
                if(input == "y")
                {
                    repeat = false;
                    try
                    {
                        _openOrder.Closed = true;
                        _orderBL.CreateOrder(_openOrder);
                        //update the store's inventory after the successful placement of the order
                        List<Inventory> allInventory =  _locationBL.GetLocationInventory(_currentLocation.Id);
                        foreach(LineItem lineItem in _openOrder.LineItems)
                        {
                            Inventory boughtItem = allInventory.Find(invenItem => invenItem.Product.Id == lineItem.Product.Id);
                            boughtItem.Quantity -= lineItem.Quantity;
                            _locationBL.UpdateInventoryItem(boughtItem);
                        }
                        Console.WriteLine("Order placed successfully!");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else if (input == "n") {
                    repeat = false;
                    return;
                }
                else {
                    Console.WriteLine("I don't understand your input, please try again");
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
                Console.WriteLine("Select an item to add to your cart");
                for(int i = 0; i < allInventory.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {allInventory[i].ToString()}");
                }
                string input = Console.ReadLine();
                Product selectedProd = allInventory[Int32.Parse(input) - 1].Product;
                LineItem item = _openOrder.LineItems.Find(item => item.Product.Id == selectedProd.Id);
                
                Console.WriteLine("How many do you want?");
                if(item is not null)
                {
                    Console.WriteLine($"You currently have {item.Quantity} in your cart");
                    input = Console.ReadLine();
                    //there is already a same product in the cart
                    //update that quantity instead
                    item.Quantity = Int32.Parse(input);
                    _openOrder.UpdateTotal();
                }
                else
                {
                    input = Console.ReadLine();
                    //this is a new product in this order. Add it.
                    item = new LineItem {
                        Product = selectedProd,
                        Quantity = Int32.Parse(input),
                        OrderId = _openOrder.Id
                    };
                    _openOrder.LineItems.Add(item);
                    _openOrder.UpdateTotal();
                    _openOrder.ToString();
                }
            }
        }

        public void ViewCart()
        {
            Console.WriteLine(_openOrder.ToString());
        }
    }
}