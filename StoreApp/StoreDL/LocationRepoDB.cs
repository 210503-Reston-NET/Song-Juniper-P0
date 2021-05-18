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
        private IMapper _mapper;
        public LocationRepoDB(Entity.wssdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.Location> GetAllLocations()
        {
            return _context.StoreFronts
            .AsNoTracking()
            .Select(
                loc => _mapper.ParseStore(loc)
            ).ToList();
        }

        public Model.Location GetLocationById(int id)
        {
            Entity.StoreFront found = _context.StoreFronts
            .AsNoTracking()
            .FirstOrDefault(loc => loc.Id == id);
            return _mapper.ParseStore(found);
        }

        public Model.Location GetLocationByName(string name)
        {
            Entity.StoreFront found = _context.StoreFronts
            .AsNoTracking()
            .FirstOrDefault(loc => loc.SName == name);
            return _mapper.ParseStore(found);
        }

        public Model.Location AddNewLocation(Model.Location loc)
        {
            Entity.StoreFront locToAdd = _context.StoreFronts
            .Add(_mapper.ParseStore(loc, true)).Entity;
            
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            return _mapper.ParseStore(locToAdd);
        }

        public List<Model.Inventory> GetLocationInventory(int locationId)
        {
            return _context.Inventories.Where(inventory => inventory.StoreId == locationId)
            .AsNoTracking()
            .Include("Product")
            .Select(
                inventory => _mapper.ParseInventory(inventory)
            ).ToList();

        }

        public Model.Inventory AddInventory(Model.Inventory inventory)
        {
            _context.Inventories.Add(
                _mapper.ParseInventory(inventory, true)
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
    }
}