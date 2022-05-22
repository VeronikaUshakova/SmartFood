using SmartFood.DAL.Entities;
using SmartFood.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartFood.DAL.Repositories;
using SmartFood.DAL.DataContext;
using SmartFood.BLL.Infrastructure;
using SmartFood.BLL.DTO;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace SmartFood.BLL.Services
{
    public class BoxService
    {
        EFUnitOfWork Database = new EFUnitOfWork();

        public async Task<List<Box>> GetALLBoxes()
        {
            List<Box> boxes = await Database.Boxes.GetAll();
            Console.WriteLine(boxes);
            return boxes;
        }

        public Box GetBox(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id ящика");
            var box = Database.Boxes.Get(id.Value);
            if (box == null)
                throw new ValidationException("Ящик не найден");
            return box;
        }
        List<Box> Find(Func<Box, Boolean> predicate)
        {
            return Database.Boxes.Find(predicate);
        }
        public void Create(Box box)
        {
            if (box == null)
            {
                throw new ValidationException("Данные введены некорректно");
            }
            var delivery = Database.Deliveries.Get(box.Id_delivery);
            if (delivery.DateTime_delivery >= box.DateEntry_box)
            {
                Product product = Database.Products.Get(box.Id_product);
                if (product.Weight_product - box.Initial_weight_product >= 0)
                {
                    product.Weight_product -= box.Initial_weight_product;
                    Database.Boxes.Create(box);
                }
                else
                {
                    throw new ValidationException("Продукта недостаточно на складе");
                }
            }
            else
            {
                throw new ValidationException("Увы, доставка уже отправлена");
            }
        }
        public void Update(Box box)
        {
            if (box == null)
            {
                throw new ValidationException("Данные введены некорректно");
            }
            var product = Database.Products.Get(box.Id_product);
            var box_old = Database.Boxes.Get(box.Id_box);
            var delivery = Database.Deliveries.Get(box.Id_delivery);
            if (delivery.DateTime_delivery >= box.DateEntry_box)
            {
                product.Weight_product += box_old.Initial_weight_product;
                product.Weight_product -= box.Initial_weight_product;
                Database.Boxes.Update(box);
            }
            else
            {
                throw new ValidationException("Увы, доставка уже отправлена");
            }
        }
        public void Delete(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id ящика");
            var box = Database.Boxes.Get(id.Value);
            if (box == null)
                throw new ValidationException("Ящик не найден");
            var product = Database.Products.Get(box.Id_delivery);
            product.Weight_product += box.Initial_weight_product;
            Database.Boxes.Delete(id.Value);
        }

        public List<HistoryBox> HistoryBoxesBox(int? id_box)
        {
            if (id_box == null)
            {
                throw new ValidationException("Не установлено id ящика");
            }
            DbConnection conn = Database.Boxes.ContextConnection();
            List<HistoryBox> historyBoxes = new List<HistoryBox>();
            string query = "SELECT * FROM HistoryBoxes";
            query += " WHERE Id_box = " + id_box;
            SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    HistoryBox historyBox = new HistoryBox();
                    historyBox.Id_history = reader.GetInt32(0);
                    historyBox.Id_box = reader.GetInt32(1);
                    historyBox.Weight_box = reader.GetDecimal(2);
                    historyBox.Temperature_box = reader.GetDecimal(3);
                    historyBox.Humidity_box = reader.GetDecimal(4);
                    historyBox.DateTime_history = reader.GetDateTime(5);
                    historyBoxes.Add(historyBox);
                }
            }
            reader.Dispose();
            conn.Close();
            return historyBoxes;
        }
    }
}
