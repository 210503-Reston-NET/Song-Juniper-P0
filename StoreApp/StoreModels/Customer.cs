using System.Collections.Generic;

namespace StoreModels
{
    /// <summary>
    /// This class should contain necessary properties and fields for customer info.
    /// </summary>
    public class Customer
    {
        public string Name { get; set; }
        //TODO: add more properties to identify the customer
        public Order CurrentOrder { get; set; }

        public List<Order> OrderHistory { get; set; }
    }
}