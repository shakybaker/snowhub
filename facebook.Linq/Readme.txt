-------------------------------------------------------------------
Facebook Developer Toolkit Linq Addon
This library was developed as part of the CodeRun browser based IDE
http://www.coderun.com
-------------------------------------------------------------------

1. Preface 
The facebook.Linq library is an addon to the Facebook Developer Toolkit, 
it allows executing fql queries using Linq over the existing facebook developer kit model.

Example 1: (Linq Query Syntax)
var db = new FacebookDataContext();
var friendsIDs = from friend in db.User_Friends where friend.UserID1 == db.UserID select friend.UserID2;

Example 2: (Linq Method Syntax )
var db = new FacebookDataContext();
var friendsIDs = db.User_Friends.Where(t => t.UserID1 == db.UserID).Select(t => t.UserID2);


Benefits:
* Type checking of queries at compile time.
* Intellisense on queries at design time.
* Using facebook / anonymous objects for query results.

for more information regarding Linq:
http://msdn.microsoft.com/en-us/library/bb397926.aspx

	
2. Usage

------------------------------------------------
If you're new to the Facebook Developer Toolkit:
------------------------------------------------
Setup an application in Facebook
2.1 Go to http://www.facebook.com/developers/ and click on "+ Set Up New Application. Name it.
2.2 On the next screen
	Update the "Callback URL" with http://{Your application URL}/FacebookCallback.ashx
2.3 In the canvas tab - select "IFrame" as the "Render Method".
2.4 Click Save Changes.
2.5 On the next screen: make note of the following settings:
	* API Key
	* Secret
	* Application ID
		


Create a new Website
1. Import settings from web.config file that is bundled with the facebook.Linq assembly / project into your newly created website.
	 This includes all appSettings, and httpHandlers (or handlers in IIS7).
2. Edit the appSettings in your web.config file to match the API key, secret, and Application ID you noted earlier.

You're all set. 
paste this code sample in a page to test it:
if(FacebookContext.Current.TryAuthenticating(true))
{
	var db = new FacebookDataContext();
	var q1 = from friend in Database.friend_info where friend.uid1 == db.uid select friend.uid2;
	var result = q1.ToArray();
}

-------------------------------------------------------
If you're already using the Facebook Developer Toolkit:
-------------------------------------------------------
1. Add a reference to facebook.Linq.dll.
2. Create an instance of FacebookDataContext using a Facebook.API instance.	
	 Example: new FacebookDataContext(myCurrentUserApi);



The following steps are already made if you started with the "Facebook application" project template.

PREREQUISITES:

1. Add a reference to facebook.Linq.dll, facebook.dll, Microsoft.Xml.Schema.Linq
2. Make sure FacebookCallback.ashx exists and contains the following content:
	<%@ WebHandler Language="C#" Class="facebook.Web.FacebookCallback, facebook.Linq, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" %>
--------------

FACEBOOK SETUP:

	
2.3 Edit debug.setup and update the correct settings from 2.3. (They are also available at http://www.facebook.com/developers/apps.php )
	<appSettings>
		...
		<add key="facebook.Linq.APIKey" value="XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"></add>
		<add key="facebook.Linq.Secret" value="XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"></add>
		<add key="facebook.Linq.ApplicationID" value="XXXXXXXXXXX"></add>
		...
	</appSettings>

WebSite Setup

--------------

2.5 In your application, you may obtain an instance of FacebookDatabase . It contains a property for each table in the facebook API.
	  For more information, visit http://wiki.developers.facebook.com/index.php/FQL

    You are done! Good luck, and remember: "If you build it, They will come".



3. Thid-party tools

This integration toolkit may contain portions of the following projects:

3.1 Facebook Developer Toolkit ( http://www.codeplex.com/FacebookToolkit ):
	  License is available at http://www.codeplex.com/FacebookToolkit/license
