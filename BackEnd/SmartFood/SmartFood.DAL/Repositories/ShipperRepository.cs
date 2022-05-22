using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartFood.DAL.DataContext;
using SmartFood.DAL.Entities;
using SmartFood.DAL.Interfaces;

namespace SmartFood.DAL.Repositories
{
    public class ShipperRepository : ShipperInterface
    {
        private DatabaseContext context;
        public ShipperRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<List<Shipper>> GetAll()
        {
            List<Shipper> shippers = new List<Shipper>();
            shippers = await context.Shippers.ToListAsync();
            return shippers;
        }
        public Shipper Get(int id)
        {
            Shipper shipper = new Shipper();
            shipper = context.Shippers.Find(id);
            return shipper;
        }
        public List<Shipper> Find(Func<Shipper, Boolean> predicate)
        {
            List<Shipper> shippers = new List<Shipper>();
            shippers = context.Shippers.Where(predicate).ToList();
            return shippers;
        }
        public void Create(Shipper shipper)
        {
            context.Shippers.Add(shipper);
            context.SaveChanges();
        }
        public void Update(Shipper shipper)
        {
            var currentShipper = context.Shippers.Find(shipper.Id_shipper);
            context.Entry(currentShipper).CurrentValues.SetValues(shipper);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            Shipper shipper = context.Shippers.Find(id);
            if (shipper != null)
            {
                context.Shippers.Remove(shipper);
                context.SaveChanges();
            }
        }

        public DbConnection ContextConnection()
        {
            return context.Database.GetDbConnection();
        }
    }
}
