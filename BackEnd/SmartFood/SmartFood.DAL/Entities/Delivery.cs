using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFood.DAL.Entities
{
    public class Delivery
    {
        [Key]
        public int Id_delivery { get; set; }
        [Required]
        public int Id_shipper { get; set; }
        [Required]
        public int Id_foodEstablishment { get; set; }
        [Required]
        public DateTime DateTime_delivery { get; set; }
        [ForeignKey("Id_delivery")]
        private ICollection<Box> Boxes { get; set; }
    }
}