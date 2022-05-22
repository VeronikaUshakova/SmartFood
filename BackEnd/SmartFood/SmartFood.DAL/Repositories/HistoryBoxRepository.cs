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
    public class HistoryBoxRepository : HistoryBoxInterface
    {
        private DatabaseContext context;
        public HistoryBoxRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<List<HistoryBox>> GetAll()
        {
            List<HistoryBox> historyBoxes = new List<HistoryBox>();
            historyBoxes = await context.HistoryBoxes.ToListAsync();
            return historyBoxes;
        }
        public HistoryBox Get(int id)
        {
            HistoryBox historyBox = new HistoryBox();
            historyBox = context.HistoryBoxes.Find(id);
            return historyBox;
        }
        public List<HistoryBox> Find(Func<HistoryBox, Boolean> predicate)
        {
            List<HistoryBox> historyBoxes = new List<HistoryBox>();
            historyBoxes = context.HistoryBoxes.Where(predicate).ToList();
            return historyBoxes;
        }
        public void Create(HistoryBox historyBox)
        {
            context.HistoryBoxes.Add(historyBox);
            context.SaveChanges();
        }
        public void Update(HistoryBox historyBox)
        {
            var currentHistoryBox = context.HistoryBoxes.Find(historyBox.Id_history);
            context.Entry(currentHistoryBox).CurrentValues.SetValues(historyBox);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            HistoryBox historyBox = context.HistoryBoxes.Find(id);
            if (historyBox != null)
            {
                context.HistoryBoxes.Remove(historyBox);
                context.SaveChanges();
            }
        }
    }
}
