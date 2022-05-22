using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFood.WEB.Models
{
    public class DeliveryFE
    {
        public int IdDelivery { get; set; }
        public string IdShipper { get; set; }
        public int IdFoodEstablishment { get; set; }
        public string DateTime_delivery { get; set; }
    }
}
