using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class ResortLinkService
    {
        private ResortLinkRepository resortLinkRepository;

        public ResortLinkService(ResortLinkRepository resortLinkRepository)
        {
            this.resortLinkRepository = resortLinkRepository;
        }

        public IList<ResortLink> GetAll()
        {
            return this.resortLinkRepository.AsQueryable().ToList<ResortLink>();
        }

        public IList<ResortLink> GetAllByRegionID(int id)
        {
            return this.resortLinkRepository.AsQueryable().Where(r => r.ResortID == id).ToList<ResortLink>();
        }

        public ResortLink Get(int id)
        {
            return resortLinkRepository.AsQueryable().SingleOrDefault(r => r.ID == id);
        }

        public int Add(ResortLink resortLink)
        {
            return resortLinkRepository.Add(resortLink);
        }

        public void Update(ResortLink resortLink)
        {
            resortLinkRepository.Update(resortLink);
        }
    }
}
