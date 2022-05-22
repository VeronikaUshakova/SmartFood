using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace SmartFood.DAL.Interfaces
{
    public interface BoxInterface
    {
        Task<List<Box>> GetAll();
        Box Get(int id);
        List<Box> Find(Func<Box, Boolean> predicate);
        void Create(Box item);
        void Update(Box item);
        void Delete(int id);
        DbConnection ContextConnection();
    }
}
