using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace SmartFood.DAL.Interfaces
{
    public interface ShipperInterface
    {
        Task<List<Shipper>> GetAll();
        Shipper Get(int id);
        List<Shipper> Find(Func<Shipper, Boolean> predicate);
        void Create(Shipper item);
        void Update(Shipper item);
        void Delete(int id);
        DbConnection ContextConnection();

    }
}
