using System.Web;

namespace Sporthub.Model
{
    public class UserContext
    {
        private const string USER = "UserModel";

        private UserContext() { }

        public static User CurrentUser
        {
            get { return (User)HttpContext.Current.Session[USER]; }
            set { HttpContext.Current.Session[USER] = value; }
        }

        public static bool UserIsLoggedIn()
        {
            if (CurrentUser != null)
                return CurrentUser.ID > 0;
            return false;
        }

        public static bool UserIsAdmin()
        {
            if (UserIsLoggedIn())
            {
                if (CurrentUser.UserRole.IsAdmin)
                    return true;
            }
            return false;
        }
    }
}
