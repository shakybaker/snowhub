using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporthub.Model
{
    public class Breadcrumb
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Breadcrumb> Breadcrumbs { get; set; }
    }
}
