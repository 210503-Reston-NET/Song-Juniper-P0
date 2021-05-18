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
        public string SName { get; set; }
        public string SAddress { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
