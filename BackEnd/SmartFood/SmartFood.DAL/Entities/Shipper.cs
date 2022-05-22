using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFood.DAL.Entities
{
    public class Shipper
    {
        [Key]
        public int Id_shipper { get; set; }
        [Required]
        public string Name_shipper { get; set; }
        [Required]
        public string Address_shipper { get; set; }
        [Required]
        public string Mobile_shipper { get; set; }
        [Required]
        public string Login_shipper { get; set; }
        [Required]
        public string Password_shipper { get; set; }
        [ForeignKey("Id_shipper")]
        private ICollection<Product> Products { get; set; }
    }
}
