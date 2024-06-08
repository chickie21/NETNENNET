using NETNENNET.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NETNENNET
{
    public partial class TIMHOADON : Form
    {
        public TIMHOADON()
        {
            InitializeComponent();
        }

        private void TIMHOADON_Load(object sender, EventArgs e)
        {
            Function.Connect();
            ResetValues();
            DataGridView.DataSource = null;
        }

        private void ResetValues()
        {
            foreach (Control Ctl in this.Controls)
                if (Ctl is TextBox)
                    Ctl.Text = "";
            txtMaTMSearch.Focus();
        }

        DataTable tblHD;

        private int TinhTienGio(DateTime gioVao, DateTime gioRa)
        {
            double soPhutSuDung = (gioRa - gioVao).TotalMinutes;
            int tienGio = (int)Math.Floor((soPhutSuDung / 60.0) * 10000.0);
            return tienGio;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaTMSearch.Text == "") && (txtMaNV.Text == "") && (txtThang.Text == "") &&
                (txtNam.Text == "") && (txtTenkhach.Text == "") && (txtTongtien.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!",
                    "Yêu cầu...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sql = @"SELECT tm.MaTM, tm.MaNV, tm.TenKhach, tm.NgayThue, tm.GioVao, tm.GioRa,
                    SUM(COALESCE(dv.Soluong * dv.Dongia, 0)) AS TongTienDichVu
                    FROM tblTHUEMAY tm
                    LEFT JOIN tblDICHVU dv ON tm.MaTM = dv.MaTM
                    WHERE 1=1";

            if (!string.IsNullOrEmpty(txtMaTMSearch.Text))
            {
                sql += " AND tm.MaTM = " + txtMaTMSearch.Text;
            }
            if (!string.IsNullOrEmpty(txtThang.Text))
            {
                sql += " AND MONTH(NgayThue) = " + txtThang.Text;
            }
            if (!string.IsNullOrEmpty(txtNam.Text))
            {
                sql += " AND YEAR(NgayThue) = " + txtNam.Text;
            }
            if (!string.IsNullOrEmpty(txtMaNV.Text))
            {
                sql += " AND MaNV Like N'%" + txtMaNV.Text + "%'";
            }
            if (!string.IsNullOrEmpty(txtTenkhach.Text))
            {
                sql += " AND TenKhach Like N'%" + txtTenkhach.Text + "%'";
            }

            sql += " GROUP BY tm.MaTM, tm.MaNV, tm.TenKhach, tm.NgayThue, tm.GioVao, tm.GioRa";

            tblHD = Function.GetDataToTable(sql);

            // Tạo cột mới để hiển thị tổng tiền
            tblHD.Columns.Add("TongTien", typeof(int));

            foreach (DataRow row in tblHD.Rows)
            {
                DateTime gioVao = Convert.ToDateTime(row["GioVao"]);
                DateTime gioRa = Convert.ToDateTime(row["GioRa"]);
                int tienGio = TinhTienGio(gioVao, gioRa);
                int tienDichVu = Convert.ToInt32(row["TongTienDichVu"]);
                row["TongTien"] = tienGio + tienDichVu;
            }

            if (!string.IsNullOrEmpty(txtTongtien.Text))
            {
                if (int.TryParse(txtTongtien.Text, out int tongTien))
                {
                    var filteredRows = tblHD.AsEnumerable()
                        .Where(row => Convert.ToInt32(row["TongTien"]) == tongTien);

                    if (filteredRows.Any())
                    {
                        tblHD = filteredRows.CopyToDataTable();
                    }
                    else
                    {
                        tblHD.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập giá trị số cho tổng tiền.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (tblHD.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetValues();
            }
            else
            {
                MessageBox.Show("Có " + tblHD.Rows.Count + " bản ghi thỏa mãn điều kiện!!!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            DataGridView.DataSource = tblHD;
            Load_DataGridView();
        }


        private void Load_DataGridView()
        {
            DataGridView.Columns[0].HeaderText = "Mã thuê máy";
            DataGridView.Columns[1].HeaderText = "Mã nhân viên";
            DataGridView.Columns[2].HeaderText = "Tên khách";
            DataGridView.Columns[3].HeaderText = "Ngày thuê";
            DataGridView.Columns[4].HeaderText = "Giờ vào";
            DataGridView.Columns[5].HeaderText = "Giờ ra";
            DataGridView.Columns[6].HeaderText = "Tổng tiền dịch vụ";
            DataGridView.Columns[7].HeaderText = "Tổng tiền";
            DataGridView.Columns[0].Width = 100;
            DataGridView.Columns[1].Width = 100;
            DataGridView.Columns[2].Width = 100;
            DataGridView.Columns[3].Width = 100;
            DataGridView.Columns[4].Width = 100;
            DataGridView.Columns[5].Width = 100;
            DataGridView.Columns[6].Width = 100;
            DataGridView.Columns[7].Width = 100;
            DataGridView.AllowUserToAddRows = false;
            DataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnTimlai_Click(object sender, EventArgs e)
        {
            ResetValues();
            DataGridView.DataSource = null;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DataGridView_DoubleClick(object sender, EventArgs e)
        {
            if (DataGridView.CurrentRow != null)
            {
                DataTable tblChiTietHD = new DataTable();
                string sql;
                if (tblHD.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu nào cả!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int r = DataGridView.CurrentCell.RowIndex;
                string maTM = tblHD.Rows[r][0].ToString();

                sql = @"SELECT tm.MaTM, tm.MaNV, tm.TenKhach, tm.NgayThue,
                       dv.MaTP, tp.TenTP, dv.Dongia, dv.Soluong, dv.Dongia * dv.Soluong AS ThanhTien
                FROM tblTHUEMAY tm
                LEFT JOIN tblDICHVU dv ON tm.MaTM = dv.MaTM
                LEFT JOIN tblTHUCPHAM tp ON dv.MaTP = tp.MaTP
                WHERE tm.MaTM = " + maTM;

                tblChiTietHD = Function.GetDataToTable(sql);

                string thongTinChiTiet = "Mã thuê máy: " + maTM + "\n" +
                                         "Mã nhân viên: " + tblChiTietHD.Rows[0]["MaNV"].ToString() + "\n" +
                                         "Tên khách: " + tblChiTietHD.Rows[0]["TenKhach"].ToString() + "\n" +
                                         "Ngày thuê: " + tblChiTietHD.Rows[0]["NgayThue"].ToString() + "\n";

                if (tblChiTietHD.AsEnumerable().Any(row => !row.IsNull("MaTP")))
                {
                    thongTinChiTiet += "\nDịch vụ đã sử dụng:\n";

                    foreach (DataRow row in tblChiTietHD.Rows)
                    {
                        if (!row.IsNull("MaTP"))
                        {
                            thongTinChiTiet += $"- {row["TenTP"]}: {row["Soluong"]} x {row["Dongia"]} = {row["ThanhTien"]} VNĐ\n";
                        }
                    }
                }
                else
                {
                    thongTinChiTiet += "\nKhông có dịch vụ nào được sử dụng.\n";
                }

                MessageBox.Show(thongTinChiTiet, "Thông tin chi tiết", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void txtMaTM_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaTMSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
