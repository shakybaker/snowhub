using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sporthub.Model;
using Sporthub.Model.Enumerators;
using Sporthub.Services;
using Sporthub.Repository;
using Sporthub.Utils;

using Sporthub.Mvc.ViewData;
using System.Text.RegularExpressions;

namespace Sporthub.Mvc.Controllers
{
    public class AdminResortsController : SporthubController
    {
        private ResortRepository resortRepository = new ResortRepository();
        private ResortService resortService;
        private CountryRepository countryRepository = new CountryRepository();
        private CountryService countryService;
        private ResortLinkRepository resortLinkRepository = new ResortLinkRepository();
        private ResortLinkService resortLinkService;
        private ConfigDataService configDataService;
        static Regex regex = new Regex(@"&#(1?\d\d);");

        public ActionResult List(string location, string letter)
        {
            AdminResortsListViewData viewData = new AdminResortsListViewData();
            resortService = new ResortService(resortRepository);
            viewData.Resorts = resortService.GetAll(letter);

            return View("List", viewData);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Default(string name)
        {
            return View("Default", GetResortViewData(name, null));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddResort()
        {
            configDataService = new ConfigDataService();
            var viewData = new ResortViewData();
            viewData.Resort = new Resort();
            viewData.Continents = configDataService.GetContinents();
            viewData.Countries = configDataService.GetCountries();

            return View("AddResort", viewData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddResort(FormCollection form)
        {
            string encodedName = string.Empty;
            resortService = new ResortService(resortRepository);
            countryService = new CountryService(countryRepository);
            Resort resort = new Resort();
            var id = int.Parse(form["ResortID"]);
            resort = resortService.Get(id);
            resort.Name = form["ResortName"];
            resort.PrettyUrl = form["PrettyUrl"];
            resort.Longitude = double.Parse(form["Longitude"]);
            resort.Latitude = double.Parse(form["Latitude"]);
            resort.ContinentID = (form["Continent"] == "-1") ? 0 : int.Parse(form["Continent"]);
            resort.CountryID = (form["Country"] == "-1") ? 0 : int.Parse(form["Country"]);

            encodedName = EncodeURL(CheckEncodedName(resort.Name));
            var resortCheck = resortService.Get(encodedName);

            if (resortCheck != null)
            {
                var c = countryService.Get(resort.CountryID);
                encodedName = EncodeURL(CheckEncodedName(resort.Name + " " + c.CountryName));
            }
            resortService.Add(resort);

            var feedback = new Feedback(FeedbackType.Success, "Resort Added OK");
            //if (1 == 1)
            //{
            //    AdminResortsListViewData viewDataList = new AdminResortsListViewData();
            //    viewDataList.Resorts = resortService.GetAll();
            //    return View("List", viewDataList);
            //}

            return View("Default", GetResortViewData(resort.PrettyUrl, feedback));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Mountain(string name)
        {
            return View("Mountain", GetResortViewData(name, null));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Lifts(string name)
        {
            return View("Lifts", GetResortViewData(name, null));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Runs(string name)
        {
            return View("Runs", GetResortViewData(name, null));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Parks(string name)
        {
            return View("Parks", GetResortViewData(name, null));
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Links(string name)
        {
            return View("Links", GetResortViewData(name, null));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Edit(string name)
        {
            return Redirect("/admin/resorts/" + name + "/default");
//            return View("Edit", GetResortViewData(name, null));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult EditResortLink(string name, string id)
        {
            return View("EditResortLink", GetResortLinkViewData(name, id));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditResortLink(FormCollection form)
        {
            resortLinkService = new ResortLinkService(resortLinkRepository);

            var resortLink = new ResortLink
            {
                ID = int.Parse(form["ResortLinkID"]),
                Name = form["Name"],
                URL = form["URL"]
            };

            resortLinkService.Update(resortLink);

            var feedback = new Feedback(FeedbackType.Success, "Resort Link Updated OK");

            return View("Links", GetResortViewData(form["ResortPrettyUrl"], feedback));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Default(FormCollection form)
        {
            resortService = new ResortService(resortRepository);
            Resort resort = new Resort();
            var id = int.Parse(form["ResortID"]);
            resort = resortService.Get(id);
            resort.Name = form["ResortName"];
            resort.PrettyUrl = form["PrettyUrl"];
            resort.Longitude = double.Parse(form["Longitude"]);
            resort.Latitude = double.Parse(form["Latitude"]);
            resort.ContinentID = (form["Continent"] == "-1") ? 0 : int.Parse(form["Continent"]);
            resort.CountryID = (form["Country"] == "-1") ? 0 : int.Parse(form["Country"]);
            
            resortService.Update(resort);

            var feedback = new Feedback(FeedbackType.Success, "Resort Updated OK");
            //if (1 == 1)
            //{
            //    AdminResortsListViewData viewDataList = new AdminResortsListViewData();
            //    viewDataList.Resorts = resortService.GetAll();
            //    return View("List", viewDataList);
            //}

            return View("Default", GetResortViewData(resort.PrettyUrl, feedback));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(FormCollection form)
        {
            resortService = new ResortService(resortRepository);
            Resort resort = new Resort();
            var id = int.Parse(form["ResortID"]);
            resort = resortService.Get(id);

            //resort.ResortStats = new ResortStats();
            resort.ResortStats.BaseLevel = form["BaseLevel"];
            resort.ResortStats.TopLevel = form["TopLevel"];
            resort.ResortStats.VerticalDrop = form["VerticalDrop"];
            resort.ResortStats.Height = form["Height"];
            resort.ResortStats.AverageSnowfall = form["AverageSnowfall"];
            resort.ResortStats.HasSnowmaking = form["HasSnowmaking"];
            resort.ResortStats.SnowmakingCoverage = form["SnowmakingCoverage"];
            resort.ResortStats.PreSeasonStartMonth = form["PreSeasonStartMonth"];
            resort.ResortStats.SeasonStartMonth = form["SeasonStartMonth"];
            resort.ResortStats.SeasonEndMonth = form["SeasonEndMonth"];
            resort.ResortStats.Population = form["Population"];
            resort.ResortStats.MountainRestaurants = form["MountainRestaurants"];
            resort.ResortStats.LiftDescription = form["LiftDescription"];
            resort.ResortStats.LiftTotal = form["LiftTotal"];
            resort.ResortStats.LiftCapacityHour = form["LiftCapacityHour"];
            resort.ResortStats.QuadPlusCount = form["QuadPlusCount"];
            resort.ResortStats.QuadCount = form["QuadCount"];
            resort.ResortStats.TripleCount = form["TripleCount"];
            resort.ResortStats.DoubleCount = form["DoubleCount"];
            resort.ResortStats.SingleCount = form["SingleCount"];
            resort.ResortStats.SurfaceCount = form["SurfaceCount"];
            resort.ResortStats.CableLiftCount = form["CableLiftCount"];
            resort.ResortStats.GondolaCount = form["GondolaCount"];
            resort.ResortStats.FunicularCount = form["FunicularCount"];
            resort.ResortStats.SurfaceTrainCount = form["SurfaceTrainCount"];
            resort.ResortStats.RunTotal = form["RunTotal"];
            resort.ResortStats.BlackRuns = form["BlackRuns"];
            resort.ResortStats.RedRuns = form["RedRuns"];
            resort.ResortStats.BlueRuns = form["BlueRuns"];
            resort.ResortStats.GreenRuns = form["GreenRuns"];
            resort.ResortStats.LongestRunDistance = form["LongestRunDistance"];
            resort.ResortStats.SnowparkTotal = form["SnowparkTotal"];
            resort.ResortStats.SnowparkDescription = form["SnowparkDescription"];
            resort.ResortStats.HalfpipeTotal = form["HalfpipeTotal"];
            resort.ResortStats.HalfpipeDescription = form["HalfpipeDescription"];
            resort.ResortStats.QuarterpipeTotal = form["QuarterpipeTotal"];
            resort.ResortStats.QuarterpipeDescription = form["QuarterpipeDescription"];
            resort.ResortStats.SkiableTerrianSize = form["SkiableTerrianSize"];
            resort.ResortStats.HasNightskiing = form["HasNightskiing"];
            resort.ResortStats.NightskiingDescription = form["NightskiingDescription"];
            resort.ResortStats.HasSummerskiing = form["HasSummerskiing"];
            resort.ResortStats.SummerskiingDescription = form["SummerskiingDescription"];
            resort.ResortStats.SummerStartMonth = form["SummerStartMonth"];
            resort.ResortStats.SummerEndMonth = form["SummerEndMonth"];

            resort.ResortStats.Snowfall1Jan = form["Snowfall1Jan"];
            resort.ResortStats.Snowfall2Feb = form["Snowfall2Feb"];
            resort.ResortStats.Snowfall3Mar = form["Snowfall3Mar"];
            resort.ResortStats.Snowfall4Apr = form["Snowfall4Apr"];
            resort.ResortStats.Snowfall5May = form["Snowfall5May"];
            resort.ResortStats.Snowfall6Jun = form["Snowfall6Jun"];
            resort.ResortStats.Snowfall7Jul = form["Snowfall7Jul"];
            resort.ResortStats.Snowfall8Aug = form["Snowfall8Aug"];
            resort.ResortStats.Snowfall9Sep = form["Snowfall9Sep"];
            resort.ResortStats.Snowfall10Oct = form["Snowfall10Oct"];
            resort.ResortStats.Snowfall11Nov = form["Snowfall11Nov"];
            resort.ResortStats.Snowfall12Dec = form["Snowfall12Dec"];



            resortService.Update(resort);

            var feedback = new Feedback(FeedbackType.Success, "Resort Updated OK");
            //if (1 == 1)
            //{
            //    AdminResortsListViewData viewDataList = new AdminResortsListViewData();
            //    viewDataList.Resorts = resortService.GetAll();
            //    return View("List", viewDataList);
            //}

            return View("Edit", GetResortViewData(resort.PrettyUrl, feedback));
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Add()
        {
            return View("Edit", GetResortViewData(string.Empty, null));
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult Add(FormCollection form)
        //{
        //    resortService = new ResortService(resortRepository);
        //    Resort resort = new Resort();
        //    resort.Name = form["ResortName"];
        //    resort.PrettyUrl = form["PrettyUrl"];
        //    resort.Longitude = double.Parse(form["Longitude"]);
        //    resort.Latitude = double.Parse(form["Latitude"]);
        //    resort.ContinentID = (form["Continent"] == "-1") ? 0 : int.Parse(form["Continent"]);
        //    resort.CountryID = (form["Country"] == "-1") ? 0 : int.Parse(form["Country"]);

        //    resortService.Update(resort);

        //    var feedback = new Feedback(FeedbackType.Success, "Resort Updated OK");
        //    //if (1 == 1)
        //    //{
        //    //    AdminResortsListViewData viewDataList = new AdminResortsListViewData();
        //    //    viewDataList.Resorts = resortService.GetAll();
        //    //    return View("List", viewDataList);
        //    //}

        //    return View("Edit", GetResortViewData(resort.PrettyUrl, feedback));
        //}

        private ResortViewData GetResortViewData(string name, Feedback feedback)
        {
            ResortViewData viewDataEdit = new ResortViewData();
            resortService = new ResortService(resortRepository);
            configDataService = new ConfigDataService();

            viewDataEdit.Feedback = feedback;
            if (string.IsNullOrEmpty(name))
            {
                viewDataEdit.Resort = new Resort();
            }
            else
            {
                viewDataEdit.Resort = resortService.Get(name);
            }
            viewDataEdit.Continents = configDataService.GetContinents();
            viewDataEdit.Countries = configDataService.GetCountriesByContinent(viewDataEdit.Resort.ContinentID);
            viewDataEdit.Regions = configDataService.GetRegionsByCountry(viewDataEdit.Resort.CountryID);
            viewDataEdit.HasPicture = (System.IO.File.Exists(string.Format("/content/resorts/{0}.png", viewDataEdit.Resort.PrettyUrl))) ? true : false;

            return viewDataEdit;
        }

        private ResortLinkViewData GetResortLinkViewData(string name, string id)
        {
            ResortLinkViewData viewDataEdit = new ResortLinkViewData();
            resortLinkService = new ResortLinkService(resortLinkRepository);
            viewDataEdit.ResortPrettyUrl = name;
            viewDataEdit.ResortLink = resortLinkService.Get(int.Parse(id));

            return viewDataEdit;
        }

        public string EncodeURL(string url)
        {
            Regex rgex = new Regex("&");
            url = rgex.Replace(url, "and");

            rgex = new Regex("[\\W\\.]");
            url = rgex.Replace(url, "-");

            rgex = new Regex("-+");
            url = rgex.Replace(url, "-");

            rgex = new Regex(":");
            url = rgex.Replace(url, "");

            return url.ToLower();
        }

        private static string CheckEncodedName(string name)
        {
            foreach (Match m in regex.Matches(name))
            {
                string i = m.Value.Substring(2, m.Value.Length - 3);

                System.Text.Encoding cp1252 = System.Text.Encoding.GetEncoding(1252);

                string s = cp1252.GetString(new byte[] { Convert.ToByte(i, 10) });

                name = name.Replace(m.Value, s);

            }

            // Perform a final conversion to remove HTML encoded values
            return HttpUtility.HtmlDecode(name);

        }
    }
}
