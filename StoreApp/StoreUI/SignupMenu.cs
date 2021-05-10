using System;
using StoreBL;
using StoreDL;
using StoreModels;

namespace StoreUI
{
    public class SignupMenu
    {
        private CustomerBL _customerBL;

        public SignupMenu(CustomerBL customerBL)
        {
            _customerBL = customerBL;
        }
        public bool Start()
        {
            Console.WriteLine("Please enter your name:");
            string name = Console.ReadLine();

            return CreateNewCustomer(name);
        }

        public bool CreateNewCustomer(string name)
        {
            try
            {
                if (_customerBL.FindCustomerByName(name) is null) 
                {
                    Customer newCustomer = new Customer(name);
                    _customerBL.AddNewCustomer(newCustomer);
                    Console.WriteLine("Sign up successful!");
                    return true;
                }
                else
                {
                    Console.WriteLine("User already exists");
                    Console.WriteLine("Please Login instead");
                    return new LoginMenu(new CustomerBL(new CustomerRepo())).Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}