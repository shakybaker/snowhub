using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class AdminRegionsListViewData : SporthubViewData
    {
        public List<Region> Regions { get; set; }
        public Country Country { get; set; }
        public Region ParentRegion { get; set; }
    }
}
