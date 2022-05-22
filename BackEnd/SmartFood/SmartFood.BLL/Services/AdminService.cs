using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using SmartFood.BLL.DTO;
using SmartFood.BLL.Infrastructure;
using SmartFood.DAL.Entities;
using SmartFood.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartFood.BLL.Services
{
    public class AdminService
    {
        EFUnitOfWork Database = new EFUnitOfWork();

        public void BackUp(string downloadPath)
        {
            try
            {
                if (File.Exists(@downloadPath))
                {
                    File.Delete(@downloadPath);
                }

                FileStream fs = File.Create(downloadPath);
                fs.Close();
                string query = "BACKUP DATABASE SmartFood TO DISK = '" + downloadPath + "';";
                Database.Admins.ContextConnection().ExecuteSqlRaw(query);
            }
            catch (Exception Ex)
            {
                throw new ValidationException(Ex.Message);
            }
        }
        public void RestoreBackUp(string downloadPath)
        {
            try
            {
                string query = "USE master";
                query += " RESTORE DATABASE SmartFood FROM DISK = '" + downloadPath + "'";
                query += " WITH REPLACE";
                Database.Admins.ContextConnection().ExecuteSqlRaw(query);
            }
            catch (Exception Ex)
            {
                throw new ValidationException(Ex.Message);
            }
        }
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
        public async Task<List<Shipper>> DownloadShippersAsync()
        {
            List<Shipper> shippers = await Database.Shippers.GetAll();
            return shippers;
        }
        public async Task<List<FoodEstablishment>> DownloadFoodEstablishmentAsync()
        {
            List<FoodEstablishment> foodEstablishments = await Database.FoodEstablishments.GetAll();
            return foodEstablishments;
        }
        public async Task<List<Product>> DownloadProductAsync()
        {
            List<Product> products = await Database.Products.GetAll();
            return products;
        }
        public async Task<List<Box>> DownloadBoxAsync()
        {
            List<Box> boxes = await Database.Boxes.GetAll();
            return boxes;
        }
        public async Task<List<HistoryBox>> DownloadHistoryBoxAsync()
        {
            List<HistoryBox> historyBoxes = await Database.HistoryBoxes.GetAll();
            return historyBoxes;
        }
        public async Task<List<Delivery>> DownloadDeliveryAsync()
        {
            List<Delivery> deliveries = await Database.Deliveries.GetAll();
            return deliveries;
        }
        public Boolean CheckAdmin(string password, string login)
        {
            if (password == null)
            {
                throw new ValidationException("Пароль не был введен");
            }
            if (login == null)
            {
                throw new ValidationException("Логин не был введен");
            }
            Admin admin = Database.Admins.Get();
            if (admin.Password== GetHashString(password) && admin.Login == GetHashString(login))
            {
                return true;
            }
            return false;
        }
    }
}
