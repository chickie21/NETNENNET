using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
// Bên trên có rồi mà bạn   viet lai di xem duoc khong, kieu du lieu datatable sao khong duoc

namespace NETNENNET.Class
{
    internal class Function
    {
        public static SqlConnection Connection;
        public static string connString; //thong cam, so bi quen =))) khong tum duoc ten bien la gi hic, vang =))
         
        public static void Connect()
        {
            connString = "Data Source=CHICKIE;Initial Catalog = NETNENNET; Integrated Security = True; Encrypt=False";
            Connection = new SqlConnection();
            Connection.ConnectionString = connString;
            Connection.Open();
        }

        public static void Close()
        {
            Connection.Close();
            Connection.Dispose();
            Connection = null;
        }

        public static DataTable GetDataToTable (string sql)
        {
            SqlDataAdapter myData = new SqlDataAdapter();
            myData.SelectCommand = new SqlCommand();
            myData.SelectCommand.Connection = Function.Connection;
            myData.SelectCommand.CommandText = sql;

            DataTable table = new DataTable();

            myData.Fill(table);
            return table;
        }

        //ExecuteQuery
        public static void ExecuteQuery(string sql)
        {
            
        }
    }
}//duoc roi, viet duoc datatable roi do
