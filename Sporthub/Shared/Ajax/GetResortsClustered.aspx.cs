using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Sporthub.Model;
using Sporthub.Model.Enumerators;
using Sporthub.Services;
using Sporthub.Repository;

namespace Sporthub.Web.Shared.Ajax
{
    public partial class GetResortsClustered : System.Web.UI.Page
    {
        private ContinentRepository continentRepository = new ContinentRepository();
        private ContinentService continentService;
        private CountryRepository countryRepository = new CountryRepository();
        private CountryService countryService;

        protected void Page_Load(object sender, EventArgs e)
        {
            string resp = string.Empty;
            string continentID = string.Empty;
            string countryID = string.Empty;

            if (Request.QueryString.Count > 0)
            {
                if ((LocationLevel)int.Parse(Request.QueryString["l"].ToString()) == LocationLevel.World)
                {
                    continentService = new ContinentService(continentRepository);
                    var continents = continentService.GetAll();
                    resp += "storeLocations({ \"count\": " + continents.Count + ",  \"locations\": [";
                    int i = 0;
                    foreach (Sporthub.Model.Continent continent in continents)
                    {
                        resp += "{";
                        i++;
                        resp += "\"name\": \"" + continent.ContinentName + "\", ";
                        resp += "\"id\": \"" + continent.ID + "\", ";
                        resp += "\"lng\": \"" + continent.Longitude + "\", ";
                        resp += "\"lat\": \"" + continent.Latitude + "\", ";
                        int resortCount = 0;
                        foreach (Sporthub.Model.Country country in continent.Countries)
                        {
                            resortCount += country.Resorts.Count;
                        }
                        resp += "\"cnt\": \"" + resortCount + "\" ";
                        resp += "}";
                        if (i < continents.Count)
                        {
                            resp += ", ";
                        }
                    }
                    resp += "]})";
                }
                else if ((LocationLevel)int.Parse(Request.QueryString["l"].ToString()) == LocationLevel.Continent)
                {
                    continentID = Request.QueryString["contid"].ToString();
                    countryService = new CountryService(countryRepository);
                    var countries = countryService.GetAll(int.Parse(continentID));
                    int i = 0;
                    int countryCnt = 0;
                    foreach (Sporthub.Model.Country country in countries)
                    {
                        if (country.Resorts.Count > 0)
                        {
                            i++;
                            countryCnt++;
                            resp += "{";
                            resp += "\"name\": \"" + country.CountryName + "\", ";
                            resp += "\"id\": \"" + country.ID + "\", ";
                            resp += "\"lng\": \"" + country.Longitude + "\", ";
                            resp += "\"lat\": \"" + country.Latitude + "\", ";
                            resp += "\"code\": \"" + country.ISO3166Alpha2 + "\", ";
                            resp += "\"cnt\": \"" + country.Resorts.Count + "\" ";
                            resp += "}";
                            if (i < countries.Count)
                            {
                                resp += ", ";
                            }
                        }
                    }
                    resp = "storeLocations({ \"count\": " + countryCnt + ",  \"locations\": [" + resp + "]})";
                }
            }

            Response.Write(resp);
        }
    }
}
