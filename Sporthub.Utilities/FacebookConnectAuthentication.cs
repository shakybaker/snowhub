using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Configuration;

namespace Sporthub.Utilities
{
    public class FacebookConnectAuthentication
    {
        public FacebookConnectAuthentication()
        {

        }

        public static bool isConnected()
        {
            return (SessionKey != null && UserID != -1);
        }

        public static string ApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["fbAPIKey"];
            }
        }

        public static string SecretKey
        {
            get
            {
                return ConfigurationManager.AppSettings["fbSecret"];
            }
        }

        public static string SessionKey
        {
            get
            {
                return GetFacebookCookie("session_key");
            }
        }

        public static long UserID
        {
            get
            {
                long userID = -1;
                long.TryParse(GetFacebookCookie("user"), out userID);
                return userID;
            }
        }

        private static string GetFacebookCookie(string cookieName)
        {
            string retString = null;
            string fullCookie = ApiKey + "_" + cookieName;

            if (HttpContext.Current.Request.Cookies[fullCookie] != null)
                retString = HttpContext.Current.Request.Cookies[fullCookie].Value;

            return retString;
        }
    }
}
