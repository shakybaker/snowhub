using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class ForumRepository
    {
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public void Update(Sporthub.Model.Forum entity)
        {
            var ThreadToUpdate = from f in db.Forums where f.ID == entity.ID select f;


        }

        public void Delete(Sporthub.Model.Forum entity)
        {
            throw new NotImplementedException();
        }

        public void Add(Sporthub.Model.Forum entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Sporthub.Model.Forum> AsQueryable()
        {
            var forum =
                from f in db.Forums
                select new Sporthub.Model.Forum
                {
                    ID = f.ID,
                    ForumName = f.ForumName,
                    ParentID = f.ParentID,
                    Sequence = f.Sequence,
                    Description = f.Description,
                    IsAdmin = f.IsAdmin,
                    CreatedDate = f.CreatedDate,
                    CreatedUserID = f.CreatedUserID,
                    UpdatedDate = f.UpdatedDate,
                    UpdatedUserID = f.UpdatedUserID,
                    Threads =
                        (from t in db.Threads
                         where t.ForumID == f.ID
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
                                      Thread = 
                                          (from t2 in db.Threads
                                           where t2.ID == p.ThreadID
                                           select new Sporthub.Model.Thread
                                           {
                                               Title = t2.Title
                                           }).SingleOrDefault(),
                                      User =
                                         (from u in db.Users
                                          where u.ID == p.CreatedUserID
                                          select new Sporthub.Model.User
                                          {
                                              ID = u.ID,
                                              FacebookUid = u.FacebookUid,
                                              RealName = u.RealName,
                                              UserName = u.UserName,
                                              HasProfilePicture = u.HasProfilePicture,
                                              IsSnowboarder = u.IsSnowboarder,
                                              IsFreerideBoarder = u.IsFreerideBoarder,
                                              IsFreestyleBoarder = u.isFreestyleBoarder,
                                              IsAlpineBoarder = u.IsAlpineBoarder,
                                              IsSkier = u.IsSkier,
                                              IsAlpineSkier = u.IsAlpineSkier,
                                              IsLanglaufSkier = u.IsLanglaufSkier,
                                              IsNonBoarderSkier = u.IsNonBoarderSkier,
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
                                          }).SingleOrDefault()
                                  }).ToList()
                         }).ToList()
                };

            return forum;
        }
    }
}
