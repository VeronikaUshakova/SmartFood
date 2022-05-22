using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using SmartFood.BLL.Infrastructure;
using SmartFood.BLL.Services;
using SmartFood.WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFood.WEB.Controllers
{
    public class ProductString
    {
        public int Id_product { get; set; }
        public string Name_product { get; set; }
        public decimal Price_product { get; set; }
        public decimal Temperature_product { get; set; }
        public decimal Humidity_product { get; set; }
        public decimal Weight_product { get; set; }
        public string Shipper { get; set; }
    }
    public class BoxString
    {
        public int Id_box { get; set; }
        public string Product { get; set; }
        public decimal Initial_weight_product { get; set; }
        public string DateEntry_box { get; set; }
        public int ShelfLife_box { get; set; }
        public string Delivery { get; set; }
    }
    public class DeliveryString
    {
        public int Id_delivery { get; set; }
        public string Shipper { get; set; }
        public string FoodEstablishment { get; set; }
        public string DateTime_delivery { get; set; }
    }
    public class HistoryBoxString
    {
        public int Id_history { get; set; }
        public int Id_box { get; set; }
        public decimal Weight_box { get; set; }
        public decimal Temperature_box { get; set; }
        public decimal Humidity_box { get; set; }
        public string DateTime_history { get; set; }
    }
    [ApiController]
    [Route("[controller]/[action]/{id?}")]
    public class AdminController : Controller
    {
        AdminService adminService = new AdminService();

        [HttpGet]
        public IActionResult BackUp(string str)
        {
            if (str != null)
            {
                try
                {
                    adminService.BackUp(@"C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\Backup\" + str+".bak");
                    return new JsonResult("Резервная копия создана");
                }
                catch (ValidationException ex)
                {
                    return Content(ex.Message);
                }
            }
            else 
            {
                return new JsonResult("Введите путь для сохранения резервной копии");
            }
        }
        [HttpGet]
        public IActionResult RestoreBackUp(string str)
        {
            if (str != null)
            {
                try
                {
                    adminService.RestoreBackUp(str);
                    return new JsonResult("Резервная копия применена");
                }
                catch (ValidationException ex)
                {
                    return Content(ex.Message);
                }
            }
            else
            {
                return new JsonResult("Введите путь для применения резервной копии");
            }
        }
        [HttpGet]
        public async Task<IActionResult> DownloadDelivery()
        {
            List<ShipperViewModel> shippers = new List<ShipperViewModel>();
            var shipper_getall = await adminService.DownloadShippersAsync();
            foreach (var shipper in shipper_getall)
            {
                ShipperViewModel shipper_one = new ShipperViewModel();
                shipper_one.Id_shipper = shipper.Id_shipper;
                shipper_one.Name_shipper = shipper.Name_shipper;
                shipper_one.Address_shipper = shipper.Address_shipper;
                shipper_one.Mobile_shipper = shipper.Mobile_shipper;
                shipper_one.Login_shipper = shipper.Login_shipper;
                shipper_one.Password_shipper = shipper.Password_shipper;
                shippers.Add(shipper_one);
            }
            List<FoodEstablishmentViewModel> foodEstablishments = new List<FoodEstablishmentViewModel>();
            var foodEstablishment_getall = await adminService.DownloadFoodEstablishmentAsync();
            foreach (var foodEstablishment in foodEstablishment_getall)
            {
                FoodEstablishmentViewModel foodEstablishment_one = new FoodEstablishmentViewModel();
                foodEstablishment_one.Id_foodEstablishment = foodEstablishment.Id_foodEstablishment;
                foodEstablishment_one.Name_foodEstablishment = foodEstablishment.Name_foodEstablishment;
                foodEstablishment_one.Address_foodEstablishment = foodEstablishment.Address_foodEstablishment;
                foodEstablishment_one.Mobile_foodEstablishment = foodEstablishment.Mobile_foodEstablishment;
                foodEstablishment_one.Login_foodEstablishment = foodEstablishment.Login_foodEstablishment;
                foodEstablishment_one.Password_foodEstablishment = foodEstablishment.Password_foodEstablishment;
                foodEstablishments.Add(foodEstablishment_one);
            }
            List<DeliveryString> deliveries = new List<DeliveryString>();
            var delivery_getall = await adminService.DownloadDeliveryAsync();
            foreach (var delivery in delivery_getall)
            {
                DeliveryString delivery_one = new DeliveryString();
                delivery_one.Id_delivery = delivery.Id_delivery;
                delivery_one.DateTime_delivery = delivery.DateTime_delivery.ToString();
                for (int i = 0; i < shippers.Count; i++)
                {
                    if (delivery.Id_shipper == shippers[i].Id_shipper)
                    {
                        delivery_one.Shipper = shippers[i].Name_shipper;
                    }
                }
                for (int i = 0; i < foodEstablishments.Count; i++)
                {
                    if (foodEstablishments[i].Id_foodEstablishment == delivery.Id_foodEstablishment)
                    {
                        delivery_one.FoodEstablishment = foodEstablishments[i].Name_foodEstablishment;
                    }
                }
                deliveries.Add(delivery_one);
            }
            return new JsonResult(deliveries);
        }
        [HttpGet]
        public async Task<IActionResult> DownloadHistoryBox()
        {
            List<HistoryBoxString> historyBoxes = new List<HistoryBoxString>();
            var historyBox_getall = await adminService.DownloadHistoryBoxAsync();
            foreach (var historyBox in historyBox_getall)
            {
                HistoryBoxString historyBox_one = new HistoryBoxString();
                historyBox_one.Id_history = historyBox.Id_history;
                historyBox_one.Weight_box = historyBox.Weight_box;
                historyBox_one.Temperature_box = historyBox.Temperature_box;
                historyBox_one.Humidity_box = historyBox.Humidity_box;
                historyBox_one.DateTime_history = historyBox.DateTime_history.ToString();
                historyBox_one.Id_box = historyBox.Id_box;
                historyBoxes.Add(historyBox_one);
            }
            return new JsonResult(historyBoxes);
        }
        [HttpGet]
        public async Task<IActionResult> DownloadBox()
        {
            List<ShipperViewModel> shippers = new List<ShipperViewModel>();
            var shipper_getall = await adminService.DownloadShippersAsync();
            foreach (var shipper in shipper_getall)
            {
                ShipperViewModel shipper_one = new ShipperViewModel();
                shipper_one.Id_shipper = shipper.Id_shipper;
                shipper_one.Name_shipper = shipper.Name_shipper;
                shipper_one.Address_shipper = shipper.Address_shipper;
                shipper_one.Mobile_shipper = shipper.Mobile_shipper;
                shipper_one.Login_shipper = shipper.Login_shipper;
                shipper_one.Password_shipper = shipper.Password_shipper;
                shippers.Add(shipper_one);
            }
            List<ProductString> products = new List<ProductString>();
            var product_getall = await adminService.DownloadProductAsync();
            foreach (var product in product_getall)
            {
                ProductString product_one = new ProductString();
                product_one.Id_product = product.Id_product;
                product_one.Name_product = product.Name_product;
                product_one.Price_product = product.Price_product;
                product_one.Temperature_product = product.Temperature_product;
                product_one.Humidity_product = product.Humidity_product;
                product_one.Weight_product = product.Weight_product;
                for (int i = 0; i < shippers.Count; i++)
                {
                    if (product.Id_shipper == shippers[i].Id_shipper)
                    {
                        product_one.Shipper = shippers[i].Name_shipper;
                    }
                }
                products.Add(product_one);
            }
            List<BoxString> boxes = new List<BoxString>();
            var box_getall = await adminService.DownloadBoxAsync();
            foreach (var box in box_getall)
            {
                BoxString box_one = new BoxString();
                box_one.Id_box = box.Id_box;
                for (int i = 0; i < products.Count; i++)
                {
                    if (box.Id_product == products[i].Id_product)
                    {
                        box_one.Product = products[i].Name_product;
                    }
                }
                box_one.Initial_weight_product = box.Initial_weight_product;
                box_one.Delivery = box.Id_delivery.ToString();
                box_one.DateEntry_box = box.DateEntry_box.ToString();
                box_one.ShelfLife_box = box.ShelfLife_box;
                boxes.Add(box_one);
            }
            return new JsonResult(boxes);
        }
        [HttpGet]
        public async Task<IActionResult> DownloadProduct()
        {
            List<ShipperViewModel> shippers = new List<ShipperViewModel>();
            var shipper_getall = await adminService.DownloadShippersAsync();
            foreach (var shipper in shipper_getall)
            {
                ShipperViewModel shipper_one = new ShipperViewModel();
                shipper_one.Id_shipper = shipper.Id_shipper;
                shipper_one.Name_shipper = shipper.Name_shipper;
                shipper_one.Address_shipper = shipper.Address_shipper;
                shipper_one.Mobile_shipper = shipper.Mobile_shipper;
                shipper_one.Login_shipper = shipper.Login_shipper;
                shipper_one.Password_shipper = shipper.Password_shipper;
                shippers.Add(shipper_one);
            }
            List<ProductString> products = new List<ProductString>();
            var product_getall = await adminService.DownloadProductAsync();
            foreach (var product in product_getall)
            {
                ProductString product_one = new ProductString();
                product_one.Id_product = product.Id_product;
                product_one.Name_product = product.Name_product;
                product_one.Price_product = product.Price_product;
                product_one.Temperature_product = product.Temperature_product;
                product_one.Humidity_product = product.Humidity_product;
                product_one.Weight_product = product.Weight_product;
                for (int i = 0; i < shippers.Count; i++)
                {
                    if (product.Id_shipper == shippers[i].Id_shipper)
                    {
                        product_one.Shipper = shippers[i].Name_shipper;
                    }
                }
                products.Add(product_one);
            }
            return new JsonResult(products);
        }
        [HttpGet]
        public async Task<IActionResult> DownloadFoodEstablishment()
        {
            List<FoodEstablishmentViewModel> foodEstablishments = new List<FoodEstablishmentViewModel>();
            var foodEstablishment_getall = await adminService.DownloadFoodEstablishmentAsync();
            foreach (var foodEstablishment in foodEstablishment_getall)
            {
                FoodEstablishmentViewModel foodEstablishment_one = new FoodEstablishmentViewModel();
                foodEstablishment_one.Id_foodEstablishment = foodEstablishment.Id_foodEstablishment;
                foodEstablishment_one.Name_foodEstablishment = foodEstablishment.Name_foodEstablishment;
                foodEstablishment_one.Address_foodEstablishment = foodEstablishment.Address_foodEstablishment;
                foodEstablishment_one.Mobile_foodEstablishment = foodEstablishment.Mobile_foodEstablishment;
                foodEstablishment_one.Login_foodEstablishment = foodEstablishment.Login_foodEstablishment;
                foodEstablishment_one.Password_foodEstablishment = foodEstablishment.Password_foodEstablishment;
                foodEstablishments.Add(foodEstablishment_one);
            }
            return new JsonResult(foodEstablishments);
        }
        [HttpGet]
        public async Task<IActionResult> DownloadShipper()
        {
            List<ShipperViewModel> shippers = new List<ShipperViewModel>();
            var shipper_getall = await adminService.DownloadShippersAsync();
            foreach (var shipper in shipper_getall)
            {
                ShipperViewModel shipper_one = new ShipperViewModel();
                shipper_one.Id_shipper = shipper.Id_shipper;
                shipper_one.Name_shipper = shipper.Name_shipper;
                shipper_one.Address_shipper = shipper.Address_shipper;
                shipper_one.Mobile_shipper = shipper.Mobile_shipper;
                shipper_one.Login_shipper = shipper.Login_shipper;
                shipper_one.Password_shipper = shipper.Password_shipper;
                shippers.Add(shipper_one);
            }
            return new JsonResult(shippers);
        }
        [HttpGet]
        public ActionResult Check([FromHeader]string password,[FromHeader] string login)
        {
            try
            {
                return new JsonResult(adminService.CheckAdmin(password, login));
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
