using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class UserViewData : SessionViewData
    {
        public User User { get; set; }
        public string Message { get; set; }
        public string ProfileImageUrl { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; }
        public bool IsActivation { get; set; }
        public bool HasProfileImage { get; set; }
        public List<Country> Countries { get; set; }
        public List<ConfigSportType> Sports { get; set; }
        public List<User> Friends { get; set; }
        public IList<Activity> RecentActivity { get; set; }
    }
}
