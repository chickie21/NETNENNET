using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
// Bên trên có rồi mà bạn   viet lai di xem duoc khong, kieu du lieu datatable sao khong duoc

namespace NETNENNET.Class
{
    internal class Function
    {
        public static SqlConnection Connection;
        public static string connString; //thong cam, so bi quen =))) khong tum duoc ten bien la gi hic, vang =))
         
        public static void Connect()
        {
            connString = "Data Source=DESKTOP-LQ9NHMT;Initial Catalog=NETNENNET;Integrated Security=True;Encrypt=False";
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
        //Ơ lỗi rồi, các máy đang bị dính giờ lẫn nhau, vừa mở cái tắt ngay nhưng vẫn bị dính giờ của lần thuê trc =))

        //GetFieldValues
        public static string GetFieldValues(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, Function.Connection);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ma = reader.GetValue(0).ToString();
            }
            reader.Close();
            return ma;

        }
        public static string GetFieldValues1(string sql)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, Function.Connection);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ma = reader.GetValue(0).ToString();
            }
            reader.Close();
            return ma;

        }

        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, Function.Connection);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            cbo.DataSource = table;

            cbo.ValueMember = ma;    // Truong gia tri
            cbo.DisplayMember = ten;    // Truong hien thi
        }

        public static void FillCombo2(string sql, ComboBox cbo, string ma)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, Function.Connection);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            cbo.DataSource = table;

            cbo.ValueMember = ma;    // Truong gia tri
        }
        public static void RunSQL(string sql)
        {
            SqlCommand cmd;		                // Khai báo đối tượng SqlCommand
            cmd = new SqlCommand();	         // Khởi tạo đối tượng
            cmd.Connection = Function.Connection;	  // Gán kết nối
            cmd.CommandText = sql;			  // Gán câu lệnh SQL
            try
            {
                cmd.ExecuteNonQuery();		  // Thực hiện câu lệnh SQL
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }
        public static bool IsDate(string d)
        {
            string[] parts = d.Split('/');
            if ((Convert.ToInt32(parts[0]) >= 1) && (Convert.ToInt32(parts[0]) <= 31) &&
                (Convert.ToInt32(parts[1]) >= 1) && (Convert.ToInt32(parts[1]) <= 12) && (Convert.ToInt32(parts[2]) >= 1900))
                return true;
            else
                return false;
        }
        public static string ConvertDateTime(string d)
        {
            string[] parts = d.Split('/');
            string dt = String.Format("{0}/{1}/{2}", parts[1], parts[0], parts[2]);
            return dt;
        }
        public static bool CheckKey(string sql)
        {
            SqlDataAdapter Mydata = new SqlDataAdapter(sql, Function.Connection);
            DataTable table = new DataTable();
            Mydata.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else
                return false;
        }

    }
}//duoc roi, viet duoc datatable roi do

