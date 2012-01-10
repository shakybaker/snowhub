using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class Country : IEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public string CountryName { get; set; }
        public int ContinentID { get; set; }
        public int MajorDestinationRank { get; set; }
        public string ISO3166Alpha2 { get; set; }
        public string ISO3166Alpha3 { get; set; }
        public string Capital { get; set; }
        public int Area { get; set; }
        public int Population { get; set; }
        public string Currency { get; set; }
        public string SubCurrency { get; set; }
        public bool HasResorts { get; set; }
        public string GenericPic { get; set; }
        public string Language { get; set; }
        public string Religion { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string GMT { get; set; }
        public string Mass { get; set; }
        public string HighestPoint { get; set; }
        public string HighestPointDescription { get; set; }
        public string PrettyUrl { get; set; }
        public Continent Continent { get; set; }
        public IList<Resort> Resorts { get; set; }
    }
}
