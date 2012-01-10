using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class LinkResortRegion
    {
        public int ID { get; set; }
        public int ResortID { get; set; }
        public int RegionID { get; set; }
        public string RegionTerm { get; set; }
        public Region Region { get; set; }
    }
}
