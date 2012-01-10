using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class CountryRepository
    {
        private SporthubDataContext db;
        //private SporthubDataContext db = new SporthubDataContext("Data Source=tcp:esql2k501.discountasp.net;Initial Catalog=SQL2005_615410_sporthub;User ID=SQL2005_615410_sporthub_user;Password=first2009;");
        //private SporthubDataContext db = new SporthubDataContext("Data Source=localhost;Initial Catalog=SQL2005_615410_sporthub;Persist Security Info=True;User ID=sa;Integrated Security=True");

        public CountryRepository()
        {
            db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
        }

        public CountryRepository(bool isWindowsApp)
        {
            db = new SporthubDataContext("Data Source=NH-LAPTOP-120;Initial Catalog=sporthub;User ID=sporthubuser;Password=first2010;");
        }

        public void Update(Sporthub.Model.Country entity)
        {
            var CountryToUpdate = from c in db.Countries where c.ID == entity.ID select c;

        }

        public void Delete(Sporthub.Model.Country entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Sporthub.Model.Country entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sporthub.Model.Country> AsQueryable()
        {
            var country =
                from c in db.Countries
                select new Sporthub.Model.Country
                {
                    ID = c.ID,
                    CountryName = c.CountryName,
                    ContinentID = c.ContinentID,
                    ISO3166Alpha2 = c.ISO3166Alpha2,
                    Longitude = c.Longitude,
                    Latitude = c.Latitude,
                    PrettyUrl = c.PrettyUrl,
                    Resorts =
                        (from r in db.Resorts
                        where r.CountryID == c.ID
                        select new Sporthub.Model.Resort
                        {
                            ID = r.ID,
                            Name = r.Name,
                            Longitude = r.Longitude,
                            Latitude = r.Latitude
                        }).ToList()
                };

            return country;
        }
    }
}
