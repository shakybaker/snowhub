using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Net.Mail;

namespace Sporthub.Mvc
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // sample url: aboutus/
            routes.MapRoute(
                "Ignore",
                "ignore/",
                new { controller = "Site", action = "Ignore" }
            );

            // sample url: aboutus/
            routes.MapRoute(
                "AboutUs",
                "aboutus/",
                new { controller = "Site", action = "AboutUs" }
            );

            // sample url: aboutus/
            routes.MapRoute(
                "404",
                "404/",
                new { controller = "Site", action = "Error404" }
            );

            // sample url: aboutus/
            routes.MapRoute(
                "ie",
                "ie/",
                new { controller = "Site", action = "IE" }
            );

            // sample url: register/
            routes.MapRoute(
                "ContactUs",
                "contactus/",
                new { controller = "Site", action = "ContactUs" }
            );

            // sample url: register/
            routes.MapRoute(
                "RegisterNonFacebook",
                "register/",
                new { controller = "Site", action = "RegisterNonFacebook" }
            );

            // sample url: advertise/
            routes.MapRoute(
                "Advertise",
                "advertise/",
                new { controller = "Site", action = "Advertise" }
            );

            // sample url: terms/
            routes.MapRoute(
                "TermsAndConditions",
                "terms/",
                new { controller = "Site", action = "TermsAndConditions" }
            );

            // sample url: guidelines/
            routes.MapRoute(
                "PostingGuidelines",
                "guidelines/",
                new { controller = "Site", action = "PostingGuidelines" }
            );

            // sample url: privacy/
            routes.MapRoute(
                "PrivacyPolicy",
                "privacy/",
                new { controller = "Site", action = "PrivacyPolicy" }
            );

            // sample url: comingsoon/
            routes.MapRoute(
                "ComingSoon",
                "comingsoon/",
                new { controller = "Site", action = "ComingSoon" }
            );

            // sample url: review/
            routes.MapRoute(
                "Review",
                "review/{id}/",
                new { controller = "Review", action = "Edit", id = string.Empty }
            );
            // sample url: airports/
            routes.MapRoute(
                "Airports",
                "airports/",
                new { controller = "Airports", action = "Index" }
            );
            // sample url: news/
            routes.MapRoute(
                "News",
                "news/",
                new { controller = "News", action = "List", type = "all" }
            );
            // sample url: news/snowboard
            routes.MapRoute(
                "News2",
                "news/{0}/",
                new { controller = "News", action = "List", type = string.Empty }
            );
            // sample url: user/edit
            routes.MapRoute(
                "ProfileEdit",
                "user/edit/",
                new { controller = "Account", action = "Edit" }
            );
            // sample url: user/addpicture
            routes.MapRoute(
                "AddProfilePicture",
                "user/addpicture/",
                new { controller = "Account", action = "AddPicture" }
            );
            // sample url: user/resorts
            routes.MapRoute(
                "UserResorts",
                "user/resorts/",
                new { controller = "Account", action = "EditResorts" }
            );
            // sample url: user/invite
            routes.MapRoute(
                "InviteFriends",
                "user/invite/",
                new { controller = "Account", action = "InviteFriends" }
            );
            // sample url: registersuccess
            routes.MapRoute(
                "RegisterSuccess",
                "RegisterSuccess/",
                new { controller = "Account", action = "RegisterSuccess" }
            );
            // sample url: user/shaky
            routes.MapRoute(
                "Profile",
                "user/{id}/",
                new { controller = "Account", action = "Profile", id = string.Empty }
            );
            // sample url: forums/1
            routes.MapRoute(
                "ForumThreads",
                "forums/{forumid}",
                new { controller = "Forums", action = "Threads", forumid = string.Empty }
            );
            // sample url: newthread/1
            routes.MapRoute(
                "NewThread",
                "newthread/{forumid}",
                new { controller = "Forums", action = "NewThread", forumid = string.Empty }
            );
            // sample url: forums/
            routes.MapRoute(
                "ForumsMain",
                "forums/",
                new { controller = "Forums", action = "List" }
            );
            // sample url: forums/1
            routes.MapRoute(
                "ThreadPosts",
                "forums/{forumid}/thread/{threadid}",
                new { controller = "Forums", action = "Posts", forumid = string.Empty, threadid = string.Empty }
            );
            // sample url: resorts/list
            routes.MapRoute(
                "ResortListWorld",
                "resorts/",
                new { controller = "Resorts", action = "ListWorld" }
            );
            routes.MapRoute(
                "ResortListWorld2",
                "resorts/list",
                new { controller = "Resorts", action = "List", location = string.Empty }
            );
            // sample url: resorts/chamonix
            routes.MapRoute(
                "ResortOverview",
                "resorts/{name}",
                new { controller = "Resorts", action = "Overview", name = string.Empty }
            );
            // sample url: resorts/checkin/chamonix
            routes.MapRoute(
                "ResortCheckIn",
                "resorts/checkin/{name}",
                new { controller = "Resorts", action = "CheckIn", name = string.Empty }
            );
            // sample url: resorts/checkout/chamonix
            routes.MapRoute(
                "ResortCheckOut",
                "resorts/checkout/{name}",
                new { controller = "Resorts", action = "CheckOut", name = string.Empty }
            );
            // sample url: resorts/chamonix/map
            routes.MapRoute(
                "ResortMap",
                "resorts/{name}/map",
                new { controller = "Resorts", action = "Map", name = string.Empty }
            );
            // sample url: resorts/chamonix/photos
            routes.MapRoute(
                "ResortPhotos",
                "resorts/{name}/photos",
                new { controller = "Resorts", action = "Photos", name = string.Empty }
            );
            // sample url: resorts/chamonix/places
            routes.MapRoute(
                "ResortPlaces",
                "resorts/{name}/places",
                new { controller = "Resorts", action = "Places", name = string.Empty }
            );
            // sample url: resorts/chamonix/videos
            routes.MapRoute(
                "ResortVideos",
                "resorts/{name}/videos",
                new { controller = "Resorts", action = "Videos", name = string.Empty }
            );
            // sample url: resorts/chamonix/webcams
            routes.MapRoute(
                "ResortWebcams",
                "resorts/{name}/webcams",
                new { controller = "Resorts", action = "Webcams", name = string.Empty }
            );
            // sample url: resorts/chamonix/reviews/1
            routes.MapRoute(
                "ResortReview",
                "resorts/{name}/reviews/{id}",
                new { controller = "Resorts", action = "Reviews", name = string.Empty, id = string.Empty }
            );
            //// sample url: resorts/chamonix/reviews
            //routes.MapRoute(
            //    "ResortReviews",
            //    "resorts/{name}/reviews",
            //    new { controller = "Resorts", action = "Reviews", name = string.Empty }
            //);
            // sample url: resorts/chamonix/info
            routes.MapRoute(
                "ResortInfo",
                "resorts/{name}/info",
                new { controller = "Resorts", action = "Info", name = string.Empty }
            );
            // sample url: resorts/europe/list
            routes.MapRoute(
                "ResortList",
                "resorts/{location}/list",
                new { controller = "Resorts", action = "List", location = string.Empty }
            );
            routes.MapRoute(
                "ResortJson",
                "resorts/{location}",
                new { controller = "Resorts", action = "List", location = string.Empty }
            );
            // sample url: resorts/chamonix
            routes.MapRoute(
                "AirportView",
                "airports/{name}",
                new { controller = "Airports", action = "View", name = string.Empty }
            );


            //------------------------
            // ADMIN
            //------------------------
            
            
            // sample url: admin/resorts/world/list
            routes.MapRoute(
                "AdminResortList",
                "admin/resorts/list/{letter}",
                new { controller = "AdminResorts", action = "List", location = "world", name = string.Empty, letter = string.Empty }
            );
            // sample url: admin/resorts/add
            routes.MapRoute(
                "AdminResortAdd",
                "admin/resorts/add",
                new { controller = "AdminResorts", action = "AddResort", location = "" }
            );
            // sample url: admin/resorts/meribel/default
            routes.MapRoute(
                "AdminResortDefault",
                "admin/resorts/{name}/default",
                new { controller = "AdminResorts", action = "Default", location = "" }
            );
            // sample url: admin/resorts/meribel/mountain
            routes.MapRoute(
                "AdminResortMountain",
                "admin/resorts/{name}/mountain",
                new { controller = "AdminResorts", action = "Mountain", location = "" }
            );
            // sample url: admin/resorts/meribel/lifts
            routes.MapRoute(
                "AdminResortLifts",
                "admin/resorts/{name}/lifts",
                new { controller = "AdminResorts", action = "Lifts", location = "" }
            );
            // sample url: admin/resorts/meribel/runs
            routes.MapRoute(
                "AdminResortRuns",
                "admin/resorts/{name}/runs",
                new { controller = "AdminResorts", action = "Runs", location = "" }
            );
            // sample url: admin/resorts/meribel/parks
            routes.MapRoute(
                "AdminResortParks",
                "admin/resorts/{name}/parks",
                new { controller = "AdminResorts", action = "Parks", location = "" }
            );
            // sample url: admin/resorts/meribel/links
            routes.MapRoute(
                "AdminResortLinks",
                "admin/resorts/{name}/links",
                new { controller = "AdminResorts", action = "Links", location = "" }
            );



            //REMOVE THIS ROUTE!!!
            // sample url: admin/resorts/meribel/edit
            routes.MapRoute(
                "AdminResortEdit",
                "admin/resorts/{name}/edit",
                new { controller = "AdminResorts", action = "Edit", location = "" }
            );

            // sample url: admin/resortlinks/chamonix/2/edit
            routes.MapRoute(
                "AdminResortLinksEdit",
                "admin/resortlinks/{name}/{id}/edit",
                new { controller = "AdminResorts", action = "EditResortLink", id = "", name  = "" }
            );

            
            //------------------------



            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            EmailException(ex);
        }

        private void EmailException(Exception ex)
        {
            MailMessage mail = new MailMessage();
            //set the addresses
            mail.From = new MailAddress("info@thesnowhub.com", "Snowhub Errors");
            var mailTo = new MailAddress("shakybaker@gmail.com", "Mark Baker");
            mail.To.Add(mailTo);
            //set the content
            mail.Subject = "An exception occurred.";
            mail.Body = ex.ToString();
            //send the message
            var smtp = new SmtpClient();

            smtp.Host = "smtp.thesnowhub.com";
        }

    }
}