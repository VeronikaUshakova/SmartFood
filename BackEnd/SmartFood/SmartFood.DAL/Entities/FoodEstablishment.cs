using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFood.DAL.Entities
{
    public class FoodEstablishment
    {
        [Key]
        public int Id_foodEstablishment { get; set; }
        [Required]
        public string Name_foodEstablishment { get; set; }
        [Required]
        public string Address_foodEstablishment { get; set; }
        [Required]
        public string Mobile_foodEstablishment { get; set; }
        [Required]
        public string Login_foodEstablishment { get; set; }
        [Required]
        public string Password_foodEstablishment { get; set; }
        [ForeignKey("Id_foodEstablishment")]
        private ICollection<Delivery> Deliveries { get; set; }
    }
}
