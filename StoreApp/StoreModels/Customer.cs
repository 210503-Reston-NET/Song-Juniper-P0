using System.Collections.Generic;

namespace StoreModels
{
    /// <summary>
    /// This class should contain necessary properties and fields for customer info.
    /// </summary>
    public class Customer
    {
        public Customer() {}
        public Customer (string name)
        {
            this.Name = name;
        }

        public Customer (int id, string name) : this(name)
        {
            this.Id = id;
        }
        public string Name { get; set; }

        public int Id { get; set; }

        public override string ToString()
        {
            return $"Name: {this.Name}";
        }

    }
}