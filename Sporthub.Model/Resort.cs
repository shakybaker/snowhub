using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class Resort : IEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public string Name { get; set; }
        public string PrettyUrl { get; set; }
        public string NameFriendlyFormat { get; set; }
        public string PrettyUrlFriendlyFormat { get; set; }
        public string AlsoKnownAs { get; set; }
        public int CountryID { get; set; }
        public int ContinentID { get; set; }
        public string CountryName { get; set; }
        public string ContinentName { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int? GeonameID { get; set; }
        public bool? IsNorthernHemisphere { get; set; }
        public string Overview { get; set; }
        public bool? IsFeaturedResort { get; set; }
        public bool CanPublish { get; set; }
        public string PictureName { get; set; }
        public string PictureUrl { get; set; }
        public string PictureLicenceType { get; set; }
        public string PictureAuthor { get; set; }
        public string PictureAuthorUrl { get; set; }

        public int VisitedCount { get; set; }
        public int FavedCount { get; set; }

        public int Score { get; set; }
        public int ScoreTotal { get; set; }
        public int ScoreCount { get; set; }

        //Ratings
        public int LiftRating { get; set; }
        public int LiftTotal { get; set; }
        public int LiftCount { get; set; }
        public int SnowRating { get; set; }
        public int SnowTotal { get; set; }
        public int SnowCount { get; set; }
        public int QueueRating { get; set; }
        public int QueueTotal { get; set; }
        public int QueueCount { get; set; }
        public int SceneryRating { get; set; }
        public int SceneryTotal { get; set; }
        public int SceneryCount { get; set; }
        public int ConvenienceRating { get; set; }
        public int ConvenienceTotal { get; set; }
        public int ConvenienceCount { get; set; }
        public int AccomodationRating { get; set; }
        public int AccomodationTotal { get; set; }
        public int AccomodationCount { get; set; }
        public int FoodRating { get; set; }
        public int FoodTotal { get; set; }
        public int FoodCount { get; set; }
        public int FacilitiesRating { get; set; }
        public int FacilitiesTotal { get; set; }
        public int FacilitiesCount { get; set; }
        public int NightlifeRating { get; set; }
        public int NightlifeTotal { get; set; }
        public int NightlifeCount { get; set; }
        
        public int ResortSuitsExpert { get; set; }
        public int ResortSuitsAdvanced { get; set; }
        public int ResortSuitsIntermediate { get; set; }
        public int ResortSuitsBeginner { get; set; }

        public int ResortSuitsLively { get; set; }
        public int ResortSuitsAverage { get; set; }
        public int ResortSuitsQuiet { get; set; }

        public int ResortSuitsSkiers { get; set; }
        public int ResortSuitsSnowboarders { get; set; }
        public int ResortSuitsBoth { get; set; }

        public int ResortSuitsExpensive { get; set; }
        public int ResortSuitsAffordable { get; set; }
        public int ResortSuitsCheap { get; set; }

        public bool Display { get; set; }

        public Continent Continent { get; set; }
        public Country Country { get; set; }
        public Region Region { get; set; }
        
        public IList<LinkResortUser> LinkResortUsers { get; set; }
        public IList<LinkResortRegion> LinkResortRegions { get; set; }

        public ResortStats ResortStats { get; set; }
        public IList<ResortLink> ResortLinks { get; set; }
        public IList<Thread> Threads { get; set; }
        public IList<Picture> Pictures  { get; set; }
        public IList<Resort> NearbyResorts { get; set; }
        public IList<Airport> NearbyAirports { get; set; }
        public IList<LinkResortSkiArea> SkiAreas { get; set; }

        public bool IsSkiArea { get; set; }

        public Resort()
        {
            NearbyAirports = new List<Airport>();
            SkiAreas = new List<LinkResortSkiArea>();
        }
    }
}
