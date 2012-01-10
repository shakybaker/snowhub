using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;
using Sporthub.Model.Enumerators;

namespace Sporthub.Mvc.ViewData
{
    public class ThreadsListViewData : SporthubViewData
    {
        public Forum Forum { get; set; }
        public IList<Thread> Threads { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; }
    }
}
