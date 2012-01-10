using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class LinkResortUserService
    {
        private LinkResortUserRepository linkResortUserRepository;

        public LinkResortUserService(LinkResortUserRepository linkResortUserRepository)
        {
            this.linkResortUserRepository = linkResortUserRepository;
        }

        public IList<LinkResortUser> GetAll()
        {
            return this.linkResortUserRepository.AsQueryable().ToList<LinkResortUser>();
        }

        public IList<LinkResortUser> GetLatestReviews(int take)
        {
            return this.linkResortUserRepository.GetLatestReviews(take);
//            return this.linkResortUserRepository.AsQueryable().Where(r => r.Score > 0).OrderByDescending(r => r.CreatedDate).Take(take).ToList<LinkResortUser>();
        }

        public IList<LinkResortUser> GetLatestReviewsForCountry(int id, int take)
        {
            return this.linkResortUserRepository.GetLatestReviewsForCountry(id, take);
            //            return this.linkResortUserRepository.AsQueryable().Where(r => r.Score > 0 && r.Resort.Country.ID == id).OrderByDescending(r => r.CreatedDate).Take(take).ToList<LinkResortUser>();
        }

        public IList<LinkResortUser> GetLatestReviewsForContinent(int id, int take)
        {
            return this.linkResortUserRepository.GetLatestReviewsForContinent(id, take);
        }

        public IList<LinkResortUser> GetLatestReviewsForResort(int id)
        {
            return this.linkResortUserRepository.AsQueryable().Where(r => r.Score > 0 && r.Resort.ID == id).OrderByDescending(r => r.CreatedDate).ToList<LinkResortUser>();
        }

        public IList<LinkResortUser> GetLatestReviewsForResort(int id, int take)
        {
            return this.linkResortUserRepository.GetLatestReviewsForResort(id, take);
//            return this.linkResortUserRepository.AsQueryable().Where(r => r.Score > 0 && r.Resort.ID == id).OrderByDescending(r => r.CreatedDate).Take(take).ToList<LinkResortUser>();
        }

        public IList<LinkResortUser> GetAllByResortID(int id)
        {
            return this.linkResortUserRepository.AsQueryable().Where(r => r.ResortID == id).OrderByDescending(r => r.CreatedDate).ToList<LinkResortUser>();
        }

        public LinkResortUser Get(int id)
        {
            return linkResortUserRepository.AsQueryable().SingleOrDefault(r => r.ID == id);
        }

        public int Add(LinkResortUser linkResortUser)
        {
            return linkResortUserRepository.Add(linkResortUser);
        }

        public void Update(LinkResortUser linkResortUser)
        {
            linkResortUserRepository.Update(linkResortUser);
        }
    }
}
