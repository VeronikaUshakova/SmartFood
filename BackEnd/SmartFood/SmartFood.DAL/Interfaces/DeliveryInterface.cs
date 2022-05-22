using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartFood.DAL.Interfaces
{
    public interface DeliveryInterface
    {
        Task<List<Delivery>> GetAll();
        Delivery Get(int id);
        List<Delivery> Find(Func<Delivery, Boolean> predicate);
        void Create(Delivery item);
        void Update(Delivery item);
        void Delete(int id);
    }
}
