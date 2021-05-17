using System;
using System.Collections.Generic;
using Model = StoreModels;

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
        public string ProdName { get; set; }
        public string ProdDesc { get; set; }
        public double Price { get; set; }

        public string Category { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }

        public Model.Product ToModel ()
        {
            return new Model.Product {
                Id = this.Id,
                Name = this.ProdName,
                Description = this.ProdDesc,
                Price = this.Price
            };
        }
    }
}
