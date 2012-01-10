using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;
using Sporthub.Model.Enumerators;

namespace Sporthub.Model
{
    public class LinkUserSportType : IEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

        public int UserID { get; set; }
        public int SportTypeID { get; set; }
        public int Seasons { get; set; }
        public int Level { get; set; }

        public ConfigSportType ConfigSportType { get; set; }

        public string GetSportLevel()
        {
            string outStr = string.Empty;

            if ((ExperienceLevel)Level == ExperienceLevel.Beginner)
            {
                outStr = "Beginner";
            }
            else if ((ExperienceLevel)Level == ExperienceLevel.Intermediate)
            {
                outStr = "Intermediate";
            }
            else if ((ExperienceLevel)Level == ExperienceLevel.Expert)
            {
                outStr = "Expert";
            }
            else if ((ExperienceLevel)Level == ExperienceLevel.Advanced)
            {
                outStr = "Advanced";
            }

            return outStr;
        }
    }
}
