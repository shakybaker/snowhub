///// Copyright 2010 Hernan Amiune (hernan.amiune.com)
///// Licensed under MIT license:
///// http://www.opensource.org/licenses/mit-license.php
///// 
///// Requires Newtonsoft.Json.Linq.JObject
///// http://james.newtonking.com/projects/json-net.aspx
///// Based on the Official Python client library for the Facebook Platform
///// http://github.com/facebook/python-sdk/
///// 
///// C# client library for the Facebook Platform
///// 
///// This client library is designed to support the Graph API and the official
///// Facebook JavaScript SDK, which is the canonical way to implement
///// Facebook authentication. Read more about the Graph API at
///// http://developers.facebook.com/docs/api. You can download the Facebook
///// JavaScript SDK at http://github.com/facebook/connect-js/.

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Text;
//using System.Security.Cryptography;
//using System.Net;
//using Newtonsoft.Json.Linq;
//using System.IO;

//namespace Sporthub.Mvc.Code
//{

//    /// <summary>
//    /// A client for the Facebook Graph API.
//    /// 
//    /// See http://developers.facebook.com/docs/api for complete documentation
//    /// for the API.
//    /// 
//    /// The Graph API is made up of the objects in Facebook (e.g., people, pages,
//    /// events, photos) and the connections between them (e.g., friends,
//    /// photo tags, and event RSVPs). This client provides access to those
//    /// primitive types in a generic way. For example, given an OAuth access
//    /// token, this will fetch the profile of the active user and the list
//    /// of the user's friends:
//    /// 
//    ///    var facebook = new FacebookGraphAPI(args["access_token"]);
//    ///    var user = facebook.GetObject("me", null);
//    ///    var friends = facebook.GetConnections("me", "friends", null);
//    /// 
//    /// You can see a list of all of the objects and connections supported
//    /// by the API at http://developers.facebook.com/docs/reference/api/.
//    /// 
//    /// You can obtain an access token via OAuth or by using the Facebook
//    /// JavaScript SDK. See http://developers.facebook.com/docs/authentication/
//    /// for details.
//    /// 
//    /// If you are using the JavaScript SDK, you can use the
//    /// get_user_from_cookie() method below to get the OAuth access token
//    /// for the active user from the cookie saved by the SDK.
//    /// </summary>
//    public class FacebookGraphAPI
//    {
//        string accessToken = null;

//        public FacebookGraphAPI(string accessToken)
//        {
//            this.accessToken = accessToken;
//        }

//        /// <summary>
//        /// Fetchs the given object from the graph.
//        /// </summary>
//        /// <param name="id">Id of the object to fetch</param>
//        /// <param name="args">List of arguments</param>
//        /// <returns>The required object</returns>
//        public JObject GetObject(string id, Dictionary<string, string> args)
//        {
//            return Request(id, args, null);
//        }

//        /// <summary>
//        /// Fetchs all of the given object from the graph.
//        /// </summary>
//        /// <param name="args"></param>
//        /// <param name="ids">Ids of the objects to return</param>
//        /// <returns>
//        /// A map from ID to object. If any of the IDs are invalid, an exception is raised
//        /// </returns>
//        public JObject GetObjects(Dictionary<string, string> args, params string[] ids)
//        {
//            string joinedIds = "";
//            for (int i = 0; i < ids.Length; i++) if (i == 0) joinedIds += ids[i]; else joinedIds += "," + ids[i];
//            if (args == null) args = new Dictionary<string, string>();
//            args["ids"] = joinedIds;

//            return Request("", args, null);
//        }

//        /// <summary>
//        /// Fetchs the connections for given object.
//        /// </summary>
//        /// <param name="id">Id of the object to fetch</param>
//        /// <param name="connectionName">Name of the connection</param>
//        /// <param name="args">List of arguments</param>
//        /// <returns>A JObject containing the required connections</returns>
//        public JObject GetConnections(string id, string connectionName, Dictionary<string, string> args)
//        {
//            return Request(id + "/" + connectionName, args, null);
//        }

