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
    public class RegionDataManager
    {
        public static List<Region> GetRegionsByCountryAndLevel(int countryID, int regionLevel)
        {
            List<Region> regions = new List<Region>();

            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetRegionsByCountryAndLevel", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CountryID", countryID));
            cmd.Parameters.Add(new SqlParameter("@RegionLevel", regionLevel));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            regions = MapRegionToModel(dataSet);

            return regions;
        }

        public static List<Region> GetRegionsByCountryNameAndLevel(string prettyUrl, int regionLevel)
        {
            List<Region> regions = new List<Region>();

            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetRegionsByCountryNameAndLevel", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PrettyUrl", prettyUrl));
            cmd.Parameters.Add(new SqlParameter("@RegionLevel", regionLevel));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            regions = MapRegionToModel(dataSet);

            return regions;
        }

        public static List<Region> GetRegionsByParentID(int parentID)
        {
            List<Region> regions = new List<Region>();

            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetRegionsByParentID", conn);
            cmd.Parameters.Add(new SqlParameter("@ParentRegionID", parentID));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            regions = MapRegionToModel(dataSet);

            return regions;
        }

        public static List<Region> MapRegionToModel(DataSet ds)
        {
            List<Region> regions = new List<Region>();

            DataTable regionTable = ds.Tables[0];
            DataTable countryTable = ds.Tables[1];

            //TODO: error handle
            try
            {
                DataRowCollection rowsCountry = countryTable.Rows;

                if (rowsCountry.Count > 0)
                {
                    foreach (DataRow country in rowsCountry)
                    {
                        DataRowCollection rowsRegion = regionTable.Rows;

                        if (rowsRegion.Count > 0)
                        {
                            foreach (DataRow region in rowsRegion)
                            {
                                Region newRegion = DataConverter.ToType<Region>(region);
                                newRegion.Country = DataConverter.ToType<Country>(country);

                                if (newRegion.ParentRegionID > 0)
                                {
                                    ////TODO: put this in a method
                                    SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
                                    SqlCommand cmd2 = new SqlCommand("GetRegionByID", conn2);
                                    cmd2.CommandType = CommandType.StoredProcedure;
                                    cmd2.Parameters.Add(new SqlParameter("@ID", newRegion.ParentRegionID));
                                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                                    DataSet dataSet2 = new DataSet();
                                    da2.Fill(dataSet2);

                                    DataRow parentRegion = dataSet2.Tables[0].Rows[0];
                                    newRegion.ParentRegion = DataConverter.ToType<Region>(parentRegion);
                                }
                                else
                                {
                                    newRegion.ParentRegion = new Region();
                                }
                                regions.Add(newRegion);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                //TODO: error handle
            }

            return regions;
        }
    }
}

