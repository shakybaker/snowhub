using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class PictureRepository
    {
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public void Update(Sporthub.Model.Picture entity)
        {
            var PictureToUpdate = from p in db.Pictures where p.ID == entity.ID select p;

        }

        public void Delete(Sporthub.Model.Picture entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Sporthub.Model.Picture entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sporthub.Model.Picture> AsQueryable()
        {
            var Picture =
                from p in db.Pictures
                join r in db.Resorts
                on p.ResortID equals r.ID into tempResort
                from resort in tempResort.DefaultIfEmpty()
                select new Sporthub.Model.Picture
                {
                    ID = p.ID,
                    OriginalFilename = p.OriginalFilename,
                    Title = p.Title,
                    Description = p.Description,
                    Filename = p.Filename,
                    ResortID = p.ResortID,
                    ResortName = resort.Name,
                    CreatedDate = p.CreatedDate,
                    CreatedUserID = p.CreatedUserID,
                    UpdatedDate = p.UpdatedDate,
                    UpdatedUserID = p.UpdatedUserID,
                };

            return Picture;
        }
    }
}
