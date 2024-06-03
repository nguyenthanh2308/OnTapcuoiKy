using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace KtraGKLan2_4551190048_
{
   public static class DataProvider
    {
        public static SqlConnection cn;
        public static SqlDataAdapter da;
        public static SqlCommand cmd;
       

        public static void moKeNoi()
        {
            cn = new SqlConnection();
            cn.ConnectionString = ConfigurationManager.ConnectionStrings["QLCHOTHUENHA"].ConnectionString.ToString();
            cn.Open();
        }
        public static void dongKetNoi()
        {
            cn.Close();
        }
        public static DataTable GetTable(string sql)
        {
            da = new SqlDataAdapter(sql, cn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }
        public static void updateData(string sql, object[] value = null, string[] name = null)
        {
            cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Clear();
            if(value == null)
            {
                for (int i = 0; i < value.Length; i++)
                    cmd.Parameters.AddWithValue(name[i], value[i]);
            }
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        public static int checkData(string sql)
        {
            cmd = new SqlCommand(sql, cn);
            int i = (int)cmd.ExecuteScalar();
            cmd.Dispose();
            return i;
        }
    }
}
