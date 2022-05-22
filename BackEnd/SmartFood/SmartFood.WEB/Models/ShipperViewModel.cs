using SmartFood.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartFood.WEB.Models
{
    public class ShipperViewModel
    {
        [Key]
        public int Id_shipper { get; set; }
        [Column]
        public string Name_shipper { get; set; }
        [Column]
        public string Address_shipper { get; set; }
        [Column]
        public string Mobile_shipper { get; set; }
        [Column]
        public string Login_shipper { get; set; }
        [Column]
        public string Password_shipper { get; set; }

        public ShipperDTO ReturnShipperDTO()
        {
            ShipperDTO shipperDTO = new ShipperDTO();
            shipperDTO.Id_shipper = this.Id_shipper;
            shipperDTO.Name_shipper = this.Name_shipper;
            shipperDTO.Address_shipper = this.Address_shipper;
            shipperDTO.Mobile_shipper = this.Mobile_shipper;
            shipperDTO.Login_shipper = this.Login_shipper;
            shipperDTO.Password_shipper = this.Password_shipper;
            return shipperDTO;
        }
    }
}
