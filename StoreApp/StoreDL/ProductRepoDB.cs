using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Entity = StoreDL.Entities;
using Model = StoreModels;

namespace StoreDL
{
    public class ProductRepoDB
    {
        private Entity.wssdbContext _context;
        private IMapper _mapper;
        public ProductRepoDB(Entity.wssdbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<Model.Product> GetAllProducts()
        {
            return _context.Products
            .Select(
                prod => _mapper.ParseProduct(prod)
            ).ToList();
        }

        public Model.Product GetProductById(int id)
        {
            Entity.Product found = _context.Products.FirstOrDefault(loc => loc.Id == id);
            return _mapper.ParseProduct(found);
        }

        public Model.Product GetProductByName(string name)
        {
            Entity.Product found = _context.Products.FirstOrDefault(loc => loc.PName == name);
            return _mapper.ParseProduct(found);
        }

        public Model.Product AddNewProduct(Model.Product product)
        {
            Entity.Product prodToAdd = _context.Products.Add(_mapper.ParseProduct(product, true)).Entity;
            _context.SaveChanges();

            return _mapper.ParseProduct(prodToAdd);
        }
    }
}