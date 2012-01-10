using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub.Model;
using System.Configuration;

namespace Sporthub.Data
{
    public class ResortDataManager
    {

        public static List<Resort> GetResortsByContinent(string prettyUrl)
        {
            List<Resort> resorts = new List<Resort>();

            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetResortsByContinent", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PrettyUrl", prettyUrl));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            resorts = MapResortsToModel(dataSet);

            return resorts;
        }

        public static Resort GetResortByName(string prettyUrl)
        {
            Resort resort = new Resort();

            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetResortByName", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PrettyUrl", prettyUrl));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            DataTable resortTable = dataSet.Tables[0];
            DataTable continentTable = dataSet.Tables[1];
            DataTable countryTable = dataSet.Tables[2];
            DataTable regionTable = dataSet.Tables[3];
            DataTable resortLinksTable = dataSet.Tables[4];

            //TODO: error handle
            try
            {
                if (resortTable.Rows.Count > 0)
                {
                    resort = DataConverter.ToType<Resort>(resortTable.Rows[0]);

                    if (continentTable.Rows.Count > 0)
                    {
                        resort.Continent = DataConverter.ToType<Continent>(continentTable.Rows[0]);
                    }
                    if (countryTable.Rows.Count > 0)
                    {
                        resort.Country = DataConverter.ToType<Country>(countryTable.Rows[0]);
                    }
                    //TODO: regions
                    if (resortLinksTable.Rows.Count > 0)
                    {
                        resort.ResortLinks = new List<ResortLink>();
                        foreach (DataRow resortLink in resortLinksTable.Rows)
                        {
                            resort.ResortLinks.Add(DataConverter.ToType<ResortLink>(resortLink));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                //TODO: error handle
            }


            return resort;
        }

        public static List<Resort> GetResortsByCountry(string prettyUrl)
        {
            List<Resort> resorts = new List<Resort>();

            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetResortsByCountry", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PrettyUrl", prettyUrl));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            resorts = MapResortsToModel(dataSet);

            return resorts;
        }

        public static int AddResortLink(ResortLink resortLink)
        {
            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("AddResortLink", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Name", resortLink.Name));
            cmd.Parameters.Add(new SqlParameter("@ResortID", resortLink.ResortID));
            cmd.Parameters.Add(new SqlParameter("@ResortLinkTypeID", resortLink.ResortLinkTypeID));
            cmd.Parameters.Add(new SqlParameter("@URL", resortLink.URL));
            cmd.Parameters.Add(new SqlParameter("@Sequence", resortLink.Sequence));
            cmd.Parameters.Add(new SqlParameter("@CreatedUserID", resortLink.CreatedUserID));
            cmd.Parameters.Add(new SqlParameter("@UpdatedUserID", resortLink.UpdatedUserID));

            conn.Open();

            string resortLinkID = cmd.ExecuteScalar().ToString();

            return int.Parse(resortLinkID);
        }
        
        public static List<ResortLink> GetResortLinks(int resortID)
        {
            List<ResortLink> resortLinks = new List<ResortLink>();

            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetResortLinks", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ResortID", resortID));
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
                        resortLinks.Add(DataConverter.ToType<ResortLink>(row));
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

            return resortLinks;
        }

        public static List<Resort> MapResortsToModel(DataSet ds)
        {
            List<Resort> resorts = new List<Resort>();

            DataTable resortTable = ds.Tables[0];

            //TODO: error handle
            try
            {
                DataRowCollection rows = resortTable.Rows;

                if (rows.Count > 0)
                {
                    foreach (DataRow resort in rows)
                    {
                        resorts.Add(DataConverter.ToType<Resort>(resort));
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                //TODO: error handle
            }

            return resorts;
        }

        public static int AddScrapeResortStats(Resort resort, ResortStats resortStats)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            //SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=SQL2005_615410_sporthub;Persist Security Info=True;User ID=sa;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("AddScrapeResortStats", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Name", resort.Name));
            cmd.Parameters.Add(new SqlParameter("@PrettyUrl", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@CountryName", resort.Country.CountryName));
            cmd.Parameters.Add(new SqlParameter("@BaseLevel", resortStats.BaseLevel));
            cmd.Parameters.Add(new SqlParameter("@TopLevel", resortStats.TopLevel));
            cmd.Parameters.Add(new SqlParameter("@VerticalDrop", resortStats.VerticalDrop));
            cmd.Parameters.Add(new SqlParameter("@Height", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@AverageSnowfall", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@HasSnowmaking", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@SnowmakingCoverage", resortStats.SnowmakingCoverage));
            cmd.Parameters.Add(new SqlParameter("@PreSeasonStartMonth", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@SeasonStartMonth", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@SeasonEndMonth", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@Population", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@MountainRestaurants", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@LiftDescription", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@LiftTotal", resortStats.LiftTotal));
            cmd.Parameters.Add(new SqlParameter("@LiftCapacityHour", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@QuadPlusCount", resortStats.QuadPlusCount));
            cmd.Parameters.Add(new SqlParameter("@QuadCount", resortStats.QuadCount));
            cmd.Parameters.Add(new SqlParameter("@TripleCount", resortStats.TripleCount));
            cmd.Parameters.Add(new SqlParameter("@DoubleCount", resortStats.DoubleCount));
            cmd.Parameters.Add(new SqlParameter("@SingleCount", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@SurfaceCount", resortStats.SurfaceCount));
            cmd.Parameters.Add(new SqlParameter("@GondolaCount", resortStats.GondolaCount));
            cmd.Parameters.Add(new SqlParameter("@FunicularCount", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@SurfaceTrainCount", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@RunTotal", string.Empty));
            cmd.Parameters.Add(new SqlParameter("@BlackRuns", resortStats.BlackRuns));
            cmd.Parameters.Add(new SqlParameter("@RedRuns", resortStats.RedRuns));
            cmd.Parameters.Add(new SqlParameter("@BlueRuns", resortStats.BlueRuns));
            cmd.Parameters.Add(new SqlParameter("@GreenRuns", resortStats.GreenRuns));
            cmd.Parameters.Add(new SqlParameter("@LongestRunDistance", resortStats.LongestRunDistance));
            cmd.Parameters.Add(new SqlParameter("@SkiableTerrianSize", resortStats.SkiableTerrianSize));

            conn.Open();

            string resortLinkID = cmd.ExecuteScalar().ToString();

            return int.Parse(resortLinkID);
        }

    }
}
