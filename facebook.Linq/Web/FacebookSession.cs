using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using facebook.Linq;
using System.Diagnostics;
using facebook;
namespace facebook.Web
{

	public class FacebookContext
	{
		FacebookContext(HttpContext context)
		{
			Context = context;
		}

		HttpContext Context;
		public static FacebookContext Get(HttpContext context)
		{
			if (context == null)
				return null;
			var fc = context.Items["FacebookContext"] as FacebookContext;
			if (fc == null)
			{
				fc = new FacebookContext(context);
				context.Items["FacebookContext"] = fc;
			}
			return fc;
		}

		public static FacebookContext Current
		{
			get
			{
				return Get(HttpContext.Current);
			}
		}

		FacebookSession _Session;
		public FacebookSession Session
		{
			get
			{
				if (_Session == null)
				{
					var session = Context.Session;
					if (session == null)
						return null;
					_Session = session["FacebookSession"] as FacebookSession;
					if(_Session==null)
					{
						_Session = new FacebookSession();
						session["FacebookSession"] = _Session;
					}
				}
				return _Session;
			}
		}

		public bool TryAuthenticating(bool gotoLoginPageIfNeeded)
		{
			var x = Session.TryAuthenticating(Context.Request);
			if (!x)
			{
				RedirectToLogin();
			}
			return x;
		}

		public bool TryAuthenticating()
		{
			return TryAuthenticating(false);
		}


		public static bool HasValidConfiguration
		{
			get
			{
				return Facebook.HasValidConfiguration;
			}
		}

		public static string FacebookLoginUrl
		{
			get
			{
				return Facebook.FacebookLoginUrl;
			}
		}

		internal void RedirectTopFrame(string url)
		{
			var response = Context.Response;
			response.ContentType = "text/html";
			response.Cache.SetNoStore();
			response.Cache.SetCacheability(HttpCacheability.NoCache);
			response.Cache.SetExpires(DateTime.Now.AddDays(-1));

			response.Write("<script type=\"text/javascript\">\n" +
									 "if (parent != self) \n" +
									 "top.location.href = \"" + url + "\";\n" +
									 "else self.location.href = \"" + url + "\";\n" +
									 "</script>");
			response.End();
		}



		internal void RedirectToLogin()
		{
			var context = Context;
			var fc = this;
			var appName = context.Request["app"];
			if (appName.IsNullOrEmpty() && FacebookConfiguration.Current.UseTesterKey)
				appName = "coderuntester";
			if (appName.IsNotNullOrEmpty())
			{
				if (appName.Contains("?"))
					appName = appName.Substring(0, appName.IndexOf('?'));
				var requestUrl = context.Request.Url;
				//var redirectedPageQueryString = HttpUtility.UrlEncode("?" + HttpUtility.UrlDecode(context.Request.QueryString.ToString()).Replace('?', '&'));
				var url = String.Format("http://apps.facebook.com/{0}/?redirectTo={1}", appName, HttpUtility.UrlEncode(requestUrl.ToString()));// requestUrl.Host, requestUrl.AbsolutePath, redirectedPageQueryString);
				fc.RedirectTopFrame(url);
			}
			else if (Facebook.HasValidConfiguration)
			{
				fc.RedirectTopFrame(Facebook.FacebookLoginUrl);
			}
			else
			{
				context.Response.Write("Facebook configuration is missing. (facebook.Linq.APIKey, facebook.Linq.Secret, facebook.Linq.ApplicationID)");
			}
		}

	}


	public class FacebookSession
	{
		protected internal FacebookSession()
		{
		}

		public static FacebookSession Current
		{
		  get
		  {
				return FacebookContext.Current.Session;
		  }
		}

		
		public string SessionKey { get; set; }

		public long UserID { get; set; }

		public string AuthenticationToken { get; set; }

		public bool IsAuthenticated
		{
			get
			{
				return SessionKey.IsNotNullOrEmpty() && UserID>0;// && AuthenticationToken.IsNotNullOrEmpty();
			}
		}
		
		public bool TryAuthenticating(string fb_sig_session_key, string fb_sig_user, string auth_token)
		{
			if (IsAuthenticated)
				return true;		
			SessionKey = fb_sig_session_key;
			if (fb_sig_user.IsNotNullOrEmpty())
				UserID = Int64.Parse(fb_sig_user);
			AuthenticationToken = auth_token;
			if (IsAuthenticated)
			{
				UpdateApi();
				return true;
			}
			else
				return false;
		}


		void UpdateApi()
		{
			if (_Api != null)
			{
				_Api.ApplicationKey = Facebook.APIKey;
				_Api.Secret = Facebook.Secret;
				_Api.AuthToken = AuthenticationToken;
				_Api.SessionKey = SessionKey;
				_Api.uid = UserID;
			}
		}
		public bool TryAuthenticating(HttpRequest request)
		{
			return TryAuthenticating(request["fb_sig_session_key"], request["fb_sig_user"], request["auth_token"]);
		}

		API _Api;
		public API Api
		{
			get
			{
				if (_Api == null)
				{
					_Api = new API();
					UpdateApi();
				}
				return _Api;
			}
			set
			{
				_Api = value;
			}
		}

		FacebookDataContext _Database;
		public FacebookDataContext Database 
		{
			get
			{
				if (_Database == null)
					_Database = new FacebookDataContext(Api);
				return _Database;
			}
		}
	}



}
