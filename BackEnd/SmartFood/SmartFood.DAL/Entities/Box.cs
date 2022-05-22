using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFood.DAL.Entities
{
    public class Box
    {
        [Key]
        public int Id_box { get; set; }
        [Required]
        public int Id_product { get; set; }
        [Required]
        public decimal Initial_weight_product { get; set; }
        [Required]
        public DateTime DateEntry_box { get; set; }
        [Required]
        public int ShelfLife_box { get; set; }
        [Required]
        public int Id_delivery { get; set; }
        [ForeignKey("Id_box")]
        private ICollection<HistoryBox> HistoryBoxes { get; set; }
    }
}