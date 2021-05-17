using System.Collections.Generic;
using StoreDL;
using StoreModels;

namespace StoreBL
{
    public class OrderBL
    {
        private OrderRepoDB _repo;

        public OrderBL(OrderRepoDB repo)
        {
            _repo = repo;
        }

        public Order GetOpenOrder(int customerId, int locationId)
        {
            return _repo.GetOpenOrder(customerId, locationId);
        }

        public Order AddItemToOrder(LineItem item)
        {
            return _repo.AddItemToOrder(item);
        }

        public Order UpdateItemToOrder(LineItem item)
        {
            return _repo.UpdateItemToOrder(item);
        }

        public Order CreateOrder (Order order)
        {
            return _repo.CreateOrder(order);
        }

        public List<Order> GetOrdersByCustomerAndLocation (int customerId, int locationId)
        {
            return _repo.GetOrdersByCustomerAndLocation(customerId, locationId);
        }
    }
}