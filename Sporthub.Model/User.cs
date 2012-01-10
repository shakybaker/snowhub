using System;
using System.Collections.Generic;
using System.Linq;
using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class User : IEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public int SignupStageReachedID { get; set; }
        public string ReferrerURL { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RealName { get; set; }
        public string Password { get; set; }
        public int UserRoleID { get; set; }
        public char? Sex { get; set; }
        public string Age { get; set; }
        public string UsualCity { get; set; }
        public int UsualCountryID { get; set; }

        public bool IsSnowboarder { get; set; }
        public bool IsFreerideBoarder { get; set; }
        public bool IsFreestyleBoarder { get; set; }
        public bool IsAlpineBoarder { get; set; }
        public bool IsSkier { get; set; }
        public bool IsAlpineSkier { get; set; }
        public bool IsLanglaufSkier { get; set; }
        public bool IsNonBoarderSkier { get; set; }

        public int Points { get; set; }
        public int AddThreshold { get; set; }
        public string IPAddressOriginal { get; set; }
        public string IPAddressLast { get; set; }

        public string DobDay { get; set; }
        public string DobMonth { get; set; }
        public string DobYear { get; set; }

        public UserRole UserRole { get; set; }
        public Country Country { get; set; }

        public DateTime? LastVisitDate { get; set; }

        public IList<LinkResortUser> LinkResortUsers { get; set; }
        public IList<LinkUserSportType> LinkUserSportTypes { get; set; }
        public IList<CheckIn> CheckIns { get; set; }
        public IList<Picture> Pictures { get; set; }

        public bool IsAuthUserProfile { get; set; }
        public bool HasProfilePicture { get; set; }

        public string FacebookUid { get; set; }
        public bool IsLoggedInToFacebook { get; set; }

        public User()
        {
            LinkResortUsers = new List<LinkResortUser>();
            LinkUserSportTypes = new List<LinkUserSportType>();
            CheckIns = new List<CheckIn>();
            Pictures = new List<Picture>();
            IsLoggedInToFacebook = false;
        }

        public CheckIn GetCheckedInResort()
        {
            return CheckIns.Where(x => x.IsActive).SingleOrDefault();
        }

        public string GetSportTypes()
        {
            return GetSportTypes(false);
        }

        public string GetSportTypes(bool newLine)
        {
            var outStr = string.Empty;
            var i = 1;
            var brk = (newLine) ? "<br />" : ", ";
            foreach (var sport in LinkUserSportTypes)
            {
                var b = (i < LinkUserSportTypes.Count) ? brk : string.Empty;
                outStr += string.Format("{0} {1}{2}", sport.GetSportLevel(), sport.ConfigSportType.Collective, b);
                i++;
            }
            return outStr;
        }

        public string GetName()
        {
            return !string.IsNullOrEmpty(RealName) ? RealName : UserName;
        }

        public string GetSmallProfilePic()
        {
            //if (!string.IsNullOrEmpty(FacebookUid))
            //    return string.Format("http://graph.facebook.com/{0}/picture", FacebookUid);
            return HasProfilePicture ? string.Format("/content/users/{0}_sml.png", UserName) : "/static/images/user-no-pic.png";
        }

        public string GetLargeProfilePic()
        {
            var rand = new Random();
            //if (!string.IsNullOrEmpty(FacebookUid))
            //    return string.Format("http://graph.facebook.com/{0}/picture?type=large", FacebookUid);
            return HasProfilePicture ? string.Format("/content/users/{0}_lge.png?{1}", UserName, rand.Next(1000)) : "/static/images/user-no-pic-lge.png";
            //TODO: put in config!!!
        }

        public string HasSportType(int id)
        {
            var outStr = string.Empty;
            var sport = LinkUserSportTypes.Where(x => x.SportTypeID == id).SingleOrDefault();
            if (sport != null)
                outStr = "checked=\"checked\" ";
            return outStr;
        }

        public string IsSeasonSelected(int id, int seasonCount)
        {
            var outStr = string.Empty;
            var sport = LinkUserSportTypes.Where(x => x.SportTypeID == id).SingleOrDefault();
            if (sport != null)
            {
                if (sport.Seasons == seasonCount)
                {
                    outStr = " selected=\"selected\" ";
                }
            }
            return outStr;
        }

        public string IsLevelSelected(int id, int level)
        {
            var outStr = string.Empty;
            var sport = LinkUserSportTypes.Where(x => x.SportTypeID == id).SingleOrDefault();
            if (sport != null)
            {
                if (sport.Level == level)
                {
                    outStr = " selected=\"selected\" ";
                }
            }
            return outStr;
        }

        public string GetLastVisitTime()
        {
            var outStr = string.Empty;

            if (LastVisitDate != null)
            {
                var now = DateTime.Now;
                var lastVisitDate = LastVisitDate ?? now;
                var span = now.Subtract(lastVisitDate);

                var s = string.Empty;
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

        public string GetMemberSince()
        {
            //return Convert.ToDateTime(CreatedDate).ToLongDateString();
            var outStr = string.Empty;

            var now = DateTime.Now;
            var memberSinceDate = CreatedDate ?? now;
            var span = now.Subtract(memberSinceDate);

            var s = string.Empty;
            if (span.Days > 0)
            {
                if (span.Days > 30)
                {
                    outStr = Convert.ToDateTime(CreatedDate).ToString("dd MMMM yyyy");
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

            return outStr;
        }

        public string GetAge()
        {
            var ret = string.Empty;

            if ((!string.IsNullOrEmpty(DobYear))
                && (!string.IsNullOrEmpty(DobMonth))
                && (!string.IsNullOrEmpty(DobDay)))
            {
                var dob = new DateTime(int.Parse(DobYear), int.Parse(DobMonth), int.Parse(DobDay));
                var span = DateTime.Now.Subtract(dob);
                ret = decimal.Round(decimal.Parse((span.TotalDays / 365.25).ToString()), 0).ToString();
            }

            return ret;
        }

        public string GetLocation()
        {
            var ret = string.Empty;

            if (!string.IsNullOrEmpty(UsualCity))
            {
                ret += UsualCity;
            }
            if (UsualCountryID > 0)
            {
                if (!string.IsNullOrEmpty(ret))
                    ret += ", ";
                ret += Country.CountryName;
            }

            return ret;
        }

        public string GetUserSummary()
        {
            var outStr = string.Empty;

            if (!string.IsNullOrEmpty(RealName))
            {
                outStr = string.Format("<strong>{0}</strong>, ", RealName);
            }

            switch (Sex)
            {
                case 'M':
                    outStr += "Male, ";
                    break;
                case 'F':
                    outStr += "Female, ";
                    break;
                default:
                    outStr += "Unknown gender, ";
                    break;
            }

            if (!string.IsNullOrEmpty(DobDay) && !string.IsNullOrEmpty(DobMonth) && !string.IsNullOrEmpty(DobYear))
            {
                try
                {
                    var dob = new DateTime(int.Parse(DobYear), int.Parse(DobMonth), int.Parse(DobDay));
                    var comparisonDate = new DateTime(dob.Year, DateTime.Now.Month, DateTime.Now.Day);

                    var age = (comparisonDate.Date < dob.Date) ? DateTime.Now.Year - dob.Year - 1 : DateTime.Now.Year - dob.Year;

                    outStr = string.Format("{0}{1}, ", outStr, age.ToString());
                } catch(Exception ex) {}
            }
            if (!string.IsNullOrEmpty(UsualCity))
            {
                outStr = string.Format("{0}{1}, ", outStr, UsualCity);
            }

            if (UsualCountryID > 0)
            {
                outStr = string.Format("{0}{1}, ", outStr, Country.CountryName);
            }

            var l = outStr.Length - 2;
            outStr = outStr.Substring(0, l);

            return outStr;
        }
    }
}
