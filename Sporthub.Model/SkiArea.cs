using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class SkiArea : IEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public int SkiAreaID { get; set; }
        public string Name { get; set; }
        public IList<Resort> Resorts { get; set; }
        public IList<Country> Countries { get; set; }
    }
}
