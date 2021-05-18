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

        public Order(int customerId, int storeId) : this()
        {
            this.CustomerId = customerId;
            this.LocationId = storeId;
        }
        public Order(int customerId, int storeId, int id) : this(customerId, storeId)
        {
            this.Id = id;
        }

        public Order(int customerId, int storeId, int id, List<LineItem> items) : this(customerId, storeId, id)
        {
            this.LineItems = items;
        }
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }
        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public List<LineItem> LineItems { get; set; }

        public bool Closed { get; set; }
        public double Total { get; set; }

        public override string ToString()
        {
            string ItemString = "";
            double total = 0.0;
            foreach(LineItem item in this.LineItems)
            {   
                ItemString += "\n" + item.ToString();
                total += item.Product.Price * item.Quantity;
            }
            return $"DateCreated: {this.DateCreated.ToString()} \nItems: {ItemString} \nTotal: {Math.Round(total, 2)}";
        }

    }
}