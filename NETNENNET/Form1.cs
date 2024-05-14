using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NETNENNET
{
    public partial class Form1 : Form
    {
        DataTable tblMAYTINH;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Class.Function.Connect();
            LoadDataGridView();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void tabRoom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadDataGridView()
        {
            string sql = "SELECT MaPhong, MaMay FROM tblMAYTINH";
            tblMAYTINH = Class.Function.GetDataToTable(sql);

            foreach (DataRow row in tblMAYTINH.Rows)
            {
                string maPhong = row["MaPhong"].ToString();
                string maMay = row["MaMay"].ToString();
/*                string gioRa = row["GioRa"].ToString();
                string gioVao = row["GioVao"].ToString();*/

                row["MaPhong"] = "Phòng " + maPhong;
                row["MaMay"] = "Máy " + maMay;

                
            }

            dataGridView1.DataSource = tblMAYTINH;
            dataGridView1.Columns[0].HeaderText = "Tên phòng";
            dataGridView1.Columns[1].HeaderText = "Mã máy";
/*            dataGridView1.Columns[2].HeaderText = "Giờ ra";
            dataGridView1.Columns[3].HeaderText = "Giờ vào";*/
        }


        private void btnMoMay_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string maMay = dataGridView1.SelectedRows[1].Cells["MaMay"].Value.ToString();
                string maPhong = dataGridView1.SelectedRows[0].Cells["MaPhong"].Value.ToString();
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string insertQuery = $"INSERT INTO tblTHUEMAY (MaPhong, MaMay, TenKhach, NgayThue, GioVao, GioRa) VALUES ('{maPhong}','{maMay}', '{currentTime}')";
                Class.Function.GetDataToTable(insertQuery);

                LoadDataGridView();
            }
        }

        private void btnTatMay_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string maMay = dataGridView1.SelectedRows[0].Cells["MaMay"].Value.ToString();
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string updateQuery = $"UPDATE tblTHUEMAY SET GioRa = '{currentTime}' WHERE MaMay = '{maMay}' AND GioRa IS NULL";
                Class.Function.GetDataToTable(updateQuery);

                LoadDataGridView();
            }
        }

    }
}
