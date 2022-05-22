using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFood.BLL.DTO
{
    public class BoxDTO
    {
        public int Id_box { get; set; }
        public int Id_product { get; set; }
        public decimal Initial_weight_product { get; set; }
        public DateTime DateEntry_box { get; set; }
        public int ShelfLife_box { get; set; }
        public int Id_delivery { get; set; }

        public Box ReturnBox()
        {
            Box box = new Box();
            box.Id_box = this.Id_box;
            box.Id_product = this.Id_product;
            box.Initial_weight_product = this.Initial_weight_product;
            box.Id_delivery = this.Id_delivery;
            box.DateEntry_box = this.DateEntry_box;
            box.ShelfLife_box = this.ShelfLife_box;
            return box;
        }
    }
}
