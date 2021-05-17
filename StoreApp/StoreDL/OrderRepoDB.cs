using System;
using System.Collections.Generic;
using System.Linq;
using StoreModels;
using Entity = StoreDL.Entities;
using Model = StoreModels;

namespace StoreDL
{
    public class OrderRepoDB
    {
        private Entity.wssdbContext _context;
        public OrderRepoDB(Entity.wssdbContext context)
        {
            _context = context;
        }

        public Model.Order GetOpenOrder(Model.Customer customer)
        {
            Entity.Order found = _context.Orders.FirstOrDefault(order => order.CustId == customer.Id && order.Closed == false);
            if(found is not null)
            {
                return found.ToModel();
            }
            else return null;
        }

        public Model.Order GetOrderById(int orderId)
        {
            Entity.Order found = _context.Orders.FirstOrDefault(order => order.Id == orderId);
            if(found is not null)
            {
                return found.ToModel();
            }
            else return null;
        }

        public Model.LineItem AddItemToOrder(Model.LineItem item)
        {
            Entity.LineItem added = _context.LineItems.Add(ToEntity(item)).Entity;
            _context.SaveChanges();
            return added.ToModel();
        }

        public Model.Order CreateOrder(Model.Order order)
        {
            Entity.Order added = _context.Orders.Add(ToEntity(order)).Entity;
            _context.SaveChanges();
            return GetOrderById(added.Id);
        }

        
        public Entity.LineItem ToEntity(Model.LineItem item)
        {
            return new Entity.LineItem {
                Id = item.Id,
                Quantity = item.Quantity,
                OrderId = item.Order.Id,
                ProdId = item.Product.Id
            };
        }

        public Entity.Order ToEntity(Model.Order order)
        {
            List<Entity.LineItem> items = new List<Entity.LineItem>();
            foreach(Model.LineItem item in order.LineItems)
            {
                items.Add(ToEntity(item));
            }
            return new Entity.Order {
                Cust = ToEntity(order.Customer),
                Store = ToEntity(order.Location),
                CustId = order.Customer.Id,
                StoreId = order.Location.Id,
                Closed = order.Closed,
                DateCreated = order.DateCreated,
                LineItems = items
            };
        }

        public Entity.Customer ToEntity(Model.Customer customer)
        {
            List<Entity.Order> orders = new List<Entity.Order>();
            foreach(Model.Order order in customer.Orders)
            {
                orders.Add(ToEntity(order));
            }
            return new Entity.Customer {
                Id = customer.Id,
                CustName = customer.Name,
                Orders = orders
            };
        }

        public Entity.StoreFront ToEntity(Model.Location location)
        {
            List<Entity.Inventory> inventories = new List<Entity.Inventory>();
            foreach(Model.Inventory inven in location.Inventory)
            {
                inventories.Add(ToEntity(inven));
            }
            return new Entity.StoreFront {
                Id = location.Id,
                Sfname = location.Name,
                Sfaddress = location.Address,
                Inventories = inventories 
            };
        }

        public Entity.Inventory ToEntity(Model.Inventory inven)
        {
            return new Entity.Inventory {
                Id = inven.Id,
                Prod = ToEntity(inven.Product),
                Quantity = inven.Quantity
            };
        }

        public Entity.Product ToEntity(Model.Product prod)
        {
            return new Entity.Product
            {
                Id = prod.Id,
                ProdName = prod.Name,
                ProdDesc = prod.Description,
                Price = prod.Price
            };
        }
    }
}