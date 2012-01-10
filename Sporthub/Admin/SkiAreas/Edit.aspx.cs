using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sporthub.Web.Admin.SkiAreas
{
    public partial class Edit : System.Web.UI.Page
    {
        public ViewData vd;

        public class ViewData
        {
            public bool IsEdit { get; set; }
            public string Name { get; set; }
            public Sporthub.Model.SkiArea SkiArea { get; set; }

            public ViewData()
            {
                IsEdit = false;
                Name = string.Empty;
                SkiArea = new Sporthub.Model.SkiArea();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            vd = new ViewData();
            string id = string.Empty;
            string action = Request["act"].ToString();

            if (action == "add")
            {
                vd.IsEdit = false;
            }
            else
            {
                vd.IsEdit = true;
                id = Request["said"].ToString();

            }
        }
    }
}
