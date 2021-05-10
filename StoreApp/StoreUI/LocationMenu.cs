using System;
using StoreModels;
using StoreBL;
using System.Collections.Generic;

namespace StoreUI
{
    public class LocationMenu : IMenu
    {
        private LocationBL _locBL;

        public LocationMenu(LocationBL locationBL)
        {
            _locBL = locationBL;
        }
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("This is Manage Locations Menu");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[1] View All Locations");
                Console.WriteLine("[2] Create a New Location");
                // Console.WriteLine("[3] Remove a Location");
                Console.WriteLine("[3] Manage a Location's Inventory");
                Console.WriteLine("[0] Go Back");
                string input = Console.ReadLine();
                switch(input)
                {
                    case "0":
                    repeat = false;
                    break;

                    case "1":
                        GetAllLocations();
                    break;

                    case "2":
                        AddNewLocation();
                    break;

                    // case "3":
                    //     RemoveLocation();
                    // break;

                    case "3":
                        MenuFactory.GetMenu("inventory");
                    break;

                    default:
                        Console.WriteLine("I don't understand your input, please try again.");
                    break;
                }
            } while(repeat);
        }

        public void AddNewLocation()
        {
            try
            {
                //gather information about the new location
                string name, address;
                Console.WriteLine("Please enter the details of the new location");
                Console.WriteLine("Enter a unique name for the location");
                name = Console.ReadLine();
                Console.WriteLine("Enter the address for the location");
                address = Console.ReadLine();
                
                if(_locBL.FindLocationByName(name) is null)
                {
                    //create new location object and send it over to BL
                    Location newLoc = new Location(name, address);
                    _locBL.AddNewLocation(newLoc);

                    Console.WriteLine("Location Creation has been successful!");
                    Console.WriteLine(newLoc.ToString());
                }
                else
                {
                    Console.WriteLine("There is already a product with the same name");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void GetAllLocations()
        {
            List<Location> allLoc = _locBL.GetAllLocations();
            if(allLoc.Count == 0)
            {
                Console.WriteLine("No locations to display!");
            }
            else
            {
                foreach (Location loc in allLoc)
                {
                    Console.WriteLine(loc.ToString() + "\n");
                }
            }
        }

        public void RemoveLocation()
        {
            try
            {
                Console.WriteLine("What is the name of the location you wish to remove?");
                string name = Console.ReadLine();
                Location loc = _locBL.FindLocationByName(name);
                Console.WriteLine("Is this the location you're looking to remove? [Y/N]");
                Console.WriteLine(loc.ToString());
                string input = Console.ReadLine();
                if(input == "y" | input == "Y")
                {
                    _locBL.RemoveLocation(loc);
                    Console.WriteLine("Successfully Removed!");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}