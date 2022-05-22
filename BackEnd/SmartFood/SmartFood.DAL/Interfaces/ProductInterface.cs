using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartFood.DAL.Interfaces
{
    public interface ProductInterface
    {
        Task<List<Product>> GetAll();
        Product Get(int id);
        List<Product> Find(Func<Product, Boolean> predicate);
        void Create(Product item);
        void Update(Product item);
        void Delete(int id);
    }
}
