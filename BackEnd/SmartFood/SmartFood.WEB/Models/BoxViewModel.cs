using SmartFood.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFood.WEB.Models
{
    public class BoxViewModel
    {
        public int Id_box { get; set; }
        public int Id_product { get; set; }
        public decimal Initial_weight_product { get; set; }
        public DateTime DateEntry_box { get; set; }
        public int ShelfLife_box { get; set; }
        public int Id_delivery { get; set; }

        public BoxDTO ReturnBoxDTO()
        {
            BoxDTO boxDTO = new BoxDTO();
            boxDTO.Id_box = this.Id_box;
            boxDTO.Id_product = this.Id_product;
            boxDTO.Initial_weight_product = this.Initial_weight_product;
            boxDTO.Id_delivery = this.Id_delivery;
            boxDTO.DateEntry_box = this.DateEntry_box;
            boxDTO.ShelfLife_box = this.ShelfLife_box;
            return boxDTO;
        }
    }
}
