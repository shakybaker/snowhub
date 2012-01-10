using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sporthub.Model.Enumerators;
using Sporthub.Model;
using System.Configuration;
using Sporthub.Utilities;

namespace Sporthub.Mvc.Code.Helpers
{
    public static class UserHelper
    {
        public static string UserList(this HtmlHelper helper, IList<User> users, int listNum, ProfileThumbSize thumbSize, string noResultsMessage, string divClassname, string footer)
        {
            string ret = string.Empty;

            if (users != null)
            {
                int u = 1;
                ret += string.Format("<div class=\"{0}\">", divClassname);

                if (users.Count > 0)
                {
                    var ids = string.Empty;
                    foreach (Sporthub.Model.User user in users)
                    {
                        ids += string.Format("{0},", user.FacebookUid);
                        ret += string.Format("<a id=\"l{0}-{1}-fb_{2}\" rel=\"\" class=\"profile {3}fb{4}\" href=\"/user/{5}\" style=\"margin: 0 10px 10px 0;\"><img alt=\"profile pic\" src=\"http://static.ak.fbcdn.net/pics/t_silhouette.jpg\" class=\"tnMedia\"/></a>", listNum, u, user.FacebookUid, ((thumbSize == ProfileThumbSize.Small) ? "sml " : ""), user.FacebookUid, user.FacebookUid);
                        ret += string.Format("<div id=\"l{0}-{1}-p_{2}\" class=\"profileSummary\">", listNum, u, user.FacebookUid);
                        ret += "<div class=\"profileSummaryIn\">";
                        ret += string.Format("<span id=\"l{0}-{1}-n_{2}\" class=\"fb-n{3}\"></span>", listNum, u, user.FacebookUid, user.FacebookUid);
                        ret += string.Format("<em>{0}</em>", user.GetSportTypes());
                        ret += "<br />";
                        //                    ret += string.Format("<a class=\"smlbutt\" href=\"/user/{0}\">Snowhub Profile</a> <a class=\"smlbutt\" href=\"http://www.facebook.com/profile.php?id={1}\" target=\"_blank\">Facebook Profile</a>", user.FacebookUid, user.FacebookUid);
                        ret += "<a class=\"smlbutt\" href=\"/user/" + user.FacebookUid + "\">Snowhub Profile</a> <a class=\"smlbutt\" href=\"http://www.facebook.com/profile.php?id=" + user.FacebookUid + "\" target=\"_blank\">Facebook Profile</a>";
                        ret += "<div class=\"cb\"></div>";
                        ret += "</div>";
                        ret += "<div class=\"beak\"></div>";
                        ret += "</div>";
                        u++;
                    }
                    ret += string.Format("<input type=\"hidden\" id=\"l{0}-idList\" name=\"l{1}-idList\" class=\"idList\" value=\"{2}\" />", listNum, listNum, ids.Substring(0, (ids.Length - 1)));
                }
                else
                {
                    ret += noResultsMessage;
                }

                ret += "</div>";

                if (string.IsNullOrEmpty(footer))
                    ret += footer;
            }

            return ret;
        }
    }
}
