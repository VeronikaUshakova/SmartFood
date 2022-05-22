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
    public class ProductController : Controller
    {
        ProductService productService = new ProductService();
        // GET: ProductController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
        {
            try
            {
                List<ProductViewModel> productList = new List<ProductViewModel>();
                var products = await productService.GetALLProducts();
                if (products.Count > 0)
                {
                    foreach (var product in products)
                    {
                        ProductViewModel currentProduct = new ProductViewModel();
                        currentProduct.Id_product = product.Id_product;
                        currentProduct.Name_product = product.Name_product;
                        currentProduct.Price_product = product.Price_product;
                        currentProduct.Temperature_product = product.Temperature_product;
                        currentProduct.Humidity_product = product.Humidity_product;
                        currentProduct.Weight_product = product.Weight_product;
                        currentProduct.Id_shipper = product.Id_shipper;
                        productList.Add(currentProduct);

                    }
                }
                return productList;
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // GET: ProductController/Details/5
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                var product = productService.GetProduct(id);
                return new JsonResult(product);
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // POST: ProductController/Create
        [HttpPost]
        public ActionResult Create(ProductViewModel product)
        {
            try
            {
                productService.Create(product.ReturnProductDTO().ReturnProduct());
                return new JsonResult("Успешно добавлен продукт");
            }
            catch(ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // PUT: ProductController/Edit/5
        [HttpPut]
        public ActionResult Edit(int id, ProductViewModel productViewModel)
        {
            try
            {
                productViewModel.Id_product = id;
                var product = productViewModel.ReturnProductDTO().ReturnProduct();
                productService.Update(product);
                return new JsonResult("Успешно обновлен продукт");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }

        // DELETE: ProductController/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                productService.Delete(id);
                return new JsonResult("Успешно удален продукт");
            }
            catch (ValidationException ex)
            {
                return Content(ex.Message);
            }
        }
    }
}
