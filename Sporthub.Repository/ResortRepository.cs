using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Repository.DataAccess;
using System.Configuration;

namespace Sporthub.Repository
{
    public class ResortRepository
    {
        private SporthubDataContext db;
        //private SporthubDataContext db = new SporthubDataContext("Data Source=tcp:esql2k501.discountasp.net;Initial Catalog=SQL2005_615410_sporthub;User ID=SQL2005_615410_sporthub_user;Password=first2009;");
        //private SporthubDataContext db = new SporthubDataContext("Data Source=localhost;Initial Catalog=SQL2005_615410_sporthub;Persist Security Info=True;User ID=sa;Integrated Security=True");

        public ResortRepository()
        {
            db = new SporthubDataContext(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
        }

        public ResortRepository(bool isWindowsApp)
        {
            db = new SporthubDataContext("Data Source=NH-LAPTOP-120;Initial Catalog=sporthub;User ID=sporthubuser;Password=first2010;");
        }

        public void Update(Sporthub.Model.Resort entity)
        {
            var resortToUpdate = from r in db.Resorts where r.ID == entity.ID select r;
            var resortStatsToUpdate = from rs in db.ResortStats where rs.ResortID == entity.ID select rs;
            var skiAreasToUpdate = from sa in db.LinkResortSkiAreas where sa.SkiAreaID == entity.ID select sa;

            foreach (Sporthub.Repository.DataAccess.Resort resort in resortToUpdate)
            {
                resort.ID = entity.ID;
                resort.Name = entity.Name;
                resort.NameFriendlyFormat = entity.NameFriendlyFormat;
                resort.PrettyUrl = entity.PrettyUrl;
                resort.PrettyUrlFriendlyFormat = entity.PrettyUrlFriendlyFormat;
                resort.Longitude = entity.Longitude;
                resort.Latitude = entity.Latitude;
                resort.ContinentID = entity.ContinentID;
                resort.CountryID = entity.CountryID;
                resort.Display = entity.Display;
                resort.Score = entity.Score;
                resort.ScoreCount = entity.ScoreCount;
                resort.ScoreTotal = entity.ScoreTotal;

                resort.LiftRating = entity.LiftRating;
                resort.LiftTotal = entity.LiftTotal;
                resort.LiftCount = entity.LiftCount;
                resort.SnowRating = entity.SnowRating;
                resort.SnowTotal = entity.ScoreTotal;
                resort.SnowCount = entity.ScoreCount;
                resort.QueueRating = entity.QueueRating;
                resort.QueueTotal = entity.QueueTotal;
                resort.QueueCount = entity.QueueCount;
                resort.SceneryRating = entity.SceneryRating;
                resort.SceneryTotal = entity.SceneryTotal;
                resort.SceneryCount = entity.SceneryCount;
                resort.ConvenienceRating = entity.ConvenienceRating;
                resort.ConvenienceTotal = entity.ConvenienceTotal;
                resort.ConvenienceCount = entity.ConvenienceCount;
                resort.AccomodationRating = entity.AccomodationRating;
                resort.AccomodationTotal = entity.AccomodationTotal;
                resort.AccomodationCount = entity.AccomodationCount;
                resort.FoodRating = entity.FoodRating;
                resort.FoodTotal = entity.FoodTotal;
                resort.FoodCount = entity.FoodCount;
                resort.FacilitiesRating = entity.FacilitiesRating;
                resort.FacilitiesTotal = entity.FacilitiesTotal;
                resort.FacilitiesCount = entity.FacilitiesCount;
                resort.NightlifeRating = entity.NightlifeRating;
                resort.NightlifeTotal = entity.NightlifeTotal;
                resort.NightlifeCount = entity.NightlifeCount;

                resort.ResortSuitsExpert = entity.ResortSuitsExpert;
                resort.ResortSuitsAdvanced = entity.ResortSuitsAdvanced;
                resort.ResortSuitsIntermediate = entity.ResortSuitsIntermediate;
                resort.ResortSuitsBeginner = entity.ResortSuitsBeginner;

                resort.ResortSuitsLively = entity.ResortSuitsLively;
                resort.ResortSuitsAverage = entity.ResortSuitsAverage;
                resort.ResortSuitsQuiet = entity.ResortSuitsQuiet;

                resort.ResortSuitsSkiers = entity.ResortSuitsSkiers;
                resort.ResortSuitsSnowboarders = entity.ResortSuitsSnowboarders;
                resort.ResortSuitsBoth = entity.ResortSuitsBoth;

                resort.ResortSuitsExpensive = entity.ResortSuitsExpensive;
                resort.ResortSuitsAffordable = entity.ResortSuitsAffordable;
                resort.ResortSuitsCheap = entity.ResortSuitsCheap;
                resort.VisitedCount = entity.VisitedCount;
                resort.FavedCount = entity.FavedCount;
                resort.Display = entity.Display;
                resort.IsSkiArea = entity.IsSkiArea;
            }

            foreach (Sporthub.Repository.DataAccess.ResortStat stat in resortStatsToUpdate)
            {
                stat.BaseLevel = entity.ResortStats.BaseLevel;
                stat.TopLevel = entity.ResortStats.TopLevel;
                stat.VerticalDrop = entity.ResortStats.VerticalDrop;

                stat.BlackRuns = entity.ResortStats.BlackRuns;
                stat.RedRuns = entity.ResortStats.RedRuns;
                stat.BlueRuns = entity.ResortStats.BlueRuns;
                stat.GreenRuns = entity.ResortStats.GreenRuns;

                stat.LiftTotal = entity.ResortStats.LiftTotal;
                stat.LiftCapacityHour = entity.ResortStats.LiftTotal;
                stat.QuadPlusCount = entity.ResortStats.QuadPlusCount;
                stat.QuadCount = entity.ResortStats.QuadCount;
                stat.TripleCount = entity.ResortStats.TripleCount;
                stat.DoubleCount = entity.ResortStats.DoubleCount;
                stat.SurfaceCount = entity.ResortStats.SurfaceCount;
                stat.GondolaCount = entity.ResortStats.GondolaCount;
                stat.FunicularCount = entity.ResortStats.FunicularCount;
                stat.SurfaceTrainCount = entity.ResortStats.SurfaceTrainCount;

                stat.LongestRunDistance = entity.ResortStats.LongestRunDistance;
                stat.RunTotalDistance = entity.ResortStats.RunTotalDistance;
                stat.AverageSnowfall = entity.ResortStats.AverageSnowfall;
                stat.LongestRunDistance = entity.ResortStats.LongestRunDistance;
                stat.SnowmakingCoverage = entity.ResortStats.SnowmakingCoverage;
                stat.HasSnowmaking = entity.ResortStats.HasSnowmaking;
                stat.Snowfall10Oct = entity.ResortStats.Snowfall10Oct;
                stat.Snowfall11Nov = entity.ResortStats.Snowfall11Nov;
                stat.Snowfall12Dec = entity.ResortStats.Snowfall12Dec;
                stat.Snowfall1Jan = entity.ResortStats.Snowfall1Jan;
                stat.Snowfall2Feb = entity.ResortStats.Snowfall2Feb;
                stat.Snowfall3Mar = entity.ResortStats.Snowfall3Mar;
                stat.Snowfall4Apr = entity.ResortStats.Snowfall4Apr;
                stat.Snowfall5May = entity.ResortStats.Snowfall5May;
                stat.Snowfall6Jun = entity.ResortStats.Snowfall6Jun;
                stat.Snowfall7Jul = entity.ResortStats.Snowfall7Jul;
                stat.Snowfall8Aug = entity.ResortStats.Snowfall8Aug;
                stat.Snowfall9Sep = entity.ResortStats.Snowfall9Sep;

                stat.HasNightskiing = entity.ResortStats.HasNightskiing;
                stat.NightskiingDescription = entity.ResortStats.NightskiingDescription;
                stat.SeasonStartMonth = entity.ResortStats.SeasonStartMonth;
                stat.SeasonEndMonth = entity.ResortStats.SeasonEndMonth;

                stat.LiftDescription = entity.ResortStats.LiftDescription;
                stat.RunTotal = entity.ResortStats.RunTotal;
                stat.LongestRunDistance = entity.ResortStats.LongestRunDistance;
                stat.HasSnowpark = entity.ResortStats.HasSnowpark;
                stat.SnowparkTotal = entity.ResortStats.SnowparkTotal;
                stat.SnowparkDescription = entity.ResortStats.SnowparkDescription;
                stat.HasHalfpipe = entity.ResortStats.HasHalfpipe;
                stat.HalfpipeTotal = entity.ResortStats.HalfpipeTotal;
                stat.HalfpipeDescription = entity.ResortStats.HalfpipeDescription;
                stat.HasQuarterpipe = entity.ResortStats.HasQuarterpipe;
                stat.QuarterpipeTotal = entity.ResortStats.QuarterpipeTotal;
                stat.QuarterpipeDescription = entity.ResortStats.QuarterpipeDescription;
                stat.SkiableTerrianSize = entity.ResortStats.SkiableTerrianSize;

                stat.SummerStartMonth = entity.ResortStats.SummerStartMonth;
                stat.SummerEndMonth = entity.ResortStats.SummerEndMonth;
            }

            //foreach (Sporthub.Repository.DataAccess.LinkResortSkiArea lrsa in skiAreasToUpdate)
            //{
            //    lrsa..BaseLevel = entity.ResortStats.BaseLevel;
            //    stat.TopLevel = entity.ResortStats.TopLevel;
            //    stat.VerticalDrop = entity.ResortStats.VerticalDrop;
            //}

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

        public void Delete(Sporthub.Model.Resort entity)
        {
            throw new NotImplementedException();
        }

        public int Add(Sporthub.Model.Resort entity)
        {
            var newResort = new Sporthub.Repository.DataAccess.Resort
            {
                Name = entity.Name,
                PrettyUrl = entity.PrettyUrl,
                NameFriendlyFormat = entity.NameFriendlyFormat,
                PrettyUrlFriendlyFormat = entity.PrettyUrlFriendlyFormat,
                CountryID = entity.CountryID,
                CountryName = entity.CountryName,
                ContinentID = entity.ContinentID,
                ContinentName = entity.ContinentName,
                Longitude = entity.Longitude,
                Latitude = entity.Latitude,
                CanPublish = entity.CanPublish,
                CreatedDate = DateTime.Now,
                CreatedUserID = entity.CreatedUserID,
                UpdatedDate = DateTime.Now,
                UpdatedUserID = entity.UpdatedUserID
            };

            db.Resorts.InsertOnSubmit(newResort);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var newResortStats = new Sporthub.Repository.DataAccess.ResortStat
            {
                ResortID = newResort.ID,
                CreatedDate = DateTime.Now,
                CreatedUserID = entity.CreatedUserID,
                UpdatedDate = DateTime.Now,
                UpdatedUserID = entity.UpdatedUserID
            };


            db.ResortStats.InsertOnSubmit(newResortStats);

            try
            {
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return newResort.ID;
        }

        public IQueryable<Sporthub.Model.Resort> AsQueryable()
        {
            var resort =
                from r in db.Resorts
                where r.Display == true
                select new Sporthub.Model.Resort
                {
                    ID = r.ID,
                    Name = r.Name,
                    PrettyUrl = r.PrettyUrl,
                    NameFriendlyFormat = r.NameFriendlyFormat,
                    PrettyUrlFriendlyFormat = r.PrettyUrlFriendlyFormat,
                    CountryID = r.CountryID,
                    CountryName = r.CountryName,
                    ContinentID = r.ContinentID,
                    ContinentName = r.ContinentName,
                    //ISO3166Alpha2 = 
                    //    (from c in db.Countries
                    //     where c.ID == r.CountryID
                    //     select new Sporthub.Model.Country
                    //     {
                    //         ISO3166Alpha2 = c.ISO3166Alpha2
                    //     }).ToString(),
                    Longitude = r.Longitude,
                    Latitude = r.Latitude,
                    GeonameID = r.GeonameID,
                    IsNorthernHemisphere = r.IsNorthernHemisphere,
                    Overview = r.Overview,
                    IsFeaturedResort = r.IsFeaturedResort,
                    CanPublish = r.CanPublish,
                    CreatedDate = r.CreatedDate,
                    CreatedUserID = r.CreatedUserID,
                    UpdatedDate = r.UpdatedDate,
                    UpdatedUserID = r.UpdatedUserID,
                    Score = r.Score,
                    ScoreTotal = r.ScoreTotal,
                    ScoreCount = r.ScoreCount,
                    
                    LiftRating = r.LiftRating,
                    SnowRating = r.SnowRating,
                    QueueRating = r.QueueRating,
                    SceneryRating = r.SceneryRating,
                    ConvenienceRating = r.ConvenienceRating,
                    AccomodationRating = r.AccomodationRating,
                    FoodRating = r.FoodRating,
                    FacilitiesRating = r.FacilitiesRating,
                    NightlifeRating = r.NightlifeRating,
                    
                    LiftTotal = r.LiftTotal,
                    LiftCount = r.LiftCount,
                    SnowTotal = r.ScoreTotal,
                    SnowCount = r.ScoreCount,
                    QueueTotal = r.QueueTotal,
                    QueueCount = r.QueueCount,
                    SceneryTotal = r.SceneryTotal,
                    SceneryCount = r.SceneryCount,
                    ConvenienceTotal = r.ConvenienceTotal,
                    ConvenienceCount = r.ConvenienceCount,
                    AccomodationTotal = r.AccomodationTotal,
                    AccomodationCount = r.AccomodationCount,
                    FoodTotal = r.FoodTotal,
                    FoodCount = r.FoodCount,
                    FacilitiesTotal = r.FacilitiesTotal,
                    FacilitiesCount = r.FacilitiesCount,
                    NightlifeTotal = r.NightlifeTotal,
                    NightlifeCount = r.NightlifeCount,

                    ResortSuitsExpert = r.ResortSuitsExpert,
                    ResortSuitsAdvanced = r.ResortSuitsAdvanced,
                    ResortSuitsIntermediate = r.ResortSuitsIntermediate,
                    ResortSuitsBeginner = r.ResortSuitsBeginner,

                    ResortSuitsLively = r.ResortSuitsLively,
                    ResortSuitsAverage = r.ResortSuitsAverage,
                    ResortSuitsQuiet = r.ResortSuitsQuiet,

                    ResortSuitsSkiers = r.ResortSuitsSkiers,
                    ResortSuitsSnowboarders = r.ResortSuitsSnowboarders,
                    ResortSuitsBoth = r.ResortSuitsBoth,

                    ResortSuitsExpensive = r.ResortSuitsExpensive,
                    ResortSuitsAffordable = r.ResortSuitsAffordable,
                    ResortSuitsCheap = r.ResortSuitsCheap,
                    VisitedCount = r.VisitedCount,
                    FavedCount = r.FavedCount,
                    Display = r.Display,
                    IsSkiArea = r.IsSkiArea,
                    SkiAreas =
                       (from lrsa in db.LinkResortSkiAreas
                        where lrsa.SkiAreaID == r.ID
                        select new Model.LinkResortSkiArea
                       {
                           Resort =
                               (from r2 in db.Resorts
                                where r2.ID == lrsa.ResortID
                                select new Sporthub.Model.Resort
                                           {
                                               Name = r2.Name,
                                               PrettyUrl = r2.PrettyUrl,
                                               Score = r2.Score,
                                               ID = r2.ID,
                                               Latitude = r2.Latitude,
                                               Longitude = r2.Longitude,
                                               CountryID = r2.CountryID,
                                               ContinentID = r2.ContinentID,
                                               Country =
                                                    (from c2 in db.Countries
                                                     where c2.ID == r2.CountryID
                                                     select new Sporthub.Model.Country
                                                     {
                                                         PrettyUrl = c2.PrettyUrl,
                                                         ID = c2.ID,
                                                         CountryName = c2.CountryName,
                                                         ISO3166Alpha2 = c2.ISO3166Alpha2
                                                     }).SingleOrDefault()
                                         }).SingleOrDefault()
                        }).ToList(),
                    LinkResortUsers =
                        (from lru in db.LinkResortUsers
                         where lru.ResortID == r.ID
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
                             Resort = null,
                             User =
                                (from u in db.Users
                                 where u.ID == lru.UserID
                                 select new Sporthub.Model.User
                                 {
                                     ID = u.ID,
                                     UserName = u.UserName
                                 }).SingleOrDefault(),
                             ReviewUsefulnessFeedback =
                                 (from ru in db.ReviewUsefulnesses
                                  where ru.LinkResortUserID == lru.ID
                                  select new Sporthub.Model.ReviewUsefulness()
                                  {
                                      ID = ru.ID,
                                      UserID = ru.UserID,
                                      IsUseful = ru.IsUseful
                                  }).ToList()
                         }).ToList(),
                    Country =
                        (from c in db.Countries
                         where c.ID == r.CountryID
                         select new Sporthub.Model.Country
                         {
                             ID = c.ID,
                             CountryName = c.CountryName,
                             ISO3166Alpha2 = c.ISO3166Alpha2,
                             PrettyUrl = c.PrettyUrl
                         }).SingleOrDefault(),
                    ResortLinks = 
                        (from l in db.ResortLinks
                         where l.ResortID == r.ID
                         select new Sporthub.Model.ResortLink
                         {
                             CreatedDate = l.CreatedDate,
                             CreatedUserID = l.CreatedUserID,
                             ID = l.ID,
                             Name = l.Name,
                             ResortID = l.ResortID,
                             ResortLinkTypeID = l.ResortLinkTypeID,
                             Sequence = l.Sequence,
                             UpdatedDate = l.UpdatedDate,
                             UpdatedUserID = l.UpdatedUserID,
                             URL = l.URL
                         }).ToList(),
                    ResortStats =
                        (from s in db.ResortStats
                         where s.ResortID == r.ID
                         select new Sporthub.Model.ResortStats
                         {
                            BlackRuns = s.BlackRuns,
                            AverageSnowfall = s.AverageSnowfall,
                            BaseLevel = s.BaseLevel,
                            GreenRuns = s.GreenRuns,
                            CableLiftCount = s.CableLiftCount,
                            CreatedDate = s.CreatedDate,
                            CreatedUserID = s.CreatedUserID,
                            DoubleCount = s.DoubleCount,
                            RedRuns = s.RedRuns,
                            FunicularCount = s.FunicularCount,
                            GondolaCount = s.GondolaCount,
                            HalfpipeDescription = s.HalfpipeDescription,
                            HalfpipeTotal = s.HalfpipeTotal,
                            HasHalfpipe = s.HasHalfpipe,
                            HasNightskiing = s.HasNightskiing,
                            HasQuarterpipe = s.HasQuarterpipe,
                            HasSnowmaking = s.HasSnowmaking,
                            HasSnowpark = s.HasSnowpark,
                            HasSummerskiing = s.HasSummerskiing,
                            Height = s.Height,
                            ID = s.ID,
                            BlueRuns = s.BlueRuns,
                            LiftCapacityHour = s.LiftCapacityHour,
                            LiftDescription = s.LiftDescription,
                            LiftTotal = s.LiftTotal,
                            LongestRunDistance = s.LongestRunDistance,
                            RunTotalDistance = s.RunTotalDistance,
                            MountainRestaurants = s.MountainRestaurants,
                            NightskiingDescription = s.NightskiingDescription,
                            Population = s.Population,
                            PreSeasonStartMonth = s.PreSeasonStartMonth,
                            QuadCount = s.QuadCount,
                            QuadPlusCount = s.QuadPlusCount,
                            QuarterpipeDescription = s.QuarterpipeDescription,
                            QuarterpipeTotal = s.QuarterpipeTotal,
                            RunTotal = s.RunTotal,
                            SeasonEndMonth = s.SeasonEndMonth,
                            SeasonStartMonth = s.SeasonStartMonth,
                            SingleCount = s.SingleCount,
                            SkiableTerrianSize = s.SkiableTerrianSize,
                            SnowmakingCoverage = s.SnowmakingCoverage,
                            SnowparkDescription = s.SnowparkDescription,
                            SnowparkTotal = s.SnowparkTotal,
                            SummerEndMonth = s.SummerEndMonth,
                            SummerskiingDescription = s.SummerskiingDescription,
                            SummerStartMonth = s.SummerStartMonth,
                            SurfaceCount = s.SurfaceCount,
                            SurfaceTrainCount = s.SurfaceTrainCount,
                            TopLevel = s.TopLevel,
                            TripleCount = s.TripleCount,
                            UpdatedDate = s.UpdatedDate,
                            UpdatedUserID = s.UpdatedUserID,
                            VerticalDrop = s.VerticalDrop,
                            Snowfall1Jan = s.Snowfall1Jan,
                            Snowfall2Feb = s.Snowfall2Feb,
                            Snowfall3Mar = s.Snowfall3Mar,
                            Snowfall4Apr = s.Snowfall4Apr,
                            Snowfall5May = s.Snowfall5May,
                            Snowfall6Jun = s.Snowfall6Jun,
                            Snowfall7Jul = s.Snowfall7Jul,
                            Snowfall8Aug = s.Snowfall8Aug,
                            Snowfall9Sep = s.Snowfall9Sep,
                            Snowfall10Oct = s.Snowfall10Oct,
                            Snowfall11Nov = s.Snowfall11Nov,
                            Snowfall12Dec = s.Snowfall12Dec

                         }).SingleOrDefault(),
                    NearbyResorts =
                        (from nr in db.Resorts
                         where ((nr.Latitude >= r.Latitude - 0.4)
                         && (nr.Latitude <= r.Latitude + 0.4)
                         && (nr.Longitude >= r.Longitude - 0.4)
                         && (nr.Longitude <= r.Longitude + 0.4)
                         && (nr.ID != r.ID)
                         && (!nr.IsSkiArea)
                         && (nr.Display == true))
                         select new Sporthub.Model.Resort
                         {
                             ID = nr.ID,
                             Name = nr.Name,
                             PrettyUrl = nr.PrettyUrl,
                             Latitude = nr.Latitude,
                             Longitude = nr.Longitude,
                             Country =
                            (from c2 in db.Countries
                             where c2.ID == nr.CountryID
                             select new Sporthub.Model.Country
                             {
                                 CountryName = c2.CountryName,
                                 ISO3166Alpha2 = c2.ISO3166Alpha2
                             }).SingleOrDefault()
                         }).OrderBy(x => x.Name).ToList(),
                    NearbyAirports =
                        (from na in db.Airports
                         where ((na.Latitude >= r.Latitude - 2)
                         && (na.Latitude <= r.Latitude + 2)
                         && (na.Longitude >= r.Longitude - 2)
                         && (na.Longitude <= r.Longitude + 2)
                         && (na.ID != r.ID))
                         select new Sporthub.Model.Airport
                         {
                             ID = na.ID,
                             Name = na.Name,
                             PrettyUrl = na.PrettyUrl,
                             Latitude = na.Latitude,
                             Longitude = na.Longitude,
                             Country =
                            (from c3 in db.Countries
                             where c3.ID == na.CountryID
                             select new Sporthub.Model.Country
                             {
                                 CountryName = c3.CountryName,
                                 ISO3166Alpha2 = c3.ISO3166Alpha2
                             }).SingleOrDefault()
                         }).OrderBy(x => x.Name).ToList()
                };

            return resort;
        }

        public IQueryable<Sporthub.Model.Resort> AsQueryableBasic()
        {
            var resort =
                from r in db.Resorts
                where r.Display == true
                select new Sporthub.Model.Resort
                {
                    ID = r.ID,
                    Name = r.Name,
                    PrettyUrl = r.PrettyUrl,
                    CountryID = r.CountryID,
                    ContinentID= r.ContinentID,
                    CountryName = r.CountryName,
                    ContinentName = r.ContinentName,
                    Longitude = r.Longitude,
                    Latitude = r.Latitude,
                    CanPublish = r.CanPublish,
                    CreatedDate = r.CreatedDate,
                    UpdatedDate = r.UpdatedDate,
                    NameFriendlyFormat = r.NameFriendlyFormat,
                    AlsoKnownAs = r.AlsoKnownAs,
                    Display = r.Display,
                    Score = r.Score,
                    VisitedCount = r.VisitedCount,
                    FavedCount = r.FavedCount,
                    IsSkiArea = r.IsSkiArea,
                    Country =
                        (from c in db.Countries
                         where c.ID == r.CountryID
                         select new Sporthub.Model.Country
                         {
                             ISO3166Alpha2 = c.ISO3166Alpha2
                         }).SingleOrDefault()
                };

            return resort;
        }

        public IQueryable<Sporthub.Model.Resort> SkiAreasAsQueryableBasic()
        {
            var resort =
                from sa in db.Resorts
                where sa.Display == true && sa.IsSkiArea == true
                select new Sporthub.Model.Resort
                           {
                               ID = sa.ID,
                               Name = sa.Name,
                               PrettyUrl = sa.PrettyUrl,
                               Longitude = sa.Longitude,
                               Latitude = sa.Latitude,
                               CanPublish = sa.CanPublish,
                               CreatedDate = sa.CreatedDate,
                               UpdatedDate = sa.UpdatedDate,
                               NameFriendlyFormat = sa.NameFriendlyFormat,
                               AlsoKnownAs = sa.AlsoKnownAs,
                               Display = sa.Display,
                               IsSkiArea = sa.IsSkiArea,
                               CountryID = sa.CountryID,
                               ContinentID =sa.ContinentID,
                               SkiAreas =
                                   (from lrsa in db.LinkResortSkiAreas
                                    where lrsa.SkiAreaID == sa.ID
                                    select new Model.LinkResortSkiArea
                                               {
                                                   Resort =
                                                       (from r in db.Resorts
                                                        where r.ID == lrsa.ResortID
                                                        select new Sporthub.Model.Resort
                                                                   {
                                                                       Name = r.Name,
                                                                       PrettyUrl = r.PrettyUrl,
                                                                       Score = r.Score,
                                                                       ID = r.ID,
                                                                       Latitude = r.Latitude,
                                                                       Longitude = r.Longitude,
                                                                       CountryID = r.CountryID,
                                                                       ContinentID = r.ContinentID,
                                                                       Country =
                                                                            (from c in db.Countries
                                                                             where c.ID == r.CountryID
                                                                             select new Sporthub.Model.Country
                                                                             {
                                                                                 PrettyUrl = c.PrettyUrl,
                                                                                 ID = c.ID,
                                                                                 CountryName = c.CountryName,
                                                                                 ISO3166Alpha2 = c.ISO3166Alpha2
                                                                             }).SingleOrDefault()
                                                                 }).SingleOrDefault()

                                        
                                                }).ToList()
                };

            return resort;
        }

        public IList<Sporthub.Model.Resort> GetTopRatedResortsByCountry(int id)
        {
            var resort =
                (from r in db.Resorts
                 where r.Display == true && r.CountryID == id && r.Score > 0 && r.IsSkiArea == false
                 select new Sporthub.Model.Resort
                 {
                     ID = r.ID,
                     Name = r.Name,
                     PrettyUrl = r.PrettyUrl,
                     NameFriendlyFormat = r.NameFriendlyFormat,
                     PrettyUrlFriendlyFormat = r.PrettyUrlFriendlyFormat,
                     CountryID = r.CountryID,
                     CountryName = r.CountryName,
                     ContinentID = r.ContinentID,
                     ContinentName = r.ContinentName,
                     Longitude = r.Longitude,
                     Latitude = r.Latitude,
                     Score = r.Score,
                     ScoreTotal = r.ScoreTotal,
                     ScoreCount = r.ScoreCount,
                     VisitedCount = r.VisitedCount,
                     FavedCount = r.FavedCount,
                     Display = r.Display,
                     Country =
                         (from c in db.Countries
                          where c.ID == r.CountryID
                          select new Sporthub.Model.Country
                          {
                              ID = c.ID,
                              CountryName = c.CountryName,
                              ISO3166Alpha2 = c.ISO3166Alpha2,
                              PrettyUrl = c.PrettyUrl
                          }).SingleOrDefault(),
                 }).OrderByDescending(x => x.Score).Take(10).ToList<Sporthub.Model.Resort>();

            return resort;
        }

        public IList<Sporthub.Model.Resort> GetTopRatedResortsByContinent(int id)
        {
            var resort =
                (from r in db.Resorts
                 where r.Display == true && r.ContinentID == id && r.Score > 0 && r.IsSkiArea == false
                 select new Sporthub.Model.Resort
                 {
                     ID = r.ID,
                     Name = r.Name,
                     PrettyUrl = r.PrettyUrl,
                     NameFriendlyFormat = r.NameFriendlyFormat,
                     PrettyUrlFriendlyFormat = r.PrettyUrlFriendlyFormat,
                     CountryID = r.CountryID,
                     CountryName = r.CountryName,
                     ContinentID = r.ContinentID,
                     ContinentName = r.ContinentName,
                     Longitude = r.Longitude,
                     Latitude = r.Latitude,
                     Score = r.Score,
                     ScoreTotal = r.ScoreTotal,
                     ScoreCount = r.ScoreCount,
                     VisitedCount = r.VisitedCount,
                     FavedCount = r.FavedCount,
                     Display = r.Display,
                     Country =
                         (from c in db.Countries
                          where c.ID == r.CountryID
                          select new Sporthub.Model.Country
                          {
                              ID = c.ID,
                              CountryName = c.CountryName,
                              ISO3166Alpha2 = c.ISO3166Alpha2,
                              PrettyUrl = c.PrettyUrl
                          }).SingleOrDefault(),
                 }).OrderByDescending(x => x.Score).Take(10).ToList<Sporthub.Model.Resort>();

            return resort;
        }


        public IList<Sporthub.Model.Resort> GetMostVisitedResortsByCountry(int id)
        {
            var resort =
                (from r in db.Resorts
                 where r.Display == true && r.VisitedCount > 0 && r.CountryID == id && r.IsSkiArea == false
                 select new Sporthub.Model.Resort
                 {
                     ID = r.ID,
                     Name = r.Name,
                     PrettyUrl = r.PrettyUrl,
                     NameFriendlyFormat = r.NameFriendlyFormat,
                     PrettyUrlFriendlyFormat = r.PrettyUrlFriendlyFormat,
                     CountryID = r.CountryID,
                     CountryName = r.CountryName,
                     ContinentID = r.ContinentID,
                     ContinentName = r.ContinentName,
                     Longitude = r.Longitude,
                     Latitude = r.Latitude,
                     Score = r.Score,
                     ScoreTotal = r.ScoreTotal,
                     ScoreCount = r.ScoreCount,
                     VisitedCount = r.VisitedCount,
                     FavedCount = r.FavedCount,
                     Display = r.Display,
                     Country =
                         (from c in db.Countries
                          where c.ID == r.CountryID
                          select new Sporthub.Model.Country
                          {
                              ID = c.ID,
                              CountryName = c.CountryName,
                              ISO3166Alpha2 = c.ISO3166Alpha2,
                              PrettyUrl = c.PrettyUrl
                          }).SingleOrDefault(),
                 }).OrderByDescending(x => x.VisitedCount).Take(10).ToList<Sporthub.Model.Resort>();

            return resort;
        }

        public IList<Sporthub.Model.Resort> GetMostVisitedResortsByContinent(int id)
        {
            var resort =
                (from r in db.Resorts
                 where r.Display == true && r.VisitedCount > 0 && r.ContinentID == id && r.IsSkiArea == false
                 select new Sporthub.Model.Resort
                 {
                     ID = r.ID,
                     Name = r.Name,
                     PrettyUrl = r.PrettyUrl,
                     NameFriendlyFormat = r.NameFriendlyFormat,
                     PrettyUrlFriendlyFormat = r.PrettyUrlFriendlyFormat,
                     CountryID = r.CountryID,
                     CountryName = r.CountryName,
                     ContinentID = r.ContinentID,
                     ContinentName = r.ContinentName,
                     Longitude = r.Longitude,
                     Latitude = r.Latitude,
                     Score = r.Score,
                     ScoreTotal = r.ScoreTotal,
                     ScoreCount = r.ScoreCount,
                     VisitedCount = r.VisitedCount,
                     FavedCount = r.FavedCount,
                     Display = r.Display,
                     Country =
                         (from c in db.Countries
                          where c.ID == r.CountryID
                          select new Sporthub.Model.Country
                          {
                              ID = c.ID,
                              CountryName = c.CountryName,
                              ISO3166Alpha2 = c.ISO3166Alpha2,
                              PrettyUrl = c.PrettyUrl
                          }).SingleOrDefault(),
                 }).OrderByDescending(x => x.VisitedCount).Take(10).ToList<Sporthub.Model.Resort>();

            return resort;
        }

        public IList<Sporthub.Model.Resort> GetTopRatedResorts()
        {
            var resort =
                (from r in db.Resorts
                 where r.Display == true && r.Score > 0 && r.IsSkiArea == false
                 select new Sporthub.Model.Resort
                 {
                     ID = r.ID,
                     Name = r.Name,
                     PrettyUrl = r.PrettyUrl,
                     NameFriendlyFormat = r.NameFriendlyFormat,
                     PrettyUrlFriendlyFormat = r.PrettyUrlFriendlyFormat,
                     CountryID = r.CountryID,
                     CountryName = r.CountryName,
                     ContinentID = r.ContinentID,
                     ContinentName = r.ContinentName,
                     Longitude = r.Longitude,
                     Latitude = r.Latitude,
                     Score = r.Score,
                     ScoreTotal = r.ScoreTotal,
                     ScoreCount = r.ScoreCount,
                     VisitedCount = r.VisitedCount,
                     FavedCount = r.FavedCount,
                     Display = r.Display,
                     Country =
                         (from c in db.Countries
                          where c.ID == r.CountryID
                          select new Sporthub.Model.Country
                          {
                              ID = c.ID,
                              CountryName = c.CountryName,
                              ISO3166Alpha2 = c.ISO3166Alpha2,
                              PrettyUrl = c.PrettyUrl
                          }).SingleOrDefault(),
                 }).OrderByDescending(x => x.Score).Take(10).ToList<Sporthub.Model.Resort>();

            return resort;
        }


        public IList<Sporthub.Model.Resort> GetMostVisitedResorts()
        {
            var resort =
                (from r in db.Resorts
                 where r.Display == true && r.VisitedCount > 0 && r.IsSkiArea == false
                 select new Sporthub.Model.Resort
                 {
                     ID = r.ID,
                     Name = r.Name,
                     PrettyUrl = r.PrettyUrl,
                     NameFriendlyFormat = r.NameFriendlyFormat,
                     PrettyUrlFriendlyFormat = r.PrettyUrlFriendlyFormat,
                     CountryID = r.CountryID,
                     CountryName = r.CountryName,
                     ContinentID = r.ContinentID,
                     ContinentName = r.ContinentName,
                     Longitude = r.Longitude,
                     Latitude = r.Latitude,
                     Score = r.Score,
                     ScoreTotal = r.ScoreTotal,
                     ScoreCount = r.ScoreCount,
                     VisitedCount = r.VisitedCount,
                     FavedCount = r.FavedCount,
                     Display = r.Display,
                     Country =
                         (from c in db.Countries
                          where c.ID == r.CountryID
                          select new Sporthub.Model.Country
                          {
                              ID = c.ID,
                              CountryName = c.CountryName,
                              ISO3166Alpha2 = c.ISO3166Alpha2,
                              PrettyUrl = c.PrettyUrl
                          }).SingleOrDefault(),
                 }).OrderByDescending(x => x.VisitedCount).Take(10).ToList<Sporthub.Model.Resort>();

            return resort;
        }
    }
}
