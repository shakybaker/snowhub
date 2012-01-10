using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class Activity : IEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public string FacebookUid { get; set; }

        public string ActionText { get; set; }
        public string ActionLink { get; set; }
        public string ActionClass { get; set; }

        public string SubjectText { get; set; }
        public string SubjectLink { get; set; }
        public int SubjectID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }

        public string FormatLink()
        {
//<a class="<%= activity.ActionClass %>" href="<%= activity.ActionLink %>"><%= activity.ActionText %></a> <a href="<%= activity.SubjectLink %>"><%= activity.SubjectText %></a><span class="r-date">
            string outStr = string.Empty;
            switch (ActionText)
            {
                case "Resort Review":
                    outStr = "posted a <a class=" + ActionClass + " href=" + ActionLink + ">" + ActionText + "</a> for <a href=" + SubjectLink + ">" + SubjectText + "</a>";
                    break;
                case "Favourite Resort":
                    outStr = "added <a href=" + SubjectLink + ">" + SubjectText + "</a> as a <a class=" + ActionClass + " href=" + ActionLink + ">" + ActionText + "</a>";
                    break;
                case "Visited":
                    outStr = "marked <a href=" + SubjectLink + ">" + SubjectText + "</a> as <a class=" + ActionClass + " href=" + ActionLink + ">" + ActionText + "</a>";
                    break;
                case "Joined":
                    outStr = "joined the Snowhub";
                    break;
                case "Posted":
                    outStr = "posted a <a href=\"" + ActionLink + "\">Reply</a> to " + SubjectText;
                    break;
                case "Topic":
                    outStr = "started a <a href=\"" + ActionLink + "\">new Topic</a> - " + SubjectText;
                    break;
                default:
                    break;
            }
            return outStr;
        }

        public string GetTimespan()
        {
            string outStr = string.Empty;

            if (CreatedDate != null)
            {
                DateTime now = DateTime.Now;
                DateTime memberSinceDate = CreatedDate ?? now;
                TimeSpan span = now.Subtract(memberSinceDate);

                string s = string.Empty;
                if (span.Days > 0)
                {
                    if (span.Days > 30)
                    {
                        outStr = " on " + Convert.ToDateTime(CreatedDate).ToString("dd MMMM yyyy");
                    }
                    else
                    {
                        s = span.Days == 1 ? string.Empty : "s";
                        outStr = string.Format("{0} day{1} ago", span.Days, s);
                    }
                }
                else if (span.Days > 0)
                {
                    s = span.Days == 1 ? string.Empty : "s";
                    outStr = string.Format("{0} hour{1} ago", span.Hours, s);
                }
                else if (span.Minutes > 0)
                {
                    s = span.Minutes == 1 ? string.Empty : "s";
                    outStr = string.Format("{0} minute{1} ago", span.Minutes, s);
                }
                else if (span.Seconds > 0)
                {
                    outStr = "less than a minute ago";
                }
            }
            return outStr;
        }
    }
}
