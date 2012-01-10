using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;
using Sporthub.Model.Enumerators;

namespace Sporthub.Mvc.ViewData
{
    public class ResortsListViewData : SporthubViewData
    {
        public LocationLevel LocationLevel { get; set; }
        public string LocationName { get; set; }
        public string LocationUrl { get; set; }

        public IList<Continent> Continents { get; set; }
        public IList<Country> Countries { get; set; }
        public IList<Region> Regions { get; set; }
        public IList<Resort> Resorts { get; set; }

        public Continent Continent { get; set; }
        public Country Country { get; set; }
        public Region Region { get; set; }
        public Resort Resort { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; }
        public IList<Resort> SkiAreas { get; set; }
        public IList<Resort> TopRatedResorts { get; set; }
        public IList<Resort> MostVisitedResorts { get; set; }
        public IList<LinkResortUser> LatestReviews { get; set; }

        public ResortsListViewData()
        {
            SkiAreas = new List<Resort>();
            TopRatedResorts = new List<Resort>();
            MostVisitedResorts = new List<Resort>();
            LatestReviews = new List<LinkResortUser>();
        }
    }
}
