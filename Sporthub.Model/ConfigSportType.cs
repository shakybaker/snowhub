using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class ConfigSportType
    {
        public int ID { get; set; }

        public int ParentID { get; set; }
        public int Sequence { get; set; }
        public string Name { get; set; }
        public string Collective { get; set; }
        public string Verb { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public bool IsSnowboard { get; set; }
        public bool IsSki { get; set; }
        public bool IsOther { get; set; }
    }
}
