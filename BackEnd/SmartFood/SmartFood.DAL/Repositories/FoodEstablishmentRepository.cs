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
    public class FoodEstablishmentRepository : FoodEstablishmentInterface
    {
        private DatabaseContext context;
        public FoodEstablishmentRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<List<FoodEstablishment>> GetAll()
        {
            List<FoodEstablishment> foodEstablishments = new List<FoodEstablishment>();
            foodEstablishments = await context.FoodEstablishments.ToListAsync();
            return foodEstablishments;
        }
        public FoodEstablishment Get(int id)
        {
            FoodEstablishment foodEstablishment = new FoodEstablishment();
            foodEstablishment = context.FoodEstablishments.Find(id);
            return foodEstablishment;
        }
        public List<FoodEstablishment> Find(Func<FoodEstablishment, Boolean> predicate)
        {
            List<FoodEstablishment> foodEstablishments = new List<FoodEstablishment>();
            foodEstablishments = context.FoodEstablishments.Where(predicate).ToList();
            return foodEstablishments;
        }
        public void Create(FoodEstablishment foodEstablishment)
        {
            context.FoodEstablishments.Add(foodEstablishment);
            context.SaveChanges();
        }
        public void Update(FoodEstablishment foodEstablishment)
        {
            var currentFoodEstablishment = context.FoodEstablishments.Find(foodEstablishment.Id_foodEstablishment);
            context.Entry(currentFoodEstablishment).CurrentValues.SetValues(foodEstablishment);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            FoodEstablishment foodEstablishment = context.FoodEstablishments.Find(id);
            if (foodEstablishment != null)
            {
                context.FoodEstablishments.Remove(foodEstablishment);
                context.SaveChanges();
            }
        }
        public DbConnection ContextConnection()
        {
            return context.Database.GetDbConnection();
        }
    }
}
