using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFood.BLL.DTO
{
    public class ShipperDTO
    {
        public int Id_shipper { get; set; }
        public string Name_shipper { get; set; }
        public string Address_shipper { get; set; }
        public string Mobile_shipper { get; set; }
        public string Login_shipper { get; set; }
        public string Password_shipper { get; set; }

        public Shipper ReturnShipper()
        {
            Shipper shipper = new Shipper();
            shipper.Id_shipper = this.Id_shipper;
            shipper.Name_shipper = this.Name_shipper;
            shipper.Address_shipper = this.Address_shipper;
            shipper.Mobile_shipper = this.Mobile_shipper;
            shipper.Login_shipper = this.Login_shipper;
            shipper.Password_shipper = this.Password_shipper;
            return shipper;
        }
    }
}
