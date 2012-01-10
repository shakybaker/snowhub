using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class ResortService
    {
        private ResortRepository resortRepository;

        public ResortService(ResortRepository resortRepository)
        {
            this.resortRepository = resortRepository;
        }

        public IList<Resort> GetAll()
        {
            return this.resortRepository.AsQueryable().ToList<Resort>();
        }

        public IList<Resort> GetAll(string letter)
        {
            return this.resortRepository.AsQueryableBasic().Where(r => r.Name.StartsWith(letter)).OrderByDescending(x => x.Name).ToList<Resort>();
        }

        public IList<Resort> GetTopRatedResorts()
        {
            return this.resortRepository.GetTopRatedResorts();
//            return this.resortRepository.AsQueryableBasic().OrderByDescending(x => x.Score).Take(10).ToList<Resort>();
        }
        public IList<Resort> GetTopRatedResortsByCountry(int id)
        {
            return this.resortRepository.GetTopRatedResortsByCountry(id);
//            return this.resortRepository.AsQueryable().Where(r => r.Score > 0 && r.Country.ID == id).OrderByDescending(x => x.Score).Take(10).ToList<Resort>();
        }
        public IList<Resort> GetTopRatedResortsByContinent(int id)
        {
            return this.resortRepository.GetTopRatedResortsByContinent(id);
            //            return this.resortRepository.AsQueryable().Where(r => r.Score > 0 && r.Country.ID == id).OrderByDescending(x => x.Score).Take(10).ToList<Resort>();
        }
        public IList<Resort> GetMostVisitedResorts()
        {
            return this.resortRepository.GetMostVisitedResorts();
            //            return this.resortRepository.AsQueryableBasic().OrderByDescending(x => x.VisitedCount).Take(10).ToList<Resort>();
        }
        public IList<Resort> GetMostVisitedResortsByCountry(int id)
        {
            return this.resortRepository.GetMostVisitedResortsByCountry(id);
            //            return this.resortRepository.AsQueryable().Where(r => r.Score > 0 && r.Country.ID == id).OrderByDescending(x => x.VisitedCount).ToList<Resort>();
        }
        public IList<Resort> GetMostVisitedResortsByContinent(int id)
        {
            return this.resortRepository.GetMostVisitedResortsByContinent(id);
        }

        public IList<Resort> GetAllByCountryID(int id)
        {
            return this.resortRepository.AsQueryable().Where(r => r.CountryID == id).OrderBy(r => r.Name).ToList<Resort>();
        }

        public IList<Resort> GetAllByCountryIdBasic(int id)
        {
            return this.resortRepository.AsQueryableBasic().Where(r => r.CountryID == id).OrderBy(r => r.Name).ToList<Resort>();
        }

        public IList<Resort> GetAllByCountryIdBasic(string name)
        {
            return this.resortRepository.AsQueryableBasic().Where(r => r.CountryName == name).OrderBy(r => r.Name).ToList<Resort>();
        }

        public IList<Resort> GetAllByRegionID(int id)
        {
            return this.resortRepository.AsQueryable().Where(r => r.Region.ID == id).OrderBy(r => r.Name).ToList<Resort>();
        }

        public IList<Resort> GetAllByBounds(double maxx, double maxy, double minx, double miny)
        {
            return this.resortRepository.AsQueryableBasic()
                .Where(
                    r => r.Latitude >= miny &&
                    r.Latitude <= maxy &&
                    r.Longitude >= minx &&
                    r.Longitude <= maxy
                    ).OrderBy(r => r.Name).ToList<Resort>();
        }

        public IList<Resort> GetAllByBounxxxxds(double maxx, double maxy, double minx, double miny)
        {
            return this.resortRepository.AsQueryable()
                .Where(
                    r => r.Latitude >= miny &&
                    r.Latitude <= maxy &&
                    r.Longitude >= minx &&
                    r.Longitude <= maxy
                    ).OrderBy(r => r.Name).ToList<Resort>();
        }

        public IList<Resort> GetAllByContinentID(int id)
        {
            return this.resortRepository.AsQueryableBasic().Where(r => r.ContinentID == id).OrderBy(r => r.Name).ToList<Resort>();
        }

        public IList<Resort> GetAllBySearch(string text, int take)
        {
            //return this.resortRepository.AsQueryable().Where(r => (r.Name.StartsWith(text) || r.NameFriendlyFormat.StartsWith(text))).OrderBy(r => r.Name).Take(take).ToList<Resort>();
            return this.resortRepository.AsQueryableBasic().Where(r => (r.Name.Contains(text) || r.NameFriendlyFormat.Contains(text) || r.AlsoKnownAs.Contains(text))).OrderBy(r => r.Name).Take(take).ToList<Resort>();
        }

        public Resort Get(int id)
        {
            return resortRepository.AsQueryable().SingleOrDefault(r => r.ID == id);
        }

        public Resort Get(string name)
        {
            return resortRepository.AsQueryable().SingleOrDefault(r => (r.PrettyUrl == name || r.PrettyUrlFriendlyFormat == name));
        }

        public Resort GetByName(string name)
        {
            return resortRepository.AsQueryable().SingleOrDefault(r => (r.Name == name));
        }

        public IList<Resort> GetEmptyPrettyUrls()
        {
            return resortRepository.AsQueryable().Where(r => r.PrettyUrl == null).ToList<Resort>();
        }

        public int Add(Resort resort)
        {
            return resortRepository.Add(resort);
        }

        public void Update(Resort resort)
        {
            resortRepository.Update(resort);
        }

        public Resort GetSkiArea(int id)
        {
            return resortRepository.SkiAreasAsQueryableBasic().SingleOrDefault(sa => sa.ID == id);
        }

        public IList<Resort> GetAllSkiAreas()
        {
            return resortRepository.SkiAreasAsQueryableBasic().ToList<Resort>();
        }
    }
}
