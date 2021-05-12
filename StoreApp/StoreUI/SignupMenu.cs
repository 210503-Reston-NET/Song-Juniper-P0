using System;
using StoreBL;
using StoreDL;
using StoreModels;

namespace StoreUI
{
    public class SignupMenu
    {
        private CustomerBL _customerBL;
        private ValidationService _validationService;

        public SignupMenu(CustomerBL customerBL, ValidationService validationService)
        {
            _customerBL = customerBL;
            _validationService = validationService;
        }
        public bool Start()
        {
            string name =_validationService.ValidateString("Please enter your name:");
            return CreateNewCustomer(name);
        }

        public bool CreateNewCustomer(string name)
        {
            try
            {
                Customer newCustomer = new Customer(name);
                _customerBL.AddNewCustomer(newCustomer);
                Console.WriteLine("Sign up successful!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new LoginMenu(new CustomerBL(new CustomerRepo())).Start();
            }
        }
    }
}