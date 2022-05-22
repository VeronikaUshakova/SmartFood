using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFood.BLL.DTO
{
    public class DeliveryDTO
    {
        public int Id_delivery { get; set; }
        public int Id_shipper { get; set; }
        public int Id_foodEstablishment { get; set; }
        public DateTime DateTime_delivery { get; set; }

        public Delivery ReturnDelivery()
        {
            Delivery delivery = new Delivery();
            delivery.Id_delivery = this.Id_delivery;
            delivery.Id_shipper = this.Id_shipper;
            delivery.Id_foodEstablishment = this.Id_foodEstablishment;
            delivery.DateTime_delivery = this.DateTime_delivery;
            return delivery;
        }
    }
}
