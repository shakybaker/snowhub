using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class Picture : IEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public int ResortID { get; set; }
        public string ResortName { get; set; }
        public string OriginalFilename { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Filename { get; set; }
    }
}
