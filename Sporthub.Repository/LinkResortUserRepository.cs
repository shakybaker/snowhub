using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class LinkResortUserRepository
    {
        private SporthubDataContext db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
        //private SporthubDataContext db = new SporthubDataContext("Data Source=tcp:esql2k501.discountasp.net;Initial Catalog=SQL2005_615410_sporthub;User ID=SQL2005_615410_sporthub_user;Password=first2009;");
        //private SporthubDataContext db = new SporthubDataContext("Data Source=localhost;Initial Catalog=SQL2005_615410_sporthub;Persist Security Info=True;User ID=sa;Integrated Security=True");

        public void Update(Sporthub.Model.LinkResortUser entity)
        {
            //var resortToUpdate = from r in db.Resorts where r.ID == entity.ID select r;
            //var resortStatsToUpdate = from rs in db.ResortStats where rs.ResortID == entity.ID select rs;

            //foreach (Sporthub.Repository.DataAccess.Resort resort in resortToUpdate)
            //{
            //    resort.ID = entity.ID;
            //    resort.Name = entity.Name;
            //    resort.NameFriendlyFormat = entity.NameFriendlyFormat;
            //    resort.PrettyUrl = entity.PrettyUrl;
            //    resort.PrettyUrlFriendlyFormat = entity.PrettyUrlFriendlyFormat;
            //    resort.Longitude = entity.Longitude;
            //    resort.Latitude = entity.Latitude;
            //    resort.ContinentID = entity.ContinentID;
            //    resort.CountryID = entity.CountryID;
            //    resort.Display = entity.Display;
            //    resort.Score = entity.Score;
            //    resort.ScoreCount = entity.ScoreCount;
            //    resort.ScoreTotal = entity.ScoreTotal;

            //    resort.LiftRating = entity.LiftRating;
            //    resort.LiftTotal = entity.LiftTotal;
            //    resort.LiftCount = entity.LiftCount;
            //    resort.SnowRating = entity.SnowRating;
            //    resort.SnowTotal = entity.ScoreTotal;
            //    resort.SnowCount = entity.ScoreCount;
            //    resort.QueueRating = entity.QueueRating;
            //    resort.QueueTotal = entity.QueueTotal;
            //    resort.QueueCount = entity.QueueCount;
            //    resort.SceneryRating = entity.SceneryRating;
            //    resort.SceneryTotal = entity.SceneryTotal;
            //    resort.SceneryCount = entity.SceneryCount;
            //    resort.ConvenienceRating = entity.ConvenienceRating;
            //    resort.ConvenienceTotal = entity.ConvenienceTotal;
            //    resort.ConvenienceCount = entity.ConvenienceCount;
            //    resort.AccomodationRating = entity.AccomodationRating;
            //    resort.AccomodationTotal = entity.AccomodationTotal;
            //    resort.AccomodationCount = entity.AccomodationCount;
            //    resort.FoodRating = entity.FoodRating;
            //    resort.FoodTotal = entity.FoodTotal;
            //    resort.FoodCount = entity.FoodCount;
            //    resort.FacilitiesRating = entity.FacilitiesRating;
            //    resort.FacilitiesTotal = entity.FacilitiesTotal;
            //    resort.FacilitiesCount = entity.FacilitiesCount;
            //    resort.NightlifeRating = entity.NightlifeRating;
            //    resort.NightlifeTotal = entity.NightlifeTotal;
            //    resort.NightlifeCount = entity.NightlifeCount;

            //    resort.ResortSuitsExpert = entity.ResortSuitsExpert;
            //    resort.ResortSuitsAdvanced = entity.ResortSuitsAdvanced;
            //    resort.ResortSuitsIntermediate = entity.ResortSuitsIntermediate;
            //    resort.ResortSuitsBeginner = entity.ResortSuitsBeginner;

            //    resort.ResortSuitsLively = entity.ResortSuitsLively;
            //    resort.ResortSuitsAverage = entity.ResortSuitsAverage;
            //    resort.ResortSuitsQuiet = entity.ResortSuitsQuiet;

            //    resort.ResortSuitsSkiers = entity.ResortSuitsSkiers;
            //    resort.ResortSuitsSnowboarders = entity.ResortSuitsSnowboarders;
            //    resort.ResortSuitsBoth = entity.ResortSuitsBoth;

            //    resort.ResortSuitsExpensive = entity.ResortSuitsExpensive;
            //    resort.ResortSuitsAffordable = entity.ResortSuitsAffordable;
            //    resort.ResortSuitsCheap = entity.ResortSuitsCheap;
            //    resort.VisitedCount = entity.VisitedCount;
            //    resort.FavedCount = entity.FavedCount;
            //}

            //foreach (Sporthub.Repository.DataAccess.ResortStat stat in resortStatsToUpdate)
            //{
            //    stat.BaseLevel = entity.ResortStats.BaseLevel;
            //    stat.TopLevel = entity.ResortStats.TopLevel;
            //    stat.VerticalDrop = entity.ResortStats.VerticalDrop;

            //    stat.BlackRuns = entity.ResortStats.BlackRuns;
            //    stat.RedRuns = entity.ResortStats.RedRuns;
            //    stat.BlueRuns = entity.ResortStats.BlueRuns;
            //    stat.GreenRuns = entity.ResortStats.GreenRuns;

            //    stat.LiftTotal = entity.ResortStats.LiftTotal;
            //    stat.LiftCapacityHour = entity.ResortStats.LiftTotal;
            //    stat.QuadPlusCount = entity.ResortStats.QuadPlusCount;
            //    stat.QuadCount = entity.ResortStats.QuadCount;
            //    stat.TripleCount = entity.ResortStats.TripleCount;
            //    stat.DoubleCount = entity.ResortStats.DoubleCount;
            //    stat.SurfaceCount = entity.ResortStats.SurfaceCount;
            //    stat.GondolaCount = entity.ResortStats.GondolaCount;
            //    stat.FunicularCount = entity.ResortStats.FunicularCount;
            //    stat.SurfaceTrainCount = entity.ResortStats.SurfaceTrainCount;

            //    stat.LongestRunDistance = entity.ResortStats.LongestRunDistance;
            //    stat.AverageSnowfall = entity.ResortStats.AverageSnowfall;
            //    stat.LongestRunDistance = entity.ResortStats.LongestRunDistance;
            //    stat.SnowmakingCoverage = entity.ResortStats.SnowmakingCoverage;
            //    stat.Snowfall10Oct = entity.ResortStats.Snowfall10Oct;
            //    stat.Snowfall11Nov = entity.ResortStats.Snowfall11Nov;
            //    stat.Snowfall12Dec = entity.ResortStats.Snowfall12Dec;
            //    stat.Snowfall1Jan = entity.ResortStats.Snowfall1Jan;
            //    stat.Snowfall2Feb = entity.ResortStats.Snowfall2Feb;
            //    stat.Snowfall3Mar = entity.ResortStats.Snowfall3Mar;
            //    stat.Snowfall4Apr = entity.ResortStats.Snowfall4Apr;
            //    stat.Snowfall5May = entity.ResortStats.Snowfall5May;
            //    stat.Snowfall6Jun = entity.ResortStats.Snowfall6Jun;
            //    stat.Snowfall7Jul = entity.ResortStats.Snowfall7Jul;
            //    stat.Snowfall8Aug = entity.ResortStats.Snowfall8Aug;
            //    stat.Snowfall9Sep = entity.ResortStats.Snowfall9Sep;
            //}

            //try
            //{
            //    db.SubmitChanges();
            //}
            //catch (Exception ex)
            //{
            //    //logger.Error(ex);
            //    throw ex;
            //}
            throw new NotImplementedException();
        }

        public void Delete(Sporthub.Model.LinkResortUser entity)
        {
            throw new NotImplementedException();
        }

        public int Add(Sporthub.Model.LinkResortUser entity)
        {
            //var newResort = new Sporthub.Repository.DataAccess.Resort
            //{
            //    Name = entity.Name,
            //    PrettyUrl = entity.PrettyUrl,
            //    NameFriendlyFormat = entity.NameFriendlyFormat,
            //    PrettyUrlFriendlyFormat = entity.PrettyUrlFriendlyFormat,
            //    CountryID = entity.CountryID,
            //    CountryName = entity.CountryName,
            //    ContinentID = entity.ContinentID,
            //    ContinentName = entity.ContinentName,
            //    Longitude = entity.Longitude,
            //    Latitude = entity.Latitude,
            //    CanPublish = entity.CanPublish,
            //    CreatedDate = DateTime.Now,
            //    CreatedUserID = 2,//TODO: get from context
            //    UpdatedDate = DateTime.Now,
            //    UpdatedUserID = 2,//TODO: get from context
            //};

            //db.Resorts.InsertOnSubmit(newResort);

            //try
            //{
            //    db.SubmitChanges();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            //return newResort.ID;
            throw new NotImplementedException();
        }

        public IQueryable<Sporthub.Model.LinkResortUser> AsQueryable()
        {
            var linkResortUser =
                from lru in db.LinkResortUsers
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
                     LastVisitDate = lru.LastVisitDate,
                     LiftRating = lru.LiftRating,
                     SnowRating = lru.SnowRating,
                     QueueRating = lru.QueueRating,
                     SceneryRating = lru.SceneryRating,
                     ConvenienceRating = lru.ConvenienceRating,
                     AccomodationRating = lru.AccomodationRating,
                     FoodRating = lru.FoodRating,
                     FacilitiesRating = lru.FacilitiesRating,
                     NightlifeRating = lru.NightlifeRating,

                     CreatedDate = lru.CreatedDate,
                     CreatedUserID = lru.CreatedUserID,
                     UpdatedDate = lru.UpdatedDate,
                     UpdatedUserID = lru.UpdatedUserID,
                     ReviewUsefulnessFeedback =
                         (from ru in db.ReviewUsefulnesses
                          where ru.LinkResortUserID == lru.ID
                          select new Sporthub.Model.ReviewUsefulness()
                          {
                              ID = ru.ID,
                              UserID = ru.UserID,
                              IsUseful = ru.IsUseful
                          }).ToList(),
                     Resort =
                        (from r in db.Resorts
                         where r.ID == lru.ResortID
                         select new Sporthub.Model.Resort
                         {
                             ID = r.ID,
                             Name = r.Name,
                             PrettyUrl = r.PrettyUrl,
                             Continent = 
                                (from ct in db.Continents
                                 where ct.ID == r.ContinentID
                                 select new Sporthub.Model.Continent
                                 {
                                     ID = r.ID,
                                     ContinentName = ct.ContinentName,
                                     PrettyUrl = ct.PrettyUrl
                                 }).SingleOrDefault(),
                             Country = 
                                (from c in db.Countries
                                 where c.ID == r.CountryID
                                 select new Sporthub.Model.Country
                                 {
                                     ID = c.ID,
                                     CountryName = c.CountryName,
                                     PrettyUrl = c.PrettyUrl,
                                     ISO3166Alpha2 = c.ISO3166Alpha2
                                 }).SingleOrDefault()
                         }).SingleOrDefault(),
                     User =
                        (from u in db.Users
                         where u.ID == lru.UserID
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

            return linkResortUser;
        }

        public IList<Sporthub.Model.LinkResortUser> GetLatestReviews(int take)
        {
            var linkResortUser =
                (from lru in db.LinkResortUsers
                 where lru.Score > 0
                 select new Sporthub.Model.LinkResortUser
                {
                    ID = lru.ID,
                    ResortID = lru.ResortID,
                    UserID = lru.UserID,
                    Score = lru.Score,
                    HasVisited = lru.HasVisited,
                    IsFavourite = lru.IsFavourite,
                    Title = lru.Title,
                    CreatedDate = lru.CreatedDate,
                    UpdatedDate = lru.UpdatedDate,
                    Resort =
                       (from r in db.Resorts
                        where r.ID == lru.ResortID
                        select new Sporthub.Model.Resort
                        {
                            ID = r.ID,
                            Name = r.Name,
                            PrettyUrl = r.PrettyUrl,
                            Country =
                               (from c in db.Countries
                                where c.ID == r.CountryID
                                select new Sporthub.Model.Country
                                {
                                    ID = c.ID,
                                    CountryName = c.CountryName,
                                    PrettyUrl = c.PrettyUrl,
                                    ISO3166Alpha2 = c.ISO3166Alpha2
                                }).SingleOrDefault()
                        }).SingleOrDefault(),
                    User =
                       (from u in db.Users
                        where u.ID == lru.UserID
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
                }).OrderByDescending(x => x.CreatedDate).Take(take).ToList<Sporthub.Model.LinkResortUser>();

            return linkResortUser;
        }

        public IList<Sporthub.Model.LinkResortUser> GetLatestReviewsForCountry(int id, int take)
        {
            var linkResortUser =
                (from lru in db.LinkResortUsers
                 where lru.Score > 0 && lru.Resort.Country.ID == id
                 select new Sporthub.Model.LinkResortUser
                 {
                     ID = lru.ID,
                     ResortID = lru.ResortID,
                     UserID = lru.UserID,
                     Score = lru.Score,
                     HasVisited = lru.HasVisited,
                     IsFavourite = lru.IsFavourite,
                     Title = lru.Title,
                     CreatedDate = lru.CreatedDate,
                     UpdatedDate = lru.UpdatedDate,
                     Resort =
                        (from r in db.Resorts
                         where r.ID == lru.ResortID
                         select new Sporthub.Model.Resort
                         {
                             ID = r.ID,
                             Name = r.Name,
                             PrettyUrl = r.PrettyUrl,
                             Country =
                                (from c in db.Countries
                                 where c.ID == r.CountryID
                                 select new Sporthub.Model.Country
                                 {
                                     ID = c.ID,
                                     CountryName = c.CountryName,
                                     PrettyUrl = c.PrettyUrl,
                                     ISO3166Alpha2 = c.ISO3166Alpha2
                                 }).SingleOrDefault()
                         }).SingleOrDefault(),
                     User =
                        (from u in db.Users
                         where u.ID == lru.UserID
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
                 }).OrderByDescending(x => x.CreatedDate).Take(take).ToList<Sporthub.Model.LinkResortUser>();

            return linkResortUser;
        }

        public IList<Sporthub.Model.LinkResortUser> GetLatestReviewsForContinent(int id, int take)
        {
            var linkResortUser =
                (from lru in db.LinkResortUsers
                 where lru.Score > 0 && lru.Resort.Continent.ID == id
                 select new Sporthub.Model.LinkResortUser
                 {
                     ID = lru.ID,
                     ResortID = lru.ResortID,
                     UserID = lru.UserID,
                     Score = lru.Score,
                     HasVisited = lru.HasVisited,
                     IsFavourite = lru.IsFavourite,
                     Title = lru.Title,
                     CreatedDate = lru.CreatedDate,
                     UpdatedDate = lru.UpdatedDate,
                     Resort =
                        (from r in db.Resorts
                         where r.ID == lru.ResortID
                         select new Sporthub.Model.Resort
                         {
                             ID = r.ID,
                             Name = r.Name,
                             PrettyUrl = r.PrettyUrl,
                             Country =
                                (from c in db.Countries
                                 where c.ID == r.CountryID
                                 select new Sporthub.Model.Country
                                 {
                                     ID = c.ID,
                                     CountryName = c.CountryName,
                                     PrettyUrl = c.PrettyUrl,
                                     ISO3166Alpha2 = c.ISO3166Alpha2
                                 }).SingleOrDefault()
                         }).SingleOrDefault(),
                     User =
                        (from u in db.Users
                         where u.ID == lru.UserID
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
                 }).OrderByDescending(x => x.CreatedDate).Take(take).ToList<Sporthub.Model.LinkResortUser>();

            return linkResortUser;
        }

        public IList<Sporthub.Model.LinkResortUser> GetLatestReviewsForResort(int id, int take)
        {
            var linkResortUser =
                (from lru in db.LinkResortUsers
                 where lru.Score > 0 && lru.Resort.ID == id
                 select new Sporthub.Model.LinkResortUser
                 {
                     ID = lru.ID,
                     ResortID = lru.ResortID,
                     UserID = lru.UserID,
                     Score = lru.Score,
                     HasVisited = lru.HasVisited,
                     IsFavourite = lru.IsFavourite,
                     Title = lru.Title,
                     CreatedDate = lru.CreatedDate,
                     UpdatedDate = lru.UpdatedDate,
                     Resort =
                        (from r in db.Resorts
                         where r.ID == lru.ResortID
                         select new Sporthub.Model.Resort
                         {
                             ID = r.ID,
                             Name = r.Name,
                             PrettyUrl = r.PrettyUrl,
                             Country =
                                (from c in db.Countries
                                 where c.ID == r.CountryID
                                 select new Sporthub.Model.Country
                                 {
                                     ID = c.ID,
                                     CountryName = c.CountryName,
                                     PrettyUrl = c.PrettyUrl,
                                     ISO3166Alpha2 = c.ISO3166Alpha2
                                 }).SingleOrDefault()
                         }).SingleOrDefault(),
                     User =
                        (from u in db.Users
                         where u.ID == lru.UserID
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
                 }).OrderByDescending(x => x.CreatedDate).Take(take).ToList<Sporthub.Model.LinkResortUser>();

            return linkResortUser;
        }
    }
}
