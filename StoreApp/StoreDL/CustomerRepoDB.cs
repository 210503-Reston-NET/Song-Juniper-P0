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
        private IMapper _mapper;
        public CustomerRepoDB(Entity.wssdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.Customer> GetAllCustomers()
        {
            return _context.Customers
            .AsNoTracking()
            .Select(
                customer => _mapper.ParseCustomer(customer)
            ).ToList();
        }

        public Model.Customer GetCustomerById(int id)
        {
            Entity.Customer found = _context.Customers
            .AsNoTracking()
            .FirstOrDefault(customer => customer.Id == id);
            return _mapper.ParseCustomer(found);
        }

        public Model.Customer GetCustomerByName(string name)
        {
            Entity.Customer found = _context.Customers
            .AsNoTracking()
            .FirstOrDefault(customer => customer.CName == name);
            return _mapper.ParseCustomer(found);
        }

        public Model.Customer AddNewCustomer(Model.Customer customer)
        {
            Entity.Customer newCust = _context.Customers.Add(_mapper.ParseCustomer(customer, true)).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return _mapper.ParseCustomer(newCust);
        }
    }
}