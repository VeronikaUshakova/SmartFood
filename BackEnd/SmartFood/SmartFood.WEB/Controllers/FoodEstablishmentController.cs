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
    public class FoodEstablishmentController : Controller
    {
        FoodEstablishmentService foodEstablishmentService = new FoodEstablishmentService();
        // GET: FoodEstablishmentController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodEstablishmentViewModel>>> Index()
        {
            try
            {
                List<FoodEstablishmentViewModel> foodEstablishmentList = new List<FoodEstablishmentViewModel>();
                var foodEstablishments = await foodEstablishmentService.GetALLFoodEstablishments();
                if (foodEstablishments.Count > 0)
                {
                    foreach (var foodEstablishment in foodEstablishments)
                    {
                        FoodEstablishmentViewModel currentFoodEstablishment = new FoodEstablishmentViewModel();
                        currentFoodEstablishment.Id_foodEstablishment = foodEstablishment.Id_foodEstablishment;
                        currentFoodEstablishment.Name_foodEstablishment = foodEstablishment.Name_foodEstablishment;
                        currentFoodEstablishment.Address_foodEstablishment = foodEstablishment.Address_foodEstablishment;
                        currentFoodEstablishment.Mobile_foodEstablishment = foodEstablishment.Mobile_foodEstablishment;
                        currentFoodEstablishment.Login_foodEstablishment = foodEstablishment.Login_foodEstablishment;
                        currentFoodEstablishment.Password_foodEstablishment = foodEstablishment.Password_foodEstablishment;
                        foodEstablishmentList.Add(currentFoodEstablishment);

                    }
                }
                return foodEstablishmentList;
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: FoodEstablishmentController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var foodEstablishment = foodEstablishmentService.GetFoodEstablishment(id);
                return new JsonResult(foodEstablishment);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // POST: FoodEstablishmentController/Create
        [HttpPost]
        public async Task<ActionResult> Create(FoodEstablishmentViewModel foodEstablishment)
        {
            try
            {
                await foodEstablishmentService.Create(foodEstablishment.ReturnFoodEstablishmentDTO().ReturnFoodEstablishment());
                return new JsonResult("Успешно добавлено заведение");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // PUT: FoodEstablishment/Edit/5
        [HttpPut]
        public async Task<ActionResult> Edit(int id, FoodEstablishmentViewModel foodEstablishmentViewModel)
        {
            try
            {
                var foodEstablishment = foodEstablishmentService.GetFoodEstablishment(id);
                foodEstablishmentViewModel.Id_foodEstablishment = id;
                foodEstablishment = foodEstablishmentViewModel.ReturnFoodEstablishmentDTO().ReturnFoodEstablishment();
                await foodEstablishmentService.Update(foodEstablishment);
                return new JsonResult("Успешно обновлено заведение");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // DELETE: FoodEstablishment/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                foodEstablishmentService.Delete(id);
                return new JsonResult("Успешно удалено заведение");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult PercentCorrectStorageProduct(int product_id, int foodEstablishment_id)
        {
            try
            {
                decimal percent = foodEstablishmentService.PercentageOfCorrectStorageOfTheProduct(product_id, foodEstablishment_id);
                return new JsonResult(percent);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult PercentageUseOrderedProduct(int foodEstablishment_id, int delivery_id)
        {
            try
            {
                decimal percent = foodEstablishmentService.PercentageUseOrderedProduct(foodEstablishment_id, delivery_id);
                return new JsonResult(percent);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet]
        public async Task<ActionResult> Check(string password, string login)
        {
            try
            {
                return new JsonResult(await foodEstablishmentService.CheckFoodEstablishment(password, login));
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<DeliveryFE>> FoodEstablishmentDeliveries(int id)
        {
            try
            {
                List<DeliveryFE> deliveries = new List<DeliveryFE>();
                var list = foodEstablishmentService.FoodEstablishmentDeliveries(id);
                for (int i = 0; i < list.Count; i++)
                {
                    DeliveryFE delivery = new DeliveryFE();
                    delivery.IdDelivery = list[i].IdDelivery;
                    delivery.IdFoodEstablishment = list[i].IdFoodEstablishment;
                    delivery.IdShipper = list[i].IdShipper;
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
        public ActionResult FoodEstablishmentShippers(int id)
        {
            try
            {
                return new JsonResult(foodEstablishmentService.FoodEstablishmentShippers(id));
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpGet]
        public ActionResult FoodEstablishmentProducts(int id)
        {
            try
            {
                return new JsonResult(foodEstablishmentService.FoodEstablishmentProducts(id));
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
