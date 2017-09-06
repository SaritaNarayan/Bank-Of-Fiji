using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BoF.Web.Database_Access_Layer
{
    public class Statement
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);

              
        public DataSet GetStatement(int prefix)
        {
            SqlCommand com = new SqlCommand("SELECT [TransAmount],[TransDetails],[TransDateTime],[Debit],[Credit],[RunningCapital] FROM[dbo].[Transaction]where Account_id = @prefix", connection);
            com.Parameters.AddWithValue("@prefix", prefix);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(com);
            da.Fill(0, 5000, ds.Tables.Add("MyTable"));
            return ds;
        }
        //public DataSet GetCustomerDetails(int prefix)
        //{
        //    SqlCommand com = new SqlCommand("select Account.[AccountNumber] from[BOF].[dbo].[Account] where [BOF].[dbo].[Account].[Customer_id] = @prefix", connection);
        //    com.Parameters.AddWithValue("@prefix", prefix);
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter(com);
        //    da.Fill(0, 5000, ds.Tables.Add("MyTable"));
        //    return ds;
        //}
        //public DataSet GetCustomerDetails(string prefix)
        //{
        //    SqlCommand com = new SqlCommand("select ARCUS.[NAMECUST], ARCUS.[NAMECTAC], ARCUS.[TEXTPHON1], ARCUS.[TEXTSTRE1]+''+ARCUS.[TEXTSTRE2]+''+ARCUS.[TEXTSTRE3]as AddressofDelivery from [TSTDAT].[dbo].[ARCUS] where upper(NAMECUST) like upper(@prefix+'%')", con);
        //    com.Parameters.AddWithValue("@prefix", prefix);
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter(com);
        //    da.Fill(0, 5000, ds.Tables.Add("MyTable"));
        //    return ds;

        //}


        //public DataSet GetCustomerDetails(string prefix)
        //{
        //    SqlCommand com = new SqlCommand("c
        //    com.Parameters.AddWithValue("@prefix", prefix);
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter(com);
        //    da.Fill(0, 5000, ds.Tables.Add("MyTable"));
        //    return ds;
        //}
    }
}