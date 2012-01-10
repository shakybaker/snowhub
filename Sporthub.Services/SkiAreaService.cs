using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class SkiAreaService
    {
        private SkiAreaRepository skiAreaRepository;

        public SkiAreaService(SkiAreaRepository skiAreaRepository)
        {
            this.skiAreaRepository = skiAreaRepository;
        }

        public IList<SkiArea> GetAll()
        {
            return this.skiAreaRepository.AsQueryable().ToList<SkiArea>();
        }

        public IList<SkiArea> GetAll(int id)
        {
            return this.skiAreaRepository.AsQueryable().Where(x => x.SkiAreaID == id).OrderBy(x => x.Name).ToList<SkiArea>();
        }

        public IList<SkiArea> GetAllForResort(int id)
        {
            return this.skiAreaRepository.GetAllForResort(id);
        }

        public IList<SkiArea> GetAllForCountry(int id)
        {
            return this.skiAreaRepository.GetAllForCountry(id);
        }

        public SkiArea Get(int id)
        {
            return skiAreaRepository.AsQueryable().SingleOrDefault(x => x.ID == id);
        }

        public int Add(SkiArea skiArea)
        {
            return skiAreaRepository.Add(skiArea);
        }

        public void Delete(SkiArea skiArea)
        {
            skiAreaRepository.Delete(skiArea);
        }
    }
}
