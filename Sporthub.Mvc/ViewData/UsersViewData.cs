using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;
using Sporthub.Model.Enumerators;

namespace Sporthub.Mvc.ViewData
{
    public class UsersListViewData : SporthubViewData
    {
        public IList<User> Users { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; }
    }
}
