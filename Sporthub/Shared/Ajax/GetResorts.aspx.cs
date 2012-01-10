using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sporthub.Model;
using Sporthub.Model.Enumerators;
using Sporthub.Services;
using Sporthub.Repository;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Web.Shared.Ajax
{
    public partial class GetResorts : System.Web.UI.Page
    {
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
        private ResortRepository resortRepository = new ResortRepository();
        private ResortService resortService;

        protected void Page_Load(object sender, EventArgs e)
        {
            string resp = string.Empty;
            string id = string.Empty;
            string action = string.Empty;
            if (Request.QueryString.Count > 0)
            {
                List<Sporthub.Model.Resort> resorts = new List<Sporthub.Model.Resort>();
                resortService = new ResortService(resortRepository);

                action = Request["a"].ToString();

                switch (action)
                {
                    case "BOUNDS":
                        double maxx = Convert.ToDouble(Request["maxx"].ToString());
                        double maxy = Convert.ToDouble(Request["maxy"].ToString());
                        double minx = Convert.ToDouble(Request["minx"].ToString());
                        double miny = Convert.ToDouble(Request["miny"].ToString());
                        resorts = resortService.GetAllByBounds(maxx, maxy, minx, miny).ToList();
                        break;
                    case "COUNTRY":
                        id = Request["id"].ToString();
                        resorts = resortService.GetAllByCountryID(int.Parse(id)).ToList();
                        break;
                    case "USER":
                        id = Request["id"].ToString();
                        var resortList = (from r in db.Resorts
                                          join lru in db.LinkResortUsers on r.ID equals lru.ResortID
                                          where lru.UserID == int.Parse(id) && r.GeonameID > 0
                                          select new { r.Name, r.ID, r.Longitude, r.Latitude });
                        foreach (var r in resortList)
                        {
                            var resort = new Sporthub.Model.Resort();
                            resort.ID = r.ID;
                            resort.Name = r.Name;
                            resort.Latitude = r.Latitude;
                            resort.Longitude = r.Longitude;
                            resorts.Add(resort);
                        }
                        break;
                    default:
                        break;
                }

                resp += "storeResorts({ \"count\": " + resorts.Count + ",  \"resorts\": [";
                int i = 0;
                foreach (Sporthub.Model.Resort resort in resorts)
                {
                    resp += "{";
                    i++;
                    resp += "\"name\": \"" + resort.Name + "\", ";
                    resp += "\"id\": \"" + resort.ID + "\", ";
                    resp += "\"lng\": \"" + resort.Longitude + "\", ";
                    resp += "\"lat\": \"" + resort.Latitude + "\", ";
                    resp += "}";
                    if (i < resorts.Count)
                    {
                        resp += ", ";
                    }
                }
                resp += "]})";
            }
            Response.Write(resp);
        }
    }
}
