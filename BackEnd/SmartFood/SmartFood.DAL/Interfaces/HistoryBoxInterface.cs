using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartFood.DAL.Interfaces
{
    public interface HistoryBoxInterface
    {
        Task<List<HistoryBox>> GetAll();
        HistoryBox Get(int id);
        List<HistoryBox> Find(Func<HistoryBox, Boolean> predicate);
        void Create(HistoryBox item);
        void Update(HistoryBox item);
        void Delete(int id);
    }
}
