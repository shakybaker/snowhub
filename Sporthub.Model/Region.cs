using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class Region : IEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public int? CountryID { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public string PrettyUrl { get; set; }
        public int? ParentRegionID { get; set; }
        public int? RegionLevel { get; set; }

        public Region ParentRegion { get; set; }
        public Country Country { get; set; }
        public IList<Region> SubRegions { get; set; }
        public IList<Resort> Resorts { get; set; }
    }
}
