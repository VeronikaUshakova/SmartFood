using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartFood.DAL.DataContext;
using SmartFood.DAL.Entities;
using SmartFood.DAL.Interfaces;

namespace SmartFood.DAL.Repositories
{
    public class ProductRepository : ProductInterface
    {
        private DatabaseContext context;
        public ProductRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<List<Product>> GetAll()
        {
            List<Product> products = new List<Product>();
            products = await context.Products.ToListAsync();
            return products;
        }
        public Product Get(int id)
        {
            Product product = new Product();
            product = context.Products.Find(id);
            return product;
        }
        public List<Product> Find(Func<Product, Boolean> predicate)
        {
            List<Product> products = new List<Product>();
            products = context.Products.Where(predicate).ToList();
            return products;
        }
        public void Create(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }
        public void Update(Product product)
        {
            var currentProduct = context.Products.Find(product.Id_product);
            context.Entry(currentProduct).CurrentValues.SetValues(product);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            Product product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }
    }
}
