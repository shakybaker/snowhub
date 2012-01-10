using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class AirportRepository
    {
        private SporthubDataContext db;
        //private SporthubDataContext db = new SporthubDataContext("Data Source=tcp:esql2k501.discountasp.net;Initial Catalog=SQL2005_615410_sporthub;User ID=SQL2005_615410_sporthub_user;Password=first2009;");
        //private SporthubDataContext db = new SporthubDataContext("Data Source=localhost;Initial Catalog=SQL2005_615410_sporthub;Persist Security Info=True;User ID=sa;Integrated Security=True");

        public AirportRepository()
        {
            db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
        }

        public AirportRepository(bool isWindowsApp)
        {
            db = new SporthubDataContext("Data Source=tcp:esql2k501.discountasp.net;Initial Catalog=SQL2005_615410_sporthub;User ID=SQL2005_615410_sporthub_user;Password=first2010;");
        }

        public void Update(Sporthub.Model.Airport entity)
        {
            var airportToUpdate = from r in db.Airports where r.ID == entity.ID select r;
            foreach (Sporthub.Repository.DataAccess.Airport airport in airportToUpdate)
            {
                airport.Name = entity.Name;
                airport.Code = entity.Code;
                airport.PrettyUrl = entity.PrettyUrl;
                airport.NameFriendlyFormat = entity.NameFriendlyFormat;
                airport.PrettyUrlFriendlyFormat = entity.PrettyUrlFriendlyFormat;
                airport.CountryID = entity.CountryID;
                airport.CountryName = entity.CountryName;
                airport.ContinentID = entity.ContinentID;
                airport.ContinentName = entity.ContinentName;
                airport.Longitude = entity.Longitude;
                airport.Latitude = entity.Latitude;
                airport.CreatedDate = entity.CreatedDate;
                airport.CreatedUserID = entity.CreatedUserID;
                airport.UpdatedDate = DateTime.Now;
                airport.UpdatedUserID = entity.UpdatedUserID;
            }

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                throw ex;
            }
        }

        public void Delete(Sporthub.Model.Airport entity)
        {
            throw new NotImplementedException();
        }

        public int Add(Sporthub.Model.Airport entity)
        {
            var newAirport = new Sporthub.Repository.DataAccess.Airport
            {
                Name = entity.Name,
                Code = entity.Code,
                PrettyUrl = entity.PrettyUrl,
                NameFriendlyFormat = entity.NameFriendlyFormat,
                PrettyUrlFriendlyFormat = entity.PrettyUrlFriendlyFormat,
                CountryID = entity.CountryID,
                CountryName = entity.CountryName,
                ContinentID = entity.ContinentID,
                ContinentName = entity.ContinentName,
                Longitude = entity.Longitude,
                Latitude = entity.Latitude,
                CreatedDate = DateTime.Now,
                CreatedUserID = entity.CreatedUserID,
                UpdatedDate = DateTime.Now,
                UpdatedUserID = entity.UpdatedUserID
            };

            db.Airports.InsertOnSubmit(newAirport);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newAirport.ID;
        }

        public IQueryable<Sporthub.Model.Airport> AsQueryable()
        {
            var airport =
                from r in db.Airports
                select new Sporthub.Model.Airport
                {
                    ID = r.ID,
                    Name = r.Name,
                    Code = r.Code,
                    PrettyUrl = r.PrettyUrl,
                    NameFriendlyFormat = r.NameFriendlyFormat,
                    PrettyUrlFriendlyFormat = r.PrettyUrlFriendlyFormat,
                    CountryID = r.CountryID,
                    CountryName = r.CountryName,
                    ContinentID = r.ContinentID,
                    ContinentName = r.ContinentName,
                    Longitude = r.Longitude,
                    Latitude = r.Latitude,
                    Website = r.Website,
                    CreatedDate = r.CreatedDate,
                    CreatedUserID = r.CreatedUserID,
                    UpdatedDate = r.UpdatedDate,
                    UpdatedUserID = r.UpdatedUserID,
                    Country =
                        (from c in db.Countries
                         where c.ID == r.CountryID
                         select new Sporthub.Model.Country
                         {
                             ID = c.ID,
                             CountryName = c.CountryName,
                             ISO3166Alpha2 = c.ISO3166Alpha2,
                             PrettyUrl = c.PrettyUrl
                         }).SingleOrDefault(),
                    NearbyResorts =
                        (from nr in db.Resorts
                         where ((nr.Latitude >= r.Latitude - 4)
                         && (nr.Latitude <= r.Latitude + 4)
                         && (nr.Longitude >= r.Longitude - 4)
                         && (nr.Longitude <= r.Longitude + 4)
                         && (!nr.IsSkiArea)
                         && (nr.ID != r.ID)
                         && (nr.Display == true))
                         select new Sporthub.Model.Resort
                         {
                             ID = nr.ID,
                             Name = nr.Name,
                             PrettyUrl = nr.PrettyUrl,
                             Latitude = nr.Latitude,
                             Longitude = nr.Longitude,
                             Country =
                            (from c2 in db.Countries
                             where c2.ID == nr.CountryID
                             select new Sporthub.Model.Country
                             {
                                 CountryName = c2.CountryName,
                                 ISO3166Alpha2 = c2.ISO3166Alpha2
                             }).SingleOrDefault()
                         }).OrderBy(x => x.Name).ToList()
                };

            return airport;
        }
    }
}
