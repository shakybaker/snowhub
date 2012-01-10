using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sporthub.Model;

namespace Sporthub.Utilities
{
    public class NearestResortsSort
    {
        public static IList<Resort> SortNearbyResortsByDistance(IList<Resort> resortsToSort, double? latitude, double? longitude, int take)
        {
            //HACK: temporarily using Latitude to hold distance - BAD!
            var i = 0;
            foreach (var r in resortsToSort.ToArray())
            {
                var distance = CalculateDistance(double.Parse(latitude.ToString()),
                    double.Parse(longitude.ToString()),
                    double.Parse(r.Latitude.ToString()),
                    double.Parse(r.Longitude.ToString()));
                resortsToSort[i] = r;
                resortsToSort[i].Latitude = distance;
                i++;
            }

            var sortedResorts =
                    from sr in resortsToSort
                    where sr.Latitude < 220
                    orderby sr.Latitude
                    select sr;
            return take > 0 ? sortedResorts.Take(take).ToList() : sortedResorts.ToList();
        }

        public static IList<Airport> SortNearbyAirportsByDistance(IList<Airport> airportsToSort, double? latitude, double? longitude, int take)
        {
            //HACK: temporarily using Latitude to hold distance - BAD!
            var i = 0;
            foreach (var r in airportsToSort.ToArray())
            {
                var distance = CalculateDistance(double.Parse(latitude.ToString()),
                    double.Parse(longitude.ToString()),
                    double.Parse(r.Latitude.ToString()),
                    double.Parse(r.Longitude.ToString()));
                airportsToSort[i] = r;
                airportsToSort[i].Latitude = distance;
                i++;
            }

            var sortedAirports =
                    from sr in airportsToSort
                    where sr.Latitude < 220
                    orderby sr.Latitude
                    select sr;
            return take > 0 ? sortedAirports.Take(take).ToList() : sortedAirports.ToList();
        }

        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // km
            var dLat = DegreeToRadian((lat2 - lat1));
            var dLon = DegreeToRadian((lon2 - lon1));
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreeToRadian(lat1)) * Math.Cos(DegreeToRadian(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c;

            return d;
        }

        private static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }


        private List<string> ShuffleList(List<string> inputList)
        {
            List<string> outputList = new List<string>();
            if (inputList.Count == 0)
                return outputList;

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                outputList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            inputList.Clear();
            inputList = null;
            r = null;

            return outputList;
        }
    }
}
