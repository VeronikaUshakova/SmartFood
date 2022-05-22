using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFood.DAL.Entities
{
    public class Product
    {
        [Key]
        public int Id_product { get; set; }
        [Required]
        public string Name_product { get; set; }
        [Required]
        public decimal Price_product { get; set; }
        [Required]
        public decimal Temperature_product { get; set; }
        [Required]
        public decimal Humidity_product { get; set; }
        [Required]
        public decimal Weight_product { get; set; }
        [Required]
        public int Id_shipper { get; set; }
        [ForeignKey("Id_product")]
        private ICollection<Box> Boxes { get; set; }
    }
}
