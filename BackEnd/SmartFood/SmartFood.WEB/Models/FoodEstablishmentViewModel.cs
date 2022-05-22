using SmartFood.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFood.WEB.Models
{
    public class FoodEstablishmentViewModel
    {
        public int Id_foodEstablishment { get; set; }
        public string Name_foodEstablishment { get; set; }
        public string Address_foodEstablishment { get; set; }
        public string Mobile_foodEstablishment { get; set; }
        public string Login_foodEstablishment { get; set; }
        public string Password_foodEstablishment { get; set; }

        public FoodEstablishmentDTO ReturnFoodEstablishmentDTO()
        {
            FoodEstablishmentDTO foodEstablishmentDTO = new FoodEstablishmentDTO();
            foodEstablishmentDTO.Id_foodEstablishment = this.Id_foodEstablishment;
            foodEstablishmentDTO.Name_foodEstablishment = this.Name_foodEstablishment;
            foodEstablishmentDTO.Address_foodEstablishment = this.Address_foodEstablishment;
            foodEstablishmentDTO.Mobile_foodEstablishment = this.Mobile_foodEstablishment;
            foodEstablishmentDTO.Login_foodEstablishment = this.Login_foodEstablishment;
            foodEstablishmentDTO.Password_foodEstablishment = this.Password_foodEstablishment;
            return foodEstablishmentDTO;
        }
    }
}
