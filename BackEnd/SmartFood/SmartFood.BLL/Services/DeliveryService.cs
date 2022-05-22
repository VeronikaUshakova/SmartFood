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
    public class DeliveryService
    {
        EFUnitOfWork Database = new EFUnitOfWork();

        public async Task<List<Delivery>> GetALLDeliveries()
        {
            List<Delivery> deliveries = await Database.Deliveries.GetAll();
            return deliveries;
        }

        public Delivery GetDelivery(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id доставки");
            var delivery = Database.Deliveries.Get(id.Value);
            if (delivery == null)
                throw new ValidationException("Доставка не найдена");
            return delivery;
        }
        List<Delivery> Find(Func<Delivery, Boolean> predicate)
        {
            return Database.Deliveries.Find(predicate);
        }
        public void Create(Delivery delivery)
        {
            if (delivery == null)
            {
                throw new ValidationException("Данные введены некорректно");
            }
            Database.Deliveries.Create(delivery);
        }
        public void Update(Delivery delivery)
        {
            if (delivery == null)
            {
                throw new ValidationException("Данные введены некорректно");
            }
            Database.Deliveries.Update(delivery);
        }
        public void Delete(int? id) 
        {
            if (id == null)
                throw new ValidationException("Не установлено id доставки" );
            var delivery = Database.Deliveries.Get(id.Value);
            if (delivery == null)
                throw new ValidationException("Доставка не найдена");
            Database.Deliveries.Delete(id.Value);
        }
    }
}
