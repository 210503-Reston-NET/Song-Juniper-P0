using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Entity = StoreDL.Entities;
using Model = StoreModels;

namespace StoreDL
{
    public class CustomerRepoDB
    {
        private Entity.wssdbContext _context;
        public CustomerRepoDB(Entity.wssdbContext context)
        {
            _context = context;
        }

        public List<Model.Customer> GetAllCustomers()
        {
            return _context.Customers
            .AsNoTracking()
            .Select(
                customer => customer.ToModel()
            ).ToList();
        }

        public Model.Customer GetCustomerById(int id)
        {
            Entity.Customer found = _context.Customers
            .AsNoTracking()
            .FirstOrDefault(customer => customer.Id == id);
            return found.ToModel();
        }

        public Model.Customer GetCustomerByName(string name)
        {
            Entity.Customer found = _context.Customers
            .AsNoTracking()
            .FirstOrDefault(customer => customer.CustName == name);
            return found.ToModel();
        }

        public Model.Customer AddNewCustomer(Model.Customer customer)
        {
            Entity.Customer newCust = _context.Customers.Add(ConvertToEntity(customer)).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return newCust.ToModel();
        }

        private static Entity.Customer ConvertToEntity(Model.Customer customer)
        {
            if(customer is not null)
            {
                if(customer.Id == 0)
                    return new Entity.Customer{
                        CustName = customer.Name
                    };
                else
                    return new Entity.Customer{
                        Id = customer.Id,
                        CustName = customer.Name
                    };
            }
            else return null;
        }

        private static Model.Customer ConvertToModel(Entity.Customer customer)
        {
            if(customer is not null)
            {
                return new Model.Customer{
                    Id = customer.Id,
                    Name = customer.CustName
                };
            }
            else return null;
        }
    }
}