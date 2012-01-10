using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporthub.Model
{
    public class GetResortsModel
    {
        public int TotalCount { get; set; }
        public List<DataReturn> Data { get; set; }
    }

    public class DataReturn
    {
        public string Name { get; set; }
        public string PrettyUrl { get; set; }
        public int Id { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
    }
}
