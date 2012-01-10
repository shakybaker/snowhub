using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sporthub.Data;
using Sporthub.Mvc.ViewData;
using Sporthub.Model;
using Sporthub.Model.Enumerators;

namespace Sporthub.Mvc.Controllers
{
    public class NewsController : SporthubController
    {
        public ActionResult List(string type)
        {
            NewsListViewData vd = new NewsListViewData();
            vd.NewsFeeds = NewsDataManager.GetNewsFeeds(3);
            vd.Breadcrumbs = new List<Breadcrumb>();
            vd.Breadcrumbs.Add(NewBreadcrumb("Home", "/"));
            vd.Breadcrumbs.Add(NewBreadcrumb("News", "/news"));
            vd.Breadcrumbs.Add(NewBreadcrumb("All", "/news/all"));

            return View("List", vd);
        }


        public Breadcrumb NewBreadcrumb(string name, string url)
        {
            Breadcrumb bc = new Breadcrumb();
            bc.Name = name;
            bc.Url = url;

            return bc;
        }
    }
}