//        /// <summary>
//        /// Writes the given object to the graph, connected to the given parent.
//        /// 
//        /// For example,
//        /// 
//        ///     var data = new Dictionary&lt;string, string&gt;();
//        ///     data.Add("message", "Hello, world");
//        ///     facebook.PutObject("me", "feed", data);
//        /// 
//        /// writes "Hello, world" to the active user's wall.
//        /// 
//        /// See http://developers.facebook.com/docs/api#publishing for all of
//        /// the supported writeable objects.
//        /// 
//        /// Most write operations require extended permissions. For example,
//        /// publishing wall posts requires the "publish_stream" permission. See
//        /// http://developers.facebook.com/docs/authentication/ for details about
//        /// extended permissions.
//        /// </summary>
//        /// <param name="parentObject">The parent object</param>
//        /// <param name="connectionName">The connection name</param>
//        /// <param name="data">Post data</param>
//        /// <returns>A JObject with the result of the operation</returns>
//        public JObject PutObject(string parentObject, string connectionName, Dictionary<string, string> data)
//        {
//            if (this.accessToken == null) throw new FacebookGraphAPIException("Authentication", "Access Token Required");
//            return Request(parentObject + "/" + connectionName, null, data);
//        }


//        /// <summary>
//        /// Writes a wall post to current user wall.
//        /// 
//        /// We default to writing to the authenticated user's wall if no
//        /// profile_id is specified.
//        /// 
//        /// attachment adds a structured attachment to the status message being
//        /// posted to the Wall. It should be a dictionary of the form:
//        /// 
//        ///     {"name": "Link name"
//        ///      "link": "http://www.example.com/",
//        ///      "caption": "{*actor*} posted a new review",
//        ///      "description": "This is a longer description of the attachment",
//        ///      "picture": "http://www.example.com/thumbnail.jpg"}
//        /// </summary>
//        /// <param name="message">The message to put on the wall</param>
//        /// <param name="attachment">Optional attachment for the message</param>
//        /// <returns>A JObject with the result of the operation</returns>
//        public JObject PutWallPost(string message, Dictionary<string, string> attachment)
//        {
//            if (attachment == null) attachment = new Dictionary<string, string>();
//            attachment.Add("message", message);
//            return PutObject("me", "feed", attachment);
//        }

//        /// <summary>
//        /// Writes a wall post to the given profile's wall.
//        /// 
//        /// We default to writing to the authenticated user's wall if no
//        /// profile_id is specified.
//        /// 
//        /// attachment adds a structured attachment to the status message being
//        /// posted to the Wall. It should be a dictionary of the form:
//        /// 
//        ///     {"name": "Link name"
//        ///      "link": "http://www.example.com/",
//        ///      "caption": "{*actor*} posted a new review",
//        ///      "description": "This is a longer description of the attachment",
//        ///      "picture": "http://www.example.com/thumbnail.jpg"}
//        /// </summary>
//        /// <param name="message">The message to put on the wall</param>
//        /// <param name="attachment">Optional attachment for the message</param>
//        /// <param name="profileId">The profile where the message is goint to be put</param>
//        /// <returns>A JObject with the result of the operation</returns>
//        public JObject PutWallPost(string message, Dictionary<string, string> attachment, string profileId)
//        {
//            if (attachment == null) attachment = new Dictionary<string, string>();
//            attachment.Add("message", message);
//            return PutObject(profileId, "feed", attachment);
//        }

//        /// <summary>
//        /// Writes the given comment on the given post.
//        /// </summary>
//        /// <param name="objectId">Id of the object</param>
//        /// <param name="message">Message</param>
//        /// <returns>A JObject with the result of the operation</returns>
//        public JObject PutComment(string objectId, string message)
//        {
//            var args = new Dictionary<string, string>();
//            args.Add("message", message);
//            return PutObject(objectId, "comments", args);
//        }

//        /// <summary>
//        /// Likes the given post.
//        /// </summary>
//        /// <param name="objectId">Id of the object to be like</param>
//        /// <returns>A JObject with the result of the operation</returns>
//        public JObject PutLike(string objectId)
//        {
//            return PutObject(objectId, "likes", null);
//        }

