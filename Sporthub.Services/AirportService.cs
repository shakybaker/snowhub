using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository;

namespace Sporthub.Services
{
    public class AirportService
    {
        private AirportRepository airportRepository;

        public AirportService(AirportRepository airportRepository)
        {
            this.airportRepository = airportRepository;
        }

        public IList<Airport> GetAll()
        {
            return this.airportRepository.AsQueryable().ToList<Airport>();
        }

        public IList<Airport> GetAllByCountryID(int id)
        {
            return this.airportRepository.AsQueryable().Where(r => r.CountryID == id).OrderBy(r => r.Name).ToList<Airport>();
        }

        public IList<Airport> GetAllByBounds(double maxx, double maxy, double minx, double miny)
        {
            return this.airportRepository.AsQueryable()
                .Where(
                    r => r.Latitude >= miny &&
                    r.Latitude <= maxy &&
                    r.Longitude >= minx &&
                    r.Longitude <= maxy
                    ).OrderBy(r => r.Name).ToList<Airport>();
        }

        public IList<Airport> GetAllByContinentID(int id)
        {
            return this.airportRepository.AsQueryable().Where(r => r.ContinentID == id).OrderBy(r => r.Name).ToList<Airport>();
        }

        public IList<Airport> GetAllBySearch(string text, int take)
        {
            //return this.airportRepository.AsQueryable().Where(r => (r.Name.StartsWith(text) || r.NameFriendlyFormat.StartsWith(text))).OrderBy(r => r.Name).Take(take).ToList<Airport>();
            return this.airportRepository.AsQueryable().Where(r => (r.Name.Contains(text) || r.NameFriendlyFormat.Contains(text))).OrderBy(r => r.Name).Take(take).ToList<Airport>();
        }

        public Airport Get(int id)
        {
            return airportRepository.AsQueryable().SingleOrDefault(r => r.ID == id);
        }

        public Airport Get(string name)
        {
            return airportRepository.AsQueryable().SingleOrDefault(r => (r.PrettyUrl == name || r.PrettyUrlFriendlyFormat == name));
        }

        public Airport GetByName(string name)
        {
            return airportRepository.AsQueryable().SingleOrDefault(r => (r.Name == name));
        }

        public IList<Airport> GetEmptyPrettyUrls()
        {
            return airportRepository.AsQueryable().Where(r => r.PrettyUrl == null).ToList<Airport>();
        }

        public int Add(Airport airport)
        {
            return airportRepository.Add(airport);
        }

        public void Update(Airport airport)
        {
            airportRepository.Update(airport);
        }
    }
}
