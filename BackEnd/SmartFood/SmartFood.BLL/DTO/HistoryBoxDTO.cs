using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFood.BLL.DTO
{
    public class HistoryBoxDTO
    {
        public int Id_history { get; set; }
        public int Id_box { get; set; }
        public decimal Weight_box { get; set; }
        public decimal Temperature_box { get; set; }
        public decimal Humidity_box { get; set; }
        public DateTime DateTime_history { get; set; }

        public HistoryBox ReturnHistoryBox()
        {
            HistoryBox historyBox = new HistoryBox();
            historyBox.Id_history= this.Id_history;
            historyBox.Id_box = this.Id_box;
            historyBox.Weight_box = this.Weight_box;
            historyBox.Temperature_box = this.Temperature_box;
            historyBox.Humidity_box = this.Humidity_box;
            historyBox.DateTime_history = this.DateTime_history;
            return historyBox;
        }
    }
}
