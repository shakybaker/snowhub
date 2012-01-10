using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;
using Sporthub.Model.Enumerators;

namespace Sporthub.Mvc.ViewData
{
    public class NewsListViewData : SporthubViewData
    {
        public IList<NewsFeed> NewsFeeds { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; }
    }
}
