using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace facebook.Web
{
	/// <summary>
	/// Configures the Facebook API
	/// </summary>
	class FacebookConfiguration
	{
		static FacebookConfiguration _Current;
		static object _Entrance = new object();
		public static FacebookConfiguration Current
		{
			get
			{
				if (_Current == null)
				{
					lock (_Entrance)
					{
						if (_Current == null)
						{
							_Current = new FacebookConfiguration();
							_Current.Load(ConfigurationManager.AppSettings);
						}
					}
				}
				return _Current;
			}
		}

		private void Load(System.Collections.Specialized.NameValueCollection settings)
		{
			APIKey = settings["facebook.Linq.APIKey"];
			Secret = settings["facebook.Linq.Secret"];
			ApplicationID = settings["facebook.Linq.ApplicationID"];
			UseTesterKey = settings["facebook.Linq.UseTesterKey"] == "true";
			DefaultPage = settings["facebook.Linq.DefaultPage"];
			Validate();
		}



		/// <summary>
		/// Facebook API Application Key (Length should be 32 chars)
		/// </summary>
		public string APIKey { get; set; }

		/// <summary>
		/// Facebook API Application Secret (Length should be 32 chars)
		/// </summary>
		public string Secret { get; set; }

		/// <summary>
		/// Facebook Application ID
		/// </summary>
		public string ApplicationID { get; set; }

		/// <summary>
		/// When set, the Facebook Application Tester application will be used. If you don't have a facebook application of your own, turn this on
		/// </summary>
		public bool UseTesterKey { get; set; }

		/// <summary>
		/// Default facebook entrance page that will be displayed when the user browses to the application
		/// </summary>		
		public string DefaultPage { get; set; }

		#region IValidatableConfiguration Members

		public void Validate()
		{
			if (UseTesterKey)
			{
				APIKey = "56776198fcd674ec896ad3d76bdde346";
				Secret = "f5983f1fd433d6d64f8f3317e57cd528";
				ApplicationID = "39301909849";
			}
			else
			{
				if (APIKey.IsNullOrEmpty() || APIKey.Length != 32)
					throw new Exception("Bad or missing Facebook facebook.Linq.APIKey");
				if (Secret.IsNullOrEmpty() || Secret.Length != 32)
					throw new Exception("Bad or missing Facebook facebook.Linq.Secret");
				if (ApplicationID.IsNullOrEmpty())
					throw new Exception("Missing Facebook facebook.Linq.ApplicationID");
			}
		}

		#endregion
	}
}
