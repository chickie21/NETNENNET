using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;


namespace NETNENNET.Class
{
    internal class Function
    {
        public static SqlConnection Connection;
        public static string connString; 
         
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

        public static DataTable GetDataToTableRange(string sql, SqlParameter[] parameters)
        {
            SqlDataAdapter myData = new SqlDataAdapter();
            myData.SelectCommand = new SqlCommand();
            myData.SelectCommand.Connection = Function.Connection;
            myData.SelectCommand.CommandText = sql;

            if (parameters != null)
            {
                myData.SelectCommand.Parameters.AddRange(parameters);
            }

            DataTable table = new DataTable();
            myData.Fill(table);
            return table;
        }


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

        public static string GetFieldValues2(string sql1)
        {
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql1, Function.Connection);
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


        public static string ChuyenSoSangChu(string sNumber)
        {
            int mLen, mDigit;
            string mTemp = "";
            string[] mNumText;
            //Xóa các dấu "," nếu có
            sNumber = sNumber.Replace(",", "");
            mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');
            mLen = sNumber.Length - 1; // trừ 1 vì thứ tự đi từ 0
            for (int i = 0; i <= mLen; i++)
            {
                mDigit = Convert.ToInt32(sNumber.Substring(i, 1));
                mTemp = mTemp + " " + mNumText[mDigit];
                if (mLen == i) // Chữ số cuối cùng không cần xét tiếp
                    break;
                switch ((mLen - i) % 9)
                {
                    case 0:
                        mTemp = mTemp + " tỷ";
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        break;
                    case 6:
                        mTemp = mTemp + " triệu";
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        break;
                    case 3:
                        mTemp = mTemp + " nghìn";
                        if (sNumber.Substring(i + 1, 3) == "000")
                            i = i + 3;
                        break;
                    default:
                        switch ((mLen - i) % 3)
                        {
                            case 2:
                                mTemp = mTemp + " trăm";
                                break;
                            case 1:
                                mTemp = mTemp + " mươi";
                                break;
                        }
                        break;
                }
            }
            //Loại bỏ trường hợp x00
            mTemp = mTemp.Replace("không mươi không ", "");
            mTemp = mTemp.Replace("không mươi không", "");
            //Loại bỏ trường hợp 00x
            mTemp = mTemp.Replace("không mươi ", "linh ");
            //Loại bỏ trường hợp x0, x>=2
            mTemp = mTemp.Replace("mươi không", "mươi");
            //Fix trường hợp 10
            mTemp = mTemp.Replace("một mươi", "mười");
            //Fix trường hợp x4, x>=2
            mTemp = mTemp.Replace("mươi bốn", "mươi tư");
            //Fix trường hợp x04
            mTemp = mTemp.Replace("linh bốn", "linh tư");
            //Fix trường hợp x5, x>=2
            mTemp = mTemp.Replace("mươi năm", "mươi lăm");
            //Fix trường hợp x1, x>=2
            mTemp = mTemp.Replace("mươi một", "mươi mốt");
            //Fix trường hợp x15
            mTemp = mTemp.Replace("mười năm", "mười lăm");
            //Bỏ ký tự space
            mTemp = mTemp.Trim();
            //Viết hoa ký tự đầu tiên
            mTemp = mTemp.Substring(0, 1).ToUpper() + mTemp.Substring(1) + " đồng";
            return mTemp;
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

        public static void RunSqlDel(string sql)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Function.Connection;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (System.Exception)
            {
                MessageBox.Show("Dữ liệu đang được dùng, không thể xóa...", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            cmd.Dispose();
            cmd = null;
        }

    }
}

