using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sporthub.Data;
using Sporthub.Model;
using Sporthub.Model.Enumerators;
using Sporthub.Services;
using Sporthub.Repository;
using Sporthub.Utils;

using Sporthub.Mvc.ViewData;


namespace Sporthub.Mvc.Controllers
{
    public class AdminLocationsController : SporthubController
    {
        public ActionResult List(string location, string name)
        {
            List<Location> locations = new List<Location>();
            switch (location.ToLower())
            {
                case "world":
                    var continents = LocationDataManager.GetContinents();
                    foreach (Continent continent in continents)
                    {
                        Location l = new Location();
                        l.LocationType = "continents";
                        l.Code = continent.Code;
                        l.ID = continent.ID;
                        l.Latitude = continent.Latitude;
                        l.Longitude = continent.Longitude;
                        l.Name = continent.ContinentName;
                        l.PrettyUrl = continent.PrettyUrl;
                        locations.Add(l);
                    }
                    break;
                case "continents":
                    var countries = LocationDataManager.GetCountries(name);
                    foreach (Country country in countries)
                    {
                        Location l = new Location();
                        l.LocationType = "countries";
                        l.Code = country.ISO3166Alpha2;
                        l.ID = country.ID;
                        l.Latitude = country.Latitude.ToString();
                        l.Longitude = country.Longitude.ToString();
                        l.Name = country.CountryName;
                        l.PrettyUrl = country.PrettyUrl;
                        locations.Add(l);
                    }
                    break;
                default:
                    break;
            }
            AdminLocationsListViewData viewData = new AdminLocationsListViewData();
            viewData.Locations = locations;

            return View("List", viewData);
        }
    }
}
