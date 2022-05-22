using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartFood.BLL.Infrastructure;
using SmartFood.BLL.Services;
using SmartFood.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFood.WEB.Controllers
{
    [ApiController]
    [Route("[controller]/[action]/{id?}")]
    public class DeliveryController : Controller
    {
        DeliveryService deliveryService = new DeliveryService();
        // GET: DeliveryController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeliveryViewModel>>> Index()
        {
            try
            {
                List<DeliveryViewModel> deliveryList = new List<DeliveryViewModel>();
                var deliveries = await deliveryService.GetALLDeliveries();
                if (deliveries.Count > 0)
                {
                    foreach (var delivery in deliveries)
                    {
                        DeliveryViewModel currentDelivery = new DeliveryViewModel();
                        currentDelivery.Id_delivery = delivery.Id_delivery;
                        currentDelivery.Id_shipper = delivery.Id_shipper;
                        currentDelivery.Id_foodEstablishment = delivery.Id_foodEstablishment;
                        currentDelivery.DateTime_delivery = delivery.DateTime_delivery;
                        deliveryList.Add(currentDelivery);

                    }
                }
                return deliveryList;
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: Delivery/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var delivery = deliveryService.GetDelivery(id);
                return new JsonResult(delivery);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // POST: Delivery/Create
        [HttpPost]
        public ActionResult Create(DeliveryViewModel delivery)
        {
            try
            {
                deliveryService.Create(delivery.ReturnDeliveryDTO().ReturnDelivery());
                return new JsonResult("Успешно добавлена поставка");
            }
            catch(ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // PUT: Delivery/Edit/5
        [HttpPut]
        public ActionResult Edit(int id, DeliveryViewModel deliveryViewModel)
        {
            try
            {
                var delivery = deliveryService.GetDelivery(id);
                deliveryViewModel.Id_delivery = id;
                delivery = deliveryViewModel.ReturnDeliveryDTO().ReturnDelivery();
                deliveryService.Update(delivery);
                return new JsonResult("Успешно обновлена поставка");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // DELETE: Delivery/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                deliveryService.Delete(id);
                return new JsonResult("Успешно удалена поставка");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
