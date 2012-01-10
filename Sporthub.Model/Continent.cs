using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class Continent : IEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public string ContinentName { get; set; }
        public string Code { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PrettyUrl { get; set; }
        public IList<Country> Countries { get; set; }
    }
}
