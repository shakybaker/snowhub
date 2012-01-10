using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sporthub.Mvc.ViewData;
using Sporthub.Model;
using Sporthub.Repository;
using Sporthub.Services;
using Sporthub.Utilities;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using Sporthub.Data;

namespace Sporthub.Mvc.Controllers
{
    [HandleError]
    public class ShopController : SporthubController
    {

        public ActionResult Index()
        {
            var viewData = new HomeViewData();

            List<Breadcrumb> breadcrumbs = new List<Breadcrumb>();
            breadcrumbs.Add(NewBreadcrumb("Home", "/"));
            breadcrumbs.Add(NewBreadcrumb("Shop", "/shop/index"));
            viewData.Breadcrumbs = breadcrumbs;

            return View("Index", viewData);
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
