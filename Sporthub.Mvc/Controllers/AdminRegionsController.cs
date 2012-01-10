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
    public class AdminRegionsController : SporthubController
    {
        /// <summary>
        /// Show all regions within a region
        /// </summary>
        /// <param name="id">Region id</param>
        /// <returns></returns>
        public ActionResult List(string id)
        {
            var regions = RegionDataManager.GetRegionsByCountryAndLevel(int.Parse(id), 1); //TODO: validate

            AdminRegionsListViewData viewData = new AdminRegionsListViewData();
            viewData.Regions = regions;

            return View("List", viewData);
        }

        /// <summary>
        /// Show all top-level regions within a country
        /// </summary>
        /// <param name="id">Country id</param>
        /// <returns></returns>
        public ActionResult CountryList(string id)
        {
            var regions = RegionDataManager.GetRegionsByCountryAndLevel(int.Parse(id), 1); //TODO: validate

            AdminRegionsListViewData viewData = new AdminRegionsListViewData();
            viewData.Regions = regions;

            return View("List", viewData);
        }
    }
}
