using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sporthub.Mvc.ViewData;
using Sporthub.Model;
using Sporthub.Repository;
using Sporthub.Services;
using Sporthub.Utilities;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using Sporthub.Data;

namespace Sporthub.Mvc.Controllers
{
    [HandleError]
    public class AirportsController : SporthubController
    {
        private readonly AirportRepository _airportRepository = new AirportRepository();
        private AirportService _airportService;

        public ActionResult Index()
        {
            _airportService = new AirportService(_airportRepository);

            var viewData = new AirportsLandingViewData
            {
                Breadcrumbs = new List<Breadcrumb>
                {
                    NewBreadcrumb("Home", "/"),
                    NewBreadcrumb("Airports", "/airports"),
                    NewBreadcrumb("Europe", "/airports/europe")
                },
                Airports = _airportService.GetAll()
            };

            return View("Index", viewData);
        }

        public ActionResult View(string name)
        {
            _airportService = new AirportService(_airportRepository);

            var airport = _airportService.Get(name);
            airport.NearbyResorts = NearestResortsSort.SortNearbyResortsByDistance(airport.NearbyResorts, airport.Latitude, airport.Longitude, 0);



            var viewData = new AirportViewViewData
            {
                Airport = airport,
                Breadcrumbs = new List<Breadcrumb>
                {
                    NewBreadcrumb("Home", "/"),
                    NewBreadcrumb("Airports", "/airports"),
                    NewBreadcrumb("Europe", "/airports"),
                    NewBreadcrumb(airport.Name, string.Format("/airports/{0}", airport.PrettyUrl))
                }
            };

            return View("View", viewData);
        }
    }
}
