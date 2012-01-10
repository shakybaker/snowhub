using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Enumerators;

namespace Sporthub.Model
{
    public class ErrorItem
    {
        public ErrorLevel ErrorLevel { get; set; }
        public string Message { get; set; }
        public string FormField { get; set; }
    }
}
