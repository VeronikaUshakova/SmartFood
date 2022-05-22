using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFood.WEB.Models
{
    public class DeliveryStr
    {
        public int Id_Delivery { get; set; }
        public int Id_Shipper { get; set; }
        public string Name_FoodEstablishment { get; set; }
        public string DateTime_delivery { get; set; }
    }
}
