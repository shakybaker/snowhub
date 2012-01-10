using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class ResortViewData : SporthubViewData
    {
        public Feedback Feedback { get; set; }
        public Resort Resort { get; set; }
        public IList<ConfigData> Regions { get; set; }
        public IList<ConfigData> Countries { get; set; }
        public IList<ConfigData> Continents { get; set; }
        public IList<Thread> Threads { get; set; }
        public IList<Picture> Pictures { get; set; }
        public IList<LinkResortUser> Favourited { get; set; }
        public IList<LinkResortUser> Visited { get; set; }
        public IList<LinkResortUser> Reviewed { get; set; }
        public IList<LinkResortUser> Reviews { get; set; }
        public List<RatingItem> Ratings { get; set; }

        public IList<User> VisitedBy { get; set; }
        public IList<User> FavedBy { get; set; }
        public IList<User> ReviewedBy { get; set; }

        public int TotalRating { get; set; }
        public int FavouriteCount { get; set; }
        public int VisitedCount { get; set; }
        public int ReviewCount { get; set; }
        public bool HasPicture { get; set; }
        public bool IsSingleReview { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; }
        public IList<LinkResortUser> LatestReviews { get; set; }

        public string Querystring { get; set; }

        public ResortViewData()
        {
            HasPicture = false;
        }
    }
}
