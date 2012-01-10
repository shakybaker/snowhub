using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sporthub.Model
{
    public class WeatherForecast
    {
        public IList<ForecastDay> ForecastDays { get; set; }

        public WeatherForecast()
        {
            ForecastDays = new List<ForecastDay>();
        }
    }

    public class ForecastDay
    {
        public string IconUrl { get; set; }
        public string IconAlt { get; set; }
        public string Conditions { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string HiTempC { get; set; }
        public string LoTempC { get; set; }
        public string HiTempF { get; set; }
        public string LoTempF { get; set; }
        public string DayOfWeek { get; set; }
        public bool IsChanceOfSnow { get; set; }

        public ForecastDay()
        {
            IsChanceOfSnow = false;
        }
    }
}
