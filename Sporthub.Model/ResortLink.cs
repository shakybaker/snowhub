using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporthub.Model
{
    public class ResortLink
    {
        public int ID { get; set; }
        public int ResortID { get; set; }
        public int? ResortLinkTypeID { get; set; }
        //public string ResortLinkType { get; set; }
        //public string ResortLinkTypeDesc { get; set; }
        public string URL { get; set; }
        public string Name { get; set; }
        public int? Sequence { get; set; }

        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }
    }
}
