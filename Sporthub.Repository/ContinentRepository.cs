using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class ContinentRepository
    {
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public void Update(Sporthub.Model.Continent entity)
        {
            var ContinentToUpdate = from r in db.Continents where r.ID == entity.ID select r;

        }

        public void Delete(Sporthub.Model.Continent entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Sporthub.Model.Continent entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sporthub.Model.Continent> AsQueryable()
        {
            var continent =
                from c in db.Continents
                select new Sporthub.Model.Continent
                {
                    ID = c.ID,
                    ContinentName = c.ContinentName,
                    Code = c.Code,
                    Latitude = c.Latitude,
                    Longitude = c.Longitude,
                    CreatedDate = (DateTime)c.CreatedDate,
                    CreatedUserID = (int)c.CreatedUserID,
                    UpdatedDate = (DateTime)c.UpdatedDate,
                    UpdatedUserID = (int)c.UpdatedUserID,
                    Countries = 
                        (from co in db.Countries
                        where co.ContinentID == c.ID
                        select new Sporthub.Model.Country
                        {
                            Resorts = 
                                (from r in db.Resorts
                                where r.CountryID == co.ID
                                select new Sporthub.Model.Resort
                                {
                                    Name = r.Name
                                }).ToList()
                        }).ToList()


                };

            return continent;
        }
    }
}
