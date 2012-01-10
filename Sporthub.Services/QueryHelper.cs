using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sporthub.Mvc.Code.Extensions
{
    public static class QueryHelper
    {
        public static bool IsIn(this int i, int[] list)
        {

            return list.Contains(i);

        }
    }
}
