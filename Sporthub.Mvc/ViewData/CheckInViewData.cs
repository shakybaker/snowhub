using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class CheckInViewData
    {
        public string Message { get; set; }
        public string ReturnUrl { get; set; }
        public Feedback Feedback { get; set; }
        public ErrorList ErrorList { get; set; }
        public Resort Resort { get; set; }
        public bool IsUpdate { get; set; }
        public bool HasFacebookPublishPermission { get; set; }

        public List<Breadcrumb> Breadcrumbs { get; set; }

        public CheckInViewData()
        {
            ErrorList = new ErrorList();
            ErrorList.Errors = new List<ErrorItem>();
        }
    }
}
