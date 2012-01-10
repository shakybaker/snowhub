using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

using Sporthub.Data;
using Sporthub.Model;
using Sporthub.Model.Enumerators;
using Sporthub.Services;
using Sporthub.Repository;
using Sporthub.Mvc.ViewData;
using System.Data;
using Sporthub.Utilities;

namespace Sporthub.Mvc.Controllers
{
    public class ResortsController : SporthubController
    {
        private readonly ResortRepository _resortRepository = new ResortRepository();
        private ResortService _resortService;
        private readonly CountryRepository _countryRepository = new CountryRepository();
        private CountryService _countryService;
        private readonly CheckInRepository _checkInRepository = new CheckInRepository();
        private CheckInService _checkInService;
        private readonly UserRepository _userRepository = new UserRepository();
        private UserService _userService;
        private readonly LinkResortUserRepository _linkResortUserRepository = new LinkResortUserRepository();
        private LinkResortUserService _linkResortUserService;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List(string location)
        {
            List<Continent> continents = null;
            List<Country> countries = null;
            List<Region> regions = null;
            List<Resort> resorts = null;
            Continent continent = null;
            Country country = null;
            Region region = null;

            var viewData = new ResortsListViewData();
            _resortService = new ResortService(_resortRepository);
            _countryService = new CountryService(_countryRepository);
            _linkResortUserService = new LinkResortUserService(_linkResortUserRepository);
            var skiAreas = _resortService.GetAllSkiAreas();

            //first get location type
            var level = (string.IsNullOrEmpty(location)) ? LocationLevel.World : LocationDataManager.GetLocationLevel(location);
            switch (level)
            {
                case LocationLevel.Continent:
                    continent = LocationDataManager.GetContinentByName(location);
                    viewData.LocationName = continent.ContinentName;
                    viewData.LocationUrl = continent.PrettyUrl;
                    var ds = LocationDataManager.GetClusteredResortsForContinent(location.ToLower().Replace(" ", "-").Replace("/", "-"));
                    continents = LocationDataManager.GetContinents();

                    countries = new List<Country>();
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        country = new Country
                        {
                            CountryName = dr["CountryName"].ToString(),
                            PrettyUrl = dr["PrettyUrl"].ToString(),
                            ISO3166Alpha2 = dr["ISO3166Alpha2"].ToString(),
                            Longitude = double.Parse(dr["Longitude"].ToString()),
                            Latitude = double.Parse(dr["Latitude"].ToString())
                        };
                        //country.ID = int.Parse(dr["ID"].ToString());
                        viewData.TopRatedResorts = _resortService.GetTopRatedResortsByContinent(continent.ID);
                        viewData.MostVisitedResorts = _resortService.GetMostVisitedResortsByContinent(continent.ID);
                        viewData.LatestReviews = _linkResortUserService.GetLatestReviewsForContinent(continent.ID, 5);
                        foreach (var skiArea in
                            skiAreas.Where(skiArea => skiArea.SkiAreas.Count(x => x.Resort.ContinentID == continent.ID) > 0))
                        {
                            viewData.SkiAreas.Add(skiArea);
                        }
                        countries.Add(country);
                    }
                    break;
                case LocationLevel.Country:
                    country = LocationDataManager.GetCountryByName(location);
                    continent = country.Continent;
                    //regions = RegionDataManager.GetRegionsByCountryNameAndLevel(location, 1);//TODO: sort regions later
                    resorts = ResortDataManager.GetResortsByCountry(location);
                    countries = _countryService.GetAllByContinentID(continent.ID);
                    viewData.LocationName = country.CountryName;
                    viewData.LocationUrl = country.PrettyUrl;
                    viewData.TopRatedResorts = _resortService.GetTopRatedResortsByCountry(country.ID);
                    viewData.MostVisitedResorts = _resortService.GetMostVisitedResortsByCountry(country.ID);
                    viewData.LatestReviews = _linkResortUserService.GetLatestReviewsForCountry(country.ID, ((level == LocationLevel.Country) ? 5 : 10));
                    foreach (var skiArea in
                        skiAreas.Where(skiArea => skiArea.SkiAreas.Count(x => x.Resort.CountryID == country.ID) > 0))
                    {
                        viewData.SkiAreas.Add(skiArea);
                    }

                    break;
                default:
                    viewData.LocationName = "World Resorts";
                    var ds2 = LocationDataManager.GetClusteredResortsForWorld();
                    continents = new List<Continent>();
                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        continent = new Continent
                        {
                            ContinentName = dr["ContinentName"].ToString(),
                            PrettyUrl = dr["PrettyUrl"].ToString(),
                            Code = dr["Code"].ToString(),
                            Longitude = dr["Longitude"].ToString(),
                            Latitude = dr["Latitude"].ToString()
                        };
                        //country.ID = int.Parse(dr["ID"].ToString());
                        continents.Add(continent);
                    }
                    break;
            }

