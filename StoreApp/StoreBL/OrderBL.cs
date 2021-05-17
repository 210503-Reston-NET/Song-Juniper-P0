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

        public Order GetOpenOrder(Customer customer)
        {
            return _repo.GetOpenOrder(customer);
        }

        public LineItem AddItemToOrder(LineItem item)
        {
            return _repo.AddItemToOrder(item);
        }

        public Order CreateOrder (Order order)
        {
            return _repo.CreateOrder(order);
        }
    }
}