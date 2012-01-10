using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Sporthub.Mvc.Code;
using Sporthub.Mvc.ViewData;
using Sporthub.Model;
using Sporthub.Repository;
using Sporthub.Services;
using Sporthub.Utilities;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using Sporthub.Data;
//using Newtonsoft.Json.Linq;

namespace Sporthub.Mvc.Controllers
{
    [HandleError]
    public class HomeController : SporthubController
    {
        public bool IsUserLoggedIn = false;
        private readonly ResortRepository _resortRepository = new ResortRepository();
        private ResortService _resortService;
        private readonly UserRepository _userRepository = new UserRepository();
        private UserService _userService;
        private readonly ActivityRepository _activityRepository = new ActivityRepository();
        private ActivityService _activityService;
        private readonly LinkResortUserRepository _linkResortUserRepository = new LinkResortUserRepository();
        private LinkResortUserService _linkResortUserService;

        public ActionResult Index()
        {
            //oAuthFacebook oFB = new oAuthFacebook();
            //var tmp = oFB.AuthorizationLinkGet();
            //Response.Redirect(tmp);

            //steps
            //1. in controller, do a "https://graph.facebook.com/me?fields=id" call then check if the user's FB id matches any exoisting SH users
            //2. if it does then log them in
            //3. if it doesn't then show FB Login buttons where relevant
            //4. on the callback url of the login register the user to SH (storing the FB id
            //FOR NEW FB USERS: -
            //5. on clicking a FB login/register button open lightbox saying "you will get an account set up with thesnowhub.com. Please agree to the T&C's below"
            //FOR EXISTING SH USERS: -
            //6. Connect your account. callback url here will update existing SH user with FB id



























            //if (FacebookConnectAuthentication.isConnected())
            //{
            //    Response.Redirect("/user");
            //}

            _userService = new UserService(_userRepository);
            _resortService = new ResortService(_resortRepository);
            _activityService = new ActivityService(_activityRepository);
            _linkResortUserService = new LinkResortUserService(_linkResortUserRepository);

            var vd = new HomeViewData
            {
                RecentVisitors = _userService.GetRecentVisitors(),
                NewMembers = _userService.GetNewMembers(),
                RecentActivity = _activityService.GetAll(20),
                TopRatedResorts = _resortService.GetTopRatedResorts(),
                MostVisitedResorts = _resortService.GetMostVisitedResorts(),
                LatestReviews = _linkResortUserService.GetLatestReviews(5)
            };

            //vd.TestUsers = GetTestUsers();

            var ds2 = LocationDataManager.GetClusteredResortsForWorld();
            var continents = new List<Continent>();
            var countries = new List<Country>();
            Continent continent = null;
            Country country = null;

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

            vd.Continents = (continents != null) ? continents : new List<Continent>();
            vd.Countries = (countries != null) ? countries : new List<Country>();

            return View("Index", vd);
        }

        //////private List<user> GetTestUsers()
        //////{
        //////    API api = new API()
        //////    {
        //////        ApplicationKey = FacebookConnectAuthentication.ApiKey,
        //////        Secret = FacebookConnectAuthentication.SecretKey,
        //////        SessionKey = FacebookConnectAuthentication.SessionKey
        //////    };

        //////    //example of reading result of FQL query with LinqToFql
        //////    var db = new FacebookDataContext(api);

        //////    var userList = new List<user>();
        //////    userList.Add(new user { uid = 1361562085 });
        //////    userList.Add(new user { uid = 100000468646138 });
        //////    userList.Add(new user { uid = 533493250 });

        //////    var myUidList = from u in userList select u.uid;

        //////    var users = (from user in db.user where myUidList.Contains(user.uid) select user).ToList();

        //////    //example of reading result of FQL query without LinqToFql
        //////    /*
        //////    string result = api.fql.query("SELECT uid, first_name, last_name FROM user WHERE uid = 533493250");
        //////    var xmlDoc = new XmlDocument();
        //////    xmlDoc.LoadXml(result);
        //////    XmlNodeList uidList = xmlDoc.GetElementsByTagName("uid");
        //////    XmlNodeList firstNList = xmlDoc.GetElementsByTagName("first_name");
        //////    XmlNodeList lastNList = xmlDoc.GetElementsByTagName("last_name");

        //////    //At this point you should have all of your information. To access it 
        //////    //do the following 

        //////    string uid = "";
        //////    foreach (XmlNode node in uidList)
        //////    {
        //////        if (!String.IsNullOrEmpty(node.InnerText))
        //////        {
        //////            uid += node.InnerText + ", ";
        //////        }
        //////    }
        //////    */

        //////    //example of reading result of FQL query using LinqToXml (and without LinqToFql)
        //////    //NOTE: this needs a session key
        //////    /*
        //////    var myUid = 533493250;
        //////    var response = api.fql.query(String.Format("SELECT uid, name FROM user WHERE uid IN (SELECT uid2 FROM friend WHERE uid1 = {0}) ORDER BY name", myUid));

        //////    XDocument xml = XDocument.Parse(response);

        //////    XNamespace fbns = XNamespace.Get("http://api.facebook.com/1.0/");
        //////    var friends = from el in xml.Root.Elements(fbns + "user")
        //////    select new
        //////    {
        //////        uid = el.Element(fbns + "uid").Value,
        //////        name = el.Element(fbns + "name").Value
        //////    };
        //////    */

        //////    return users;
        //////}

        public ActionResult About()
        {
            return View();
        }
    }
}
