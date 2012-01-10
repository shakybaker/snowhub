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
using Sporthub.Repository.DataAccess;
using Sporthub.Utils;

namespace Sporthub.Web.Resorts
{
    public partial class List : System.Web.UI.Page
    {
        private ContinentRepository continentRepository = new ContinentRepository();
        private ContinentService continentService;
        private CountryRepository countryRepository = new CountryRepository();
        private CountryService countryService;
        private RegionRepository regionRepository = new RegionRepository();
        private RegionService regionService;
        private string pageHeading = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = "0";
            switch (GetQueryStringType())
            {
                case QS.ContinentID:
                    GetContinent(int.Parse(id));
                    break;
                case QS.CountryID:
                    GetCountry(int.Parse(id));
                    break;
                case QS.RegionID:
                    GetRegion(int.Parse(id));
                    break;
                default:
                    pageHeading = "World Resorts";
                    hidLat.Value = "0";
                    hidLng.Value = "0";
                    hidID.Value = "0";
                    hidLevel.Value = "World";
                    break;
            }
        }

        private void GetContinent(int id)
        {
            continentService = new ContinentService(continentRepository);
            var continent = continentService.Get(id);

            pageHeading = string.Format("{0} Resorts", continent.ContinentName);
            hidLat.Value = continent.Latitude;
            hidLng.Value = continent.Longitude;
            hidID.Value = id.ToString();
            hidLevel.Value = "Continent";
        }

        private void GetCountry(int id)
        {
            countryService = new CountryService(countryRepository);
            var country = countryService.Get(id);

            pageHeading = string.Format("{0} Resorts", country.CountryName);
            hidLat.Value = "0";
            hidLng.Value = "0";
            hidID.Value = id.ToString();
            hidLevel.Value = "Country";
        }

        private void GetRegion(int id)
        {
            regionService = new RegionService(regionRepository);
            var region = regionService.Get(id);

            pageHeading = string.Format("{0} Resorts", region.Name);
            hidLat.Value = "0";
            hidLng.Value = "0";
            hidID.Value = id.ToString();
            hidLevel.Value = "Region";
        }

        private QS GetQueryStringType()
        {
            QS qs = new QS();
            if (Request.QueryString.Count > 0)
            {
                for (int i = 0; i < Request.QueryString.Count; i++)
                {
                    if (Request.QueryString[i] == Enums.GetName(QS.CountryID))
                    {
                        return QS.CountryID;
                    }
                    if (Request.QueryString[i] == Enums.GetName(QS.ContinentID))
                    {
                        return QS.ContinentID;
                    }
                    if (Request.QueryString[i] == Enums.GetName(QS.RegionID))
                    {
                        return QS.RegionID;
                    }
                }
            }
            return qs;
        }
    }
}
