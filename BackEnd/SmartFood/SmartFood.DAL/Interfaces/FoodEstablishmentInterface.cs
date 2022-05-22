using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace SmartFood.DAL.Interfaces
{
    public interface FoodEstablishmentInterface
    {
        Task<List<FoodEstablishment>> GetAll();
        FoodEstablishment Get(int id);
        List<FoodEstablishment> Find(Func<FoodEstablishment, Boolean> predicate);
        void Create(FoodEstablishment item);
        void Update(FoodEstablishment item);
        void Delete(int id);
        DbConnection ContextConnection();
    }
}
