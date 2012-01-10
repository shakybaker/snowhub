using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class NewsFeedRepository
    {
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public void Update(Sporthub.Model.NewsFeed entity)
        {
            var NewsFeedToUpdate = from n in db.NewsFeeds where n.ID == entity.ID select n;

        }

        public void Delete(Sporthub.Model.NewsFeed entity)
        {
            throw new NotImplementedException();
        }

        public int Add(Sporthub.Model.NewsFeed entity)
        {
            var newNewsFeed = new Sporthub.Repository.DataAccess.NewsFeed
            {
                FeedDescription = entity.FeedDescription,
                FeedName = entity.FeedName,
                FeedSite = entity.FeedSite,
                FeedURL = entity.FeedURL,
                FaviconURL = entity.FaviconURL,
                NewsFeedTypeID = entity.NewsFeedTypeID,
                UseFavicon = entity.UseFavicon,
                CreatedDate = DateTime.Now,
                CreatedUserID = entity.CreatedUserID,
                UpdatedDate = DateTime.Now,
                UpdatedUserID = entity.UpdatedUserID
            };

            db.NewsFeeds.InsertOnSubmit(newNewsFeed);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newNewsFeed.ID;
        }

        public IQueryable<Sporthub.Model.NewsFeed> AsQueryable()
        {
            var NewsFeed =
                from n in db.NewsFeeds
                select new Sporthub.Model.NewsFeed
                {
                    ID = n.ID,
                    FeedDescription = n.FeedDescription,
                    FeedName = n.FeedName,
                    FeedSite = n.FeedSite,
                    FeedURL = n.FeedURL,
                    FaviconURL = n.FaviconURL,
                    NewsFeedTypeID = n.NewsFeedTypeID,
                    UseFavicon = n.UseFavicon,
                    CreatedDate = n.CreatedDate,
                    CreatedUserID = n.CreatedUserID,
                    UpdatedDate = n.UpdatedDate,
                    UpdatedUserID = n.UpdatedUserID
                };

            return NewsFeed;
        }
    }
}
