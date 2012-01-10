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
using Sporthub.Utils;


namespace Sporthub.Web.Admin.Resorts
{
    public partial class Edit : System.Web.UI.Page
    {
        private ContinentRepository continentRepository = new ContinentRepository();
        private ContinentService continentService;
        private CountryRepository countryRepository = new CountryRepository();
        private CountryService countryService;
        private ResortRepository resortRepository = new ResortRepository();
        private ResortService resortService;
        private ConfigDataService configDataService;
        public ViewData vd;

        public class ViewData
        {
            public bool IsNew { get; set; }
            public string Action { get; set; }
            public string Name { get; set; }
            public int? CountryID { get; set; }
            public int? RegionID { get; set; }
            public Sporthub.Model.Resort Resort { get; set; }

            public ViewData()
            {
                IsNew = false;
                Action = string.Empty;
                Name = string.Empty;
                Resort = new Sporthub.Model.Resort();
                CountryID = 0;
                RegionID = 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vd = new ViewData();
            configDataService = new ConfigDataService();
            string id = string.Empty;

            if (Request[Enums.GetName(QS.Action)] == Enums.GetName(QS.Add))
            {
                vd.IsNew = true;
                vd.CountryID = int.Parse(Request[Enums.GetName(QS.CountryID)]);//TODO: error handle
                vd.RegionID = int.Parse(Request[Enums.GetName(QS.RegionID)]);//TODO: error handle
            }
            else
            {
                vd.IsNew = false;
                //TODO:
            }
            vd.Action = (vd.IsNew) ? "Add" : "Edit";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Sporthub.Model.Resort resort = new Resort();
            Sporthub.Model.Continent continent = new Continent();
            Sporthub.Model.Country country = new Country();
            
            resortService = new ResortService(resortRepository);
            countryService = new CountryService(countryRepository);
            continentService = new ContinentService(continentRepository);

            resort.CountryID = int.Parse(Request[Enums.GetName(QS.CountryID)]);//TODO: error handle
            resort.Name = Request.Form["tbName"];
            resort.Region.ID = int.Parse(Request[Enums.GetName(QS.RegionID)]);//TODO: error handle
            if (Request.Form["tbCoord"].Length > 0)
            {
                string[] arrCoords = Request.Form["tbCoord"].Replace(" ","").Split(',');
                resort.Latitude = double.Parse(arrCoords[0]);
                resort.Longitude = double.Parse(arrCoords[1]);
            }
//            resort.WikipediaUrl = Request.Form["tbWikipediaUrl"];

            country = countryService.Get(resort.CountryID);
            continent = continentService.Get(country.ContinentID);

            resort.CountryName = country.CountryName;
            resort.ContinentName = continent.ContinentName;
            resort.ContinentID = continent.ID;

            if (vd.IsNew)
            {
                int newId = resortService.Add(resort);
            }
            else
            {
                //resort.ID = vd.Region.ID;
                //resortService.Update(resort);//TODO: error handle
            }

            //TODO: check saved ok
            Response.Redirect(string.Format("/Admin/Regions/Edit.aspx?{0}={1}&{2}={3}", Enums.GetName(QS.CountryID), resort.CountryID, Enums.GetName(QS.RegionID), resort.Region.ID));
            //Response.Redirect(string.Format("List.aspx?{0}={1}&{2}={3}", Enums.GetName(QS.CountryID), resort.CountryID, Enums.GetName(QS.ParentRegionID), resort.ParentRegionID));

        }
    }
}
