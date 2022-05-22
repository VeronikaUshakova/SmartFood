using SmartFood.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFood.WEB.Models
{
    public class ProductViewModel
    {
        public int Id_product { get; set; }
        public string Name_product { get; set; }
        public decimal Price_product { get; set; }
        public decimal Temperature_product { get; set; }
        public decimal Humidity_product { get; set; }
        public decimal Weight_product { get; set; }
        public int Id_shipper { get; set; }

        public ProductDTO ReturnProductDTO()
        {
            ProductDTO productDTO = new ProductDTO();
            productDTO.Id_product = this.Id_product;
            productDTO.Name_product = this.Name_product;
            productDTO.Price_product = this.Price_product;
            productDTO.Temperature_product = this.Temperature_product;
            productDTO.Humidity_product= this.Humidity_product;
            productDTO.Weight_product = this.Weight_product;
            productDTO.Id_shipper = this.Id_shipper;
            return productDTO;
        }
    }
}
