using System.Collections.Generic;
using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class ResortRatingsViewData
    {
        public string Message { get; set; }
        public string ReturnUrl { get; set; }
        public Feedback Feedback { get; set; }
        public ErrorList ErrorList { get; set; }
        public Resort Resort { get; set; }
        public LinkResortUser CurrentUserRatings { get; set; }
        public bool IsUpdate { get; set; }
        public bool HasFacebookPublishPermission { get; set; }

        public string ErrorDateVisited { get; set; }
        public string ErrorOverallScore { get; set; }
        public string ErrorReviewTitle { get; set; }
        public string ErrorReviewBody { get; set; }

        public List<Breadcrumb> Breadcrumbs { get; set; }

        public ResortRatingsViewData()
        {
            ErrorList = new ErrorList();
            ErrorList.Errors = new List<ErrorItem>();
        }
    }
}
