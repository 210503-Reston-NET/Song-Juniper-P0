using System;
using System.Collections.Generic;
using StoreDL;
using StoreModels;

namespace StoreBL
{
    public class ProductBL
    {
        private ProductRepo _repo;
        public ProductBL(ProductRepo repo)
        {
            _repo = repo;
        }

        public Product AddNewProduct(Product prod)
        {
            return _repo.AddNewProduct(prod);
        }

        public List<Product> GetAllProducts()
        {
            return _repo.GetAllProducts();
        }

        public Product FindProductByName(string name)
        {
            return _repo.GetOneProduct(name);
        }
    }
}
