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
    public partial class Edit : System.Web.UI.Page
    {
        private RegionRepository regionRepository = new RegionRepository();
        private RegionService regionService;
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
            public int? ParentRegionID { get; set; }
            public int? RegionLevel { get; set; }
            public Sporthub.Model.Region Region { get; set; }
            public IList<Sporthub.Model.Region> ChildRegions { get; set; }
            public IList<Sporthub.Model.Resort> Resorts { get; set; }

            public ViewData()
            {
                IsNew = false;
                Name = string.Empty;
                Region = new Sporthub.Model.Region();
                ChildRegions = new List<Sporthub.Model.Region>();
                Resorts = new List<Sporthub.Model.Resort>();
                CountryID = 0;
                ParentRegionID = 0;
                RegionLevel = 1;
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
                vd.ParentRegionID = int.Parse(Request[Enums.GetName(QS.ParentRegionID)]);//TODO: error handle
                vd.RegionLevel = (vd.ParentRegionID > 0) ? 2 : 1;
            }
            else
            {
                vd.IsNew = false;
                id = Request[Enums.GetName(QS.RegionID)].ToString();
                regionService = new RegionService(regionRepository);
                resortService = new ResortService(resortRepository);
                vd.Region = regionService.Get(int.Parse(id));
                vd.Resorts = resortService.GetAllByRegionID(int.Parse(id));
                vd.CountryID = vd.Region.CountryID;
                vd.ParentRegionID = vd.ParentRegionID;
                vd.RegionLevel = vd.RegionLevel;
                tbName.Text = vd.Region.Name;
            }
            vd.Action = (vd.IsNew) ? "Add" : "Edit";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Sporthub.Model.Region region = new Region();
            regionService = new RegionService(regionRepository);

            region.CountryID = int.Parse(Request[Enums.GetName(QS.CountryID)]);//TODO: error handle
            region.Name = Request.Form["tbName"];
            region.CountryName = string.Empty;
            region.ParentRegionID = int.Parse(Request[Enums.GetName(QS.ParentRegionID)]);//TODO: error handle
            region.RegionLevel = (region.ParentRegionID > 0) ? 2 : 1;
            if (vd.IsNew)
            {
                int newId = regionService.Add(region);
            }
            else
            {
                region.ID = vd.Region.ID;
                regionService.Update(region);//TODO: error handle
            }

            //TODO: check saved ok
            Response.Redirect(string.Format("List.aspx?{0}={1}&{2}={3}", Enums.GetName(QS.CountryID), region.CountryID, Enums.GetName(QS.ParentRegionID), region.ParentRegionID));

        }
    }
}
