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
            .AsNoTracking()
            .Select(
                loc => ConvertToModel(loc)
            ).ToList();
        }

        public Model.Location GetLocationById(int id)
        {
            Entity.StoreFront found = _context.StoreFronts
            .AsNoTracking()
            .FirstOrDefault(loc => loc.Id == id);
            return ConvertToModel(found);
        }

        public Model.Location GetLocationByName(string name)
        {
            Entity.StoreFront found = _context.StoreFronts
            .AsNoTracking()
            .FirstOrDefault(loc => loc.Sfname == name);
            return ConvertToModel(found);
        }

        public Model.Location AddNewLocation(Model.Location loc)
        {
            Entity.StoreFront locToAdd = _context.StoreFronts.Add(ConvertToEntity(loc)).Entity;
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return ConvertToModel(locToAdd);
        }

        public List<Model.Inventory> GetLocationInventory(int locationId)
        {
            return _context.Inventories.Where(inventory => inventory.StoreId == locationId)
            .AsNoTracking()
            .Select(
                inventory => new Model.Inventory
                {
                    Id = inventory.Id,
                    Product = ProductRepoDB.ConvertToModel(inventory.Prod),
                    Quantity = inventory.Quantity,
                    Location = LocationRepoDB.ConvertToModel(inventory.Store),
                }
            ).ToList();

        }

        public Model.Inventory AddInventory(Model.Inventory inventory)
        {
            _context.Inventories.Add(
                new Entity.Inventory{
                    StoreId = inventory.Location.Id,
                    ProdId = inventory.Product.Id,
                    Quantity = inventory.Quantity
                }
            );
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return inventory;
        }

        public Model.Inventory UpdateInventoryItem(Model.Inventory inventory)
        {
            Entity.Inventory toUpdate = _context.Inventories
            .FirstOrDefault(inven => inven.Id == inventory.Id);
            toUpdate.Quantity = inventory.Quantity;
            
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return inventory;
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