using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class HomeViewData : SporthubViewData
    {
        public IList<User> RecentVisitors { get; set; }
        public IList<User> NewMembers { get; set; }
        public IList<Resort> TopRatedResorts { get; set; }
        public IList<Resort> MostVisitedResorts { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; }
        public IList<LinkResortUser> LatestReviews { get; set; }
        public IList<Activity> RecentActivity { get; set; }
        public IList<Continent> Continents { get; set; }
        public IList<Country> Countries { get; set; }
    }
}
