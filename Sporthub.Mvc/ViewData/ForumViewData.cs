using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;
using Sporthub.Model.Enumerators;

namespace Sporthub.Mvc.ViewData
{
    public class ForumViewData : SporthubViewData
    {
        public Forum Forum { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; }
        public string ThreadTitle { get; set; }
        public string PostText { get; set; }
        public string ErrorMessage { get; set; }
    }
}
