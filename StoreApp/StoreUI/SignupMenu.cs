using System;
using StoreBL;
using StoreDL;
using StoreModels;
using Serilog;

namespace StoreUI
{
    public class SignupMenu : IAuthMenu
    {
        private CustomerBL _customerBL;
        private ValidationService _validationService;

        public SignupMenu(CustomerBL customerBL, ValidationService validationService)
        {
            _customerBL = customerBL;
            _validationService = validationService;
        }
        public Customer Start()
        {
            string name =_validationService.ValidateString("Please enter your name:");
            return CreateNewCustomer(name);
        }

        public Customer CreateNewCustomer(string name)
        {
            using (var log = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().WriteTo.File("../logs/logs.txt", rollingInterval: RollingInterval.Day).CreateLogger())
            {
                try
                {
                    Customer newCustomer = new Customer(name);
                    _customerBL.AddNewCustomer(newCustomer);
                    Console.WriteLine("Sign up successful!");
                    return _customerBL.FindCustomerByName(name);
                }
                catch (Exception ex)
                {
                    log.Information(ex, ex.Message);
                    return AuthMenuFactory.GetMenu("signup").Start();
                }
            }
        }
    }
}