using System.Collections.Generic;
using Sporthub.Model;

namespace Sporthub.Mvc.ViewData
{
    public class AirportViewViewData : SporthubViewData
    {
        public List<Breadcrumb> Breadcrumbs { get; set; }
        public Airport Airport { get; set; }
    }
}
