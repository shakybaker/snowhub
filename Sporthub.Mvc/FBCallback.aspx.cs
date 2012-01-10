using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Sporthub.Mvc.Code;

namespace Sporthub.Mvc
{
    public partial class FBCallback : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = "";
            oAuthFacebook oAuth = new oAuthFacebook();

            if (Request["code"] == null)
            {
                //Redirect the user back to Facebook for authorization.
                Response.Redirect(oAuth.AuthorizationLinkGet());
            }
            else
            {
                //Get the access token and secret.
                oAuth.AccessTokenGet(Request["code"]);

                if (oAuth.Token.Length > 0)
                {
                    //We now have the credentials, so we can start making API calls
                    //url = "https://graph.facebook.com/me/picture?access_token=" + oAuth.Token;
                    url = "https://graph.facebook.com/me?fields=id,name,picture&access_token=" + oAuth.Token;
                    string json = oAuth.WebRequest(oAuthFacebook.Method.GET, url, String.Empty);
                }
            }
        }
    }
}
