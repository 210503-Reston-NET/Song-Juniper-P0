using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string CustName { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public StoreModels.Customer ToModel()
        {
            List<StoreModels.Order> modelOrders = new List<StoreModels.Order>();
            foreach(Order order in this.Orders) {
                modelOrders.Add(order.ToModel());
            }
            return new StoreModels.Customer {
                Id = this.Id,
                Name = this.CustName,
                Orders = modelOrders
            };
        }
    }
}
