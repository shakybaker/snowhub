using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class RegionService
    {
        private RegionRepository regionRepository;

        public RegionService(RegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        public IList<Region> GetAll()
        {
            return this.regionRepository.AsQueryable().ToList<Region>();
        }

        public IList<Region> GetAllForCountry(int id)
        {
            return this.regionRepository.GetAllForCountry(id);
        }

        public IList<Region> GetAllForRegion(int id)
        {
            return this.regionRepository.GetAllForRegion(id);
        }

        public Region Get(int id)
        {
            return regionRepository.AsQueryable().SingleOrDefault(x => x.ID == id);
        }

        public int Add(Region region)
        {
            return regionRepository.Add(region);
        }

        public void Update(Region region)
        {
            regionRepository.Update(region);
        }

        public void Delete(Region region)
        {
            regionRepository.Delete(region);
        }
    }
}
