using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class Order
    {
        public Order()
        {
            LineItems = new HashSet<LineItem>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int StoreId { get; set; }
        public DateTime DateCreated { get; set; }
        public double Total { get; set; }
        public bool Placed { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual StoreFront Store { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
