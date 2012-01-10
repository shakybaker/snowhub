using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Model.Enumerators;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class NewsFeedService
    {
        private NewsFeedRepository newsFeedRepository;

        public NewsFeedService(NewsFeedRepository newsFeedRepository)
        {
            this.newsFeedRepository = newsFeedRepository;
        }

        public IList<NewsFeed> GetAll()
        {
            return this.newsFeedRepository.AsQueryable().ToList<NewsFeed>();
        }

        public IList<NewsFeed> GetAll(NewsFeedType newsFeedType)
        {
            return this.newsFeedRepository.AsQueryable().Where(n => n.NewsFeedTypeID == (int)newsFeedType).OrderByDescending(n => n.UpdatedDate).ToList<NewsFeed>();
        }

        public NewsFeed Get(int id)
        {
            return newsFeedRepository.AsQueryable().SingleOrDefault(n => n.ID == id);
        }

        public int Add(NewsFeed newsFeed)
        {
            return newsFeedRepository.Add(newsFeed);
        }

    }
}
