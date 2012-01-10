using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporthub.Model
{
    public class NewsFeed
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int UpdatedUserID { get; set; }

        public string FeedURL { get; set; }
        public string FeedSite { get; set; }
        public string FeedName { get; set; }
        public string FeedDescription { get; set; }
        public string FaviconURL { get; set; }
        public int NewsFeedTypeID { get; set; }
        public bool UseFavicon { get; set; }
    }
}
