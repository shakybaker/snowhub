using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class Thread 
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public int ForumID { get; set; }
        public int ThreadStatusID { get; set; }
        public string Title { get; set; }
        public bool IsVisible { get; set; }
        public bool IsSticky { get; set; }
        public bool IsAdmin { get; set; }
        public int ResortID { get; set; }
        public IList<Post> Posts { get; set; }
        public User StartedBy { get; set; }

        public string GetCreatedTimespan()
        {
            return GetTimespan(CreatedDate);
        }

        public string GetUpdatedTimespan()
        {
            return GetTimespan(UpdatedDate);
        }

        public string GetTimespan(DateTime? date)
        {
            string outStr = string.Empty;

            if (date != null)
            {
                DateTime now = DateTime.Now;
                DateTime memberSinceDate = date ?? now;
                TimeSpan span = now.Subtract(memberSinceDate);

                string s = string.Empty;
                if (span.Days > 0)
                {
                    if (span.Days > 30)
                    {
                        outStr = " on " + Convert.ToDateTime(date).ToString("dd MMMM yyyy");
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

        public string GetCroppedTitle()
        {
            return (Title.Length > 30) ? string.Format("{0} ...", Title.Substring(0, 30)) : Title;
        }

    }
}
