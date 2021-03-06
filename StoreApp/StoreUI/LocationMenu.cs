using System;
using StoreModels;
using StoreBL;
using System.Collections.Generic;
using Serilog;

namespace StoreUI
{
    public class LocationMenu : IMenu
    {
        private LocationBL _locBL;
        private ValidationService _validationService;

        public LocationMenu(LocationBL locationBL, ValidationService validationService)
        {
            _locBL = locationBL;
            _validationService = validationService;
        }
        public void Start(Customer customer)
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("This is Manage Locations Menu");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[1] View All Locations");
                Console.WriteLine("[2] Manage an Existing Location");
                Console.WriteLine("[3] Create a New Location");
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
                        MenuFactory.GetMenu("inventory").Start(customer);
                    break;

                    case "3":
                        AddNewLocation();
                    break;

                    default:
                        Console.WriteLine("I don't understand your input, please try again.");
                    break;
                }
            } while(repeat);
        }

        public void AddNewLocation()
        {
            //gather information about the new location
            Console.WriteLine("Please enter the details of the new location");
            string name = _validationService.ValidateString("Enter a unique name for the location");
            string address = _validationService.ValidateString("Enter the address for the location");
            
            using (var log = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console().WriteTo.File("../logs/logs.txt", rollingInterval: RollingInterval.Day).CreateLogger())
            {
                try
                {
                    //create new location object and send it over to BL
                    Location newLoc = new Location(name, address);
                    Location createdLoc = _locBL.AddNewLocation(newLoc);
                    Console.WriteLine("Location Creation has been successful!");
                    Console.WriteLine(createdLoc.ToString());
                }
                catch (Exception ex)
                {
                    log.Warning(ex.Message);
                }
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
    }
}