using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporthub.Model
{
    public class Location
    {
        public int ID { get; set; }
        public string LocationType { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PrettyUrl { get; set; }
    }
}
