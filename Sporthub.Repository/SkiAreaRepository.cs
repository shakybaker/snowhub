using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class SkiAreaRepository
    {
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public void Update(Sporthub.Model.SkiArea entity)
        {
            var objectToUpdate = from dbObj in db.SkiAreas where dbObj.ID == entity.ID select dbObj;

            foreach (Sporthub.Repository.DataAccess.SkiArea obj in objectToUpdate)
            {
                obj.Name = entity.Name;
                obj.UpdatedDate = DateTime.Now;
                obj.UpdatedUserID = 2; //TODO: to come from user context
            }

            //remove linked resorts from db then add updated ones
            var linkedResorts = (
                from lrsa in db.LinkResortSkiAreas
                where lrsa.SkiAreaID == entity.ID
                select lrsa).ToList();
            db.LinkResortSkiAreas.DeleteAllOnSubmit(linkedResorts);
            db.LinkResortSkiAreas.InsertAllOnSubmit(
                (from r in entity.Resorts
                select new Sporthub.Repository.DataAccess.LinkResortSkiArea
                {
                    ResortID = r.ID,
                    SkiAreaID = entity.ID
                }).ToList());

            //remove linked countries from db then add updated ones
            var linkedCountries = (
                from lsac in db.LinkSkiAreaCountries
                where lsac.SkiAreaID == entity.ID
                select lsac).ToList();
            db.LinkSkiAreaCountries.DeleteAllOnSubmit(linkedCountries);
            db.LinkSkiAreaCountries.InsertAllOnSubmit(
                (from c in entity.Countries
                 select new Sporthub.Repository.DataAccess.LinkSkiAreaCountry
                 {
                     CountryID = c.ID,
                     SkiAreaID = entity.ID
                 }).ToList());

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

        public void Delete(Sporthub.Model.SkiArea entity)
        {
            this.Delete(entity);
        }

        public int Add(Sporthub.Model.SkiArea entity)
        {
            //add skiarea
            var newSkiArea = new Sporthub.Repository.DataAccess.SkiArea
            {
                Name = entity.Name,
                CreatedDate = DateTime.Now,
                CreatedUserID = 2,//TODO: this will come from user context
                UpdatedDate = DateTime.Now,
                UpdatedUserID = 2
            };
            db.SkiAreas.InsertOnSubmit(newSkiArea);

            //add all linked resorts
            var linkedResorts = (
                from r in entity.Resorts
                select new Sporthub.Repository.DataAccess.Resort
                {
                    ID = r.ID
                }).ToList();
            foreach (Sporthub.Repository.DataAccess.Resort resort in linkedResorts)
            {
                var linkedResort = new Sporthub.Repository.DataAccess.LinkResortSkiArea
                {
                    ResortID = resort.ID,
                    SkiAreaID = newSkiArea.ID
                };
                db.LinkResortSkiAreas.InsertOnSubmit(linkedResort);
            }

            //add all linked countries
            var linkedCountries = (
                from c in entity.Countries
                select new Sporthub.Repository.DataAccess.Country
                {
                    ID = c.ID
                }).ToList();
            foreach (Sporthub.Repository.DataAccess.Country country in linkedCountries)
            {
                var linkedCountry = new Sporthub.Repository.DataAccess.LinkSkiAreaCountry
                {
                    CountryID = country.ID,
                    SkiAreaID = newSkiArea.ID
                };
                db.LinkSkiAreaCountries.InsertOnSubmit(linkedCountry);
            }

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newSkiArea.ID;
        }

        public IQueryable<Sporthub.Model.SkiArea> AsQueryable()
        {
            var SkiArea =
                from sa in db.SkiAreas
                select new Sporthub.Model.SkiArea
                {
                    ID = sa.ID,
                    Name = sa.Name,
                    Resorts =
                        (from r in db.Resorts
                         join lrsa in db.LinkResortSkiAreas
                         on r.ID equals lrsa.ResortID into tempLink
                         from link in tempLink.DefaultIfEmpty()
                         select new Sporthub.Model.Resort
                         {
                             ID = r.ID,
                             Name = r.Name
                         }).ToList(),
                    Countries =
                        (from c in db.Countries
                         join lsac in db.LinkSkiAreaCountries
                         on c.ID equals lsac.CountryID into tempLink
                         from link in tempLink.DefaultIfEmpty()
                         select new Sporthub.Model.Country
                         {
                             ID = c.ID,
                             CountryName = c.CountryName
                         }).ToList(),
                };

            return SkiArea;
        }

        public IList<Sporthub.Model.SkiArea> GetAllForResort(int id)
        {
            var skiAreas =
                (from sa in db.SkiAreas
                select new Sporthub.Model.SkiArea
                {
                    ID = sa.ID,
                    Name = sa.Name,
                    Resorts =
                        (from r in db.Resorts
                         join lrsa in db.LinkResortSkiAreas
                         on r.ID equals lrsa.ResortID into tempLink
                         from link in tempLink.DefaultIfEmpty()
                         where r.ID == id
                         select new Sporthub.Model.Resort
                         {
                             ID = r.ID,
                             Name = r.Name
                         }).ToList(),
                    Countries =
                        (from c in db.Countries
                         join lsac in db.LinkSkiAreaCountries
                         on c.ID equals lsac.CountryID into tempLink
                         from link in tempLink.DefaultIfEmpty()
                         select new Sporthub.Model.Country
                         {
                             ID = c.ID,
                             CountryName = c.CountryName
                         }).ToList(),
                }).ToList();

            return skiAreas;
        }

        public IList<Sporthub.Model.SkiArea> GetAllForCountry(int id)
        {
            var skiAreas =
                (from sa in db.SkiAreas
                 select new Sporthub.Model.SkiArea
                 {
                     ID = sa.ID,
                     Name = sa.Name,
                     Resorts =
                         (from r in db.Resorts
                          join lrsa in db.LinkResortSkiAreas
                          on r.ID equals lrsa.ResortID into tempLink
                          from link in tempLink.DefaultIfEmpty()
                          select new Sporthub.Model.Resort
                          {
                              ID = r.ID,
                              Name = r.Name
                          }).ToList(),
                     Countries =
                         (from c in db.Countries
                          join lsac in db.LinkSkiAreaCountries
                          on c.ID equals lsac.CountryID into tempLink
                          from link in tempLink.DefaultIfEmpty()
                          where c.ID == id
                          select new Sporthub.Model.Country
                          {
                              ID = c.ID,
                              CountryName = c.CountryName
                          }).ToList(),
                 }).ToList();

            return skiAreas;
        }
    }
}
