using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class CountryService
    {
        private CountryRepository countryRepository;

        public CountryService(CountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public IList<Country> GetAll()
        {
            return this.countryRepository.AsQueryable().ToList<Country>();
        }

        public IList<Country> GetAll(int id)
        {
            return this.countryRepository.AsQueryable().Where(c => c.ContinentID == id).OrderBy(c => c.CountryName).ToList<Country>(); 
        }

        public IList<Country> GetAllWithResorts()
        {
            return this.countryRepository.AsQueryable().Where(c => c.Resorts.Count > 0).OrderBy(c => c.CountryName).ToList<Country>();
        }

        public IList<Country> GetAllWithResorts(int id)
        {
            return this.countryRepository.AsQueryable().Where(c => c.ContinentID == id && c.Resorts.Count > 0).OrderBy(c => c.CountryName).ToList<Country>();
        }

        public Country Get(int id)
        {
            return countryRepository.AsQueryable().SingleOrDefault(c => c.ID == id);
        }   

        public List<Country> GetAllByContinentID(int id)
        {
            return GetAllWithResorts(id).Where(c => c.ContinentID == id).OrderBy(c => c.CountryName).ToList<Country>();
        }
    }
}
