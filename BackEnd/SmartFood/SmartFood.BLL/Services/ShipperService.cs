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
    public class ShipperService
    {
        public class CountProduct
        {
            public int IdProduct;
            public decimal AllWeightProduct;

            public CountProduct(int idProduct, decimal allWeightProduct)
            {
                IdProduct = idProduct;
                AllWeightProduct = allWeightProduct;
            }
        }

        public class DeliveryString
        {
            public int IdDelivery;
            public int IdShipper;
            public string NameFoodEstablishment;
            public string DateTime_delivery;
        }

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

        public async Task<List<Shipper>> GetALLShippers()
        {
            List<Shipper> shippers = await Database.Shippers.GetAll();
            return shippers;
        }

        public Shipper GetShipper(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id поставщика");
            var shipper = Database.Shippers.Get(id.Value);
            if (shipper == null)
                throw new ValidationException("Поставщик не найден");
            return shipper;
        }
        List<Shipper> Find(Func<Shipper, Boolean> predicate)
        {
            return Database.Shippers.Find(predicate);
        }
        public async Task Create(Shipper shipper)
        {
            if (shipper == null)
            {
                throw new ValidationException("Данные введены некорректно");
            }
            bool check = true;
            List<Shipper> shippers = await Database.Shippers.GetAll();
            for (int i = 0; i < shippers.Count; i++)
            {
                if (GetHashString(shipper.Login_shipper) == shippers[i].Login_shipper)
                {
                    check = false;
                }
            }
            List<FoodEstablishment> foodEstablishments = await Database.FoodEstablishments.GetAll();
            for (int i = 0; i < foodEstablishments.Count; i++)
            {
                if (GetHashString(shipper.Login_shipper) == foodEstablishments[i].Login_foodEstablishment)
                {
                    check = false;
                }
            }
            if (check == true)
            {
                shipper.Login_shipper = GetHashString(shipper.Login_shipper);
                shipper.Password_shipper = GetHashString(shipper.Password_shipper);
                Database.Shippers.Create(shipper);
            }
            else
            {
                throw new ValidationException("Такой логин уже есть");
            }
        }
        public async Task Update(Shipper shipper)
        {
            if (shipper == null)
            {
                throw new ValidationException("Данные введены некорректно");
            }
            Shipper shipper_old = Database.Shippers.Get(shipper.Id_shipper);
            bool check = true;
            if (shipper_old.Login_shipper != GetHashString(shipper.Login_shipper) && shipper.Login_shipper != "")
            {
                List<Shipper> shippers = await Database.Shippers.GetAll();
                for (int i = 0; i < shippers.Count; i++)
                {
                    if (GetHashString(shipper.Login_shipper) == shippers[i].Login_shipper)
                    {
                        check = false;
                    }
                }
                List<FoodEstablishment> foodEstablishments = await Database.FoodEstablishments.GetAll();
                for (int i = 0; i < foodEstablishments.Count; i++)
                {
                    if (GetHashString(shipper.Login_shipper) == foodEstablishments[i].Login_foodEstablishment)
                    {
                        check = false;
                    }
                }
            }
            if (GetHashString(shipper.Login_shipper) == shipper_old.Login_shipper || shipper.Login_shipper == shipper_old.Login_shipper)
            {
                shipper.Login_shipper = shipper_old.Login_shipper;
            }
            else
            {
                shipper.Login_shipper = GetHashString(shipper.Login_shipper);
            }
            if (GetHashString(shipper.Password_shipper) == shipper_old.Password_shipper || shipper.Password_shipper == shipper_old.Password_shipper)
            {
                shipper.Password_shipper = shipper_old.Password_shipper;
            }
            else
            {
                shipper.Password_shipper = GetHashString(shipper.Password_shipper);
            }
            if (check == true)
            {
                Database.Shippers.Update(shipper);
            }
            else
            {
                throw new ValidationException("Такой логин уже есть");
            }
        }
        public void Delete(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id поставщика");
            var shipper = Database.Shippers.Get(id.Value);
            if (shipper == null)
                throw new ValidationException("Поставщик не найден");
            Database.Shippers.Delete(id.Value);
        }
        public decimal PercentWeek(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id поставщика");
            DbConnection conn = Database.Shippers.ContextConnection();
            int countProductsShipper = 0;
            string query = "SELECT COUNT(Id_product) FROM Products, Shippers WHERE Products.Id_shipper=Shippers.Id_shipper AND Shippers.Id_shipper=" + id;
            SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    countProductsShipper = reader.GetInt32(0);
                }
            }
            reader.Dispose();
            conn.Close();
            if (countProductsShipper == 0)
            {
                throw new ValidationException("У поставщика нет товаров");
            }
            decimal percentProductShipper = 100 / countProductsShipper;
            List<CountProduct> countSpentProducts = new List<CountProduct>();

            query = "SELECT Products.Id_product, SUM(Weight_product) AS 'All_weight' FROM Products, Boxes, Shippers, Deliveries ";
            query += "WHERE Products.Id_shipper = Shippers.Id_shipper AND Boxes.Id_product = Products.Id_product ";
            query += "AND Deliveries.Id_delivery = Boxes.Id_delivery AND DATEADD(DAY, -7, GETDATE())< Deliveries.DateTime_delivery ";
            query += "AND Shippers.Id_shipper =" + id + " GROUP BY Products.Id_product";
            command = new SqlCommand(query, (SqlConnection)conn);
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    countSpentProducts.Add(new CountProduct(reader.GetInt32(0), reader.GetDecimal(1)));
                }
            }
            reader.Dispose();
            conn.Close();
            List<CountProduct> countStorageProducts = new List<CountProduct>();
            query = "SELECT Products.Id_product, SUM(Weight_product) AS 'All_weight' FROM Products, Shippers ";
            query += "WHERE Products.Id_shipper = Shippers.Id_shipper ";
            query += "AND Shippers.Id_shipper =" + id + " GROUP BY Products.Id_product";
            command = new SqlCommand(query, (SqlConnection)conn);
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    countStorageProducts.Add(new CountProduct(reader.GetInt32(0), reader.GetDecimal(1)));
                }
            }
            reader.Dispose();
            conn.Close();
            List<CountProduct> differenceProducts = new List<CountProduct>();
            for (int j = 0; j < countSpentProducts.Count; j++)
            {
                differenceProducts.Add(new CountProduct(countSpentProducts[j].IdProduct, countStorageProducts[j].AllWeightProduct - countSpentProducts[j].AllWeightProduct));
            }
            decimal percent = 0;
            for (int j = 0; j < differenceProducts.Count; j++)
            {
                if (differenceProducts[j].AllWeightProduct > 0)
                {
                    percent += percentProductShipper;
                }
                else
                {
                    decimal no_percent = Math.Abs((percentProductShipper * differenceProducts[j].AllWeightProduct) / countSpentProducts[j].AllWeightProduct);
                    percent += no_percent;
                }
            }
            return percent;
        }
        public decimal PercentMonth(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id поставщика");
            DbConnection conn = Database.Shippers.ContextConnection();
            int countProductsShipper = 0;
            string query = "SELECT COUNT(Id_product) FROM Products, Shippers WHERE Products.Id_shipper=Shippers.Id_shipper AND Shippers.Id_shipper=" + id;
            SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    countProductsShipper = reader.GetInt32(0);
                }
            }
            reader.Dispose();
            conn.Close();
            if (countProductsShipper == 0)
            {
                throw new ValidationException("У поставщика нет товаров");
            }
            decimal percentProductShipper = 100 / countProductsShipper;
            List<CountProduct> countSpentProducts = new List<CountProduct>();

            query = "SELECT Products.Id_product, SUM(Weight_product) AS 'All_weight' FROM Products, Boxes, Shippers, Deliveries ";
            query += "WHERE Products.Id_shipper = Shippers.Id_shipper AND Boxes.Id_product = Products.Id_product ";
            query += "AND Deliveries.Id_delivery = Boxes.Id_delivery AND DATEADD(MONTH, -1, GETDATE())< Deliveries.DateTime_delivery ";
            query += "AND Shippers.Id_shipper =" + id + " GROUP BY Products.Id_product";
            command = new SqlCommand(query, (SqlConnection)conn);
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    countSpentProducts.Add(new CountProduct(reader.GetInt32(0), reader.GetDecimal(1)));
                }
            }
            reader.Dispose();
            conn.Close();
            List<CountProduct> countStorageProducts = new List<CountProduct>();
            query = "SELECT Products.Id_product, SUM(Weight_product) AS 'All_weight' FROM Products, Shippers ";
            query += "WHERE Products.Id_shipper = Shippers.Id_shipper ";
            query += "AND Shippers.Id_shipper =" + id + " GROUP BY Products.Id_product";
            command = new SqlCommand(query, (SqlConnection)conn);
            conn.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    countStorageProducts.Add(new CountProduct(reader.GetInt32(0), reader.GetDecimal(1)));
                }
            }
            reader.Dispose();
            conn.Close();
            List<CountProduct> differenceProducts = new List<CountProduct>();
            for (int j = 0; j < countSpentProducts.Count; j++)
            {
                differenceProducts.Add(new CountProduct(countSpentProducts[j].IdProduct, countStorageProducts[j].AllWeightProduct - countSpentProducts[j].AllWeightProduct));
            }
            decimal percent = 0;
            for (int j = 0; j < differenceProducts.Count; j++)
            {
                if (differenceProducts[j].AllWeightProduct > 0)
                {
                    percent += percentProductShipper;
                }
                else
                {
                    decimal no_percent = Math.Abs((percentProductShipper * differenceProducts[j].AllWeightProduct) / countSpentProducts[j].AllWeightProduct);
                    percent += no_percent;
                }
            }
            return percent;
        }
        public async Task<int> CheckShipperAsync(string password, string login)
        {
            if (password == null)
            {
                throw new ValidationException("Пароль не был введен");
            }
            if (login == null)
            {
                throw new ValidationException("Логин не был введен");
            }
            List<Shipper> shippers = await Database.Shippers.GetAll();
            for (int i = 0; i < shippers.Count; i++)
            {
                if (shippers[i].Password_shipper == GetHashString(password) && shippers[i].Login_shipper == GetHashString(login))
                {
                    return shippers[i].Id_shipper;
                }
            }
            return 0;
        }
        public List<Product> ShipperProducts(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id поставщика");
            DbConnection conn = Database.Shippers.ContextConnection();
            string query = "SELECT * FROM Products WHERE Id_shipper=" + id;
            SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Product> products = new List<Product>();
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
            return products;
        }
        public List<DeliveryString> ShipperDeliveries(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id поставщика");
            DbConnection conn = Database.Shippers.ContextConnection();
            string query = "SELECT Id_delivery,Id_shipper, Name_foodEstablishment, DateTime_delivery FROM Deliveries, FoodEstablishments WHERE FoodEstablishments.Id_foodEstablishment = Deliveries.Id_foodEstablishment AND Id_shipper=" + id;
            SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<DeliveryString> deliveries = new List<DeliveryString>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DeliveryString delivery = new DeliveryString();
                    delivery.IdDelivery = reader.GetInt32(0);
                    delivery.IdShipper = reader.GetInt32(1);
                    delivery.NameFoodEstablishment = reader.GetString(2);
                    delivery.DateTime_delivery = reader.GetDateTime(3).ToString();
                    deliveries.Add(delivery);
                }
            }
            return deliveries;
        }
        public List<FoodEstablishment> ShipperFoodEstablishments(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id поставщика");
            DbConnection conn = Database.Shippers.ContextConnection();
            string query = "SELECT FoodEstablishments.Id_foodEstablishment, Name_foodEstablishment, Address_foodEstablishment, Mobile_foodEstablishment, Login_foodEstablishment, Password_foodEstablishment FROM FoodEstablishments, Deliveries WHERE FoodEstablishments.Id_foodEstablishment=Deliveries.Id_foodEstablishment AND Id_shipper = " + id;
            SqlCommand command = new SqlCommand(query, (SqlConnection)conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<FoodEstablishment> foodEstablishments = new List<FoodEstablishment>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    FoodEstablishment foodEstablishment = new FoodEstablishment();
                    foodEstablishment.Id_foodEstablishment = reader.GetInt32(0);
                    foodEstablishment.Name_foodEstablishment = reader.GetString(1);
                    foodEstablishment.Address_foodEstablishment = reader.GetString(2);
                    foodEstablishment.Mobile_foodEstablishment = reader.GetString(3);
                    foodEstablishment.Login_foodEstablishment = reader.GetString(4);
                    foodEstablishment.Password_foodEstablishment = reader.GetString(5);
                    foodEstablishments.Add(foodEstablishment);
                }
            }
            return foodEstablishments;
        }
    }
}
