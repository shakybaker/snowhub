using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class ThreadRepository
    {
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public void Update(Sporthub.Model.Thread entity)
        {
            var ThreadToUpdate = from t in db.Threads where t.ID == entity.ID select t;

        }

        public void Delete(Sporthub.Model.Thread entity)
        {
            throw new NotImplementedException();
        }

        public int Add(Sporthub.Model.Thread entity)
        {
            var newThread = new Sporthub.Repository.DataAccess.Thread
            {
                ForumID = entity.ForumID,
                ThreadStatusID = entity.ThreadStatusID,
                Title = entity.Title,
                IsVisible = entity.IsVisible,
                IsAdmin = entity.IsAdmin,
                IsSticky = entity.IsSticky,
                ResortID = entity.ResortID,
                CreatedDate = DateTime.Now,
                CreatedUserID = entity.CreatedUserID,
                UpdatedDate = DateTime.Now,
                UpdatedUserID = entity.UpdatedUserID
            };

            db.Threads.InsertOnSubmit(newThread);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newThread.ID;
        }

        public IQueryable<Sporthub.Model.Thread> AsQueryable()
        {
            var Thread =
                from t in db.Threads
                select new Sporthub.Model.Thread
                {
                    ID = t.ID,
                    ForumID = t.ForumID,
                    ThreadStatusID = t.ThreadStatusID,
                    Title = t.Title,
                    IsVisible = t.IsVisible,
                    IsAdmin = t.IsAdmin,
                    IsSticky = t.IsSticky,
                    ResortID = t.ResortID,
                    CreatedDate = t.CreatedDate,
                    CreatedUserID = t.CreatedUserID,
                    UpdatedDate = t.UpdatedDate,
                    UpdatedUserID = t.UpdatedUserID,
                    StartedBy = 
                              (from u in db.Users
                              where u.ID == t.CreatedUserID
                              select new Sporthub.Model.User
                              {
                                  ID = u.ID,
                                  SignupStageReachedID = u.SignupStageReachedID,
                                  ReferrerURL = u.ReferrerURL,
                                  UserName = u.UserName,
                                  HasProfilePicture = u.HasProfilePicture,
                                  Email = u.Email,
                                  Phone = u.Phone,
                                  RealName = u.RealName,
                                  Password = u.Password,
                                  UserRoleID = u.UserRoleID,
                                  Sex = u.Sex,
                                  Age = u.Age,
                                  DobDay = u.DobDay,
                                  DobMonth = u.DobMonth,
                                  DobYear = u.DobYear,
                                  UsualCity = u.UsualCity,
                                  UsualCountryID = u.UsualCountryID,
                                  IsSnowboarder = u.IsSnowboarder,
                                  IsFreerideBoarder = u.IsFreerideBoarder,
                                  IsFreestyleBoarder = u.isFreestyleBoarder,
                                  IsAlpineBoarder = u.IsAlpineBoarder,
                                  IsSkier = u.IsSkier,
                                  IsAlpineSkier = u.IsAlpineSkier,
                                  IsLanglaufSkier = u.IsLanglaufSkier,
                                  IsNonBoarderSkier = u.IsNonBoarderSkier,
                                  Points = u.Points,
                                  AddThreshold = u.AddThreshold,
                                  IPAddressOriginal = u.IPAddressOriginal,
                                  IPAddressLast = u.IPAddressLast,
                                  LastVisitDate = u.LastVisitDate,
                                  CreatedDate = u.CreatedDate,
                                  CreatedUserID = u.CreatedUserID,
                                  UpdatedDate = u.UpdatedDate,
                                  UpdatedUserID = u.UpdatedUserID,
                                  FacebookUid = u.FacebookUid,
                                  LinkResortUsers =
                                      (from lru in db.LinkResortUsers
                                       where lru.UserID == u.ID
                                       select new Sporthub.Model.LinkResortUser
                                       {
                                           ID = lru.ID,
                                           ResortID = lru.ResortID,
                                           UserID = lru.UserID,
                                           Score = lru.Score,
                                           HasVisited = lru.HasVisited,
                                           IsFavourite = lru.IsFavourite,
                                           Title = lru.Title,
                                           ReviewText = lru.ReviewText,
                                           CreatedDate = lru.CreatedDate,
                                           CreatedUserID = lru.CreatedUserID,
                                           UpdatedDate = lru.UpdatedDate,
                                           UpdatedUserID = lru.UpdatedUserID
                                       }).ToList(),
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
                                               }).SingleOrDefault()
                                       }).ToList()
                              }).SingleOrDefault(),
                    Posts =
                        (from p in db.Posts
                         where p.ThreadID == t.ID
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
                             User =
                              (from u in db.Users
                              where u.ID == p.CreatedUserID
                              select new Sporthub.Model.User
                              {
                                  ID = u.ID,
                                  SignupStageReachedID = u.SignupStageReachedID,
                                  ReferrerURL = u.ReferrerURL,
                                  UserName = u.UserName,
                                  HasProfilePicture = u.HasProfilePicture,
                                  Email = u.Email,
                                  Phone = u.Phone,
                                  RealName = u.RealName,
                                  Password = u.Password,
                                  UserRoleID = u.UserRoleID,
                                  Sex = u.Sex,
                                  Age = u.Age,
                                  DobDay = u.DobDay,
                                  DobMonth = u.DobMonth,
                                  DobYear = u.DobYear,
                                  UsualCity = u.UsualCity,
                                  UsualCountryID = u.UsualCountryID,
                                  IsSnowboarder = u.IsSnowboarder,
                                  IsFreerideBoarder = u.IsFreerideBoarder,
                                  IsFreestyleBoarder = u.isFreestyleBoarder,
                                  IsAlpineBoarder = u.IsAlpineBoarder,
                                  IsSkier = u.IsSkier,
                                  IsAlpineSkier = u.IsAlpineSkier,
                                  IsLanglaufSkier = u.IsLanglaufSkier,
                                  IsNonBoarderSkier = u.IsNonBoarderSkier,
                                  Points = u.Points,
                                  AddThreshold = u.AddThreshold,
                                  IPAddressOriginal = u.IPAddressOriginal,
                                  IPAddressLast = u.IPAddressLast,
                                  LastVisitDate = u.LastVisitDate,
                                  CreatedDate = u.CreatedDate,
                                  CreatedUserID = u.CreatedUserID,
                                  UpdatedDate = u.UpdatedDate,
                                  UpdatedUserID = u.UpdatedUserID,
                                  LinkResortUsers =
                                      (from lru in db.LinkResortUsers
                                       where lru.UserID == u.ID
                                       select new Sporthub.Model.LinkResortUser
                                       {
                                           ID = lru.ID,
                                           ResortID = lru.ResortID,
                                           UserID = lru.UserID,
                                           Score = lru.Score,
                                           HasVisited = lru.HasVisited,
                                           IsFavourite = lru.IsFavourite,
                                           Title = lru.Title,
                                           ReviewText = lru.ReviewText,
                                           CreatedDate = lru.CreatedDate,
                                           CreatedUserID = lru.CreatedUserID,
                                           UpdatedDate = lru.UpdatedDate,
                                           UpdatedUserID = lru.UpdatedUserID
                                       }).ToList(),
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
                         }).OrderBy(p => p.UpdatedDate).ToList()
                };

            return Thread;
        }
    }
}
