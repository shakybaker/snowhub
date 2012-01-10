using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporthub.Model
{
    public class ReviewUsefulness
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int LinkResortUserID { get; set; }
        public bool IsUseful { get; set; }
    }
}
