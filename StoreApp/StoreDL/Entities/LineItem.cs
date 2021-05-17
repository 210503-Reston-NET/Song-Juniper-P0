﻿using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class LineItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProdId { get; set; }
        public int Quantity { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Prod { get; set; }

        internal StoreModels.LineItem ToModel()
        {
            return new StoreModels.LineItem {
                Id = this.Id,
                OrderId = this.OrderId,
                Product = this.Prod.ToModel(),
                Quantity = this.Quantity
            };
        }
    }
}
