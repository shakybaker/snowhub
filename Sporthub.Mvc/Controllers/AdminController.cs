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
using System.Web.Security;
using Sporthub.Utilities;


namespace Sporthub.Mvc.Controllers
{
    public class AdminController : SporthubController
    {
        public ActionResult Index()
        {
            if (!IsAdmin())
            {
                //return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public bool IsAdmin()
        {
            //if (FacebookConnectAuthentication.isConnected())
            //{
                if (Sporthub.Model.UserContext.UserIsLoggedIn())
                {
                    if (Sporthub.Model.UserContext.CurrentUser.UserRole.IsAdmin)
                    {
                        return true;
                    }
                }
                else
                {
                    FormsAuthentication.SignOut();
                }
            //}
            return false;
        }


    }
}
