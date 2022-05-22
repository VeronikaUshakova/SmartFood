using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFood.DAL.Entities
{
    public class HistoryBox
    {
        [Key]
        public int Id_history { get; set; }
        [Required]
        public int Id_box { get; set; }
        [Required]
        public decimal Weight_box { get; set; }
        [Required]
        public decimal Temperature_box { get; set; }
        [Required]
        public decimal Humidity_box { get; set; }
        [Required]
        public DateTime DateTime_history { get; set; }
    }
}
