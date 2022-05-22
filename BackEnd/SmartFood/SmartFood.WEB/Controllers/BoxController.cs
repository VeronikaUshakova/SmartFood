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
    public class BoxController : Controller
    {
        BoxService boxService = new BoxService();
        // GET: BoxController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoxViewModel>>> Index()
        {
            try
            {
                List<BoxViewModel> boxList = new List<BoxViewModel>();
                var boxes = await boxService.GetALLBoxes();
                if (boxes.Count > 0)
                {
                    foreach (var box in boxes)
                    {
                        BoxViewModel currentBox = new BoxViewModel();
                        currentBox.Id_box = box.Id_box;
                        currentBox.Id_product = box.Id_product;
                        currentBox.Initial_weight_product = box.Initial_weight_product;
                        currentBox.DateEntry_box = box.DateEntry_box;
                        currentBox.ShelfLife_box = box.ShelfLife_box;
                        currentBox.Id_delivery = box.Id_delivery;
                        boxList.Add(currentBox);

                    }
                }
                return boxList;
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: BoxController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var box = boxService.GetBox(id);
                return new JsonResult(box);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // POST: BoxController/Create
        [HttpPost]
        public ActionResult Create(BoxViewModel box)
        {
            try
            {
                boxService.Create(box.ReturnBoxDTO().ReturnBox());
                return new JsonResult("Успешно добавлен ящик");
            }
            catch(ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // PUT: BoxController/Edit/5
        [HttpPut]
        public ActionResult Edit(int id,BoxViewModel boxViewModel)
        {
            try
            {
                var box = boxService.GetBox(id);
                boxViewModel.Id_box = id;
                box = boxViewModel.ReturnBoxDTO().ReturnBox();
                boxService.Update(box);
                return new JsonResult("Успешно обновлен ящик");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // DELETE: BoxController/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                boxService.Delete(id);
                return new JsonResult("Успешно удален ящик");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult HistoryBoxesBox(int id)
        {
            try 
            {
                return new JsonResult(boxService.HistoryBoxesBox(id));
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
