using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporthub.Model
{
    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string TownOrCity { get; set; }
        public string PostCode { get; set; }
        public Country Country { get; set; }
        public Continent Continent { get; set; }
    }
}
