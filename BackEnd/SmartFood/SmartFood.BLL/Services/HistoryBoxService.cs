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
    public class HistoryBoxService
    {
        EFUnitOfWork Database = new EFUnitOfWork();

        public async Task<List<HistoryBox>> GetALLHistoryBoxes()
        {
            List<HistoryBox> historyBoxes = await Database.HistoryBoxes.GetAll();
            return historyBoxes;
        }

        public HistoryBox GetHistoryBox(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id истории ящика");
            var historyBox = Database.HistoryBoxes.Get(id.Value);
            if (historyBox == null)
                throw new ValidationException("История ящика не найдена");
            return historyBox;
        }
        List<HistoryBox> Find(Func<HistoryBox, Boolean> predicate)
        {
            return Database.HistoryBoxes.Find(predicate);
        }
        public void Create(HistoryBox historyBox)
        {
            if (historyBox == null)
            {
                throw new ValidationException("Данные введены некорректно");
            }
            Database.HistoryBoxes.Create(historyBox);
        }
        public void Update(HistoryBox historyBox)
        {
            if (historyBox == null)
            {
                throw new ValidationException("Данные введены некорректно");
            }
            Database.HistoryBoxes.Update(historyBox);
        }
        public void Delete(int? id) 
        {
            if (id == null)
                throw new ValidationException("Не установлено id истории ящика");
            var historyBox = Database.HistoryBoxes.Get(id.Value);
            if (historyBox == null)
                throw new ValidationException("История ящика не найдена");
            Database.HistoryBoxes.Delete(id.Value);
        }
    }
}
