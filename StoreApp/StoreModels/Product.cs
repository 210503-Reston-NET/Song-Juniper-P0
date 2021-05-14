namespace StoreModels
{
    //This class should contain all necessary fields to define a product.
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description {get; set; }
        public double Price { get; set; }
        public string Category { get; set; }

        public Product(string name, string desc, double price, string cat)
        {
            this.Name = name;
            this.Description = desc;
            this.Price = price;
            this.Category = cat;
        }

        public Product(int id, string name, string desc, double price, string cat) : this (name, desc, price, cat)
        {
            this.Id = id;
        }

        public Product(){ }

        public override string ToString()
        {
            return $"Name: {this.Name} \nDescription: {this.Description} \nPrice: {this.Price} \nCategory: {this.Category}";
        }
    }
}