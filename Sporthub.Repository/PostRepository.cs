using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class PostRepository
    {
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public void Update(Sporthub.Model.Post entity)
        {
            var postToUpdate = from t in db.Posts where t.ID == entity.ID select t;

        }

        public void Delete(Sporthub.Model.Post entity)
        {
            throw new NotImplementedException();
        }

        public int Add(Sporthub.Model.Post entity)
        {
            var newPost = new Sporthub.Repository.DataAccess.Post
            {
                ThreadID = entity.ThreadID,
                PostText = entity.PostText,
                IsAdmin = entity.IsAdmin,
                IsSticky = entity.IsSticky,
                IsVisible = entity.IsVisible,
                PostStatusID = entity.PostStatusID,
                CreatedDate = DateTime.Now,
                CreatedUserID = entity.CreatedUserID,
                UpdatedDate = DateTime.Now,
                UpdatedUserID = entity.UpdatedUserID
            };

            db.Posts.InsertOnSubmit(newPost);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
                return -1;
            }

            return newPost.ID;
        }

        public IQueryable<Sporthub.Model.Post> AsQueryable()
        {
            var Post =
                from p in db.Posts
                join t in db.Threads
                on p.ThreadID equals t.ID
                select new Sporthub.Model.Post
                {
                     ID = p.ID,
                     ThreadID = p.ThreadID,
                     PostText = p.PostText,
                     IsAdmin = p.IsAdmin,
                     IsSticky = p.IsSticky,
                     IsVisible = p.IsVisible,
                     PostStatusID = p.PostStatusID,
                     CreatedDate = p.CreatedDate,
                     CreatedUserID = p.CreatedUserID,
                     UpdatedDate = p.UpdatedDate,
                     UpdatedUserID = p.UpdatedUserID,
                     ThreadTitle = t.Title,
                     User =
                        (from u in db.Users
                         where u.ID == p.CreatedUserID
                         select new Sporthub.Model.User
                         {
                             ID = u.ID,
                             FacebookUid = u.FacebookUid,
                             IsSnowboarder = u.IsSnowboarder,
                             IsFreerideBoarder = u.IsFreerideBoarder,
                             IsFreestyleBoarder = u.isFreestyleBoarder,
                             IsAlpineBoarder = u.IsAlpineBoarder,
                             IsSkier = u.IsSkier,
                             IsAlpineSkier = u.IsAlpineSkier,
                             IsLanglaufSkier = u.IsLanglaufSkier,
                             IsNonBoarderSkier = u.IsNonBoarderSkier,
                             UserName = u.UserName,
                             RealName = u.RealName,
                             HasProfilePicture = u.HasProfilePicture,
                             LinkUserSportTypes =
                                (from lrst in db.LinkUserSportTypes
                                 where lrst.UserID == u.ID
                                 select new Sporthub.Model.LinkUserSportType
                                 {
                                     ID = lrst.ID,
                                     UserID = lrst.UserID,
                                     SportTypeID = lrst.SportTypeID,
                                     Seasons = lrst.Seasons,
                                     Level = lrst.Level,
                                     CreatedDate = lrst.CreatedDate,
                                     CreatedUserID = lrst.CreatedUserID,
                                     UpdatedDate = lrst.UpdatedDate,
                                     UpdatedUserID = lrst.UpdatedUserID,
                                     ConfigSportType =
                                        (from cst in db.ConfigSportTypes
                                         where cst.ID == lrst.SportTypeID
                                         select new Sporthub.Model.ConfigSportType
                                         {
                                             ID = cst.ID,
                                             ParentID = cst.ParentID,
                                             Name = cst.Name,
                                             Collective = cst.Collective,
                                             Verb = cst.Verb,
                                             Alias = cst.Alias,
                                             Description = cst.Description,
                                             IsSki = cst.IsSki,
                                             IsSnowboard = cst.IsSnowboard,
                                             IsOther = cst.IsOther
                                         }).SingleOrDefault(),
                                 }).ToList()
                         }).SingleOrDefault()
                };

            return Post;
        }
    }
}
