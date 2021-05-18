using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StoreModels;
using Entity = StoreDL.Entities;
using Model = StoreModels;

namespace StoreDL
{
    public class OrderRepoDB
    {
        private Entity.wssdbContext _context;
        private IMapper _mapper;
        public OrderRepoDB(Entity.wssdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.Order> GetOrdersByCustomerAndLocation(int customerId, int locationId) {
            return _context.Orders
            .AsNoTracking()
            .Where(order => order.CustomerId == customerId && order.StoreId == locationId)
            .Select(
                order => _mapper.ParseOrder(order)
            )
            .ToList();
        }

        // public List<Model.Order> GetOrdersByLocation(Location location)
        // {

        // }

        public Model.Order GetOpenOrder(int customerId, int locationId)
        {
            Entity.Order found = _context.Orders.AsNoTracking().FirstOrDefault(order => order.CustomerId == customerId && order.StoreId == locationId && order.Placed == false);
            if(found is not null)
            {
                return _mapper.ParseOrder(found);
            }
            else return null;
        }

        public Model.Order GetOrderById(int orderId)
        {
            Entity.Order found = _context.Orders.AsNoTracking().FirstOrDefault(order => order.Id == orderId);
            if(found is not null)
            {
                return _mapper.ParseOrder(found);
            }
            else return null;
        }
/// <summary>
/// This method is for adding a new product to an open order
/// </summary>
/// <param name="item"></param>
/// <returns></returns>
        public Model.Order AddItemToOrder(Model.LineItem item)
        {
            _context.LineItems.Add(ToEntity(item));
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return GetOrderById(item.OrderId);
        }

/// <summary>
/// this is for changing quantities of an item already in the cart
/// </summary>
/// <param name="item"></param>
/// <returns></returns>
        public Model.Order UpdateItemToOrder(Model.LineItem item)
        {
            Entity.LineItem toUpdate = _context.LineItems
            .FirstOrDefault(it => it.Id == item.Id);
            toUpdate.Quantity = item.Quantity;

            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return GetOrderById(item.OrderId);
        }
        public Model.Order CreateOrder(Model.Order order)
        {
            _context.Orders.Add(_mapper.ParseOrder(order, true));
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return order;
        }

        
        public Entity.LineItem ToEntity(Model.LineItem item)
        {
            return new Entity.LineItem {
                Id = item.Id,
                Quantity = item.Quantity,
                OrderId = item.OrderId,
                ProductId = item.Product.Id,
                Product = ToEntity(item.Product)
            };
        }

        public Entity.Order ToEntity(Model.Order order)
        {
            List<Entity.LineItem> items = new List<Entity.LineItem>();
            if(order.LineItems is not null && order.LineItems.Count > 0)
            {
                foreach(Model.LineItem item in order.LineItems)
                {
                    items.Add(ToEntity(item));
                }
            }
            return new Entity.Order {
                CustomerId = order.CustomerId,
                StoreId = order.LocationId,
                Placed = order.Closed,
                DateCreated = order.DateCreated,
                LineItems = items
            };
        }

        public Entity.Customer ToEntity(Model.Customer customer)
        {
            List<Entity.Order> orders = new List<Entity.Order>();
            if(customer.Orders is not null)
            {
                foreach(Model.Order order in customer.Orders)
                {
                    orders.Add(ToEntity(order));
                }
            }
            return new Entity.Customer {
                Id = customer.Id,
                CName = customer.Name,
                Orders = orders
            };
        }

        public Entity.StoreFront ToEntity(Model.Location location)
        {
            List<Entity.Inventory> inventories = new List<Entity.Inventory>();
            if(location.Inventory is not null)
            {
                foreach(Model.Inventory inven in location.Inventory)
                {
                    inventories.Add(ToEntity(inven));
                }
            }
            return new Entity.StoreFront {
                Id = location.Id,
                SName = location.Name,
                SAddress = location.Address,
                Inventories = inventories 
            };
        }

        public Entity.Inventory ToEntity(Model.Inventory inven)
        {
            return new Entity.Inventory {
                Id = inven.Id,
                ProductId = inven.Product.Id,
                StoreId = inven.LocationId,
                Quantity = inven.Quantity
            };
        }

        public Entity.Product ToEntity(Model.Product prod)
        {
            return new Entity.Product
            {
                Id = prod.Id,
                PName = prod.Name,
                PDesc = prod.Description,
                Price = prod.Price
            };
        }
    }
}