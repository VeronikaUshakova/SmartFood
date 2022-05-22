using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartFood.DAL.Interfaces;
using SmartFood.DAL.DataContext;
using SmartFood.DAL.Entities;
using System.Data.Common;

namespace SmartFood.DAL.Repositories
{
    public class BoxRepository : BoxInterface
    {
        private DatabaseContext context;
        public BoxRepository(DatabaseContext context)
        {
            this.context = context;
        }
        public async Task<List<Box>> GetAll()
        {
            List<Box> boxes = new List<Box>();
            boxes = await context.Boxes.ToListAsync();
            return boxes;
        }
        public Box Get(int id)
        {
            Box box = new Box();
            box = context.Boxes.Find(id);
            return box;
        }
        public List<Box> Find(Func<Box, Boolean> predicate)
        {
            List<Box> boxes = new List<Box>();
            boxes = context.Boxes.Where(predicate).ToList();
            return boxes;
        }
        public void Create(Box box)
        {
            context.Boxes.Add(box);
            context.SaveChanges();
        }
        public void Update(Box box)
        {
            var currentBox = context.Boxes.Find(box.Id_box);
            context.Entry(currentBox).CurrentValues.SetValues(box);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            Box box = context.Boxes.Find(id);
            if (box != null)
            {
                context.Boxes.Remove(box);
                context.SaveChanges();
            }
        }
        public DbConnection ContextConnection()
        {
            return context.Database.GetDbConnection();
        }
    }
}
