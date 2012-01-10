using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class ContinentService
    {
        private ContinentRepository continentRepository;

        public ContinentService(ContinentRepository continentRepository)
        {
            this.continentRepository = continentRepository;
        }

        public IList<Continent> GetAll()
        {
            return this.continentRepository.AsQueryable().ToList<Continent>();
        }

        public Continent Get(int id)
        {
            return continentRepository.AsQueryable().SingleOrDefault(c => c.ID == id);
        }
    }
}
