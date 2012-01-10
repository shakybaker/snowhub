using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using System.Globalization;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.Diagnostics;

namespace facebook.Web
{
	//[Remotable]
	class Facebook
	{
		static FacebookConfiguration Configuration;
		static Facebook()
		{
			Configuration = FacebookConfiguration.Current;
			APIKey = Configuration.APIKey;
			Secret = Configuration.Secret;
			ApplicationID = Configuration.ApplicationID;
			DefaultPage = Configuration.DefaultPage;
		}

		public static bool HasValidConfiguration
		{
			get
			{
				return  APIKey.IsNotNullOrEmpty() && ApplicationID.IsNotNullOrEmpty() && Secret.IsNotNullOrEmpty();
			}
		}

		internal static string APIKey { get; set; }

		public static string DefaultPage { get; set; }

		//[Remotable(false)]
		internal static string Secret { get; set; }

		internal static string ApplicationID { get; set; }

		public static bool IsSessionAuthenticated
		{
			get
			{
				return FacebookSession.Current.IsAuthenticated;
			}
		}

		private static string FACEBOOK_LOGIN_URL = @"http://www.facebook.com/login.php?api_key=";
		private static string FACEBOOK_CANVAS_PARAM = "&canvas";
		private static string FACEBOOK_VERSION_PARAM = "&v=1.0";
		public static string FacebookLoginUrl
		{
			get
			{
				return FACEBOOK_LOGIN_URL + APIKey + FACEBOOK_CANVAS_PARAM + FACEBOOK_VERSION_PARAM;;
			}
		}

		/// <summary>
		/// When an application requires a certain (new) permission, it needs to redirect the user to the URL provided by this method. After confirmation, HasApplicationPermission may be called again.
		/// </summary>
		/// <param name="perm"></param>
		/// <returns></returns>
		public string GetGrantAccessUrl(facebook.Types.Enums.Extended_Permissions perm)
		{
			//TODO: check for permission
			return "http://www.facebook.com/authorize.php?api_key=" + APIKey + "&v=1.0&ext_perm=" + perm.ToString();
		}	
	}
}
