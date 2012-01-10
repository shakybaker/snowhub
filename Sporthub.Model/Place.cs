using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class Place : IEntity
    {
        public int ID { get; set; }

        public string GoogleId { get; set; }
        public string Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Vicinity { get; set; }
        public PlaceType PlaceType { get; set; }
        public Address Address { get; set; }
        public string Telephone { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }

        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }
    }
}
