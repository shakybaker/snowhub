using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;
using Sporthub.Model.Enumerators;

namespace Sporthub.Mvc.ViewData
{
    public class PostsViewData : SporthubViewData
    {
        public Forum Forum { get; set; }
        public Thread Thread { get; set; }
        public IList<Post> Posts { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; }
        public int StartPostNum { get; set; }
        public int CurrentPageNum { get; set; }
        public int TotalPageCount { get; set; }
        public string PostText { get; set; }
    }
}