            viewData.LocationLevel = level;
            viewData.Continents = continents ?? new List<Continent>();
            viewData.Countries = countries ?? new List<Country>();
            viewData.Regions = (regions != null) ? regions : new List<Region>();
            viewData.Resorts = resorts ?? new List<Resort>();
            viewData.Continent = continent ?? new Continent();
            viewData.Country = country ?? new Country();
            viewData.Region = (region != null) ? region : new Region();

            var breadcrumbs = new List<Breadcrumb> {NewBreadcrumb("Home", "/"), NewBreadcrumb("Resorts", "/resorts")};
            if ((level == LocationLevel.Continent) || (level == LocationLevel.Country) || (level == LocationLevel.Region))
            {
                breadcrumbs.Add(NewBreadcrumb(continent.ContinentName, string.Format("/resorts/{0}/list/", continent.PrettyUrl)));
            }
            if ((level == LocationLevel.Country) || (level == LocationLevel.Region))
            {
                breadcrumbs.Add(NewBreadcrumb(country.CountryName, string.Format("/resorts/{0}/list/", country.PrettyUrl)));
            }
            viewData.Breadcrumbs = breadcrumbs;
            ////////if world level then just return continents
            ////////TODO: add mountain ranges, top rated, featured, most popular etc
            //////if (level == LocationLevel.World)
            //////{
            //////    continents = LocationDataManager.GetContinents();
            //////}
            //////else
            //////{
            //////    switch (level)
            //////    {
            //////        case LocationLevel.Continent:
            //////            //get all countries in a continent
            //////            countries = LocationDataManager.GetCountries(location);
            //////            //resorts = ResortDataManager.GetResortsByContinent(location);
            //////            break;
            //////        case LocationLevel.Country:
            //////            regions = RegionDataManager.GetRegionsByCountryNameAndLevel(location, 1);
            //////            //resorts = ResortDataManager.GetResortsByCountry(location);
            //////            break;
            //////        default:
            //////            //TODO: return default values
            //////            break;
            //////    }
            //////}

