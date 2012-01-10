using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class ReviewUsefulnessRepository
    {
        private SporthubDataContext db;
        //private SporthubDataContext db = new SporthubDataContext("Data Source=tcp:esql2k501.discountasp.net;Initial Catalog=SQL2005_615410_sporthub;User ID=SQL2005_615410_sporthub_user;Password=first2009;");
        //private SporthubDataContext db = new SporthubDataContext("Data Source=localhost;Initial Catalog=SQL2005_615410_sporthub;Persist Security Info=True;User ID=sa;Integrated Security=True");

        public ReviewUsefulnessRepository()
        {
            db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
        }

        public ReviewUsefulnessRepository(bool isWindowsApp)
        {
            db = new SporthubDataContext("Data Source=tcp:esql2k501.discountasp.net;Initial Catalog=SQL2005_615410_sporthub;User ID=SQL2005_615410_sporthub_user;Password=first2010;");
        }

        public void Update(Sporthub.Model.ReviewUsefulness entity)
        {
            var toUpdate = from r in db.ReviewUsefulnesses where r.ID == entity.ID select r;

            foreach (Sporthub.Repository.DataAccess.ReviewUsefulness ru in toUpdate)
            {
                ru.ID = entity.ID;
                ru.IsUseful = entity.IsUseful;
                ru.LinkResortUserID = entity.LinkResortUserID;
                ru.UserID = entity.UserID;
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

        public void Delete(Sporthub.Model.ReviewUsefulness entity)
        {
            throw new NotImplementedException();
        }

        public int Add(Sporthub.Model.ReviewUsefulness entity)
        {
            var newReviewUsefulness = new Sporthub.Repository.DataAccess.ReviewUsefulness
            {
                ID = entity.ID,
                IsUseful = entity.IsUseful,
                LinkResortUserID = entity.LinkResortUserID,
                UserID = entity.UserID
            };

            db.ReviewUsefulnesses.InsertOnSubmit(newReviewUsefulness);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newReviewUsefulness.ID;
        }
    }
}
