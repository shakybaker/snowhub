using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class SporthubViewData
    {
        public string Controller { get; set; }
        public string Action { get; set; }

        public SporthubViewData()
        {
            Controller = string.Empty;
            Action = string.Empty;
        }
    }
}
