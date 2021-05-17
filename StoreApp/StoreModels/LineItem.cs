namespace StoreModels
{
    public class LineItem
    {
        public LineItem() {}

        public LineItem(Product product, Order order, int quant)
        {
            this.Product = product;
            this.Order = order;
            this.Quantity = quant;
        }

        public LineItem(Product product, Order order, int quant, int id): this(product, order, quant)
        {
            this.Id = id;
        }

        public int Id { get; set; }
        public Product Product { get; set; }

        public Order Order { get; set; }

        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"Name: {this.Product.Name} \nDescription: {this.Product.Description} \nQuantity: {this.Quantity}";
        }
    }
}