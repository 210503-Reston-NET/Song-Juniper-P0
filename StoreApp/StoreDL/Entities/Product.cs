using System;
using System.Collections.Generic;

#nullable disable

namespace StoreDL.Entities
{
    public partial class Product
    {
        public Product()
        {
            Inventories = new HashSet<Inventory>();
            LineItems = new HashSet<LineItem>();
        }

        public int Id { get; set; }
        public string PName { get; set; }
        public string PDesc { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
