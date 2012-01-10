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

namespace Sporthub.Web.Resorts
{
    public partial class Index : System.Web.UI.Page
    {
        private CountryRepository countryRepository = new CountryRepository();
        private CountryService countryService;
        private ContinentRepository continentRepository = new ContinentRepository();
        private ContinentService continentService;
        private ResortRepository resortRepository = new ResortRepository();
        private ResortService resortService;
        private ThreadRepository threadRepository = new ThreadRepository();
        private ThreadService threadService;
        public ViewData vd;

        protected void Page_Load(object sender, EventArgs e)
        {
            string rid = "0";
            vd = new ViewData();

            if (Request.QueryString.Count > 0)
            {
                rid = Request[Enums.GetName(QS.ResortID)];//TODO: error handle
            }

            //continentService = new ContinentService(continentRepository);
            //countryService = new CountryService(countryRepository);
            resortService = new ResortService(resortRepository);
            //threadService = new ThreadService(threadRepository);

            var resort = resortService.Get(int.Parse(rid));
            //var threads = threadService.GetAllForResort(int.Parse(rid));

            vd.Resort = resort;
            //vd.Threads = threads;

            //foreach (Sporthub.Model.LinkResortUser lru in resort.LinkResortUsers)
            //{
            //    if (lru.HasVisited)
            //    {
            //        vd.Visited++;
            //    }
            //    if (lru.IsFavourite)
            //    {
            //        vd.Favourited++;
            //    }
            //    if (lru.Score > 0)
            //    {
            //        vd.TotalRating += lru.Score;
            //        vd.Votes++;
            //    }
            //}
            //vd.Rating = Math.Round(((double)vd.TotalRating / (double)vd.Votes), 1);

            //SetPageHeading("snowhub / resorts");
            //SetMainHeading(vd.Resort.Name);
            //SetHeading("/ resorts");
            //SetMainNavigation();
            //SetTabNavigation();
            //SetBreadcrumbNavigation();
            hidResortID.Value = rid;
            if (int.Parse(rid) > 0)
            {
                hidLat.Value = resort.Latitude.ToString();
                hidLng.Value = resort.Longitude.ToString();
            }
        }

        public class ViewData
        {
            public Sporthub.Model.Resort Resort { get; set; }
            public IList<Sporthub.Model.Thread> Threads { get; set; }
            public int Favourited { get; set; }
            public int Visited { get; set; }
            public int Votes { get; set; }
            public int TotalRating { get; set; }
            public double Rating { get; set; }

            public ViewData()
            {
                Resort = new Sporthub.Model.Resort();
                Threads = new List<Sporthub.Model.Thread>();
                Favourited = 0;
                Visited = 0;
                Votes = 0;
                TotalRating = 0;
                Rating = 0.0;
            }
        }

    }
}
