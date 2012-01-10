using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class AirportsLandingViewData : SporthubViewData
    {
        public List<Breadcrumb> Breadcrumbs { get; set; }
        public IList<Airport> Airports { get; set; }

        public AirportsLandingViewData()
        {
            Airports = new List<Airport>();
        }
    }
}
