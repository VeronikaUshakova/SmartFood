using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartFood.BLL.Infrastructure;
using SmartFood.BLL.Services;
using SmartFood.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartFood.WEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]/{id?}")]

    public class ShipperController : Controller
    {
        ShipperService shipperService = new ShipperService();
        // GET: ShipperController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShipperViewModel>>> Index()
        {
            try
            {
                List<ShipperViewModel> shipperList = new List<ShipperViewModel>();
                var shippers = await shipperService.GetALLShippers();
                if (shippers.Count > 0)
                {
                    foreach (var shipper in shippers)
                    {
                        ShipperViewModel currentShipper = new ShipperViewModel();
                        currentShipper.Id_shipper = shipper.Id_shipper;
                        currentShipper.Name_shipper = shipper.Name_shipper;
                        currentShipper.Address_shipper = shipper.Address_shipper;
                        currentShipper.Mobile_shipper = shipper.Mobile_shipper;
                        currentShipper.Login_shipper = shipper.Login_shipper;
                        currentShipper.Password_shipper = shipper.Password_shipper;
                        shipperList.Add(currentShipper);
                    }
                }
                return shipperList;
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: ShipperController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var shipper = shipperService.GetShipper(id);
                return new JsonResult(shipper);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult ShipperProducts(int id)
        {
            try
            {
                var products = shipperService.ShipperProducts(id);
                return new JsonResult(products);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult<List<DeliveryStr>> ShipperDeliveries(int id)
        {
            try
            {
                List<DeliveryStr> deliveries = new List<DeliveryStr>();
                var list = shipperService.ShipperDeliveries(id);
                for (int i = 0; i < list.Count; i++)
                {
                    DeliveryStr delivery = new DeliveryStr();
                    delivery.Id_Delivery = list[i].IdDelivery;
                    delivery.Id_Shipper = list[i].IdShipper;
                    delivery.Name_FoodEstablishment = list[i].NameFoodEstablishment;
                    delivery.DateTime_delivery = list[i].DateTime_delivery;
                    deliveries.Add(delivery);
                }
                return deliveries;
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult ShipperFoodEstablishments(int id)
        {
            try
            {
                return new JsonResult(shipperService.ShipperFoodEstablishments(id));
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // POST: ShipperController/Create
        [HttpPost]
        public async Task<ActionResult> Create(ShipperViewModel shipper)
        {
            try
            {
                await shipperService.Create(shipper.ReturnShipperDTO().ReturnShipper());
                return new JsonResult("Успешно добавлен поставщик");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // PUT: ShipperController/Edit/5
        [HttpPut]
        public async Task<IActionResult> Edit(int id, ShipperViewModel shipperViewModel)
        {
            try
            {
                var shipper = shipperService.GetShipper(id);
                shipperViewModel.Id_shipper = id;
                shipper = shipperViewModel.ReturnShipperDTO().ReturnShipper();
                await shipperService.Update(shipper);
                return new JsonResult("Успешно обновлен поставщик");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // DELETE: ShipperController/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                shipperService.Delete(id);
                return new JsonResult("Успешно удален поставщик");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult PercentWeek(int id)
        {
            try
            {
                decimal percent = shipperService.PercentWeek(id);
                return new JsonResult(percent);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult PercentMonth(int id)
        {
            try
            {
                decimal percent = shipperService.PercentMonth(id);
                return new JsonResult(percent);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Check(string password, string login)
        {
            try
            {
                return new JsonResult(await shipperService.CheckShipperAsync(password, login));
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