//        /// <summary>
//        /// Deletes the object with the given ID from the graph.
//        /// </summary>
//        /// <param name="id">Id of the object to delete</param>
//        /// <returns>A JObject with the result of the operation</returns>
//        public JObject DeleteObject(string id)
//        {
//            var postArgs = new Dictionary<string, string>();
//            postArgs.Add("method", "delete");
//            return Request(id, null, postArgs);
//        }

//        /// <summary>
//        /// Fetches the given path in the Graph API.
//        /// 
//        /// Translates args to a valid query string. If post_args is given,
//        /// sends a POST request to the given path with the given arguments.
//        /// </summary>
//        /// <param name="path">The path where the request is to be send</param>
//        /// <param name="args">The Query arguments</param>
//        /// <param name="postArgs">The POST arguments</param>
//        /// <returns>A JObject of the facebook response</returns>
//        private JObject Request(string path, Dictionary<string, string> args, Dictionary<string, string> postArgs)
//        {
//            if (args == null) args = new Dictionary<string, string>();
//            if (this.accessToken != null)
//            {
//                if (postArgs != null) postArgs["access_token"] = this.accessToken;
//                else args["access_token"] = this.accessToken;
//            }

//            string postData = null;
//            if (postArgs != null) postData = EncodeDictionary(postArgs);

//            string reply = "";
//            using (WebClient wc = new WebClient())
//            {
//                wc.Encoding = System.Text.Encoding.UTF8;
//                try
//                {
//                    if (postArgs == null)
//                    {
//                        reply = wc.DownloadString("https://graph.facebook.com/" + path + "?" + EncodeDictionary(args));
//                    }
//                    else
//                    {
//                        wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
//                        reply = wc.UploadString("https://graph.facebook.com/" + path + "?" + EncodeDictionary(args), "POST", postData);
//                    }
//                }
//                catch (WebException ex)
//                {
//                    StreamReader SR = new StreamReader(ex.Response.GetResponseStream(), wc.Encoding);
//                    reply = SR.ReadToEnd();
//                }
//            }

//            JObject jo = JObject.Parse(reply);
//            if (jo["error"] != null)
//                throw new FacebookGraphAPIException(jo["error"]["type"].ToString(),
//                    jo["error"]["message"].ToString());

//            return jo;
//        }

//        /// <summary>
//        /// Encodes a dictionary keys to send them via HTTP request
//        /// </summary>
//        /// <param name="dict">Dictionary to be encoded</param>
//        /// <returns>Encoded dictionary keys</returns>
//        private string EncodeDictionary(Dictionary<string, string> dict)
//        {
//            string ret = "";
//            if (dict != null)
//            {
//                foreach (var item in dict)
//                    ret += HttpUtility.UrlEncode(item.Key) + "=" + HttpUtility.UrlEncode(item.Value) + "&";
//                ret = ret.TrimEnd('&');
//            }
//            return ret;
//        }

//        /// <summary>
//        /// Parses the cookie set by the official Facebook JavaScript SDK.
//        /// 
//        /// cookies should be a dictionary-like object mapping cookie names to
//        /// cookie values.
//        /// 
//        /// If the user is logged in via Facebook, we return a dictionary with the
//        /// keys "uid" and "access_token". The former is the user's Facebook ID,
//        /// and the latter can be used to make authenticated requests to the Graph API.
//        /// If the user is not logged in, we return None.
//        /// 
//        /// Download the official Facebook JavaScript SDK at
//        /// http://github.com/facebook/connect-js/. Read more about Facebook
//        /// authentication at http://developers.facebook.com/docs/authentication/.
//        /// </summary>
//        /// <param name="cookies">HttpCookieCollection</param>
//        /// <param name="appId">Facebook Application Id</param>
//        /// <param name="appSecret">Facebook Application Secret</param>
//        /// <returns>Dictionary with the keys "uid" and "access_token"</returns>
//        public static Dictionary<string, string> GetUserFromCookie(HttpCookieCollection cookies, string appId, string appSecret)
//        {
//            var args = new Dictionary<string, string>();
//            try
//            {
//                string[] fbsig = HttpUtility.UrlDecode(cookies["fbs_" + appId].Value.Trim('"')).Split('&');
//                foreach (var s in fbsig)
//                {
//                    string[] tmp = s.Split('=');
//                    args.Add(tmp[0], tmp[1]);
//                }
//                var sortedArgs = (from entry in args orderby entry.Key ascending select entry);

