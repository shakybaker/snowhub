using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class SiteViewData : SporthubViewData
    {
        public List<Breadcrumb> Breadcrumbs { get; set; }
    }
}
