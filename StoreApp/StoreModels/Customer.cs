using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StoreModels
{
    /// <summary>
    /// This class should contain necessary properties and fields for customer info.
    /// </summary>
    public class Customer
    {
        private string _name;
        public Customer() {}
        public Customer (string name)
        {
            this.Name = name;
        }

        public Customer (int id, string name) : this(name)
        {
            this.Id = id;
        }
        public string Name 
        {
            get { return _name; }
            set 
            {
                if(value.Length == 0)
                {
                    throw new Exception("Name cannot be empty");
                }
                if(!Regex.IsMatch(value, @"^[A-Za-z .-]+$"))
                {
                    throw new Exception("Name is not valid");
                }
                _name = value;
            } 
        }

        public int Id { get; set; }

        public override string ToString()
        {
            return $"Name: {this.Name}";
        }

    }
}