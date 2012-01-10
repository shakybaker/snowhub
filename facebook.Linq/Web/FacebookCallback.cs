using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace facebook.Web
{
	/// <summary>
	/// Facebook callback URL should be pointed to here
	/// </summary>
	public class FacebookCallback : IHttpHandler, IRequiresSessionState
	{
		public void ProcessRequest(HttpContext context)
		{
			var fc = FacebookContext.Get(context);
			var session = fc.Session;
			var authenticated = session.IsAuthenticated;
			if (!authenticated)
				authenticated = session.TryAuthenticating(context.Request);
			if (authenticated)
			{
				var page = Facebook.DefaultPage ?? "./?";
				if (page.Contains("?"))
					page += "&";
				else
					page += "?";
				page += HttpUtility.UrlDecode(context.Request.QueryString.ToString());
				context.Response.Redirect(page);
			}
			else
			{
				fc.RedirectToLogin();
			}
		}



		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}
