using System;

namespace StoreModels
{

    /// <summary>
    /// This data structure models a product and its quantity. The quantity was separated from the product as it could vary from orders and locations.  
    /// </summary>
    public class Inventory
    {
        public Inventory() {}

        public Inventory(Product product, int storeId, int quant)
        {
            this.Product = product;
            this.LocationId = storeId;
            this.Quantity = quant;
        }

        public Inventory(Product product, int storeId, int quant, int id): this(product, storeId, quant)
        {
            this.Id = id;
        }

        public int Id { get; set; }
        public Product Product { get; set; }

        public int LocationId { get; set; }

        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"Name: {this.Product.Name} \nDescription: {this.Product.Description} \nQuantity: {this.Quantity}";
        }
    }
}