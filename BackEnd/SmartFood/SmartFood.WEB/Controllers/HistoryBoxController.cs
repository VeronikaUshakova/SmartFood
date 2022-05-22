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
    public class HistoryBoxController : Controller
    {
        HistoryBoxService historyBoxService = new HistoryBoxService();
        // GET: HistoryBoxController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryBoxViewModel>>> Index()
        {
            try
            {
                List<HistoryBoxViewModel> historyBoxList = new List<HistoryBoxViewModel>();
                var historyBoxes = await historyBoxService.GetALLHistoryBoxes();
                if (historyBoxes.Count > 0)
                {
                    foreach (var historyBox in historyBoxes)
                    {
                        HistoryBoxViewModel currentHistoryBox = new HistoryBoxViewModel();
                        currentHistoryBox.Id_history = historyBox.Id_history;
                        currentHistoryBox.Id_box = historyBox.Id_box;
                        currentHistoryBox.Humidity_box = historyBox.Humidity_box;
                        currentHistoryBox.Temperature_box = historyBox.Temperature_box;
                        currentHistoryBox.DateTime_history = historyBox.DateTime_history;
                        currentHistoryBox.Weight_box = historyBox.Weight_box;
                        historyBoxList.Add(currentHistoryBox);

                    }
                }
                return historyBoxList;
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: HistoryBoxController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var historyBox = historyBoxService.GetHistoryBox(id);
                return new JsonResult(historyBox);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // POST: HistoryBoxController/Create
        [HttpPost]
        public ActionResult Create(HistoryBoxViewModel historyBox)
        {
            try
            {
                historyBoxService.Create(historyBox.ReturnHistoryBoxDTO().ReturnHistoryBox());
                return new JsonResult("Успешно добавлена история ящика");
            }
            catch(ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // PUT: HistoryBoxController/Edit/5
        [HttpPut]
        public ActionResult Edit(int id,HistoryBoxViewModel historyBoxViewModel)
        {
            try
            {
                var historyBox = historyBoxService.GetHistoryBox(id);
                historyBoxViewModel.Id_history = id;
                historyBox = historyBoxViewModel.ReturnHistoryBoxDTO().ReturnHistoryBox();
                historyBoxService.Update(historyBox);
                return new JsonResult("Успешно обновлена история ящика");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // DELETE: HistoryBoxController/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                historyBoxService.Delete(id);
                return new JsonResult("Успешно удалена история ящика");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
