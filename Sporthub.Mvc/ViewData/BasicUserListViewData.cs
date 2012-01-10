using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class BasicUserListViewData : SporthubViewData
    {
        public IList<User> Users { get; set; }
    }
}
