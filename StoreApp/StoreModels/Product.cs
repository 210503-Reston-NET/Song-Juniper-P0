namespace StoreModels
{
    //This class should contain all necessary fields to define a product.
    public class Product
    {
        public string Name { get; set; }
        public string Description {get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
    }
}