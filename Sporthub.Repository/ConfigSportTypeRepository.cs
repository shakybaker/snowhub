using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class ConfigSportTypeRepository
    {
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public void Update(Sporthub.Model.ConfigSportType entity)
        {
            var configSportTypeToUpdate = from cst in db.ConfigSportTypes where cst.ID == entity.ID select cst;

        }

        public void Delete(Sporthub.Model.ConfigSportType entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Sporthub.Model.ConfigSportType entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sporthub.Model.ConfigSportType> AsQueryable()
        {
            var configSportType =
                from r in db.ConfigSportTypes
                select new Sporthub.Model.ConfigSportType
                {
                    ID = r.ID,
                    ParentID = r.ParentID,
                    Name = r.Name,
                    Collective = r.Collective,
                    Verb = r.Verb,
                    Alias = r.Alias,
                    Description = r.Description,
                    IsSki = r.IsSki,
                    IsSnowboard = r.IsSnowboard,
                    IsOther = r.IsOther
                };

            return configSportType;
        }
    }
}
