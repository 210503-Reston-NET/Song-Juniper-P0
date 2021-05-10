using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using StoreModels;

namespace StoreDL
{
    public class LocationRepo
    {
        private const string locfile = "../StoreDL/locations.json";
        private string jsonString;
        public List<Location> GetAllLocations()
        {
            try
            {
                jsonString = File.ReadAllText(locfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Location>();
            }
            return JsonSerializer.Deserialize<List<Location>>(jsonString);
        }

        public Location GetOneLocation(string name)
        {
            return GetAllLocations().Where(loc => loc.Name == name).FirstOrDefault();
        }

        public Location AddNewLocation(Location location)
        {
            List<Location> allLocations = GetAllLocations();
            allLocations.Add(location);
            jsonString = JsonSerializer.Serialize(allLocations);
            File.WriteAllText(locfile, jsonString);
            return location;
        }

        public void RemoveLocation(Location location)
        {
            try
            {
                List<Location> allLocations = GetAllLocations();
                //This is broken... :((
                allLocations.Remove(location);
                jsonString =JsonSerializer.Serialize(allLocations);
                File.WriteAllText(locfile, jsonString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        
    }
}
