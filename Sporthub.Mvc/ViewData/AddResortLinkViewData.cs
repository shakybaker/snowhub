using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class AddResortLinkViewData : SporthubViewData
    {
        public string Message { get; set; }
        public ResortLink ResortLink { get; set; }
    }
}
