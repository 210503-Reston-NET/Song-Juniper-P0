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
        public ProductRepoDB(Entity.wssdbContext context)
        {
            _context = context;
        }

        public List<Model.Product> GetAllProducts()
        {
            return _context.Products
            .Select(
                prod => ConvertToModel(prod)
            ).ToList();
        }

        public Model.Product GetProductById(int id)
        {
            Entity.Product found = _context.Products.FirstOrDefault(loc => loc.Id == id);
            return ConvertToModel(found);
        }

        public Model.Product GetProductByName(string name)
        {
            Entity.Product found = _context.Products.FirstOrDefault(loc => loc.ProdName == name);
            return ConvertToModel(found);
        }

        public Model.Product AddNewProduct(Model.Product product)
        {
            Entity.Product prodToAdd = _context.Products.Add(ConvertToEntity(product)).Entity;
            _context.SaveChanges();

            return ConvertToModel(prodToAdd);
        }

        private static Entity.Product ConvertToEntity(Model.Product prod)
        {
            if(prod is not null)
            {
                if(prod.Id == 0)
                    return new Entity.Product{
                        ProdName = prod.Name,
                        ProdDesc = prod.Description,
                        Price = prod.Price,
                        Category = prod.Category
                    };
                else
                    return new Entity.Product{
                        Id = prod.Id,
                        ProdName = prod.Name,
                        ProdDesc = prod.Description,
                        Price = prod.Price,
                        Category = prod.Category
                    };
            }
            else return null;
        }

        private static Model.Product ConvertToModel(Entity.Product product)
        {
            if(product is not null)
            {
                return new Model.Product{
                    Id = product.Id,
                    Name = product.ProdName,
                    Description = product.ProdDesc,
                    Price = product.Price,
                    Category = product.Category
                };
            }
            else return null;
        }
    }
}