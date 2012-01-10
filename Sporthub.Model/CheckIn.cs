using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class CheckIn
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ResortID { get; set; }
        public DateTime CheckinDate { get; set; }
        public string IPAddress { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public Resort Resort { get; set; }
    }
}
