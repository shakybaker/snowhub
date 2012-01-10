using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class LinkResortUser : IEntity
    {
        public LinkResortUser()
        {
            ReviewUsefulnessFeedback = new List<ReviewUsefulness>();
        }
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public int ResortID { get; set; }
        public int UserID { get; set; }
        public int Score { get; set; }
        public bool HasVisited { get; set; }
        public bool IsFavourite { get; set; }
        public string Title { get; set; }
        public string LastVisitDate { get; set; }
        public string ReviewText { get; set; }

        public int ResortSuitsExpert { get; set; }
        public int ResortSuitsAdvanced { get; set; }
        public int ResortSuitsIntermediate { get; set; }
        public int ResortSuitsBeginner { get; set; }

        public int ResortSuitsLively { get; set; }
        public int ResortSuitsAverage { get; set; }
        public int ResortSuitsQuiet { get; set; }

        public int ResortSuitsSkiers { get; set; }
        public int ResortSuitsSnowboarders { get; set; }
        public int ResortSuitsBoth { get; set; }

        public int ResortSuitsExpensive { get; set; }
        public int ResortSuitsAffordable { get; set; }
        public int ResortSuitsCheap { get; set; }

        public int LiftRating { get; set; }
        public int SnowRating { get; set; }
        public int QueueRating { get; set; }
        public int SceneryRating { get; set; }
        public int ConvenienceRating { get; set; }
        public int AccomodationRating { get; set; }
        public int FoodRating { get; set; }
        public int FacilitiesRating { get; set; }
        public int NightlifeRating { get; set; }
        public IList<ReviewUsefulness> ReviewUsefulnessFeedback { get; set; }

        //model only
        public Resort Resort { get; set; }
        public User User { get; set; }

        public string GetCreatedTime()
        {
            string outStr = string.Empty;

            if (CreatedDate != null)
            {
                DateTime now = DateTime.Now;
                DateTime createdDate = CreatedDate ?? now;
                TimeSpan span = now.Subtract(createdDate);

                string s = string.Empty;
                if (span.Days > 0)
                {
                    s = span.Days == 1 ? string.Empty : "s";
                    outStr = string.Format("{0} day{1} ago", span.Days, s);
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

        public string GetLastVisitedDate()
        {
            string outStr = string.Empty;

            if (LastVisitDate.Contains('-'))
            {
                var arr = LastVisitDate.Split('-');
                string[] months = { "--Month--", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                try
                {
                    outStr = string.Format("{0} {1}", months[int.Parse(arr[1])], arr[0]);
                }
                catch (Exception ex)
                {
                    outStr = LastVisitDate;
                }
            }
            else
            {
                outStr = LastVisitDate;
            }

            return outStr;
        }

        public string GetCroppedTitle()
        {
            return (Title.Length>35) ? string.Format("{0} ...", Title.Substring(0, 35)) : Title;
        }

    }
}
