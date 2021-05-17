using System;
using StoreBL;
using StoreDL;
using StoreModels;

namespace StoreUI
{
    public class MainMenu : IMenu
    {
        private Customer _currentCustomer;

        public MainMenu() {}
        public void Start(Customer customer)
        {
            _currentCustomer = customer;
            bool repeat = true;
            do
            {
                string input;
                if(_currentCustomer is null)
                {
                    Console.WriteLine("Welcome to the Wild Side Story!");
                    Console.WriteLine("We specialize in sourdough supplies and products");
                    Console.WriteLine("Have you shopped with us before? [y/n]");
                    input = Console.ReadLine();
                    switch(input.ToLower())
                    {
                        case "y":
                            _currentCustomer = AuthMenuFactory.GetMenu("login").Start();
                        break;

                        case "n":
                            Console.WriteLine("Please sign up before continuing");
                            _currentCustomer = AuthMenuFactory.GetMenu("signup").Start();
                        break;

                        case "42":
                            _currentCustomer = new Customer("admin");
                            MenuFactory.GetMenu("admin").Start(_currentCustomer);
                        break;

                        default:
                            Console.WriteLine("I don't understand your input, please try again.");
                        break;
                    }
                }

                
                Console.WriteLine("What would you like to do today?");
                Console.WriteLine("[1] Browse Items");
                Console.WriteLine("[0] Exit");
                input = Console.ReadLine();
                switch(input)
                {
                    case "0":
                        Console.WriteLine("Goodbye, come back soon!");
                        repeat = false;
                    break;

                    case "1":
                        MenuFactory.GetMenu("browse").Start(_currentCustomer);
                    break;
                    
                    case "42":
                        MenuFactory.GetMenu("admin").Start(_currentCustomer);
                    break;
                    
                    default:
                        Console.WriteLine("I don't understand your input, please try again.");
                    break;
                }
            } while(repeat);
        }
    }
}