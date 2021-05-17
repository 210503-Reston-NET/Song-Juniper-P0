namespace StoreModels
{
    public class LineItem
    {
        public LineItem() {}

        public LineItem(Product product, int orderId, int quant)
        {
            this.Product = product;
            this.OrderId = orderId;
            this.Quantity = quant;
        }

        public LineItem(Product product, int orderId, int quant, int id): this(product, orderId, quant)
        {
            this.Id = id;
        }

        public int Id { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"Name: {this.Product.Name}, Price: {this.Product.Price}, Quantity: {this.Quantity}";
        }
    }
}