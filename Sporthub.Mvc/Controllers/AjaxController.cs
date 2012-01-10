using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

using Sporthub.Data;
using Sporthub.Model;
using Sporthub.Model.Enumerators;
using Sporthub.Services;
using Sporthub.Repository;
using Sporthub.Utils;

using Sporthub.Mvc.ViewData;
using System.Configuration;
using Sporthub.Utilities;

namespace Sporthub.Mvc.Controllers
{
    public class AjaxController : Controller
    {
        private ContinentRepository continentRepository = new ContinentRepository();
        private ContinentService continentService;
        private CountryRepository countryRepository = new CountryRepository();
        private CountryService countryService;
        private ResortRepository resortRepository = new ResortRepository();
        private ResortService resortService;
        private ReviewUsefulnessRepository reviewUsefulnessRepository = new ReviewUsefulnessRepository();
        private ReviewUsefulnessService reviewUsefulnessService;
        private AirportRepository airportRepository = new AirportRepository();
        private AirportService airportService;
        private UserRepository userRepository = new UserRepository();
        private UserService userService;
        static Regex regex = new Regex(@"&#(1?\d\d);");

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetPrettyUrl(string resortName)
        {
            BasicJsonModel resp = new BasicJsonModel();

            resortService = new ResortService(resortRepository);

            string encodedName = EncodeURL(CheckEncodedName(resortName));
            var resortCheck = resortService.Get(encodedName);

            resp.Value = encodedName;

            if (resortCheck != null)
            {
                resp.Flag = false;
                resp.Message = "Already Exists";
            }
            else
            {
                resp.Flag = true;
                resp.Message = "Updated";
            }

            return this.Json(resp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetResortPopup(string name)
        {
            ResortsListViewData viewData = new ResortsListViewData();
            //resortService = new ResortService(resortRepository);
            //viewData.Resort = resortService.Get(name);
            viewData.LocationName = name;
            return View("ResortPopup", viewData);
        }

        public ActionResult GetFeed(string feedUrl)
        {
            NewsFeedItemsViewData vd = new NewsFeedItemsViewData();
            vd.Items = new List<NewsFeedItem>();

            XNamespace slashNamespace = "http://purl.org/rss/1.0/modules/slash/";
            XDocument rssFeed = XDocument.Load(feedUrl);

            try
            {
                var items = from item in rssFeed.Descendants("item")
                            select new
                            {
                                Title = item.Element("title").Value,
                                //Published = DateTime.Parse(item.Element("pubDate").Value),
                                Url = (string.IsNullOrEmpty(item.Element("link").Value)) ? null : item.Element("link").Value
                            };

                //TODO: temporarily limiting to 5 - will be paged a la Netvibes
                int i = 0;
                foreach (var item in items)
                {
                    if (i < 5)
                    {
                        NewsFeedItem newItem = new NewsFeedItem();
                        newItem.Title = item.Title;
                        newItem.Published = DateTime.MinValue;
                        newItem.Url = item.Url;

                        vd.Items.Add(newItem);
                        i++;
                    }
                }
                if (vd.Items.Count == 0)
                {
                    vd.Message = "No items are available for this feed";
                }
            }
            catch (Exception ex)
            {
                //vd.Message = ex.Message;
                vd.Message = "Error obtaining feed";
            }


            return View("GetFeed", vd);
        }

        public ActionResult GetResortWeather(int resortID)
        {
            XDocument feed;
            var forecast = new WeatherForecast();

            resortService = new ResortService(resortRepository);
            var resort = resortService.Get(resortID);
            try
            {
                feed = XDocument.Load(string.Format(@"http://api.wunderground.com/auto/wui/geo/ForecastXML/index.xml?query={0},{1}", resort.Latitude, resort.Longitude));
                foreach (var day in feed.Descendants("simpleforecast").First().Descendants("forecastday"))
                {
                    var forecastDay = new ForecastDay
                    {
                        IconUrl = string.Format("http://icons-ecast.wxug.com/i/c/k/{0}.gif", day.Element("icon").Value),
                        IconAlt = day.Element("icon").Value,
                        //Title = day.Element("title").Value,
                        //Text = day.Element("fcttext").Value
                        Conditions = day.Element("conditions").Value,
                        DayOfWeek = day.Descendants("date").First().Element("weekday").Value,
                        HiTempC = day.Descendants("high").First().Element("celsius").Value,
                        HiTempF = day.Descendants("high").First().Element("fahrenheit").Value,
                        LoTempC = day.Descendants("low").First().Element("celsius").Value,
                        LoTempF = day.Descendants("low").First().Element("fahrenheit").Value,
                        IsChanceOfSnow = day.Element("conditions").Value.ToLower().Contains("snow")
                    };
                    forecast.ForecastDays.Add(forecastDay);
                }
            }
            catch { }

            return View("ResortWeather", forecast);
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
        
        public JsonResult CheckUserName(string userName)
        {
            BasicJsonModel resp = new BasicJsonModel();
            Regex pattern = new Regex("^[a-zA-Z0-9]+$");

            if (userName.Length<4 || userName.Length>15)
            {
                resp.Flag = false;
                resp.Value = "invalid";//css class
                resp.Message = "Username must be between 4 and 15 characters";
            }
            else if (!pattern.IsMatch(userName))
            {
                resp.Flag = false;
                resp.Value = "invalid";//css class
                resp.Message = "Username must only contain alpha-numeric characters (numbers or letters)";
            }
            else
            {

                userService = new UserService(userRepository);
                var checkUser = userService.GetByUserName(userName);
                if (checkUser == null)
                {
                    resp.Flag = true;
                    resp.Value = "valid";//css class
                    resp.Message = "";
                }
                else
                {
                    resp.Flag = false;
                    resp.Value = "invalid";//css class
                    resp.Message = "Already in use - please choose another";
                }
            }

            return this.Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckEmail(string email)
        {
            userService = new UserService(userRepository);
            BasicJsonModel resp = new BasicJsonModel();
            var checkUser = userService.GetByEmail(email);
            if (checkUser == null)
            {
                resp.Flag = true;
                resp.Value = "valid";//css class
                resp.Message = "";
            }
            else
            {
                resp.Flag = false;
                resp.Value = "invalid";//css class
                resp.Message = "Email already in use - please choose another";
            }
            return this.Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Search(string text)
        {
            resortService = new ResortService(resortRepository);
            Search results = new Search();

            var query = resortService.GetAllBySearch(text, 10);

            foreach (Sporthub.Model.Resort resort in query)
            {
                SearchResults result = new SearchResults();
                result.id = resort.PrettyUrl;
                result.value = resort.Name;
                result.info = string.Format("<span class=\"flag {0}\">&nbsp;</span>", resort.Country.ISO3166Alpha2);
                //result.info = string.Format("<img alt='{0}' src='/Static/Images/Flags/{0}.png' />{1}, {2}", resort.Country.ISO3166Alpha2, resort.CountryName, resort.ContinentName);

                results.results.Add(result);
            }

            return this.Json(results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetResortsByUserId(string id)
        {
            Sporthub.Repository.DataAccess.SporthubDataContext db = new Sporthub.Repository.DataAccess.SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

            var resortList = (from r in db.Resorts
                                join lru in db.LinkResortUsers on r.ID equals lru.ResortID
                                where lru.UserID == int.Parse(id)
                                select new { r.Name, r.ID, r.Longitude, r.Latitude, r.PrettyUrl });

            GetResortsModel resp = new GetResortsModel();
            List<Resort> resorts = new List<Resort>();

            foreach (var r in resortList)
            {
                var resort = new Resort();
                resort.ID = r.ID;
                resort.Name = r.Name;
                resort.Latitude = r.Latitude;
                resort.Longitude = r.Longitude;
                resort.PrettyUrl = r.PrettyUrl;
                resorts.Add(resort);
            }

            resp.TotalCount = resorts.Count;
            resp.Data = new List<DataReturn>();

            foreach (var r in resortList)
            {
                DataReturn ret = new DataReturn();
                ret.Name = r.Name;
                ret.Id = r.ID;
                ret.Longitude = r.Longitude.ToString();
                ret.Latitude = r.Latitude.ToString();
                ret.PrettyUrl = r.PrettyUrl;
                //ret.Code = r.Country.ISO3166Alpha2;
                ret.Code = "XX";//TODO:
                resp.Data.Add(ret);
            }
            return this.Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetResortsByBounds(string minx, string miny, string maxx, string maxy, string rid)
        {
            GetResortsModel resp = new GetResortsModel();
            List<Resort> resorts = new List<Resort>();
            resortService = new ResortService(resortRepository);

            double dmaxx = Convert.ToDouble(maxx);
            double dmaxy = Convert.ToDouble(maxy);
            double dminx = Convert.ToDouble(minx);
            double dminy = Convert.ToDouble(miny);
            resorts = resortService.GetAllByBounds(dmaxx, dmaxy, dminx, dminy).ToList();
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

            resp.TotalCount = resorts.Count;
            resp.Data = new List<DataReturn>();

            foreach (Resort resort in resorts)
            {
                if (int.Parse(rid) != resort.ID)
                {
                    DataReturn ret = new DataReturn();
                    ret.Name = resort.Name;
                    ret.PrettyUrl = resort.PrettyUrl;
                    ret.Id = resort.ID;
                    ret.Longitude = resort.Longitude.ToString();
                    ret.Latitude = resort.Latitude.ToString();
                    ret.Code = resort.Country.ISO3166Alpha2;
                    resp.Data.Add(ret);
                }
            }

            return this.Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAirportsByBounds(string minx, string miny, string maxx, string maxy, string rid)
        {
            GetResortsModel resp = new GetResortsModel();
            List<Airport> airports = new List<Airport>();
            airportService = new AirportService(airportRepository);

            double dmaxx = Convert.ToDouble(maxx);
            double dmaxy = Convert.ToDouble(maxy);
            double dminx = Convert.ToDouble(minx);
            double dminy = Convert.ToDouble(miny);
            airports = airportService.GetAllByBounds(dmaxx, dmaxy, dminx, dminy).ToList();

            resp.TotalCount = airports.Count;
            resp.Data = new List<DataReturn>();

            foreach (Airport airport in airports)
            {
                if (int.Parse(rid) != airport.ID)
                {
                    DataReturn ret = new DataReturn();
                    ret.Name = airport.Name;
                    ret.Id = airport.ID;
                    ret.Longitude = airport.Longitude.ToString();
                    ret.Latitude = airport.Latitude.ToString();
                    ret.Code = airport.Country.ISO3166Alpha2;
                    ret.PrettyUrl = airport.PrettyUrl;
                    resp.Data.Add(ret);
                }
            }

            return this.Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetResortsClustered(string level, string name)
        {
            GetResortsModel resp = new GetResortsModel();

            if ((LocationLevel)int.Parse(level) == LocationLevel.World)
            {
                DataSet ds = LocationDataManager.GetClusteredResortsForWorld();

                resp.Data = new List<DataReturn>();

                var tot = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataReturn ret = new DataReturn();
                    ret.Name = dr["ContinentName"].ToString();
                    ret.PrettyUrl = dr["PrettyUrl"].ToString();
                    ret.Id = int.Parse(dr["ID"].ToString());
                    ret.Longitude = dr["Longitude"].ToString();
                    ret.Latitude = dr["Latitude"].ToString();
                    ret.Code = dr["Code"].ToString();
                    ret.Count = int.Parse(dr["ResortCount"].ToString());//TODO: validate
                    tot += ret.Count;
                    resp.Data.Add(ret);
                }
                resp.TotalCount = tot;
            }
            else if ((LocationLevel)int.Parse(level) == LocationLevel.Continent)
            {
                DataSet ds = new DataSet();

                if (name == "World")
                {
                    ds = LocationDataManager.GetClusteredCountriesForWorld();
                }
                else
                {
                    ds = LocationDataManager.GetClusteredResortsForContinent(name.ToLower().Replace(" ", "-").Replace("/", "-"));
                }
                resp.Data = new List<DataReturn>();

                var tot = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    DataReturn ret = new DataReturn();
                    ret.Name = dr["CountryName"].ToString();
                    ret.PrettyUrl = dr["PrettyUrl"].ToString();
                    ret.Id = int.Parse(dr["ID"].ToString());
                    ret.Longitude = dr["Longitude"].ToString();
                    ret.Latitude = dr["Latitude"].ToString();
                    ret.Code = dr["ISO3166Alpha2"].ToString();
                    ret.Count = int.Parse(dr["ResortCount"].ToString());//TODO: validate
                    tot += ret.Count;
                    resp.Data.Add(ret);
                }
                resp.TotalCount = tot;
            }

            return this.Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetResorts(string level, string name)
        {
            GetResortsModel resp = new GetResortsModel();
            List<Resort> resorts = new List<Resort>();

            if ((LocationLevel)int.Parse(level) == LocationLevel.Continent)
            {
                resorts = ResortDataManager.GetResortsByContinent(name.ToLower().Replace(" ", "-").Replace("/", "-"));

                resp.Data = new List<DataReturn>();

                foreach (Resort resort in resorts)
                {
                    DataReturn ret = new DataReturn();
                    ret.Name = resort.Name;
                    ret.PrettyUrl = resort.PrettyUrl;
                    ret.Id = resort.ID;
                    ret.Longitude = resort.Longitude.ToString();
                    ret.Latitude = resort.Latitude.ToString();
                    ret.Code = string.Empty;
                    ret.Count = 0;
                    resp.Data.Add(ret);
                }
                resp.TotalCount = resorts.Count;
            }
            else if ((LocationLevel)int.Parse(level) == LocationLevel.Country)
            {
                resorts = ResortDataManager.GetResortsByCountry(name);

                resp.Data = new List<DataReturn>();

                foreach (Resort resort in resorts)
                {
                    DataReturn ret = new DataReturn();
                    ret.Name = resort.Name;
                    ret.PrettyUrl = resort.PrettyUrl;
                    ret.Id = resort.ID;
                    ret.Longitude = resort.Longitude.ToString();
                    ret.Latitude = resort.Latitude.ToString();
                    ret.Code = string.Empty;
                    ret.Count = 0;
                    resp.Data.Add(ret);
                }
                resp.TotalCount = resorts.Count;
            }
            //TODO: Continent, Region, World

            return this.Json(resp, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetResortLinksList(string resortID)
        {
            int rID;
            try
            {
                rID = int.Parse(resortID);
            }
            catch
            {
                rID = 0;
            }
            var viewData = new ResortLinksListViewData();
            viewData.ResortLinks = ResortDataManager.GetResortLinks(rID);

            return View("ResortLinksList", viewData);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult AddResortLink(string resortID)
        {
            AddResortLinkViewData viewData = new AddResortLinkViewData
            {
                ResortLink = new ResortLink
                {
                    ResortID = int.Parse(resortID)
                },//TODO: validate
                Message = string.Empty
            };
            return View("AddResortLink", viewData);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddResortLink(FormCollection form)
        {
            ResortLink resortLink = new ResortLink
            {
                Name = form["Name"],
                URL = form["Url"],
                Sequence = int.Parse(form["Sequence"]),//TODO: validate
                ResortID = int.Parse(form["ResortID"]),//TODO: validate
                ResortLinkTypeID = 3,
                CreatedUserID = UserContext.CurrentUser.ID,
                UpdatedUserID = UserContext.CurrentUser.ID
            };

            int newResortLinkID = ResortDataManager.AddResortLink(resortLink);

            AddResortLinkViewData viewData = new AddResortLinkViewData
            {
                ResortLink = resortLink,
                Message = string.Format("New row entered. ID = {0}", newResortLinkID)
            };

            return View("AddResortLink", viewData);
        }

        public ActionResult NotLoggedIn()
        {
            return View("NotLoggedIn");
        }

        public ActionResult BasicUserList()
        {
            return View("BasicUserList");
        }

        //private List<user> GetTestUsers()
        //{
        //    API api = new API()
        //    {
        //        ApplicationKey = FacebookConnectAuthentication.ApiKey,
        //        Secret = FacebookConnectAuthentication.SecretKey
        //        ,
        //        SessionKey = FacebookConnectAuthentication.SessionKey
        //    };

        //    var db = new FacebookDataContext(api);

        //    var userList = new List<user>();
        //    userList.Add(new user { uid = 1361562085 });
        //    userList.Add(new user { uid = 100000468646138 });
        //    userList.Add(new user { uid = 533493250 });

        //    var uidList = from u in userList select u.uid;

        //    var users = (from user in db.user where uidList.Contains(user.uid) select user).ToList();

        //    return users;
        //}

        public JsonResult GetAirportsByContinent(string id)
        {
            Sporthub.Repository.DataAccess.SporthubDataContext db = new Sporthub.Repository.DataAccess.SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

            airportService = new AirportService(airportRepository);

            GetResortsModel resp = new GetResortsModel();
            var airports = airportService.GetAllByContinentID(int.Parse(id));

            resp.TotalCount = airports.Count;
            resp.Data = new List<DataReturn>();

            foreach (var a in airports)
            {
                DataReturn ret = new DataReturn();
                ret.Name = a.Name;
                ret.Id = a.ID;
                ret.Longitude = a.Longitude.ToString();
                ret.Latitude = a.Latitude.ToString();
                ret.PrettyUrl = a.PrettyUrl;
                //ret.Code = r.Country.ISO3166Alpha2;
                ret.Code = a.Code;
                resp.Data.Add(ret);
            }
            return this.Json(resp, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GiveReviewFeedback(string feedback)
        {
            var arr = feedback.Split('-');
            //TODO: check if exists for user
            //TODO: don't allow feedback on own review
            if (arr[0] =="flag")
            {
                var emailService = new EmailService();
                var message = string.Empty;
                try
                {
                    bool useSmtpPickup = HttpContext.Request.Url.Host.ToLower().Contains("localhost");
                    emailService.ReportAbuse(string.Format("User ID {0} reported review {1} as inappropriate", UserContext.CurrentUser.ID, arr[1]), useSmtpPickup);
                }
                catch (Exception ex)
                {
                    //TODO: handle this
                }
            }
            else
            {
                reviewUsefulnessService = new ReviewUsefulnessService(reviewUsefulnessRepository);
                var newReviewUsefulness = new ReviewUsefulness
                {
                    IsUseful = (arr[0] == "yes") ? true : false,
                    UserID = UserContext.CurrentUser.ID,
                    LinkResortUserID = int.Parse(arr[1])
                };
                reviewUsefulnessService.Add(newReviewUsefulness);
            }
            return this.Json(new
            {
                Result = true,
                IsAuthenticated = true,
                ErrorMessage = "",
                IsRemove = false
            },
            JsonRequestBehavior.AllowGet);
        }

        public ActionResult GoogleSearchAPI(string lng, string lat)
        {
            //Base URL for calling the Google Places API
            string BaseAPIURL = "https://maps.googleapis.com/maps/api/place/search/json?location=" + lat + "," + lng + "&radius=2000&sensor=false&key=AIzaSyB1BdZ2CW1PJSpbsA4hBGmMLAhEO0tsVFc";
            //if (!string.IsNullOrEmpty(query.Name))
            //    //Append the name parameter only if data is sent from Client side.
            //    BaseAPIURL = String.Concat(BaseAPIURL, String.Format("&name={0}", query.Name));
            //Include the API Key which is necessary
            //BaseAPIURL = String.Concat(BaseAPIURL, String.Format("&sensor=false&key={0}", GetAPIKey()));
            //Get the XML result data from Google Places using a helper method whichc makes the call.
            string _response = MakeHttpRequestAndGetResponse(BaseAPIURL);
            //Wrap XML in a ContentResult and pass it back the Javascript
            return Content(_response);
        }

        //Helper method to call the URL and send the response back.
        private string MakeHttpRequestAndGetResponse(string BaseAPIURL)
        {
            var request = (HttpWebRequest)WebRequest.Create(BaseAPIURL);
            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";
            string text;
            var response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                text = sr.ReadToEnd();
            }

            return text;
        }
    }
}