            switch (level)
            {
                case LocationLevel.World:
                case LocationLevel.Continent:
                    return View("ListContinent", viewData);
                case LocationLevel.Country:
                    return View("ListCountry", viewData);
                default:
                    return View("ListContinent", viewData);
            }
        }

        public ActionResult ListWorld()
        {
            List<Continent> continents = null;
            List<Country> countries = null;
            List<Region> regions = null;
            List<Resort> resorts = null;
            Continent continent = null;
            Country country = null;
            Region region = null;

            var viewData = new ResortsListViewData {LocationName = "World Resorts", LocationUrl = "World"};

            var ds2 = LocationDataManager.GetClusteredResortsForWorld();
            continents = new List<Continent>();
            countries = new List<Country>();
            foreach (DataRow dr in ds2.Tables[0].Rows)
            {
                continent = new Continent
                {
                    ContinentName = dr["ContinentName"].ToString(),
                    PrettyUrl = dr["PrettyUrl"].ToString(),
                    ID = int.Parse(dr["ID"].ToString()),
                    Code = dr["Code"].ToString(),
                    Longitude = dr["Longitude"].ToString(),
                    Latitude = dr["Latitude"].ToString()
                };
                continents.Add(continent);
                var ds = LocationDataManager.GetClusteredResortsForContinent(continent.PrettyUrl);
                foreach (DataRow dr2 in ds.Tables[0].Rows)
                {
                    country = new Country
                    {
                        CountryName = dr2["CountryName"].ToString(),
                        PrettyUrl = dr2["PrettyUrl"].ToString(),
                        ID = int.Parse(dr["ID"].ToString()),
                        ContinentID = continent.ID,
                        ISO3166Alpha2 = dr2["ISO3166Alpha2"].ToString(),
                        Longitude = double.Parse(dr2["Longitude"].ToString()),
                        Latitude = double.Parse(dr2["Latitude"].ToString())
                    };
                    countries.Add(country);
                }
            }

            viewData.LocationLevel = LocationLevel.Continent;
            viewData.Continents = (continents != null) ? continents : new List<Continent>();
            viewData.Countries = (countries != null) ? countries : new List<Country>();
            viewData.Regions = (regions != null) ? regions : new List<Region>();
            viewData.Resorts = (resorts != null) ? resorts : new List<Resort>();
            viewData.Continent = continent ?? new Continent();
            viewData.Country = country ?? new Country();
            viewData.Region = (region != null) ? region : new Region();

            var breadcrumbs = new List<Breadcrumb> {NewBreadcrumb("Home", "/"), NewBreadcrumb("Resorts", "/resorts")};
            viewData.Breadcrumbs = breadcrumbs;
            
            _resortService = new ResortService(_resortRepository);
            _linkResortUserService = new LinkResortUserService(_linkResortUserRepository);

            viewData.TopRatedResorts = _resortService.GetTopRatedResorts();
            viewData.MostVisitedResorts = _resortService.GetMostVisitedResorts();
            viewData.LatestReviews = _linkResortUserService.GetLatestReviews(5);

            return View("ListWorld", viewData);
        }

        public Breadcrumb NewBreadcrumb(string name, string url)
        {
            return new Breadcrumb {Name = name, Url = url};
        }

        public ActionResult Overview(string name)
        {


            var viewData = GetResortViewData(name);
            GetGooglePlaces(viewData.Resort.Longitude, viewData.Resort.Latitude);
            var country = LocationDataManager.GetCountryByID(viewData.Resort.CountryID.ToString());
            var continent = country.Continent;
            viewData.Breadcrumbs = new List<Breadcrumb>
            {
                NewBreadcrumb("Home", "/"),
                NewBreadcrumb("Resorts", "/resorts"),
                NewBreadcrumb(continent.ContinentName, string.Format("/resorts/{0}/list/", continent.PrettyUrl)),
                NewBreadcrumb(country.CountryName, string.Format("/resorts/{0}/list/", country.PrettyUrl)),
                NewBreadcrumb(string.Format("{0}{1}", viewData.Resort.Name, (viewData.Resort.IsSkiArea) ? " Ski Area" : string.Empty), string.Format("/resorts/{0}", viewData.Resort.PrettyUrl))
            };

            if (viewData.Resort.IsSkiArea)
            {
                _resortService = new ResortService(_resortRepository);
                var skiArea = _resortService.GetSkiArea(viewData.Resort.ID);
                viewData.Resort.SkiAreas = skiArea.SkiAreas;
                double? maxlat = 0;
                double? maxlng = 0;
                double? minlat = 0;
                double? minlng = 0;
                var first = true;
                foreach (var lrsa in viewData.Resort.SkiAreas)
                {
                    if ((lrsa.Resort.Latitude > maxlat) || (first))
                        maxlat = lrsa.Resort.Latitude;
                    if ((lrsa.Resort.Latitude < minlat) || (first))
                        minlat = lrsa.Resort.Latitude;
                    if ((lrsa.Resort.Longitude > maxlng) || (first))
                        maxlng = lrsa.Resort.Longitude;
                    if ((lrsa.Resort.Longitude < minlng) || (first))
                        minlng = lrsa.Resort.Longitude;
                    first = false;
                }
                viewData.Resort.Longitude = ((maxlng - minlng) / 2) + minlng;
                viewData.Resort.Latitude = ((maxlat - minlat) / 2) + minlat;

                return View("SkiArea", viewData);
            }

            return View("Overview", viewData);
        }

        private void GetGooglePlaces(double? lng, double? lat)
        {
            string postData = "parameter=text&param2=text2";
            ASCIIEncoding encoding = new ASCIIEncoding( );
            byte[] baASCIIPostData = encoding.GetBytes( postData );
            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create("https://maps.googleapis.com/maps/api/place/search/json?location=" + (lng ?? 0) + "," + (lat ?? 0) + "&radius=500&types=food&name=harbour&sensor=false&key=AIzaSyB1BdZ2CW1PJSpbsA4hBGmMLAhEO0tsVFc");
            HttpWReq.Method = "POST";
            HttpWReq.Accept = "text/plain";
            HttpWReq.ContentType = "application/x-www-form-urlencoded";
            HttpWReq.ContentLength = baASCIIPostData.Length;
// Prepare web request and send the data.
            Stream streamReq = HttpWReq.GetRequestStream( );
            streamReq.Write( baASCIIPostData, 0, baASCIIPostData.Length );
// grab the response
            HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse( );
            Stream streamResponse = HttpWResp.GetResponseStream( );
// And read it out
            StreamReader reader = new StreamReader( streamResponse );
            string response = reader.ReadToEnd( );


        }

        private ResortViewData GetResortViewData(string name)
        {
            var viewData = new ResortViewData();
            _resortService = new ResortService(_resortRepository);
            _userService = new UserService(_userRepository);

            viewData.Resort = _resortService.Get(name);
            viewData.Resort.NearbyResorts = NearestResortsSort.SortNearbyResortsByDistance(viewData.Resort.NearbyResorts, viewData.Resort.Latitude, viewData.Resort.Longitude, 10);
            viewData.Resort.NearbyAirports = NearestResortsSort.SortNearbyAirportsByDistance(viewData.Resort.NearbyAirports, viewData.Resort.Latitude, viewData.Resort.Longitude, 10);
            viewData.FavouriteCount = viewData.Resort.FavedCount;
            viewData.VisitedCount = viewData.Resort.VisitedCount;
            viewData.ReviewCount = viewData.Resort.LinkResortUsers.Where(item => item.Score>0).Count();
            _linkResortUserService = new LinkResortUserService(_linkResortUserRepository);
            viewData.LatestReviews = _linkResortUserService.GetLatestReviewsForResort(viewData.Resort.ID, 5);

            viewData.FavedBy = _userService.GetUsersThatFavedResort(viewData.Resort.ID, 6);
            viewData.VisitedBy = _userService.GetUsersThatVisitedResort(viewData.Resort.ID, 6);
            //viewData.ReviewedBy = userService.GetUsersThatReviewedResort(viewData.Resort.ID, 4);
            //viewData.FavedBy = new List<User>();
            //viewData.VisitedBy = new List<User>();
            viewData.ReviewedBy = new List<User>();

            viewData.Ratings = GetRatings(viewData.Resort);
            viewData.HasPicture = (System.IO.File.Exists(Server.MapPath((string.Format("/Content/Resorts/{0}.png", viewData.Resort.PrettyUrl))))) ? true : false;

            return viewData;
        }

        //TODO: refactor
        private static List<RatingItem> GetRatings(Resort resort)
        {
            var ratings = new List<RatingItem>();
            var score = from lru in resort.LinkResortUsers
                        select lru.LiftRating;
            var avg = 0;
            if (score.Count() > 0)
                avg = (score.Sum()>0) ? (int)(score.Sum() / score.Count()) : 0;
            ratings.Add(AddRating("Lift System", avg, score.Count()));

            score = from lru in resort.LinkResortUsers
                    select lru.SnowRating;
            avg = 0;
            if (score.Count() > 0)
                avg = (score.Sum() > 0) ? (int)(score.Sum() / score.Count()) : 0;
            ratings.Add(AddRating("Snow", avg, score.Count()));

            score = from lru in resort.LinkResortUsers
                    select lru.QueueRating;
            avg = 0;
            if (score.Count() > 0)
                avg = (score.Sum() > 0) ? (int)(score.Sum() / score.Count()) : 0;
            ratings.Add(AddRating("Queueing", avg, score.Count()));

            score = from lru in resort.LinkResortUsers
                    select lru.SceneryRating;
            avg = 0;
            if (score.Count() > 0)
                avg = (score.Sum() > 0) ? (int)(score.Sum() / score.Count()) : 0;
            ratings.Add(AddRating("Scenery", avg, score.Count()));

            score = from lru in resort.LinkResortUsers
                    select lru.ConvenienceRating;
            avg = 0;
            if (score.Count() > 0)
                avg = (score.Sum() > 0) ? (int)(score.Sum() / score.Count()) : 0;
            ratings.Add(AddRating("Convenience", avg, score.Count()));

            score = from lru in resort.LinkResortUsers
                    select lru.AccomodationRating;
            avg = 0;
            if (score.Count() > 0)
                avg = (score.Sum() > 0) ? (int)(score.Sum() / score.Count()) : 0;
            ratings.Add(AddRating("Accomodation", avg, score.Count()));

            score = from lru in resort.LinkResortUsers
                    select lru.FoodRating;
            avg = 0;
            if (score.Count() > 0)
                avg = (score.Sum() > 0) ? (int)(score.Sum() / score.Count()) : 0;
            ratings.Add(AddRating("Food", avg, score.Count()));

            score = from lru in resort.LinkResortUsers
                    select lru.FacilitiesRating;
            avg = 0;
            if (score.Count() > 0)
                avg = (score.Sum() > 0) ? (int)(score.Sum() / score.Count()) : 0;
            ratings.Add(AddRating("Facilities", avg, score.Count()));

            score = from lru in resort.LinkResortUsers
                    select lru.NightlifeRating;
            avg = 0;
            if (score.Count() > 0)
                avg = (score.Sum() > 0) ? (int)(score.Sum() / score.Count()) : 0;
            ratings.Add(AddRating("Nightlife", avg, score.Count()));

            return ratings;
        }

        private static RatingItem AddRating(string name, int score, int scoreCount)
        {
            return new RatingItem {Name = name, Score = score, ScoreCount = scoreCount};
        }

        public ActionResult Map(string name, string m)
        {
            var viewData = new ResortViewData {Resort = ResortDataManager.GetResortByName(name)};
            var country = LocationDataManager.GetCountryByID(viewData.Resort.CountryID.ToString());
            var continent = country.Continent;
            viewData.Querystring = m;

            var breadcrumbs = new List<Breadcrumb>
            {
                NewBreadcrumb("Home", "/"),
                NewBreadcrumb("Resorts", "/resorts"),
                NewBreadcrumb(continent.ContinentName,
                        string.Format("/resorts/{0}/list/", continent.PrettyUrl)),
                NewBreadcrumb(country.CountryName,
                        string.Format("/resorts/{0}/list/", country.PrettyUrl)),
                NewBreadcrumb(viewData.Resort.Name,
                        string.Format("/resorts/{0}", viewData.Resort.PrettyUrl)),
                NewBreadcrumb("Map", string.Format("/resorts/{0}/map", viewData.Resort.PrettyUrl))
            };
            viewData.Breadcrumbs = breadcrumbs;
            //viewData.HasPicture = (System.IO.File.Exists(Server.MapPath((string.Format("/Content/Resorts/{0}.png", viewData.Resort.PrettyUrl))))) ? true : false;

            return View("Map", viewData);
        }

        public ActionResult Photos(string name)
        {
            var viewData = GetResortViewData(name);

            var country = LocationDataManager.GetCountryByID(viewData.Resort.CountryID.ToString());
            var continent = country.Continent;

            var breadcrumbs = new List<Breadcrumb>
            {
                NewBreadcrumb("Home", "/"),
                NewBreadcrumb("Resorts", "/resorts"),
                NewBreadcrumb(continent.ContinentName,
                            string.Format("/resorts/{0}/list/", continent.PrettyUrl)),
                NewBreadcrumb(country.CountryName,
                            string.Format("/resorts/{0}/list/", country.PrettyUrl)),
                NewBreadcrumb(viewData.Resort.Name,
                            string.Format("/resorts/{0}", viewData.Resort.PrettyUrl)),
                NewBreadcrumb("Photos",
                            string.Format("/resorts/{0}/photos", viewData.Resort.PrettyUrl))
            };
            viewData.Breadcrumbs = breadcrumbs;

            return View("Photos", viewData);
        }

        public ActionResult Places(string name)
        {
            var viewData = GetResortViewData(name);

            var country = LocationDataManager.GetCountryByID(viewData.Resort.CountryID.ToString());
            var continent = country.Continent;

            var breadcrumbs = new List<Breadcrumb>
            {
                NewBreadcrumb("Home", "/"),
                NewBreadcrumb("Resorts", "/resorts"),
                NewBreadcrumb(continent.ContinentName,
                            string.Format("/resorts/{0}/list/", continent.PrettyUrl)),
                NewBreadcrumb(country.CountryName,
                            string.Format("/resorts/{0}/list/", country.PrettyUrl)),
                NewBreadcrumb(viewData.Resort.Name,
                            string.Format("/resorts/{0}", viewData.Resort.PrettyUrl)),
                NewBreadcrumb("Places",
                            string.Format("/resorts/{0}/places", viewData.Resort.PrettyUrl))
            };
            viewData.Breadcrumbs = breadcrumbs;

            return View("Places", viewData);
        }

        //single review
        public ActionResult Reviews(string name, string id)
        {
            var viewData = GetResortViewData(name);
            if (!string.IsNullOrEmpty(id))
                viewData.IsSingleReview = true;

            _linkResortUserService = new LinkResortUserService(_linkResortUserRepository);

            var country = LocationDataManager.GetCountryByID(viewData.Resort.CountryID.ToString());
            var continent = country.Continent;

            if (viewData.IsSingleReview)
            {
                var review = _linkResortUserService.Get(int.Parse(id));
                viewData.Reviews = new List<LinkResortUser> {review};
            }
            else
            {
                viewData.Reviews = _linkResortUserService.GetLatestReviewsForResort(viewData.Resort.ID);
            }

            var breadcrumbs = new List<Breadcrumb>
            {
                NewBreadcrumb("Home", "/"),
                NewBreadcrumb("Resorts", "/resorts"),
                NewBreadcrumb(continent.ContinentName, string.Format("/resorts/{0}/list/", continent.PrettyUrl)),
                NewBreadcrumb(country.CountryName, string.Format("/resorts/{0}/list/", country.PrettyUrl)),
                NewBreadcrumb(viewData.Resort.Name, string.Format("/resorts/{0}", viewData.Resort.PrettyUrl)),
                NewBreadcrumb("Reviews", string.Format("/resorts/{0}/reviews", viewData.Resort.PrettyUrl))
            };

            if (viewData.IsSingleReview)
                breadcrumbs.Add(NewBreadcrumb("Member Review", string.Format("/resorts/{0}/reviews", viewData.Resort.PrettyUrl)));

            viewData.Breadcrumbs = breadcrumbs;

            return View("Reviews", viewData);
        }

        public ActionResult Videos(string name)
        {
            var viewData = GetResortViewData(name);

            var country = LocationDataManager.GetCountryByID(viewData.Resort.CountryID.ToString());
            var continent = country.Continent;

            var breadcrumbs = new List<Breadcrumb>
            {
                NewBreadcrumb("Home", "/"),
                NewBreadcrumb("Resorts", "/resorts"),
                NewBreadcrumb(continent.ContinentName,
                            string.Format("/resorts/{0}/list/", continent.PrettyUrl)),
                NewBreadcrumb(country.CountryName,
                            string.Format("/resorts/{0}/list/", country.PrettyUrl)),
                NewBreadcrumb(viewData.Resort.Name,
                            string.Format("/resorts/{0}", viewData.Resort.PrettyUrl)),
                NewBreadcrumb("Videos",
                            string.Format("/resorts/{0}/videos", viewData.Resort.PrettyUrl))
            };
            viewData.Breadcrumbs = breadcrumbs;

            return View("Videos", viewData);
        }

        public ActionResult Webcams(string name)
        {
            var viewData = GetResortViewData(name);

            var country = LocationDataManager.GetCountryByID(viewData.Resort.CountryID.ToString());
            var continent = country.Continent;

            var breadcrumbs = new List<Breadcrumb>
            {
                NewBreadcrumb("Home", "/"),
                NewBreadcrumb("Resorts", "/resorts"),
                NewBreadcrumb(continent.ContinentName,
                            string.Format("/resorts/{0}/list/", continent.PrettyUrl)),
                NewBreadcrumb(country.CountryName,
                            string.Format("/resorts/{0}/list/", country.PrettyUrl)),
                NewBreadcrumb(viewData.Resort.Name,
                            string.Format("/resorts/{0}", viewData.Resort.PrettyUrl)),
                NewBreadcrumb("Webcams",
                            string.Format("/resorts/{0}/webcams", viewData.Resort.PrettyUrl))
            };
            viewData.Breadcrumbs = breadcrumbs;

            return View("Webcams", viewData);
        }

        public ActionResult CheckIn(string name, string ReturnUrl)
        {
            if (!UserContext.UserIsLoggedIn())
            {
                if (SessionContext.CurrentSession == null)
                {
                    SessionContext.CurrentSession = new Session();
                }
                return RedirectToAction("Index", "home");
            }

            return View("CheckIn", GetCheckInViewData(name, ReturnUrl));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult CheckIn(FormCollection form)
        {
            _checkInService = new CheckInService(_checkInRepository);
            var viewData = GetCheckInViewData(form["hidPrettyUrl"], form["hidReturnUrl"]);

            //set all check-ins to 'inactive' for this user
            _checkInService.UpdateAllToInactiveForUser(UserContext.CurrentUser.ID);

            //check the user in
            var checkIn = new CheckIn
            {
                CheckinDate = DateTime.Now,
                IPAddress = string.Empty,
                IsActive = true,
                ResortID = viewData.Resort.ID,
                UserID = UserContext.CurrentUser.ID,
                Status = string.Empty,
                Longitude = form["myLng"],
                Latitude = form["myLat"]
            };
            var newId = _checkInService.Add(checkIn);

            if (newId > 0)
            {
                //publish to Facebook
                //bool isPublish = viewData.HasFacebookPublishPermission;
                //if (isPublish)
                //{
                //    isPublish = (form["publish"] == "1") ? true : false;
                //}

                //if (isPublish)
                //{
                //    facebook.API api = new facebook.API()
                //    {
                //        ApplicationKey = FacebookConnectAuthentication.ApiKey,
                //        Secret = FacebookConnectAuthentication.SecretKey,
                //        SessionKey = FacebookConnectAuthentication.SessionKey
                //    };

                //    string name = string.Empty;
                //    string url = string.Empty;
                //    string defaultMediaUrl = string.Empty;
                //    string attachCaption = string.Empty;
                //    string attachDesc = string.Empty;

                //    name = viewData.Resort.Name;
                //    defaultMediaUrl = "http://www.thesnowhub.com/static/images/fb-snowhub.png";
                //    url = string.Format("http://www.thesnowhub.com/resorts/{0}", viewData.Resort.PrettyUrl);
                //    attachCaption = string.Format("{0}, {1}, {2}", viewData.Resort.Name, viewData.Resort.CountryName, viewData.Resort.ContinentName);
                //    attachDesc = string.Format("Are you in {0} too? Click 'Check-In' below!", viewData.Resort.Name);

                //    if (!string.IsNullOrEmpty(name))
                //    {
                //        attachment attach = new attachment();
                //        attach.caption = attachCaption;
                //        attach.description = attachDesc;
                //        attach.href = url;
                //        attach.name = viewData.Resort.Name;
                //        attachment_media attach_media = new attachment_media();
                //        attach_media.type = attachment_media_type.image;
                //        attachment_media_image image = new attachment_media_image();
                //        image.type = attachment_media_type.image;
                //        image.href = url;
                //        image.src = defaultMediaUrl;
                //        List<attachment_media> attach_media_list = new List<attachment_media>();
                //        attach_media_list.Add(image);
                //        attach.media = attach_media_list;
                //        attachment_property attach_prop = new attachment_property();
                //        attachment_category attach_cat = new attachment_category();
                //        attach_cat.text = "Snow Sports";
                //        attach_cat.href = "#";
                //        attach_prop.category = attach_cat;
                //        attach_prop.ratings = "";
                //        attach.properties = attach_prop;
                //        /* action links */
                //        List<action_link> actionlink = new List<action_link>();
                //        action_link al1 = new action_link();
                //        al1.href = url;
                //        al1.text = "Check-In";
                //        actionlink.Add(al1);

                //        api.stream.publish(string.Format("has Checked-In to {0} on the Snowhub.", name), attach, actionlink, api.uid.ToString(), 0);
                //    }
                //}

                var html2 = string.Format("<strong style='color: #000;'>You are Checked-In!</strong>&nbsp;You will now be shown as 'Checked-In' to {0}.", viewData.Resort.Name);
                Utilities.FeedbackManager.AddFeedback(FeedbackType.Thanks, html2);

                _userService = new UserService(_userRepository);
                UserContext.CurrentUser = _userService.Get(UserContext.CurrentUser.ID);

                if (!string.IsNullOrEmpty(viewData.ReturnUrl))
                    return Redirect(viewData.ReturnUrl);
            }

            //TODO: error handle

            return View("CheckIn", viewData);
        }

        public ActionResult CheckOut(string name, string ReturnUrl)
        {
            if (!UserContext.UserIsLoggedIn())
            {
                if (SessionContext.CurrentSession == null)
                {
                    SessionContext.CurrentSession = new Session();
                }
                return RedirectToAction("Index", "home");
            }
            _checkInService = new CheckInService(_checkInRepository);
            _resortService = new ResortService(_resortRepository);
            _userService = new UserService(_userRepository);

            var resort = _resortService.Get(name);

            //set all check-ins to 'inactive' for this user
            _checkInService.UpdateAllToInactiveForUser(UserContext.CurrentUser.ID);

            var html2 = string.Format("<strong style='color: #000;'>Checked-Out</strong>&nbsp;You have just Checked-Out of {0}.", resort.Name);
            Utilities.FeedbackManager.AddFeedback(FeedbackType.Thanks, html2);
            UserContext.CurrentUser = _userService.Get(UserContext.CurrentUser.ID);

            //TODO: return url
            return RedirectToAction("Index", "home");
        }


        public CheckInViewData GetCheckInViewData(string name, string ReturnUrl)
        {
            var viewData = new CheckInViewData();
            _resortService = new ResortService(_resortRepository);
            viewData.Resort = _resortService.Get(name);
            viewData.ReturnUrl = ReturnUrl;

            var userId = (UserContext.UserIsLoggedIn()) ? UserContext.CurrentUser.ID : 0;

            var breadcrumbs = new List<Breadcrumb>
                                   {
                                       NewBreadcrumb("Home", "/"),
                                       NewBreadcrumb("Check-In", string.Format("/resorts/checkin/{0}", viewData.Resort.PrettyUrl))
                                   };
            viewData.Breadcrumbs = breadcrumbs;

            ////check FB permissions
            //facebook.API api = new facebook.API()
            //{
            //    ApplicationKey = FacebookConnectAuthentication.ApiKey,
            //    Secret = FacebookConnectAuthentication.SecretKey,
            //    SessionKey = FacebookConnectAuthentication.SessionKey
            //};
            //try
            //{
            //    viewData.HasFacebookPublishPermission = api.users.hasAppPermission(facebook.Types.Enums.Extended_Permissions.publish_stream);
            //}
            //catch (Exception ex)
            //{
            //    viewData.HasFacebookPublishPermission = false;
            //}

            return viewData;
        }

        public ActionResult Info(string name)
        {
            var viewData = new ResortViewData();
            _resortService = new ResortService(_resortRepository);

            viewData.Resort = _resortService.Get(name);
            var country = LocationDataManager.GetCountryByID(viewData.Resort.CountryID.ToString());
            var continent = country.Continent;

            var breadcrumbs = new List<Breadcrumb>
            {
                NewBreadcrumb("Home", "/"),
                NewBreadcrumb("Resorts", "/resorts"),
                NewBreadcrumb(continent.ContinentName,
                            string.Format("/resorts/{0}/list/", continent.PrettyUrl)),
                NewBreadcrumb(country.CountryName,
                            string.Format("/resorts/{0}/list/", country.PrettyUrl)),
                NewBreadcrumb(viewData.Resort.Name,
                            string.Format("/resorts/{0}", viewData.Resort.PrettyUrl)),
                NewBreadcrumb("Info",
                            string.Format("/resorts/{0}/info", viewData.Resort.PrettyUrl))
            };
            viewData.Breadcrumbs = breadcrumbs;

            return View("Info", viewData);
        }


        public JsonResult GetResorts(string action, string minx, string miny, string maxx, string maxy)
        {
            var resp = new GetResortsModel();
            var id = string.Empty;
            var resorts = new List<Sporthub.Model.Resort>();
            _resortService = new ResortService(_resortRepository);

            switch (action)
            {
                case "BOUNDS":
                    var dmaxx = Convert.ToDouble(maxx);
                    var dmaxy = Convert.ToDouble(maxy);
                    var dminx = Convert.ToDouble(minx);
                    var dminy = Convert.ToDouble(miny);
                    resorts = _resortService.GetAllByBounds(dmaxx, dmaxy, dminx, dminy).ToList();
                    break;
                //case "COUNTRY":
                //    id = Request["id"].ToString();
                //    resorts = resortService.GetAllByCountryID(int.Parse(id)).ToList();
                //    break;
                //case "USER":
                //    id = Request["id"].ToString();
                //    var resortList = (from r in db.Resorts
                //                      join lru in db.LinkResortUsers on r.ID equals lru.ResortID
                //                      where lru.UserID == int.Parse(id) && r.GeonameID > 0
                //                      select new { r.Name, r.ID, r.Longitude, r.Latitude });
                //    foreach (var r in resortList)
                //    {
                //        var resort = new Sporthub.Model.Resort();
                //        resort.ID = r.ID;
                //        resort.Name = r.Name;
                //        resort.Latitude = r.Latitude;
                //        resort.Longitude = r.Longitude;
                //        resorts.Add(resort);
                //    }
                //    break;
                default:
                    break;
            }

            resp.TotalCount = resorts.Count;
            resp.Data = new List<DataReturn>();

            foreach (var resort in resorts)
            {
                var ret = new DataReturn
                {
                    Name = resort.Name,
                    Id = resort.ID,
                    Longitude = resort.Longitude.ToString(),
                    Latitude = resort.Latitude.ToString()
                };
            }

            return this.Json(resp, JsonRequestBehavior.AllowGet);
        }
    }
}
