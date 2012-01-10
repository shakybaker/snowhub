using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class NewsFeedItemsViewData : SporthubViewData
    {
        public List<NewsFeedItem> Items { get; set; }
        public string Message { get; set; }
    }
}
