using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Model.Enumerators;
using Sporthub.Repository.DataAccess;
using System.Configuration;


namespace Sporthub.Services
{
    public class ConfigDataService
    {
        private Sporthub.Repository.DataAccess.SporthubDataContext db = new Sporthub.Repository.DataAccess.SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public IList<ConfigData> GetContinents()
        {
            var continents =
                (
                    from c in db.Continents

                    select new ConfigData
                    {
                        Value = c.ID.ToString(),
                        Text = c.ContinentName
                    }
                )
                .OrderBy(c => c.Text)
                .ToList();

            return continents;
        }

        public IList<ConfigData> GetCountriesByContinent(int continentID)
        {
            var countries =
                (
                    from c in db.Countries
                    where (c.ContinentID == continentID)

                    select new ConfigData
                    {
                        Value = c.ID.ToString(),
                        Text = c.CountryName
                    }
                )
                .OrderBy(c => c.Text)
                .ToList();

            return countries;
        }

        public IList<ConfigData> GetRegionsByCountry(int countryID)
        {
            var regions =
                (
                    from c in db.Regions
                    where (c.CountryID == countryID)

                    select new ConfigData
                    {
                        Value = c.ID.ToString(),
                        Text = c.Name
                    }
                )
                .OrderBy(c => c.Text)
                .ToList();

            return regions;
        }

        public IList<ConfigData> GetCountries()
        {
            var countries =
                (
                    from c in db.Countries
                    select new ConfigData
                    {
                        Value = c.ID.ToString(),
                        Text = c.CountryName
                    }
                )
                .OrderBy(c => c.Text)
                .ToList();

            return countries;
        }

        public IList<ConfigData> GetRegions()
        {
            var regions =
                (
                    from r in db.Regions
                    select new ConfigData
                    {
                        Value = r.ID.ToString(),
                        Text = r.Name
                    }
                )
                .OrderBy(r => r.Text)
                .ToList();

            return regions;
        }
    }
}
