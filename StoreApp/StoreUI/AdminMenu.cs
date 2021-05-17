using System;
using StoreModels;

namespace StoreUI
{
    public class AdminMenu : IMenu
    {
        private Customer _currentCustomer;
        public void Start(Customer customer)
        {
            _currentCustomer = customer;
            bool repeat = true;
            bool access = false;
            do
            {
                //Entry and a silly authentication
                if(!access)
                {
                    Console.WriteLine("Welcome to the admin menu");
                    Console.WriteLine("Please verify your admin identity");
                    string pw = Console.ReadLine();
                    if(pw != "da")
                    {
                        Console.WriteLine("Access Denied");
                        break;
                    }
                    access = true;
                }

                Console.WriteLine("Welcome Admin");
                Console.WriteLine("What would you like to do today?");
                Console.WriteLine("[1] Manage Locations");
                Console.WriteLine("[2] Manage Products");
                Console.WriteLine("[3] Manage Inventory");
                Console.WriteLine("[0] Go Back to the Main Menu");

                string input = Console.ReadLine();
                switch(input)
                {
                    case "0":
                        repeat = false;
                    break;

                    case "1":
                        MenuFactory.GetMenu("location").Start(_currentCustomer);
                    break;

                    case "2":
                        MenuFactory.GetMenu("product").Start(_currentCustomer);
                    break;

                    case "3":
                        MenuFactory.GetMenu("inventory").Start(_currentCustomer);
                    break;

                    default:
                        Console.WriteLine("I don't understand your input, please try again.");
                    break;
                }
            } while(repeat);
        }
    }
}