using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class ResortLinkViewData : SporthubViewData
    {
        public ResortLink ResortLink { get; set; }
        public string ResortPrettyUrl { get; set; }

        public ResortLinkViewData()
        {
        }
    }
}
