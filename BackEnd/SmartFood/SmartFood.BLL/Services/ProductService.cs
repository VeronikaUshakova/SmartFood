using SmartFood.DAL.Entities;
using SmartFood.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartFood.DAL.Repositories;
using SmartFood.BLL.Infrastructure;

namespace SmartFood.BLL.Services
{
    public class ProductService
    {
        EFUnitOfWork Database = new EFUnitOfWork();

        public async Task<List<Product>> GetALLProducts()
        {
            List<Product> products = await Database.Products.GetAll();
            return products;
        }

        public Product GetProduct(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id продукта");
            var product = Database.Products.Get(id.Value);
            if (product == null)
                throw new ValidationException("Продукт не найден");
            return product;
        }
        List<Product> Find(Func<Product, Boolean> predicate)
        {
            return Database.Products.Find(predicate);
        }
        public void Create(Product product)
        {
            if (product == null)
            {
                throw new ValidationException("Данные введены некорректно");
            }
            Database.Products.Create(product);
        }
        public void Update(Product product)
        {
            if (product == null)
            {
                throw new ValidationException("Данные введены некорректно");
            }
            Database.Products.Update(product);
        }
        public void Delete(int? id) 
        {
            if (id == null)
                throw new ValidationException("Не установлено id продукта");
            var product = Database.Products.Get(id.Value);
            if (product == null)
                throw new ValidationException("Продукт не найден");
            Database.Products.Delete(id.Value);
        }
    }
}
