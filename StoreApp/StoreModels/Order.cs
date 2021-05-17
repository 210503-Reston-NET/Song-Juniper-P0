using System;
using System.Collections.Generic;

namespace StoreModels
{
    /// <summary>
    /// This class should contain all the fields and properties that define a customer order. 
    /// </summary>
    public class Order
    {
        public Order() {
            this.DateCreated = DateTime.Now;
            this.Closed = false;
        }

        public Order(Customer customer, Location location) : this()
        {
            this.Customer = customer;
            this.Location = location;
        }
        public Order(Customer customer, Location location, int id) : this(customer, location)
        {
            this.Id = id;
        }

        public Order(Customer customer, Location location, int id, List<LineItem> items)
        {
            this.LineItems = items;
            this.Total = CalculateTotal(items);
        }
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }
        public Customer Customer { get; set; }
        public Location Location { get; set; }
        public List<LineItem> LineItems { get; set; }

        public double CalculateTotal(List<LineItem> items)
        {
            double total = 0.0;
            foreach(LineItem item in items)
            {
                total += item.Product.Price * item.Quantity;
            }
            return total;
        }

        public bool Closed { get; set; }
        public double? Total { get; set; }

        public override string ToString()
        {
            string ItemString = "";
            foreach(LineItem item in this.LineItems)
            {
                ItemString += "\n" + item.ToString();
            }
            return $"Location: {this.Location.ToString()} \nDateCreated: {this.DateCreated.ToString()} \nItems: {ItemString}";
        }

    }
}