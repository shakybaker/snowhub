using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class SessionViewData : SporthubViewData
    {
        public Session Session { get; set; }

        public SessionViewData()
        {
        }
    }
}
