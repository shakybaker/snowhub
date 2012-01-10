using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class Accomodation : IEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public int CountryID { get; set; }
        public int ResortID { get; set; }
        public string Name { get; set; }
        public int AccomodationTypeID { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Stars { get; set; }
        public int RoomCount { get; set; }
        public int BedCount { get; set; }
        public int BunksCount { get; set; }
        public int SinglesCount { get; set; }
        public int DoublesCount { get; set; }
        public int FamilyCount { get; set; }
        public int SuitesCount { get; set; }
        public int ApartmentsCount { get; set; }
        public bool HasHealthClub { get; set; }
        public bool HasSauna { get; set; }
        public bool HasSteamBath { get; set; }
        public bool HasIndoorSwimmingPool { get; set; }
        public bool HasOutdoorSwimmingPool { get; set; }
        public bool HasMeetingRooms { get; set; }
        public bool HasOvergroundCarPark { get; set; }
        public bool HasUndergroundCarPark { get; set; }
        public bool IsPetsAllowed { get; set; }
        public bool IsSkiInOut { get; set; }
        public bool HasMobilityAccessibility { get; set; }
        public bool HasRestaurant { get; set; }
        public bool HasGolf { get; set; }
        public bool HasBowling { get; set; }
        public bool HasCreche { get; set; }
        public bool HasInternetAccess { get; set; }
        public bool HasWirelessInternet { get; set; }
        public bool HasElevator { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string SiteURL { get; set; }
    }
}
