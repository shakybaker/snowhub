using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class CheckInRepository
    {
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);

        public void Update(Sporthub.Model.CheckIn entity)
        {
            var checkInToUpdate = from ci in db.CheckIns where ci.ID == entity.ID select ci;

            foreach (Sporthub.Repository.DataAccess.CheckIn checkIn in checkInToUpdate)
            {
                checkIn.CheckinDate = entity.CheckinDate;
                checkIn.IPAddress = entity.IPAddress;
                checkIn.ResortID = entity.ResortID;
                checkIn.Status = entity.Status;
                checkIn.UserID = entity.UserID;
                checkIn.IsActive = entity.IsActive;
                checkIn.Latitude = entity.Latitude;
                checkIn.Longitude = entity.Longitude;
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

        public void UpdateAllToInactiveForUser(int userID)
        {
            var checkInToUpdate = from ci in db.CheckIns where ci.UserID == userID select ci;

            foreach (Sporthub.Repository.DataAccess.CheckIn checkIn in checkInToUpdate)
            {
                checkIn.IsActive = false;
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

        public void Delete(Sporthub.Model.User entity)
        {
            throw new NotImplementedException();
        }

        public int Add(Sporthub.Model.CheckIn entity)
        {
            var newCheckIn = new Sporthub.Repository.DataAccess.CheckIn
            {
                CheckinDate = entity.CheckinDate,
                IPAddress = entity.IPAddress,
                ResortID = entity.ResortID,
                Status = entity.Status,
                UserID = entity.UserID,
                IsActive = entity.IsActive,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude
            };

            db.CheckIns.InsertOnSubmit(newCheckIn);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                newCheckIn.ID = 0;
            }

            return newCheckIn.ID;
        }

        //public IQueryable<Sporthub.Model.User> AsQueryable()
        //{
        //    var user =
        //        from u in db.Users
        //        select new Sporthub.Model.User
        //        {
        //            ID = u.ID,
        //            SignupStageReachedID = u.SignupStageReachedID,
        //            ReferrerURL = u.ReferrerURL,
        //            UserName = u.UserName,
        //            Email = u.Email,
        //            Phone = u.Phone,
        //            RealName = u.RealName,
        //            Password = u.Password,
        //            UserRoleID = u.UserRoleID,
        //            Sex = u.Sex,
        //            Age = u.Age,
        //            DobDay = u.DobDay,
        //            DobMonth = u.DobMonth,
        //            DobYear = u.DobYear,
        //            UsualCity = u.UsualCity,
        //            UsualCountryID = u.UsualCountryID,
        //            IsSnowboarder = u.IsSnowboarder,
        //            IsFreerideBoarder = u.IsFreerideBoarder,
        //            IsFreestyleBoarder = u.isFreestyleBoarder,
        //            IsAlpineBoarder = u.IsAlpineBoarder,
        //            IsSkier = u.IsSkier,
        //            IsAlpineSkier = u.IsAlpineSkier,
        //            IsLanglaufSkier = u.IsLanglaufSkier,
        //            IsNonBoarderSkier = u.IsNonBoarderSkier,
        //            Points = u.Points,
        //            AddThreshold = u.AddThreshold,
        //            IPAddressOriginal = u.IPAddressOriginal,
        //            IPAddressLast = u.IPAddressLast,
        //            LastVisitDate = u.LastVisitDate,
        //            FacebookUid = u.FacebookUid,
        //            CreatedDate = u.CreatedDate,
        //            CreatedUserID = u.CreatedUserID,
        //            UpdatedDate = u.UpdatedDate,
        //            UpdatedUserID = u.UpdatedUserID,
        //            Country =
        //            (from c in db.Countries
        //             where c.ID == u.UsualCountryID
        //             select new Sporthub.Model.Country
        //             {
        //                 ID = c.ID,
        //                 CountryName = c.CountryName,
        //                 ISO3166Alpha2 = c.ISO3166Alpha2
        //             }).SingleOrDefault(),
        //            UserRole =
        //            (from ur in db.UserRoles
        //             where ur.ID == u.UserRoleID
        //             select new Sporthub.Model.UserRole
        //             {
        //                 ID = ur.ID,
        //                 Name = ur.Name,
        //                 UserRoleCode = ur.UserRoleCode,
        //                 UserRoleDesc = ur.UserRoleDesc,
        //                 IsAdmin = ur.IsAdmin,
        //                 IsDataAdmin = ur.IsDataAdmin,
        //                 IsModerator = ur.IsModerator,
        //                 IsSuperAdmin = ur.IsSuperAdmin,
        //                 IsUserAdmin = ur.IsUserAdmin,
        //                 PointsRequired = ur.PointsRequired
        //             }).SingleOrDefault(),
        //            //Pictures =
        //            //    (from p in db.Pictures
        //            //     where p.CreatedUserID == u.ID
        //            //     select new Sporthub.Model.Picture
        //            //     {
        //            //         ID = p.ID,
        //            //         Description = p.Description,
        //            //         Filename = p.Filename,
        //            //         OriginalFilename = p.OriginalFilename,
        //            //         Title = p.Title,
        //            //         CreatedDate = p.CreatedDate,
        //            //         CreatedUserID = p.CreatedUserID,
        //            //         UpdatedDate = p.UpdatedDate,
        //            //         UpdatedUserID = p.UpdatedUserID
        //            //     }).ToList(),
        //            CheckIns =
        //                (from ci in db.CheckIns
        //                 where ci.UserID == u.ID
        //                 select new Sporthub.Model.CheckIn
        //                 {
        //                     ID = ci.ID,
        //                     CheckinDate = ci.CheckinDate,
        //                     IPAddress = ci.IPAddress,
        //                     ResortID = ci.ResortID,
        //                     Status = ci.Status,
        //                     UserID = ci.UserID
        //                 }).ToList(),
        //            LinkResortUsers =
        //                (from lru in db.LinkResortUsers
        //                 where lru.UserID == u.ID
        //                 select new Sporthub.Model.LinkResortUser
        //                 {
        //                     ID = lru.ID,
        //                     ResortID = lru.ResortID,
        //                     UserID = lru.UserID,
        //                     Score = lru.Score,
        //                     HasVisited = lru.HasVisited,
        //                     IsFavourite = lru.IsFavourite,
        //                     Title = lru.Title,
        //                     ReviewText = lru.ReviewText,
        //                     CreatedDate = lru.CreatedDate,
        //                     CreatedUserID = lru.CreatedUserID,
        //                     UpdatedDate = lru.UpdatedDate,
        //                     UpdatedUserID = lru.UpdatedUserID,
        //                     Resort =
        //                        (from r in db.Resorts
        //                         where r.ID == lru.ResortID
        //                         select new Sporthub.Model.Resort
        //                         {
        //                             ID = r.ID,
        //                             Name = r.Name,
        //                             PrettyUrl = r.PrettyUrl,
        //                             Country =
        //                                (from c in db.Countries
        //                                 where c.ID == r.CountryID
        //                                 select new Sporthub.Model.Country
        //                                 {
        //                                     ID = r.ID,
        //                                     CountryName = c.CountryName,
        //                                     ISO3166Alpha2 = c.ISO3166Alpha2,
        //                                     PrettyUrl = c.PrettyUrl
        //                                 }).SingleOrDefault()
        //                         }).SingleOrDefault()
        //                 }).ToList(),
        //            LinkUserSportTypes =
        //                (from lrst in db.LinkUserSportTypes
        //                 where lrst.UserID == u.ID
        //                 select new Sporthub.Model.LinkUserSportType
        //                 {
        //                     ID = lrst.ID,
        //                     UserID = lrst.UserID,
        //                     SportTypeID = lrst.SportTypeID,
        //                     Seasons = lrst.Seasons,
        //                     Level = lrst.Level,
        //                     CreatedDate = lrst.CreatedDate,
        //                     CreatedUserID = lrst.CreatedUserID,
        //                     UpdatedDate = lrst.UpdatedDate,
        //                     UpdatedUserID = lrst.UpdatedUserID,
        //                     ConfigSportType =
        //                        (from cst in db.ConfigSportTypes
        //                         where cst.ID == lrst.SportTypeID
        //                         select new Sporthub.Model.ConfigSportType
        //                         {
        //                             ID = cst.ID,
        //                             ParentID = cst.ParentID,
        //                             Name = cst.Name,
        //                             Collective = cst.Collective,
        //                             Verb = cst.Verb,
        //                             Alias = cst.Alias,
        //                             Description = cst.Description,
        //                             IsSki = cst.IsSki,
        //                             IsSnowboard = cst.IsSnowboard,
        //                             IsOther = cst.IsOther
        //                         }).SingleOrDefault(),
        //                 }).ToList()
        //        };

        //    return user;
        //}
    }
}
