using System;
using System.Collections.Generic;
using System.Text;

namespace Sporthub.Model
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataColumnReference : Attribute
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
