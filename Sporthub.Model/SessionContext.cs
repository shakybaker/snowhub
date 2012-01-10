using System.Web;

namespace Sporthub.Model
{
    public class SessionContext
    {
        private static readonly string SESSION = "SessionModel";

        private SessionContext() 
        {
        }

        public static Session CurrentSession
        {
            get { return (Session)HttpContext.Current.Session[SESSION]; }
            set { HttpContext.Current.Session[SESSION] = value; }
        }


    }
}
