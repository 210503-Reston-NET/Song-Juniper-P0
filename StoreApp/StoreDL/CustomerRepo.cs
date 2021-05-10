using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using StoreModels;

namespace StoreDL
{
    public class CustomerRepo
    {
        private const string filepath = "../StoreDL/customers.json";
        private string jsonString;
        public List<Customer> GetAllCustomers()
        {
            try
            {
                jsonString = File.ReadAllText(filepath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Customer>();
            }
            return JsonSerializer.Deserialize<List<Customer>>(jsonString);
        }

        public Customer GetOneCustomer(string name)
        {
            return GetAllCustomers().Where(cust => cust.Name == name).FirstOrDefault();
        }

        public Customer AddNewCustomer(Customer customer)
        {
            List<Customer> allCustomers = GetAllCustomers();
            allCustomers.Add(customer);
            jsonString = JsonSerializer.Serialize(allCustomers);
            File.WriteAllText(filepath, jsonString);
            return customer;
        }
        
    }
}
