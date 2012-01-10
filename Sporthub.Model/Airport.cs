using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class Airport : IEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public string PrettyUrl { get; set; }
        public string NameFriendlyFormat { get; set; }
        public string PrettyUrlFriendlyFormat { get; set; }
        public int? CountryID { get; set; }
        public int? ContinentID { get; set; }
        public string CountryName { get; set; }
        public string ContinentName { get; set; }
        public string Website { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }


        public Continent Continent { get; set; }
        public Country Country { get; set; }
        public Region Region { get; set; }

        public IList<Resort> NearbyResorts { get; set; }
        public IList<Resort> ResortsWithinOneHour { get { return NearbyResorts.Where(x => x.Latitude < 90).ToList(); } }
        public IList<Resort> ResortsWithinOneAndAHalfHours { get { return NearbyResorts.Where(x => x.Latitude < 114 && x.Latitude >= 90).ToList(); } }
        public IList<Resort> ResortsWithinTwoHour { get { return NearbyResorts.Where(x => x.Latitude < 142 && x.Latitude >= 114).ToList(); } }
        public IList<Resort> ResortsWithinTwoAndAHalfHours { get { return NearbyResorts.Where(x => x.Latitude < 190 && x.Latitude >= 142).ToList(); } }
        public IList<Resort> ResortsFurtherThanTwoAndAHalfHours { get { return NearbyResorts.Where(x => x.Latitude >= 190).ToList(); } }

        public Airport()
        {
            NearbyResorts = new List<Resort>();
        }
    }
}
