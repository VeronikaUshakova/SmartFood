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
    public class DeliveryRepository : DeliveryInterface
    {
        private DatabaseContext context;
        public DeliveryRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<List<Delivery>> GetAll()
        {
            List<Delivery> deliveries = new List<Delivery>();
            deliveries = await context.Deliveries.ToListAsync();
            return deliveries;
        }
        public Delivery Get(int id)
        {
            Delivery deliveries = new Delivery();
            deliveries = context.Deliveries.Find(id);
            return deliveries;
        }
        public List<Delivery> Find(Func<Delivery, Boolean> predicate)
        {
            List<Delivery> deliveries = new List<Delivery>();
            deliveries = context.Deliveries.Where(predicate).ToList();
            return deliveries;
        }
        public void Create(Delivery delivery)
        {
            context.Deliveries.Add(delivery);
            context.SaveChanges();
        }
        public void Update(Delivery delivery)
        {
            var currentDelivery= context.Deliveries.Find(delivery.Id_delivery);
            context.Entry(currentDelivery).CurrentValues.SetValues(delivery);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            Delivery delivery = context.Deliveries.Find(id);
            if (delivery != null)
            {
                context.Deliveries.Remove(delivery);
                context.SaveChanges();
            }
        }
    }
}
