using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class AdminLocationsListViewData : SporthubViewData
    {
        public List<Location> Locations { get; set; }
    }
}
