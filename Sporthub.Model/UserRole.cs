using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class UserRole
    {
        public int ID { get; set; }
        public string UserRoleCode { get; set; }
        public string Name { get; set; }
        public string UserRoleDesc { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsModerator { get; set; }
        public bool IsUserAdmin { get; set; }
        public bool IsDataAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }

        public int PointsRequired { get; set; }
    }
}
