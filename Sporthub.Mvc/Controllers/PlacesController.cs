using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sporthub.Data;
using Sporthub.Model;
using Sporthub.Model.Enumerators;
using Sporthub.Services;
using Sporthub.Repository;
using Sporthub.Utils;

using Sporthub.Mvc.ViewData;
using System.Data;
using System.Configuration;
using Sporthub.Utilities;

namespace Sporthub.Mvc.Controllers
{
    public class PlacesController : SporthubController
    {
        public ActionResult AddStep1()
        {
            return View("AddPlace");
        }
    }
}
