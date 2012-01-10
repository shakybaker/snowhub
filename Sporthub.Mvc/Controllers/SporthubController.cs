using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
//using Newtonsoft.Json.Linq;
using Sporthub.Model;
using Sporthub.Mvc.Code;
using Sporthub.Services;
using Sporthub.Repository;

namespace Sporthub.Mvc.Controllers
{
    public class SporthubController : Controller
    {
        private readonly UserRepository _userRepository = new UserRepository();
        private UserService _userService;
        private bool doRedirect = false;

        protected override void Initialize(RequestContext requestContext)
        {
            _userService = new UserService(_userRepository);

            FuckInternetExplorerFuckItWithABroom(requestContext);

            if (SessionContext.CurrentSession == null)
                SessionContext.CurrentSession = new Session();
            SessionContext.CurrentSession.IsNotTracked = CheckIgnoreCookie(requestContext);

            if (SessionContext.CurrentSession.IsLogout)
            {
                if (!UserContext.UserIsLoggedIn())
                    UserContext.CurrentUser = PopulateUserContextFromCookie(requestContext);
                //UserContext.CurrentUser = CheckFacebookLogin(UserContext.CurrentUser, requestContext);
            }
            else
            {
                SessionContext.CurrentSession.IsLogout = false;
            }

            if (!requestContext.HttpContext.Request.IsAjaxRequest())
            {
                if (doRedirect)
                    requestContext.HttpContext.Response.Redirect("/user");
            }

            base.Initialize(requestContext);
        }

        private static void FuckInternetExplorerFuckItWithABroom(RequestContext requestContext)
        {
            if (requestContext.HttpContext.Request.RawUrl.EndsWith("/ie")) return;
            var browser = requestContext.HttpContext.Request.Browser;
            if (browser.Browser == "IE" && browser.MajorVersion < 8)
                requestContext.HttpContext.Response.Redirect("/ie");
        }

        //private User CheckFacebookLogin(User currentUser, RequestContext requestContext)
        //{
        //    if (currentUser != null)
        //    {
        //        if (currentUser.ID > 0)
        //        {
        //            if (string.IsNullOrEmpty(currentUser.FacebookUid))
        //                return currentUser;
        //            if (currentUser.IsLoggedInToFacebook)
        //                return currentUser;
        //        }
        //    }

        //    try
        //    {
        //        try
        //        {
        //            var args = FacebookGraphAPI.GetUserFromCookie(requestContext.HttpContext.Request.Cookies,
        //                                                          "154838327354", "b07023e6cc8e73375ea2c86badff4e87");
        //            if (args != null)
        //            {
        //                var fbUser = userService.GetByFacebookId(args["uid"]);
        //                if (fbUser != null)
        //                {
        //                    currentUser = fbUser;
        //                    currentUser.IsLoggedInToFacebook = true;
        //                }
        //                if ((!requestContext.HttpContext.Request.IsAuthenticated) && (currentUser != null))
        //                {
        //                    FormsAuthentication.SetAuthCookie(currentUser.UserName, false);
        //                    requestContext.HttpContext.Response.Cookies.Add(new HttpCookie("sh-user") { Value = currentUser.UserName });
        //                }
        //                doRedirect = true;
        //            }
        //        }
        //        catch (FacebookGraphAPIException ex)
        //        {
                    
        //            throw;
        //        }
        //    }
        //    catch (FacebookGraphAPIException ex) { }
        //    catch (Exception ex) { }

        //    return currentUser;
        //}

        public bool CheckIgnoreCookie(RequestContext requestContext)
        {
            HttpCookie cookie = requestContext.HttpContext.Request.Cookies["ignore_me"];
            return cookie != null;
        }

        //public static user GetFacebookUserByUid(long uid, FacebookDataContext db)
        //{
        //    return db.user.Where(p => p.uid == uid).FirstOrDefault();
        //}

        public User PopulateUserContextFromCookie(RequestContext context)
        {
            _userService = new UserService(_userRepository);
            var cookie = context.HttpContext.Request.Cookies[".ASPXFORMSAUTH"];
            if (cookie != null)
            {
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                if (ticket != null)
                {
                    var user = _userService.GetByUserName(ticket.Name);
                    if (user != null)
                    {
                        user.LastVisitDate = DateTime.Now;
                        _userService.Update(user);
                        return user;
                    }
                }
            }
            return null;
        }

        public Breadcrumb NewBreadcrumb(string name, string url)
        {
            return new Breadcrumb {Name = name, Url = url};
        }
    }
}
//facebook.Components.FacebookService fb = new facebook.Components.FacebookService()
//{ 
//    ApplicationKey = FacebookConnectAuthentication.ApiKey, 
//    Secret = FacebookConnectAuthentication.SecretKey
//};

//api.CreateSession("lalala");
//var sess = api.auth.getSession("lalala");
//api.SessionKey = sess.session_key;
//api.SessionKey = sess.session_key;

//try
//{
//    var testUsr = GetFacebookUserByUid((long)533493250, db);

//    var friendIDs = from friend in db.friend_info
//                    where friend.uid1 ==
//                        533493250
//                    select friend.uid2;
//    var friendDetails = from u in db.user
//                        where friendIDs.Contains(u.uid)
//                        select new { Name = u.name, Picture = u.pic_small };
//    ViewData["BUMHOLE"] = friendIDs.Count();

//}
//catch (Exception ex)
//{
//    ViewData["BUMHOLE"] = ex.Message;
//    //API api = new API();
//    //api.users..LogOff();
//}
