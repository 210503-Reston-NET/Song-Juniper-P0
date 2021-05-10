﻿using System;
using System.Collections.Generic;
using StoreDL;
using StoreModels;

namespace StoreBL
{
    public class LocationBL
    {
        private LocationRepo _repo;
        public LocationBL(LocationRepo repo)
        {
            _repo = repo;
        }

        public Location AddNewLocation(Location newLoc)
        {
            return _repo.AddNewLocation(newLoc);
        }

        public void RemoveLocation(Location location)
        {
            _repo.RemoveLocation(location);
        }
        public List<Location> GetAllLocations()
        {
            return _repo.GetAllLocations();
        }

        public Location FindLocationByName(string name)
        {
            return _repo.GetOneLocation(name);
        }
    }
}