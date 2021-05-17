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
        public bool Closed { get; set; }

        public virtual Customer Cust { get; set; }
        public virtual StoreFront Store { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }

        public StoreModels.Order ToModel()
        {
            List<StoreModels.LineItem> modelItems = new List<StoreModels.LineItem>();
            if(this.LineItems is not null)
            {
                foreach(LineItem item in this.LineItems){
                    modelItems.Add(item.ToModel());
                }
            }
            return new StoreModels.Order {
                Id = this.Id,
                Customer = this.Cust.ToModel(),
                Location = this.Store.ToModel(),
                LineItems = modelItems,
                Closed = this.Closed,
                Total = this.Total is null? 0.0 : this.Total
            };
        }
    }
}
