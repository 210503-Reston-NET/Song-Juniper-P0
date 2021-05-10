using System;
using System.Collections.Generic;
using StoreDL;
using StoreModels;

namespace StoreBL
{
    public class CustomerBL
    {
        private CustomerRepo _repo;
        public CustomerBL(CustomerRepo repo)
        {
            _repo = repo;
        }

        public Customer AddNewCustomer(Customer customer)
        {
            return _repo.AddNewCustomer(customer);
        }

        public List<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

        public Customer FindCustomerByName(string name)
        {
            return _repo.GetOneCustomer(name);
        }
    }
}