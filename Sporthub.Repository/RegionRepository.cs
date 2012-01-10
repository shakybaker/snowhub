using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class RegionRepository
    {
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public void Update(Sporthub.Model.Region entity)
        {
            var objectToUpdate = from dbObj in db.Regions where dbObj.ID == entity.ID select dbObj;

            foreach (Sporthub.Repository.DataAccess.Region obj in objectToUpdate)
            {
                obj.Name = entity.Name;
                obj.RegionLevel = entity.RegionLevel;
                obj.ParentRegionID = entity.ParentRegionID;
                obj.UpdatedDate = DateTime.Now;
                obj.UpdatedUserID = 2; //TODO: to come from user context
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

        public void Delete(Sporthub.Model.Region entity)
        {
            this.Delete(entity);
        }

        public int Add(Sporthub.Model.Region entity)
        {
            //add skiarea
            var newRegion = new Sporthub.Repository.DataAccess.Region
            {
                Name = entity.Name,
                ParentRegionID = entity.ParentRegionID,
                RegionLevel = entity.RegionLevel,
                CountryID = entity.CountryID,
                CreatedDate = DateTime.Now,
                CreatedUserID = 2,//TODO: this will come from user context
                UpdatedDate = DateTime.Now,
                UpdatedUserID = 2
            };
            db.Regions.InsertOnSubmit(newRegion);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newRegion.ID;
        }

        public IQueryable<Sporthub.Model.Region> AsQueryable()
        {
            var Region =
                from re in db.Regions
                select new Sporthub.Model.Region
                {
                    ID = re.ID,
                    Name = re.Name,
                    ParentRegionID = re.ParentRegionID,
                    RegionLevel = re.RegionLevel,
                    CountryID = re.CountryID,
                    CountryName =
                        (from c in db.Countries
                         where c.ID == re.CountryID
                         select new Sporthub.Model.Country
                         {
                             CountryName = c.CountryName
                         }).ToString()
                    //Resorts =
                    //    (from r in db.Resorts
                    //     where r.RegionID == re.ID
                    //     select new Sporthub.Model.Resort
                    //     {
                    //         ID = r.ID,
                    //         Name = r.Name
                    //     }).ToList(),
                };

            return Region;
        }

        public IList<Sporthub.Model.Region> GetAllForCountry(int id)
        {
            var regions =
                (from re in db.Regions
                where re.CountryID == id && re.RegionLevel == 1
                select new Sporthub.Model.Region
                {
                    ID = re.ID,
                    Name = re.Name,
                    ParentRegionID = re.ParentRegionID,
                    RegionLevel = re.RegionLevel,
                    CountryID = re.CountryID,
                    CountryName = 
                        (from c in db.Countries
                         where c.ID== re.CountryID
                         select new Sporthub.Model.Country
                         {
                             CountryName = c.CountryName
                         }).ToString()
                    //Resorts =
                    //(from r in db.Resorts
                    //where r.RegionID == re.ID
                    //select new Sporthub.Model.Resort
                    //{
                    //    ID = r.ID,
                    //    Name = r.Name
                    //}).ToList(),
                }).ToList();

            return regions;
        }

        public IList<Sporthub.Model.Region> GetAllForRegion(int id)
        {
            var regions =
                (from re in db.Regions
                 where re.ParentRegionID == id && re.RegionLevel > 1
                 select new Sporthub.Model.Region
                 {
                     ID = re.ID,
                     Name = re.Name,
                     ParentRegionID = re.ParentRegionID,
                     RegionLevel = re.RegionLevel,
                     CountryID = re.CountryID,
                     CountryName =
                         (from c in db.Countries
                          where c.ID == re.CountryID
                          select new Sporthub.Model.Country
                          {
                              CountryName = c.CountryName
                          }).ToString()
                     //Resorts =
                     //(from r in db.Resorts
                     // where r.RegionID == re.ID
                     // select new Sporthub.Model.Resort
                     // {
                     //     ID = r.ID,
                     //     Name = r.Name
                     // }).ToList(),
                 }).ToList();

            return regions;
        }
    }
}
