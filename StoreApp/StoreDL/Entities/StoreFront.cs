using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class StoreFront
    {
        public StoreFront()
        {
            Inventories = new HashSet<Inventory>();
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Sfname { get; set; }
        public string Sfaddress { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        internal StoreModels.Location ToModel()
        {
            List<StoreModels.Inventory> modelInven = new List<StoreModels.Inventory>();
            List<StoreModels.Order> modelOrder = new List<StoreModels.Order>();
            foreach(Inventory item in this.Inventories)
            {
                modelInven.Add(item.ToModel());
            }
            foreach(Order order in this.Orders)
            {
                modelOrder.Add(order.ToModel());
            }
            return new StoreModels.Location {
                Id = this.Id,
                Name = this.Sfname,
                Address = this.Sfaddress,
                Inventory = modelInven,
                Orders = modelOrder
            };
        }
    }
}
