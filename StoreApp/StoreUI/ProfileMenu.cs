using System;
using System.Collections.Generic;
using StoreBL;
using StoreModels;

namespace StoreUI
{
    public class ProfileMenu : IMenu
    {
        private OrderBL _orderBL;
        private ProductBL _productBL;
        private LocationBL _locationBL;
        private List<Location> _allLocations;
        private List<Order> _ordersFromCustomer;
        private Customer _currentCustomer;

        public ProfileMenu(OrderBL orderBL, ProductBL productBL, LocationBL locationBL)
        {
            _orderBL = orderBL;
            _productBL = productBL;
            _locationBL = locationBL;
        }
        public void Start(Customer customer)
        {
            _currentCustomer = customer;
            bool repeat = true;
            string input;
            do
            {
                Console.WriteLine("This is profile menu");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[0] Go Back");
                Console.WriteLine("[1] View My Profile");
                Console.WriteLine("[2] View My Orders");

                input = Console.ReadLine();

                switch(input)
                {
                    case "0":
                        repeat = false;
                    break;

                    case "1":
                        ViewProfile();
                    break;

                    case "2":
                        ViewOrders();
                    break;

                    default:
                        Console.WriteLine("I don't understand your input, please try again.");
                    break;
                }
            } while(repeat);
        }

        private void ViewOrders()
        {
            _ordersFromCustomer = _orderBL.GetOrdersByCustomerId(_currentCustomer.Id);
            _allLocations = _locationBL.GetAllLocations();
            Location orderLocation;
            foreach(Order order in _ordersFromCustomer)
            {
                order.LineItems = _orderBL.GetLineItemsByOrderId(order.Id);
                orderLocation = _allLocations.Find(loc => loc.Id == order.LocationId);
                Console.WriteLine($"Location: \n{orderLocation.ToString()} \nOrder Detail: \n{order.ToString()} \n\n");
            }
        }

        private void ViewProfile()
        {
            Console.WriteLine($"Name: {_currentCustomer.Name}");
        }
    }
}