using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class BarRestaurant : IEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public int CountryID { get; set; }
        public int ResortID { get; set; }
        public string Name { get; set; }
        public int BarRestaurantTypeID { get; set; }
        public string Description { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string SiteURL { get; set; }
        public bool HasFood { get; set; }
        public bool IsBar { get; set; }
        public bool HasTakeaway { get; set; }
        public bool HasDelivery { get; set; }
        public int CuisineTypeID { get; set; }
    }
}