//                string payload = "";
//                foreach (var item in sortedArgs) if (item.Key != "sig") payload += item.Key + "=" + item.Value;

//                string sig = Md5Hash(payload + appSecret);
//                int expires = int.Parse(args["expires"]);
//                int epoch = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;

//                if (sig == args["sig"] && (expires == 0 || epoch < expires)) return args;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//            return null;
//        }

//        /// <summary>
//        /// Parses the get parameters sent on canvas iframe.
//        /// 
//        /// You need to enable the OAuth 2.0 for Canvas option in 
//        /// Application -> Edit Settings -> Advanced -> Migrations
//        /// 
//        /// If the user is logged in via Facebook, we return a dictionary with the
//        /// keys "uid" and "access_token". The former is the user's Facebook ID,
//        /// and the latter can be used to make authenticated requests to the Graph API.
//        /// If the user is not logged in, we return None.
//        /// </summary>
//        /// <param name="request">HttpRequest</param>
//        /// <param name="appId">Facebook Application Id</param>
//        /// <param name="appSecret">Facebook Application Secret</param>
//        /// <returns>Dictionary with the keys "uid" and "access_token"</returns>
//        public static Dictionary<string, string> GetUserFromQueryString(HttpRequest request, string appId, string appSecret)
//        {

//            var args = new Dictionary<string, string>();
//            //var signed_request = request.QueryString["signed_request"]; // Deprecate, POST for Canvas disabled 
//            var signed_request = request.Form["signed_request"];
//            if (signed_request == null) return null;

//            var splitted = signed_request.Split('.');
//            var encoded_sig = splitted[0];
//            var payload = splitted[1];

//            var sig = base64UrlDecode(encoded_sig);
//            var jObject = JObject.Parse(base64UrlDecode(payload));
//            var algorithm = jObject["algorithm"].ToString().ToUpper().Trim('"');
//            if (algorithm != "HMAC-SHA256") return null;

//            var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(appSecret));
//            hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(payload));
//            var encoding = new UTF8Encoding();
//            var expected_sig = encoding.GetString(hmacsha256.Hash);
//            if (sig != expected_sig) return null;

//            var data = new Dictionary<string, string>();
//            data.Add("user_id", (string)jObject["user_id"] ?? "");
//            data.Add("uid", (string)jObject["user_id"] ?? "");
//            data.Add("oauth_token", (string)jObject["oauth_token"] ?? "");
//            data.Add("access_token", (string)jObject["oauth_token"] ?? "");
//            var expires = ((long?) jObject["expires"] ?? 0);
//            data.Add("expires", expires > 0 ? expires.ToString() : "") ;
//            data.Add("profile_id", (string)jObject["profile_id"] ?? "");

//            return data;
//        }

//        /// <summary>
//        /// Performs base 64 url safe decoding
//        /// </summary>
//        /// <param name="str">The string to be decoded</param>
//        /// <returns></returns>
//        private static string base64UrlDecode(string str)
//        {
//            var encoding = new UTF8Encoding();
//            var decodedJson = str.Replace("=", string.Empty).Replace('-', '+').Replace('_', '/');
//            var base64JsonArray = Convert.FromBase64String(decodedJson.PadRight(decodedJson.Length + (4 - decodedJson.Length % 4) % 4, '='));
//            return encoding.GetString(base64JsonArray);
//        }

//        /// <summary>
//        /// Gets the MD5 Hash string for the given input
//        /// </summary>
//        /// <param name="input">Input string</param>
//        /// <returns>Hashed string</returns>
//        private static string Md5Hash(string input)
//        {
//            byte[] data = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
//            StringBuilder sb = new StringBuilder();
//            for (int i = 0; i < data.Length; i++) sb.Append(data[i].ToString("x2"));
//            return sb.ToString();
//        }
//    }

//    /// <summary>
//    /// FacebookGraphAPIException
//    /// </summary>
//    public class FacebookGraphAPIException : Exception
//    {
//        public string Type { get; set; }
//        public string Message { get; set; }

//        public FacebookGraphAPIException(string type, string message)
//        {
//            Type = type;
//            Message = message;
//        }
//    }

//}
