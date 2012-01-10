using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Enumerators;

namespace Sporthub.Utils
{
    public static class Enums
    {
        public static string GetName(this QS input)
        {
            switch (input)
            {
                case QS.Action:
                    return "act";
                case QS.Add:
                    return "add";
                case QS.BarRestaurantID:
                    return "brid";
                case QS.ContinentID:
                    return "contid";
                case QS.CountryID:
                    return "cid";
                case QS.Delete:
                    return "delete";
                case QS.Edit:
                    return "edit";
                case QS.ForumID:
                    return "fid";
                case QS.ManufacturerID:
                    return "mid";
                case QS.NewsFeedID:
                    return "nfid";
                case QS.ParentRegionID:
                    return "prid";
                case QS.PictureID:
                    return "picid";
                case QS.PostID:
                    return "pid";
                case QS.ProConID:
                    return "pcid";
                case QS.RegionID:
                    return "regid";
                case QS.ResortID:
                    return "rid";
                case QS.ServiceID:
                    return "sid";
                case QS.SkiAreaID:
                    return "said";
                case QS.ThreadID:
                    return "tid";
                case QS.UserID:
                    return "uid";
            }

            return string.Empty;
        }

        public static QS GetEnum(this string input)
        {
            switch (input.ToLower())
            {
                case "act":
                    return QS.Action;
                case "add":
                    return QS.Add;
                case "brid":
                    return QS.BarRestaurantID;
                case "contid":
                    return QS.ContinentID;
                case "cid":
                    return QS.CountryID;
                case "delete":
                    return QS.Delete;
                case "edit":
                    return QS.Edit;
                case "fid":
                    return QS.ForumID;
                case "mid":
                    return QS.ManufacturerID;
                case "nfid":
                    return QS.NewsFeedID;
                case "prid":
                    return QS.ParentRegionID;
                case "picid":
                    return QS.PictureID;
                case "pid":
                    return QS.PostID;
                case "pcid":
                    return QS.ProConID;
                case "regid":
                    return QS.RegionID;
                case "rid":
                    return QS.ResortID;
                case "sid":
                    return QS.ServiceID;
                case "said":
                    return QS.SkiAreaID;
                case "tid":
                    return QS.ThreadID;
                case "uid":
                    return QS.UserID;
            }

            return QS.InvalidQueryString;
        }
    }
}
