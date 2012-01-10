using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model.Interfaces;

namespace Sporthub.Model
{
    public class ResortStats : IEntity
    {
        public int ID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserID { get; set; }

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
        public string CableLiftCount { get; set; }
        public string GondolaCount { get; set; }
        public string FunicularCount { get; set; }
        public string SurfaceTrainCount { get; set; }
        public string RunTotal { get; set; }
        public string BlackRuns { get; set; }
        public string RedRuns { get; set; }
        public string BlueRuns { get; set; }
        public string GreenRuns { get; set; }
        public string LongestRunDistance { get; set; }
        public string RunTotalDistance { get; set; }
        public string HasSnowpark { get; set; }
        public string SnowparkTotal { get; set; }
        public string SnowparkDescription { get; set; }
        public string HasHalfpipe { get; set; }
        public string HalfpipeTotal { get; set; }
        public string HalfpipeDescription { get; set; }
        public string HasQuarterpipe { get; set; }
        public string QuarterpipeTotal { get; set; }
        public string QuarterpipeDescription { get; set; }
        public string SkiableTerrianSize { get; set; }
        public string HasNightskiing { get; set; }
        public string NightskiingDescription { get; set; }
        public string HasSummerskiing { get; set; }
        public string SummerskiingDescription { get; set; }
        public string SummerStartMonth { get; set; }
        public string SummerEndMonth { get; set; }
        public string Snowfall1Jan { get; set; }
        public string Snowfall2Feb { get; set; }
        public string Snowfall3Mar { get; set; }
        public string Snowfall4Apr { get; set; }
        public string Snowfall5May { get; set; }
        public string Snowfall6Jun { get; set; }
        public string Snowfall7Jul { get; set; }
        public string Snowfall8Aug { get; set; }
        public string Snowfall9Sep { get; set; }
        public string Snowfall10Oct { get; set; }
        public string Snowfall11Nov { get; set; }
        public string Snowfall12Dec { get; set; }

        public bool HasLiftInfo()
        {
            if (!string.IsNullOrEmpty(LiftTotal) ||
               !string.IsNullOrEmpty(DoubleCount) ||
               !string.IsNullOrEmpty(VerticalDrop) ||
               !string.IsNullOrEmpty(TripleCount) ||
               !string.IsNullOrEmpty(QuadCount) ||
               !string.IsNullOrEmpty(SurfaceCount) ||
               !string.IsNullOrEmpty(GondolaCount) ||
               !string.IsNullOrEmpty(QuadPlusCount))
            {
                return true;
            }
            return false;
        }

        public bool HasRunsInfo()
        {
            if (!string.IsNullOrEmpty(GreenRuns) ||
               !string.IsNullOrEmpty(BlueRuns) ||
               !string.IsNullOrEmpty(RedRuns) ||
               !string.IsNullOrEmpty(BlackRuns))
            {
                return true;
            }
            return false;
        }

        //public bool HasSnowfallInfo()
        //{
        ////    Snowfall1Jan { get; set; }
        ////public string Snowfall2Feb { get; set; }
        ////public string Snowfall3Mar { get; set; }
        ////public string Snowfall4Apr { get; set; }
        ////public string Snowfall5May { get; set; }
        ////public string Snowfall6Jun { get; set; }
        ////public string Snowfall7Jul { get; set; }
        ////public string Snowfall8Aug { get; set; }
        ////public string Snowfall9Sep { get; set; }
        ////public string Snowfall10Oct { get; set; }
        ////public string Snowfall11Nov { get; set; }
        ////public string Snowfall12Dec
        //}
    }
}
