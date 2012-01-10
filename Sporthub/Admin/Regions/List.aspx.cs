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

namespace Sporthub.Web.Admin.Regions
{
    public partial class List : System.Web.UI.Page
    {
        private RegionRepository regionRepository = new RegionRepository();
        private RegionService regionService;
        private ConfigDataService configDataService;
        public ViewData vd;

        public class ViewData
        {
            public bool NoCountrySpecified { get; set; }
            public string CountryName { get; set; }
            public int CountryID { get; set; }
            public int ParentRegionID { get; set; }
            public int RegionLevel { get; set; }
            public IList<Sporthub.Model.Region> Regions { get; set; }

            public ViewData()
            {
                NoCountrySpecified = true;
                CountryName = string.Empty;
                Regions = new List<Sporthub.Model.Region>();
                CountryID = 0;
                ParentRegionID = 0;
                RegionLevel = 0;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                vd = new ViewData();
                regionService = new RegionService(regionRepository);
                configDataService = new ConfigDataService();
                string id = string.Empty;
                vd.NoCountrySpecified = false;
                if (Request.QueryString.Count > 0)
                {
                    vd.CountryID = int.Parse(Request[Enums.GetName(QS.CountryID)]);//TODO: error handle
                    vd.ParentRegionID = int.Parse(Request[Enums.GetName(QS.ParentRegionID)]);//TODO: error handle
                    if (vd.ParentRegionID > 0)
                    {
                        vd.Regions = regionService.GetAllForRegion(vd.ParentRegionID);
                    }
                    else
                    {
                        vd.Regions = regionService.GetAllForCountry(vd.CountryID);
                    }
                }
                else
                {
                    vd.NoCountrySpecified = true;
                }
                Utils.Helpers.PopulateDropDownList(ddlCountries, vd.NoCountrySpecified, configDataService.GetCountries(), "Country", vd.CountryID.ToString());
            }
        }

        protected void ddlCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("List.aspx?{0}={1}&{2}={3}", Enums.GetName(QS.CountryID), ddlCountries.SelectedValue, Enums.GetName(QS.ParentRegionID), 0));
        }
    }
}
