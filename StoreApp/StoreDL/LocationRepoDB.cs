using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Entity = StoreDL.Entities;
using Model = StoreModels;

namespace StoreDL
{
    public class LocationRepoDB
    {
        private Entity.wssdbContext _context;
        public LocationRepoDB(Entity.wssdbContext context)
        {
            _context = context;
        }

        public List<Model.Location> GetAllLocations()
        {
            return _context.StoreFronts
            .Select(
                loc => ConvertToModel(loc)
            ).ToList();
        }

        public Model.Location GetLocationById(int id)
        {
            Entity.StoreFront found = _context.StoreFronts.FirstOrDefault(loc => loc.Id == id);
            return ConvertToModel(found);
        }

        public Model.Location GetLocationByName(string name)
        {
            Entity.StoreFront found = _context.StoreFronts.FirstOrDefault(loc => loc.Sfname == name);
            return ConvertToModel(found);
        }

        public Model.Location AddNewLocation(Model.Location loc)
        {
            Entity.StoreFront locToAdd = _context.StoreFronts.Add(ConvertToEntity(loc)).Entity;
            _context.SaveChanges();

            return ConvertToModel(locToAdd);
        }

        private static Entity.StoreFront ConvertToEntity(Model.Location location)
        {
            if(location is not null)
            {
                if(location.Id == 0)
                    return new Entity.StoreFront{
                        Sfname = location.Name,
                        Sfaddress = location.Address
                    };
                else
                    return new Entity.StoreFront{
                        Id = location.Id,
                        Sfname = location.Name,
                        Sfaddress = location.Address
                    };
            }
            else return null;
        }

        private static Model.Location ConvertToModel(Entity.StoreFront store)
        {
            if(store is not null)
            {
                return new Model.Location{
                    Id = store.Id,
                    Name = store.Sfname,
                    Address = store.Sfaddress
                };
            }
            else return null;
        }
    }
}