using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class ResortLinkRepository
    {
        private SporthubDataContext db;
        //private SporthubDataContext db = new SporthubDataContext("Data Source=tcp:esql2k501.discountasp.net;Initial Catalog=SQL2005_615410_sporthub;User ID=SQL2005_615410_sporthub_user;Password=first2009;");
        //private SporthubDataContext db = new SporthubDataContext("Data Source=localhost;Initial Catalog=SQL2005_615410_sporthub;Persist Security Info=True;User ID=sa;Integrated Security=True");

        public ResortLinkRepository()
        {
            db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
        }

        public ResortLinkRepository(bool isWindowsApp)
        {
            db = new SporthubDataContext("Data Source=tcp:esql2k501.discountasp.net;Initial Catalog=SQL2005_615410_sporthub;User ID=SQL2005_615410_sporthub_user;Password=first2010;");
        }

        public void Update(Sporthub.Model.ResortLink entity)
        {
            var resortLinkToUpdate = from rl in db.ResortLinks where rl.ID == entity.ID select rl;

            foreach (Sporthub.Repository.DataAccess.ResortLink link in resortLinkToUpdate)
            {
                link.ResortID = entity.ResortID;
                link.Name = entity.Name;
                link.URL = entity.URL;
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

        public void Delete(Sporthub.Model.ResortLink entity)
        {
            throw new NotImplementedException();
        }

        public int Add(Sporthub.Model.ResortLink entity)
        {
            var newResortLink = new Sporthub.Repository.DataAccess.ResortLink
            {
                Name = entity.Name,
                URL = entity.URL,
                ResortLinkTypeID = 3,
                ResortID = entity.ResortID
            };

            db.ResortLinks.InsertOnSubmit(newResortLink);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newResortLink.ID;
        }

        public IQueryable<Sporthub.Model.ResortLink> AsQueryable()
        {
            var resort =
                from r in db.ResortLinks
                select new Sporthub.Model.ResortLink
                {
                    ID = r.ID,
                    Name = r.Name,
                    URL = r.URL,
                    ResortLinkTypeID = r.ResortLinkTypeID,
                    ResortID = r.ResortID
                };

            return resort;
        }
    }
}
