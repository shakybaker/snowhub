using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using Sporthub.Model.Enumerators;
using System.Configuration;

namespace Sporthub.Data
{
    public class LocationDataManager
    {
        public static LocationLevel GetLocationLevel(string prettyUrl)
        {
            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand("GetLocationLevel", conn);
            cmd.Parameters.Add(new SqlParameter("@PrettyUrl", prettyUrl));
            cmd.CommandType = CommandType.StoredProcedure;

            string retVal = cmd.ExecuteScalar().ToString();

            return (LocationLevel)int.Parse(retVal);
        }

        public static List<Continent> GetContinents()
        {
            List<Continent> continents = new List<Continent>();

            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetContinents", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            //TODO: error handle
            try
            {
                DataRowCollection rows = dataSet.Tables[0].Rows;

                if (rows.Count > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        continents.Add(DataConverter.ToType<Continent>(row));
                    }
                }
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return continents;
        }

        public static List<Country> GetCountries(string name)
        {
            List<Country> countries = new List<Country>();

            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetCountriesByContinent", conn);
            cmd.Parameters.Add(new SqlParameter("@PrettyUrl", name));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            //TODO: error handle
            try
            {
                DataRowCollection rows = dataSet.Tables[0].Rows;

                if (rows.Count > 0)
                {
                    foreach (DataRow row in rows)
                    {
                        countries.Add(DataConverter.ToType<Country>(row));
                    }
                }
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return countries;
        }

        public static DataSet GetClusteredResortsForWorld()
        {
            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetClusteredResortsForWorld", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            if (conn != null)
            {
                conn.Close();
            }

            return dataSet;
        }

        public static DataSet GetClusteredResortsForContinent(string prettyUrl)
        {
            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetClusteredResortsForContinent", conn);
            cmd.Parameters.Add(new SqlParameter("@PrettyUrl", prettyUrl));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            if (conn != null)
            {
                conn.Close();
            }

            return dataSet;
        }

        public static DataSet GetClusteredCountriesForWorld()
        {
            //TODO: put all this in a method
            //SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=SQL2005_615410_sporthub;Persist Security Info=True;User ID=sa;Integrated Security=True");
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetClusteredCountriesForWorld", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            if (conn != null)
            {
                conn.Close();
            }

            return dataSet;
        }

        public static Continent GetContinentByName(string prettyUrl)
        {
            Continent continent = new Continent();

            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetContinentByName", conn);
            cmd.Parameters.Add(new SqlParameter("@PrettyUrl", prettyUrl));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            //TODO: error handle
            try
            {
                continent = DataConverter.ToType<Continent>(dataSet.Tables[0].Rows[0]);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return continent;
        }

        public static Country GetCountryByName(string prettyUrl)
        {
            Country country = new Country();

            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetCountryByName", conn);
            cmd.Parameters.Add(new SqlParameter("@PrettyUrl", prettyUrl));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            //TODO: error handle
            try
            {
                country = DataConverter.ToType<Country>(dataSet.Tables[0].Rows[0]);
                country.Continent = DataConverter.ToType<Continent>(dataSet.Tables[1].Rows[0]);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return country;
        }

        public static ScrapeResort GetScrapeResort(string name, string countryName)
        {
            ScrapeResort scrapeResort = new ScrapeResort();

            //TODO: put all this in a method
//            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlConnection conn = new SqlConnection("Data Source=tcp:esql2k501.discountasp.net;Initial Catalog=SQL2005_615410_sporthub;User ID=SQL2005_615410_sporthub_user;Password=first2009;");
            SqlCommand cmd = new SqlCommand("GetScrapeResort", conn);
            cmd.Parameters.Add(new SqlParameter("@Name", name));
            cmd.Parameters.Add(new SqlParameter("@CountryName", countryName));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            //TODO: error handle
            try
            {
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    scrapeResort = DataConverter.ToType<ScrapeResort>(dataSet.Tables[0].Rows[0]);
                }
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return scrapeResort;
        }

        public static Country GetCountryByID(string id)
        {
            Country country = new Country();

            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetCountryByID", conn);
            cmd.Parameters.Add(new SqlParameter("@ID", id));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            //TODO: error handle
            try
            {
                country = DataConverter.ToType<Country>(dataSet.Tables[0].Rows[0]);
                country.Continent = DataConverter.ToType<Continent>(dataSet.Tables[1].Rows[0]);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return country;
        }

        public static int AddResortToMountainRangeLink(int resortID, int mountainRangeID)
        {
            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("AddResortToMountainRangeLink", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ResortID", resortID));
            cmd.Parameters.Add(new SqlParameter("@MountainRangeID", mountainRangeID));

            conn.Open();

            string mountainRangLinkeID = cmd.ExecuteScalar().ToString();

            return int.Parse(mountainRangLinkeID);
        }
    }
}
