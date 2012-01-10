using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void GetAllResorts()
    {
        SqlConnection cnn = new SqlConnection("Data Source=tcp:esql2k501.discountasp.net;Initial Catalog=SQL2005_615410_sporthub;Persist Security Info=True;User ID=SQL2005_615410_sporthub_user;Password=first2009");
        cnn.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = cnn;
        cmd.CommandText = "select * from resort";
        SqlDataReader reader = cmd.ExecuteReader();
        SqlContext.Pipe.Send(reader);
        reader.Close();
        cnn.Close();
    }
};
