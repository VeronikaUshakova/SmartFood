using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFood.BLL.DTO
{
    public class FoodEstablishmentDTO
    {
        public int Id_foodEstablishment { get; set; }
        public string Name_foodEstablishment { get; set; }
        public string Address_foodEstablishment { get; set; }
        public string Mobile_foodEstablishment { get; set; }
        public string Login_foodEstablishment { get; set; }
        public string Password_foodEstablishment { get; set; }

        public FoodEstablishment ReturnFoodEstablishment()
        {
            FoodEstablishment foodEstablishment = new FoodEstablishment();
            foodEstablishment.Id_foodEstablishment = this.Id_foodEstablishment;
            foodEstablishment.Name_foodEstablishment = this.Name_foodEstablishment;
            foodEstablishment.Address_foodEstablishment = this.Address_foodEstablishment;
            foodEstablishment.Mobile_foodEstablishment = this.Mobile_foodEstablishment;
            foodEstablishment.Login_foodEstablishment = this.Login_foodEstablishment;
            foodEstablishment.Password_foodEstablishment = this.Password_foodEstablishment;
            return foodEstablishment;
        }
    }
}
