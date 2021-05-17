using System;
using StoreBL;
using StoreDL;
using StoreModels;

namespace StoreUI
{
    public class LoginMenu : IAuthMenu
    {
        private CustomerBL _customerBL;

        public LoginMenu(CustomerBL customerBL)
        {
            _customerBL = customerBL;
        }
        public Customer Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Please log in before continuing");
                Console.WriteLine("Please enter your name");
                string name = Console.ReadLine();
                Customer currentCustomer = FindCustomerByName(name);
                if(currentCustomer is null)
                {
                    Console.WriteLine("Uh oh, we didn't find a match.");
                    Console.WriteLine("[1] Try Again");
                    Console.WriteLine("[2] Sign up");
                    Console.WriteLine("[0] Go Back");
                    string input = Console.ReadLine();
                    switch(input)
                    {
                        case "0":
                        break;
                        case "1":
                        break;
                        case "2":
                            AuthMenuFactory.GetMenu("signup").Start();
                        break;
                        default:
                            Console.WriteLine("I don't understand your input, please try again.");
                        break;
                    }
                }
                else 
                {
                    Console.WriteLine($"Welcome {currentCustomer.Name}!");
                    return currentCustomer;
                }
            } while(repeat);
            return null;
        }

        public Customer FindCustomerByName(string name)
        {
            return _customerBL.FindCustomerByName(name);
        }
    }
}