using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class ActivityRepository
    {
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public void Update(Sporthub.Model.Activity entity)
        {
            var activityToUpdate = from u in db.Activities where u.ID == entity.ID select u;

            foreach (Sporthub.Repository.DataAccess.Activity activity in activityToUpdate)
            {
                activity.FacebookUid = entity.FacebookUid;
                activity.ActionClass = entity.ActionClass;
                activity.ActionLink = entity.ActionLink;
                activity.ActionText = entity.ActionText;
                activity.SubjectLink = entity.SubjectLink;
                activity.SubjectText = entity.SubjectText;
                activity.SubjectID = entity.SubjectID;
                activity.FacebookUid = entity.FacebookUid;
                activity.CreatedDate = entity.CreatedDate ?? DateTime.Now;
                activity.CreatedUserID = entity.CreatedUserID ?? 0;
                activity.UpdatedDate = DateTime.Now;
                activity.UpdatedUserID = entity.UpdatedUserID ?? 0;
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

        public void Delete(Sporthub.Model.Activity entity)
        {
            throw new NotImplementedException();
        }

        public int Add(Sporthub.Model.Activity entity)
        {
            var newActivity = new Sporthub.Repository.DataAccess.Activity
            {
                ActionClass = entity.ActionClass,
                ActionLink = entity.ActionLink,
                ActionText = entity.ActionText,
                SubjectLink = entity.SubjectLink,
                SubjectText = entity.SubjectText,
                SubjectID = entity.SubjectID,
                FacebookUid = entity.FacebookUid,
                CreatedDate = DateTime.Now,
                CreatedUserID = entity.CreatedUserID ?? 0,
                UpdatedDate = DateTime.Now,
                UpdatedUserID = entity.UpdatedUserID ?? 0
            };

            db.Activities.InsertOnSubmit(newActivity);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newActivity.ID;
        }

        public IQueryable<Sporthub.Model.Activity> AsQueryable()
        {
            var activity =
                from a in db.Activities
                select new Sporthub.Model.Activity
                {
                    ID = a.ID,
                    ActionClass = a.ActionClass,
                    ActionLink = a.ActionLink,
                    ActionText = a.ActionText,
                    SubjectLink = a.SubjectLink,
                    SubjectText = a.SubjectText,
                    SubjectID = a.SubjectID ?? 0,
                    FacebookUid = a.FacebookUid,
                    CreatedDate = a.CreatedDate,
                    CreatedUserID = a.CreatedUserID,
                    UpdatedDate = a.UpdatedDate,
                    UpdatedUserID = a.UpdatedUserID,
                    User =
                   (from u in db.Users
                    where u.ID == a.UserID
                    select new Sporthub.Model.User
                    {
                        ID = u.ID,
                        UserName = u.UserName,
                        Email = u.Email,
                        RealName = u.RealName,
                        UserRoleID = u.UserRoleID,
                        FacebookUid = u.FacebookUid,
                        HasProfilePicture = u.HasProfilePicture
                    }).SingleOrDefault()
                };

            return activity;
        }
    }
}
