using System;
using System.Collections.Generic;
using System.Text;
using SmartFood.BLL.DTO;

namespace SmartFood.WEB.Models
{
    public class DeliveryViewModel
    {
        public int Id_delivery { get; set; }
        public int Id_shipper { get; set; }
        public int Id_foodEstablishment { get; set; }
        public DateTime DateTime_delivery { get; set; }

        public DeliveryDTO ReturnDeliveryDTO()
        {
            DeliveryDTO deliveryDTO = new DeliveryDTO();
            deliveryDTO.Id_delivery = this.Id_delivery;
            deliveryDTO.Id_shipper = this.Id_shipper;
            deliveryDTO.Id_foodEstablishment = this.Id_foodEstablishment;
            deliveryDTO.DateTime_delivery = this.DateTime_delivery;
            return deliveryDTO;
        }
    }
}
