using SmartFood.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFood.WEB.Models
{
    public class HistoryBoxViewModel
    {
        public int Id_history { get; set; }
        public int Id_box { get; set; }
        public decimal Weight_box { get; set; }
        public decimal Temperature_box { get; set; }
        public decimal Humidity_box { get; set; }
        public DateTime DateTime_history { get; set; }

        public HistoryBoxDTO ReturnHistoryBoxDTO()
        {
            HistoryBoxDTO historyBoxDTO = new HistoryBoxDTO();
            historyBoxDTO.Id_history= this.Id_history;
            historyBoxDTO.Id_box = this.Id_box;
            historyBoxDTO.Weight_box = this.Weight_box;
            historyBoxDTO.Temperature_box = this.Temperature_box;
            historyBoxDTO.Humidity_box = this.Humidity_box;
            historyBoxDTO.DateTime_history = this.DateTime_history;
            return historyBoxDTO;
        }
    }
}
