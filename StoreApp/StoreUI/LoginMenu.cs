using System;
using StoreBL;
using StoreDL;
using StoreModels;

namespace StoreUI
{
    public class LoginMenu
    {
        private CustomerBL _customerBL;

        public LoginMenu(CustomerBL customerBL)
        {
            _customerBL = customerBL;
        }
        public bool Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Please log in before continuing");
                Console.WriteLine("Please enter your name");
                string name = Console.ReadLine();
                if(FindCustomerByName(name) is null)
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
                            new SignupMenu(new CustomerBL(new CustomerRepo()), new ValidationService()).Start();
                        break;
                        default:
                            Console.WriteLine("I don't understand your input, please try again.");
                        break;
                    }
                }
                else 
                {
                    Console.WriteLine($"Welcome {name}!");
                    return true;
                }
            } while(repeat);
            return false;
        }

        public Customer FindCustomerByName(string name)
        {
            return _customerBL.FindCustomerByName(name);
        }
    }
}