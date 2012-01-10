using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class ResortLinksListViewData : SporthubViewData
    {
        public List<ResortLink> ResortLinks { get; set; }
    }
}
