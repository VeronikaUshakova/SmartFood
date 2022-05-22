using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFood.BLL.DTO
{
    public class ProductDTO
    {
        public int Id_product { get; set; }
        public string Name_product { get; set; }
        public decimal Price_product { get; set; }
        public decimal Temperature_product { get; set; }
        public decimal Humidity_product { get; set; }
        public decimal Weight_product { get; set; }
        public int Id_shipper { get; set; }

        public Product ReturnProduct()
        {
            Product product = new Product();
            product.Id_product = this.Id_product;
            product.Name_product = this.Name_product;
            product.Price_product = this.Price_product;
            product.Temperature_product = this.Temperature_product;
            product.Humidity_product= this.Humidity_product;
            product.Weight_product = this.Weight_product;
            product.Id_shipper = this.Id_shipper;
            return product;
        }
    }
}
