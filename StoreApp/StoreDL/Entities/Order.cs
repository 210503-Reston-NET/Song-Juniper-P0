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
        public int? CustId { get; set; }
        public int? StoreId { get; set; }
        public DateTime? DateCreated { get; set; }
        public double? Total { get; set; }
        public bool? Closed { get; set; }

        public virtual Customer Cust { get; set; }
        public virtual StoreFront Store { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
