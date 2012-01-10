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
    public class NewsDataManager
    {
        public static List<NewsFeed> GetNewsFeeds(int type)
        {
            List<NewsFeed> newsFeeds = new List<NewsFeed>();

            //TODO: put all this in a method
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SQL2005_615410_sporthubConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("GetNewsFeeds", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Type", type));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);

            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                newsFeeds.Add(DataConverter.ToType<NewsFeed>(dr));
            }

            return newsFeeds;
        }
    }
}
