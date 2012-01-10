using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sporthub.Mvc.ViewData;
using Sporthub.Model;
using Sporthub.Repository;
using Sporthub.Services;
using Sporthub.Utils;
using Sporthub.Model.Enumerators;

namespace Sporthub.Mvc.Controllers
{
    public class SiteController : SporthubController
    {
        public ActionResult AboutUs()
        {
            var vd = new SiteViewData();
            vd.Breadcrumbs = new List<Breadcrumb>();
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Home", Url = "/" });
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "About Us", Url = "/aboutus" });

            return View("AboutUs", vd);
        }

        public ActionResult ContactUs()
        {
            var vd = new SiteViewData();
            vd.Breadcrumbs = new List<Breadcrumb>();
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Home", Url = "/" });
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Contact Us", Url = "/contactus" });

            return View("ContactUs", vd);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult AboutUs(FormCollection form)
        {
            var email = form["email"];
            var message = form["message"];

            var emailService = new EmailService();
            try
            {
                emailService.GeneralEnquiry(email, message, HttpContext.Request.Url.Host.ToLower().Contains("localhost"));
                var html2 = "<strong style='color: #000;'>Thankyou!</strong>&nbsp;We will answer your enquiry as soon as possible.";
                Utilities.FeedbackManager.AddFeedback(FeedbackType.Thanks, html2);
            }
            catch (Exception ex)
            {
                var html3 = ex.Message;
                Utilities.FeedbackManager.AddFeedback(FeedbackType.Error, html3);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Error404()
        {
            var vd = new SiteViewData();
            vd.Breadcrumbs = new List<Breadcrumb>();
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Home", Url = "/" });
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Page not found", Url = "/404" });

            return View("404", vd);
        }

        public ActionResult IE()
        {
            var vd = new SiteViewData();
            vd.Breadcrumbs = new List<Breadcrumb>();
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Home", Url = "/" });
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Internet Explorer", Url = "/ie" });

            return View("ie", vd);
        }

        public ActionResult RegisterNonFacebook()
        {
            if (UserContext.UserIsLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            var vd = new SiteViewData();
            vd.Breadcrumbs = new List<Breadcrumb>();
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Home", Url = "/" });
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Register", Url = "/register" });

            return View("RegisterNonFacebook", vd);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult RegisterNonFacebook(FormCollection form)
        {
            var email = form["email"];

            var emailService = new EmailService();
            var message = string.Empty;
            try
            {
                emailService.NonFacebookRegistrationEnquiry(email, HttpContext.Request.Url.Host.ToLower().Contains("localhost"));
                var html2 = "<strong style='color: #000;'>Thankyou!</strong>&nbsp;We will contact you as soon as the registration process is ready.";
                Utilities.FeedbackManager.AddFeedback(FeedbackType.Thanks, html2);
            }
            catch (Exception ex)
            {
                var html3 = ex.Message;
                Utilities.FeedbackManager.AddFeedback(FeedbackType.Error, html3);
            }


            return RedirectToAction("Index", "Home");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]
        public ActionResult Advertise(FormCollection form)
        {
            var email = form["email"];
            var message = form["message"];

            var emailService = new EmailService();
            try
            {
                emailService.AdvertisingEnquiry(email, message, HttpContext.Request.Url.Host.ToLower().Contains("localhost"));
                var html2 = "<strong style='color: #000;'>Thankyou!</strong>&nbsp;We will contact you as soon as advertising is available.";
                Utilities.FeedbackManager.AddFeedback(FeedbackType.Thanks, html2);
            }
            catch (Exception ex)
            {
                var html3 = ex.Message;
                Utilities.FeedbackManager.AddFeedback(FeedbackType.Error, html3);
            }


            return RedirectToAction("Index", "Home");
        }

        public ActionResult TermsAndConditions()
        {
            var vd = new SiteViewData();
            vd.Breadcrumbs = new List<Breadcrumb>();
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Home", Url = "/" });
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Terms &amp; Conditions", Url = "/terms" });

            return View("TermsAndConditions", vd);
        }

        public ActionResult PostingGuidelines()
        {
            var vd = new SiteViewData();
            vd.Breadcrumbs = new List<Breadcrumb>();
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Home", Url = "/" });
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Posting Guidelines", Url = "/guidelines" });

            return View("PostingGuidelines", vd);
        }

        public ActionResult PrivacyPolicy()
        {
            var vd = new SiteViewData();
            vd.Breadcrumbs = new List<Breadcrumb>();
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Home", Url = "/" });
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Privacy Policy", Url = "/privacy" });

            return View("PrivacyPolicy", vd);
        }

        public ActionResult ComingSoon()
        {
            var vd = new SiteViewData();
            vd.Breadcrumbs = new List<Breadcrumb>();
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Home", Url = "/" });
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Coming Soon", Url = "/comingsoon" });

            return View("ComingSoon", vd);
        }

        public ActionResult Advertise()
        {
            var vd = new SiteViewData();
            vd.Breadcrumbs = new List<Breadcrumb>();
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Home", Url = "/" });
            vd.Breadcrumbs.Add(new Breadcrumb() { Name = "Advertise", Url = "/advertise" });

            return View("Advertise", vd);
        }

        public ActionResult Ignore()
        {
            //set cookie to ignore
            HttpCookie cookie = Request.Cookies["ignore_me"];
            if (cookie == null)
            {
                cookie = new HttpCookie("ignore_me");
            }

            cookie["Name"] = "ignore_me";
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }
    }
}
