using System;
using System.Collections.Generic;

#nullable disable

namespace StoreUI.Entities
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public int? StoreId { get; set; }
        public int? ProdId { get; set; }
        public int Quantity { get; set; }

        public virtual Product Prod { get; set; }
        public virtual StoreFront Store { get; set; }
    }
}
