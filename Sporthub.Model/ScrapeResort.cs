using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class ScrapeResort
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PrettyUrl { get; set; }
        public string CountryName { get; set; }
        public string BaseLevel { get; set; }
        public string TopLevel { get; set; }
        public string VerticalDrop { get; set; }
        public string Height { get; set; }
        public string AverageSnowfall { get; set; }
        public string HasSnowmaking { get; set; }
        public string SnowmakingCoverage { get; set; }
        public string PreSeasonStartMonth { get; set; }
        public string SeasonStartMonth { get; set; }
        public string SeasonEndMonth { get; set; }
        public string Population { get; set; }
        public string MountainRestaurants { get; set; }
        public string LiftDescription { get; set; }
        public string LiftTotal { get; set; }
        public string LiftCapacityHour { get; set; }
        public string QuadPlusCount { get; set; }
        public string QuadCount { get; set; }
        public string TripleCount { get; set; }
        public string DoubleCount { get; set; }
        public string SingleCount { get; set; }
        public string SurfaceCount { get; set; }
        public string GondolaCount { get; set; }
        public string FunicularCount { get; set; }
        public string SurfaceTrainCount { get; set; }
        public string RunTotal { get; set; }
        public string BlackRuns { get; set; }
        public string RedRuns { get; set; }
        public string BlueRuns { get; set; }
        public string GreenRuns { get; set; }
        public string LongestRunDistance { get; set; }
        public string SkiableTerrianSize { get; set; }
    }
}
