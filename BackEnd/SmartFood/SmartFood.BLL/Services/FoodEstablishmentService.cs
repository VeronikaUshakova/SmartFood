using SmartFood.DAL.Entities;
using SmartFood.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SmartFood.DAL.Repositories;
using SmartFood.BLL.Infrastructure;
using System.Security.Cryptography;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace SmartFood.BLL.Services
{
    public class DeliveryString
    {
        public int IdDelivery;
        public string IdShipper;
        public int IdFoodEstablishment;
        public string DateTime_delivery;
    }
    public class FoodEstablishmentService
    {
        EFUnitOfWork Database = new EFUnitOfWork();

        string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = string.Empty;
            foreach (byte b in byteHash)
            {
                hash += string.Format("{0:x2}", b);
            }
            return hash;
        }

        public async Task<List<FoodEstablishment>> GetALLFoodEstablishments()
        {
            List<FoodEstablishment> foodEstablishments = await Database.FoodEstablishments.GetAll();
            return foodEstablishments;
        }

        public FoodEstablishment GetFoodEstablishment(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id заведения");
            var foodEstablishment = Database.FoodEstablishments.Get(id.Value);
            if (foodEstablishment == null)
                throw new ValidationException("Заведение не найдено");
            return foodEstablishment;
        }
        List<FoodEstablishment> Find(Func<FoodEstablishment, Boolean> predicate)
        {
            return Database.FoodEstablishments.Find(predicate);
        }
        public async Task Create(FoodEstablishment foodEstablishment)
        {
            if (foodEstablishment == null)
            {
                throw new ValidationException("Данные введены некорректно");
            }
            bool check = true;
            List<Shipper> shippers = await Database.Shippers.GetAll();
            for (int i = 0; i < shippers.Count; i++)
            {
                if (GetHashString(foodEstablishment.Login_foodEstablishment) == shippers[i].Login_shipper)
                {
                    check = false;
                }
            }
            List<FoodEstablishment> foodEstablishments = await Database.FoodEstablishments.GetAll();
            for (int i = 0; i < foodEstablishments.Count; i++)
            {
                if (GetHashString(foodEstablishment.Login_foodEstablishment) == foodEstablishments[i].Login_foodEstablishment)
                {
                    check = false;
                }
            }
            if (check == true)
            {
                foodEstablishment.Login_foodEstablishment = GetHashString(foodEstablishment.Login_foodEstablishment);
                foodEstablishment.Password_foodEstablishment = GetHashString(foodEstablishment.Password_foodEstablishment);
                Database.FoodEstablishments.Create(foodEstablishment);
            }
            else
            {
                throw new ValidationException("Такой логин уже есть");
            }
        }
        public async Task Update(FoodEstablishment foodEstablishment)
        {
            FoodEstablishment foodEstablishment_old = Database.FoodEstablishments.Get(foodEstablishment.Id_foodEstablishment);
            bool check = true;
            if (foodEstablishment_old.Login_foodEstablishment != GetHashString(foodEstablishment.Login_foodEstablishment) && foodEstablishment.Login_foodEstablishment != "")
            {
                List<Shipper> shippers = await Database.Shippers.GetAll();
                for (int i = 0; i < shippers.Count; i++)
                {
                    if (GetHashString(foodEstablishment.Login_foodEstablishment) == shippers[i].Login_shipper)
                    {
                        check = false;
                    }
                }
                List<FoodEstablishment> foodEstablishments = await Database.FoodEstablishments.GetAll();
                for (int i = 0; i < foodEstablishments.Count; i++)
                {
                    if (GetHashString(foodEstablishment.Login_foodEstablishment) == foodEstablishments[i].Login_foodEstablishment)
                    {
                        check = false;
                    }
                }
            }
            if (foodEstablishment.Login_foodEstablishment == foodEstablishment_old.Login_foodEstablishment)
            {
                foodEstablishment.Login_foodEstablishment = foodEstablishment_old.Login_foodEstablishment;
            }
            else
            {
                foodEstablishment.Login_foodEstablishment = GetHashString(foodEstablishment.Login_foodEstablishment);
            }
            if (foodEstablishment.Password_foodEstablishment == foodEstablishment_old.Password_foodEstablishment)
            {
                foodEstablishment.Password_foodEstablishment = foodEstablishment_old.Password_foodEstablishment;
            }
            else
            {
                foodEstablishment.Password_foodEstablishment = GetHashString(foodEstablishment.Password_foodEstablishment);
            }
            if (check == true)
            {
                Database.FoodEstablishments.Update(foodEstablishment);
            }
            else
            {
                throw new ValidationException("Такой логин уже есть");
            }
        }
        public async Task<int> CheckFoodEstablishment(string password, string login)
        {
            if (password == null)
            {
                throw new ValidationException("Пароль не был введен");
            }
            if (login == null)
            {
                throw new ValidationException("Логин не был введен");
            }
            List<FoodEstablishment> foodEstablishments = await Database.FoodEstablishments.GetAll();
            for (int i = 0; i < foodEstablishments.Count; i++)
            {
                if (foodEstablishments[i].Password_foodEstablishment == GetHashString(password) && foodEstablishments[i].Login_foodEstablishment == GetHashString(login))
                {
                    return foodEstablishments[i].Id_foodEstablishment;
                }
            }
            return 0;
        }
        public void Delete(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id заведения");
            var foodEstablishment = Database.FoodEstablishments.Get(id.Value);
            if (foodEstablishment == null)
                throw new ValidationException("Заведение не найдено");
            Database.FoodEstablishments.Delete(id.Value);
        }
        public decimal PercentageOfCorrectStorageOfTheProduct(int? productId, int? foodEstablishmentId)
        {
            if (productId == null)
            {
                throw new ValidationException("Не установлено id продукта");
            }
            if (foodEstablishmentId == null)
            {
                throw new ValidationException("Не установлено id заведения");
            }
            DbConnection conn = Database.Boxes.ContextConnection();
            Product product = new Product();
            conn.Open();
            string query = "SELECT * FROM Products WHERE Id_product=" + productId;
            SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    product.Id_product = reader.GetInt32(0);
                    product.Name_product = reader.GetString(1);
                    product.Price_product = reader.GetDecimal(2);
                    product.Temperature_product = reader.GetDecimal(3);
                    product.Humidity_product = reader.GetDecimal(4);
                    product.Weight_product = reader.GetDecimal(5);
                    product.Id_shipper = reader.GetInt32(6);
                }
            }
            reader.Dispose();
            conn.Close();
            Box box = new Box();
            query = "SELECT MAX(Id_box) AS 'Id_box', Id_product, Initial_weight_product, DateEntry_box, ShelfLife_box, ";
            query += "Boxes.Id_delivery FROM Boxes,Deliveries WHERE Boxes.Id_delivery = Deliveries.Id_delivery AND ";
            query += "DATEADD(DAY, ShelfLife_box, DateTime_delivery)<= GETDATE() AND Id_product=" + product.Id_product;
            query += " AND Deliveries.Id_foodEstablishment =" + foodEstablishmentId;
            query += " GROUP BY Id_product, Initial_weight_product, DateEntry_box, ShelfLife_box, Boxes.Id_delivery";
            conn.Open();
            command = new SqlCommand(query, (SqlConnection)conn);
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    box.Id_box = reader.GetInt32(0);
                    box.Id_product = reader.GetInt32(1);
                    box.Initial_weight_product = reader.GetDecimal(2);
                    box.DateEntry_box = reader.GetDateTime(3);
                    box.ShelfLife_box = reader.GetInt32(4);
                    box.Id_delivery = reader.GetInt32(5);
                }
            }
            else
            {
                throw new ValidationException("Заведение не заказывало данный продукт");
            }
            reader.Dispose();
            conn.Close();
            List<HistoryBox> historyBoxes = new List<HistoryBox>();
            query = "SELECT * FROM HistoryBoxes WHERE Id_box = " + box.Id_box;
            conn.Open();
            command = new SqlCommand(query, (SqlConnection)conn);
            reader = command.ExecuteReader();
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
            List<Boolean> warning = new List<bool>();
            for (int i = 0; i < box.ShelfLife_box; i++)
            {
                bool temperature = true;
                bool humidity = true;
                DateTime day = box.DateEntry_box;
                day = day.AddDays(i);
                for (int j = 0; j < historyBoxes.Count; j++)
                {
                    if (day.Day == historyBoxes[j].DateTime_history.Day)
                    {
                        if (day.Month == historyBoxes[j].DateTime_history.Month)
                        {
                            if (day.Year == historyBoxes[j].DateTime_history.Year)
                            {
                                if (historyBoxes[j].Humidity_box <= product.Humidity_product + 5 && historyBoxes[j].Humidity_box >= product.Humidity_product - 5)
                                {
                                    if (humidity == true)
                                    {
                                        humidity = true;
                                    }
                                }
                                else
                                {
                                    humidity = false;
                                }
                                if (historyBoxes[j].Temperature_box <= product.Temperature_product + 2 && historyBoxes[j].Temperature_box >= product.Temperature_product - 2)
                                {
                                    if (temperature == true)
                                    {
                                        temperature = true;
                                    }
                                }
                                else
                                {
                                    temperature = false;
                                }
                            }
                        }
                    }
                }
                if (humidity == true && temperature == true)
                {
                    warning.Add(true);
                }
                else
                {
                    warning.Add(false);
                }
            }
            int warn_day = 0;
            for (int i = 0; i < warning.Count; i++)
            {
                if (warning[i] == false)
                {
                    warn_day++;
                }
            }
            decimal percent = 100 - ((100 * warn_day) / warning.Count);
            return percent;
        }

        public decimal PercentageUseOrderedProduct(int? foodEstablishmentId, int? deliveryId)
        {
            if (foodEstablishmentId == null)
            {
                throw new ValidationException("Не установлено id заведения");
            }
            if (deliveryId == null)
            {
                throw new ValidationException("Не установлено id поставки");
            }
            var conn = Database.FoodEstablishments.ContextConnection();
            Delivery delivery = Database.Deliveries.Get((int)deliveryId);
            List<Box> boxes = new List<Box>();
            string query = "SELECT * FROM Boxes";
            query += " WHERE Id_delivery =" + delivery.Id_delivery;
            SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Box box = new Box();
                    box.Id_box = reader.GetInt32(0);
                    box.Id_product = reader.GetInt32(1);
                    box.Initial_weight_product = reader.GetDecimal(2);
                    box.DateEntry_box = reader.GetDateTime(3);
                    box.ShelfLife_box = reader.GetInt32(4);
                    box.Id_delivery = reader.GetInt32(5);
                    boxes.Add(box);
                }
            }
            reader.Dispose();
            conn.Close();
            List<HistoryBox> historyBoxes = new List<HistoryBox>();
            for (int i = 0; i < boxes.Count; i++)
            {
                query = "SELECT Id_history, Id_box, Weight_box, Temperature_box, Humidity_box, DateTime_history  FROM HistoryBoxes";
                query += " WHERE Id_box =" + boxes[i].Id_box + " AND Id_history =";
                query += " (SELECT MAX(Id_history) FROM HistoryBoxes WHERE Id_box = " + boxes[i].Id_box + ")";
                command = new SqlCommand(query, (SqlConnection)conn);
                conn.Open();
                reader = command.ExecuteReader();
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
            }
            List<Boolean> failed = new List<bool>();
            for (int i = 0; i < historyBoxes.Count; i++)
            {
                if (historyBoxes[i].Weight_box > 0)
                {
                    failed.Add(true);
                }
                else
                {
                    failed.Add(false);
                }
            }
            decimal percentUseOrderedProduct = 0;
            decimal percentProduct = 100 / boxes.Count;
            for (int i = 0; i < historyBoxes.Count; i++)
            {
                if (failed[i] == false)
                {
                    decimal percentOfUseProduct = (percentProduct * (boxes[i].Initial_weight_product - historyBoxes[i].Weight_box)) / (boxes[i].Initial_weight_product);
                    percentOfUseProduct += percentOfUseProduct;
                }
                else
                {
                    percentUseOrderedProduct += percentProduct;
                }
            }
            return percentUseOrderedProduct;
        }
        public List<DeliveryString> FoodEstablishmentDeliveries(int? id_foodEstablishment)
        {
            if (id_foodEstablishment == null)
            {
                throw new ValidationException("Не установлено id заведения");
            }
            DbConnection conn = Database.FoodEstablishments.ContextConnection();
            List<DeliveryString> deliveries = new List<DeliveryString>();
            string query = "SELECT Id_delivery, Id_foodEstablishment, Name_shipper, DateTime_delivery FROM Shippers, Deliveries";
            query += " WHERE Shippers.Id_shipper = Deliveries.Id_shipper AND Id_foodEstablishment=" + id_foodEstablishment;
            conn.Open();
            SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DeliveryString delivery = new DeliveryString();
                    delivery.IdDelivery = reader.GetInt32(0);
                    delivery.IdShipper = reader.GetString(2);
                    delivery.IdFoodEstablishment = reader.GetInt32(1);
                    delivery.DateTime_delivery = reader.GetDateTime(3).ToUniversalTime().ToString();
                    deliveries.Add(delivery);
                }
            }
            reader.Dispose();
            conn.Close();
            return deliveries;
        }

        public List<Shipper> FoodEstablishmentShippers(int? id_foodEstablishment)
        {
            if (id_foodEstablishment == null)
            {
                throw new ValidationException("Не установлено id заведения");
            }
            DbConnection conn = Database.FoodEstablishments.ContextConnection();
            List<Shipper> shippers = new List<Shipper>();

            string query = "SELECT * FROM Shippers";
            query += " WHERE Id_shipper IN (SELECT Id_shipper FROM Deliveries WHERE ";
            query += "Id_foodEstablishment = " + id_foodEstablishment + ")";
            conn.Open();
            SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Shipper shipper = new Shipper();
                    shipper.Id_shipper = reader.GetInt32(0);
                    shipper.Name_shipper = reader.GetString(1);
                    shipper.Address_shipper = reader.GetString(2);
                    shipper.Mobile_shipper = reader.GetString(3);
                    shipper.Login_shipper = reader.GetString(4);
                    shipper.Password_shipper = reader.GetString(5);
                    shippers.Add(shipper);
                }
            }
            reader.Dispose();
            conn.Close();
            return shippers;
        }

        public List<Product> FoodEstablishmentProducts(int? id_foodEstablishment)
        {
            if (id_foodEstablishment == null)
            {
                throw new ValidationException("Не установлено id заведения");
            }
            DbConnection conn = Database.FoodEstablishments.ContextConnection();
            List<Product> products = new List<Product>();

            string query = "SELECT Products.Id_product, Name_product, Price_product, Temperature_product, Humidity_product, Weight_product, Id_shipper FROM Products, Boxes";
            query += " WHERE Boxes.Id_product = Products.Id_product AND Id_box IN(SELECT Id_box FROM Boxes, Deliveries";
            query += " WHERE Boxes.Id_delivery = Deliveries.Id_delivery AND Deliveries.Id_delivery IN(SELECT Id_delivery FROM Deliveries";
            query += " WHERE Id_foodEstablishment = " + id_foodEstablishment + "))";
            conn.Open();
            SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Product product = new Product();
                    product.Id_product = reader.GetInt32(0);
                    product.Name_product = reader.GetString(1);
                    product.Price_product = reader.GetDecimal(2);
                    product.Temperature_product = reader.GetDecimal(3);
                    product.Humidity_product = reader.GetDecimal(4);
                    product.Weight_product = reader.GetDecimal(5);
                    product.Id_shipper = reader.GetInt32(6);
                    products.Add(product);
                }
            }
            reader.Dispose();
            conn.Close();
            conn = Database.FoodEstablishments.ContextConnection();
            for (int i = 0; i < products.Count; i++)
            {
                query = "SELECT MAX(Id_history),Id_box, Weight_box, Temperature_box, Humidity_box, DateTime_history FROM HistoryBoxes";
                query += " WHERE Id_history = (SELECT MAX(Id_history) FROM HistoryBoxes WHERE Id_box IN(SELECT Id_box FROM Boxes, Products";
                query += " WHERE Products.Id_product = Boxes.Id_product AND Products.Id_product = " + products[i].Id_product + "))";
                query += " GROUP BY Id_box, Weight_box, Temperature_box, Humidity_box, DateTime_history";
                conn.Open();
                command = new SqlCommand(query, (SqlConnection)conn);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        products[i].Temperature_product = reader.GetDecimal(3);
                        products[i].Humidity_product = reader.GetDecimal(4);
                        products[i].Weight_product = reader.GetDecimal(2);
                    }
                }
                reader.Dispose();
                conn.Close();
            }
            return products;
        }
    }
}
