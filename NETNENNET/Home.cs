using Microsoft.Office.Interop.Excel;
using NETNENNET.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using SystemTableData = System.Data.DataTable;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMExcel = Microsoft.Office.Interop.Excel;


namespace NETNENNET
{

    public partial class Home : Form
    {
        public string CustomFormat { get; set; }
        SystemTableData tblMAYTINH;
        SystemTableData tblDICHVU;
        SystemTableData HoaDon;

        public Home()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Class.Function.Connect();
            RefreshCboMaTM();
            LoadDataGridView();
            dtpGioVao.Text = "";
            dtpGioRa.Text = "";
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            cboMaTM.Enabled = false;
            Function.FillCombo("SELECT MaNV, TenNV FROM tblNHANVIEN",
cboNhanVien, "MaNV", "TenNV");

            Function.FillCombo("SELECT MaPhong, TenPhong FROM tblPhong", cboPhong, "MaPhong", "TenPhong");
            cboPhong.SelectedIndex = -1;

            Function.FillCombo2("SELECT MaTM FROM tblTHUEMAY", cboMaTM, "MaTM");
            cboMaTM.SelectedIndex = -1;

            Function.FillCombo("SELECT MaTP, TenTP FROM tblTHUCPHAM", cboMaTP, "MaTP", "TenTP");
            cboMaTP.SelectedIndex = -1;
        }
        private void LoadDataGridView()
        {

            //Tab thuê máy
            string sql = "SELECT MaPhong, MaMay,TinhTrangThue FROM tblMAYTINH";
            tblMAYTINH = Class.Function.GetDataToTable(sql);
            dataGridView1.DataSource = tblMAYTINH;

            dataGridView1.Columns[0].HeaderText = "Mã Phòng";
            dataGridView1.Columns[1].HeaderText = "Mã Máy";
            dataGridView1.Columns[2].HeaderText = "Tình Trạng Thuê";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.AllowUserToDeleteRows = false;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[2].Value.ToString() == "Chưa thuê")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
            SetMyCustomFormat();

            //Tab dịch vụ
            string sqlDichVu = "SELECT MaTM, MaTP, Soluong, Dongia FROM tblDICHVU";
            tblDICHVU = Class.Function.GetDataToTable(sqlDichVu);
            dgvDichVu.DataSource = tblDICHVU;
            dgvDichVu.Columns[0].HeaderText = "Mã thuê máy";
            dgvDichVu.Columns[1].HeaderText = "Mã thực phẩm";
            dgvDichVu.Columns[2].HeaderText = "Số lượng";
            dgvDichVu.Columns[3].HeaderText = "Đơn giá";
            dgvDichVu.AllowUserToAddRows = false;
            dgvDichVu.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvDichVu.AllowUserToDeleteRows = false;

            // Tab hóa đơn
            string sqlHoaDon = "SELECT tblTHUEMAY.MaTM, tblTHUEMAY.MaPhong, tblTHUEMAY.MaMay, tblTHUEMAY.TenKhach, tblTHUEMAY.GioVao, tblTHUEMAY.GioRa,tblDICHVU.MaTP, tblDICHVU.Soluong, tblDICHVU.Dongia, tblNHANVIEN.TenNV FROM tblTHUEMAY\r\nLEFT JOIN tblDICHVU\r\nON tblTHUEMAY.MaTM = tblDICHVU.MaTM LEFT JOIN tblNHANVIEN on tblTHUEMAY.MaNV = tblNHANVIEN.MaNV";
            HoaDon = Class.Function.GetDataToTable(sqlHoaDon);
            dataGridView2.DataSource = HoaDon;
            dataGridView2.Columns[0].HeaderText = "Mã thuê máy";
            dataGridView2.Columns[1].HeaderText = "Mã phòng";
            dataGridView2.Columns[2].HeaderText = "Mã máy";
            dataGridView2.Columns[3].HeaderText = "Tên khách";
            dataGridView2.Columns[4].HeaderText = "Giờ vào";
            dataGridView2.Columns[5].HeaderText = "Giờ ra";
            dataGridView2.Columns[6].HeaderText = "Mã thực phẩm";
            dataGridView2.Columns[7].HeaderText = "Số lượng";
            dataGridView2.Columns[8].HeaderText = "Đơn giá";
            dataGridView2.Columns[9].HeaderText = "Tên nhân viên";
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.EditMode = DataGridViewEditMode.EditProgrammatically;

            string sqlSoLuongTP = "SELECT TenTP, Soluong FROM tblTHUCPHAM";
            SystemTableData dtSoLuongTP = Class.Function.GetDataToTable(sqlSoLuongTP);
            dataGridView3.DataSource = dtSoLuongTP;
            dataGridView3.Columns[0].HeaderText = "Tên thực phẩm";
            dataGridView3.Columns[1].HeaderText = "Số lượng";
            dataGridView3.AllowUserToAddRows = false;
            dataGridView3.AllowUserToDeleteRows = false;
            dataGridView3.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        public void SetMyCustomFormat()
        {
            dtpGioVao.Format = DateTimePickerFormat.Custom;
            dtpGioVao.CustomFormat = "dd-MM--yyyy ,HH:mm:ss";

            dtpGioRa.Format = DateTimePickerFormat.Custom;
            dtpGioRa.CustomFormat = "dd-MM--yyyy ,HH:mm:ss";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            dtpGioRa.Value = DateTime.Now;
            txtMaMay.Text = dataGridView1.CurrentRow.Cells["MaMay"].Value.ToString();
            txtNgayThue.Text = DateTime.Now.ToShortDateString();
            txtMaPhong.Text = dataGridView1.CurrentRow.Cells["MaPhong"].Value.ToString();
            txtTenKhachHang.Text = Function.GetFieldValues("SELECT TenKhach FROM tblTHUEMAY WHERE MaMay = N'" + txtMaMay.Text + "'");
        }
        private void tabRoom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void btnMoMay_Click_1(object sender, EventArgs e)
        {
            if (txtMaMay.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn máy tính nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtTenKhachHang.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenKhachHang.Focus();
                return;
            }

            if (cboNhanVien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboNhanVien.Focus();
                return;
            }

            string sqlCheckTinhTrang = "SELECT TinhTrangThue FROM tblMAYTINH WHERE MaMay = N'" + txtMaMay.Text + "'";
            string tinhTrang = Function.GetFieldValues(sqlCheckTinhTrang);
            if (tinhTrang != "Chưa thuê")
            {
                MessageBox.Show("Máy tính này đã được thuê hoặc đang thuê, vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime gioVao = DateTime.Now;
            string gioVaoString = gioVao.ToString("yyyy-MM-dd HH:mm:ss");
            string ngayThueString = gioVao.ToString("yyyy-MM-dd");

            DateTime gioRaMacDinh = gioVao.AddMinutes(1);
            string gioRaMacDinhString = gioRaMacDinh.ToString("yyyy-MM-dd HH:mm:ss");

            string sqlInsert = "INSERT INTO tblTHUEMAY(MaPhong, MaMay, TenKhach, NgayThue, GioVao, GioRa, MaNV) VALUES (N'" + txtMaPhong.Text + "', N'" + txtMaMay.Text + "', N'" + txtTenKhachHang.Text + "', N'" + ngayThueString + "', N'" + gioVaoString + "', N'" + gioRaMacDinhString + "', N'" + cboNhanVien.SelectedValue + "')";
            Class.Function.RunSQL(sqlInsert);

            string sqlUpdateMayTinh = "UPDATE tblMAYTINH SET TinhTrangThue = N'Đang thuê' WHERE MaMay = N'" + txtMaMay.Text + "'";
            Class.Function.RunSQL(sqlUpdateMayTinh);

            LoadDataGridView();
            UpdateRowColors();
            MessageBox.Show("Mở máy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void txtNgayThue_TextChanged(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnTatMay_Click(object sender, EventArgs e)
        {
            if (txtMaMay.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn máy tính nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sqlCheckTinhTrang = "SELECT TinhTrangThue FROM tblMAYTINH WHERE MaMay = N'" + txtMaMay.Text + "'";
            string tinhTrang = Function.GetFieldValues(sqlCheckTinhTrang);
            if (tinhTrang != "Đang thuê")
            {
                MessageBox.Show("Máy tính này không đang được thuê hoặc đã được tắt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sqlGetMaTM = "SELECT MaTM FROM tblTHUEMAY WHERE MaMay = N'" + txtMaMay.Text + "'";
            string maTM = Function.GetFieldValues(sqlGetMaTM);

            DateTime gioRa = DateTime.Now;
            string gioRaString = gioRa.ToString("yyyy-MM-dd HH:mm:ss");
            string sqlUpdateThueMay = "UPDATE tblTHUEMAY SET GioRa = N'" + gioRaString + "' WHERE MaTM = N'" + maTM + "'";
            Class.Function.RunSQL(sqlUpdateThueMay);

            DateTime gioVao = DateTime.Parse(Function.GetFieldValues("SELECT GioVao FROM tblTHUEMAY WHERE MaMay = N'" + txtMaMay.Text + "'"));
            TimeSpan thoiGianSuDung = gioRa - gioVao;
            int gio = thoiGianSuDung.Hours;
            int phut = thoiGianSuDung.Minutes;
            int giay = thoiGianSuDung.Seconds;
            double giaThue = (gio * 60 + phut + giay / 60.0) * 10000 / 60.0;

            string sqlDichVu = "SELECT MaTP, Soluong, Dongia FROM tblDICHVU WHERE MaTM = N'" + maTM + "'";
            SystemTableData dtDichVu = Class.Function.GetDataToTable(sqlDichVu);
            double tongTienDichVu = 0;
            foreach (DataRow row in dtDichVu.Rows)
            {
                int soLuong = Convert.ToInt32(row["Soluong"]);
                double donGia = Convert.ToDouble(row["Dongia"]);
                tongTienDichVu += soLuong * donGia;
            }

            double tongTienPhaiTra = giaThue + tongTienDichVu;
            MessageBox.Show("Thời gian sử dụng: " + gio + " giờ, " + phut + " phút, " + giay + " giây\n" +
                            "Tổng tiền thuê máy: " + giaThue.ToString() + " VNĐ\n" +
                            "Tổng tiền dịch vụ: " + tongTienDichVu.ToString() + " VNĐ\n" +
                            "Tổng tiền phải trả: " + tongTienPhaiTra.ToString() + " VNĐ",
                            "Kết quả tính toán", MessageBoxButtons.OK, MessageBoxIcon.Information);

            string sqlUpdateMayTinh = "UPDATE tblMAYTINH SET TinhTrangThue = N'Chưa thuê' WHERE MaMay = N'" + txtMaMay.Text + "'";
            Class.Function.RunSQL(sqlUpdateMayTinh);

            LoadDataGridView();
            UpdateRowColors();
            MessageBox.Show("Tắt máy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void UpdateRowColors()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[2].Value.ToString() == "Chưa thuê")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }
        private void label10_Click(object sender, EventArgs e)
        {

        }
        private void txtRefresh_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            dtpGioVao.Value = dt;

            MessageBox.Show("Phần mềm đã được làm mới!");
        }
        private void cboPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "SELECT MaPhong, MaMay,TinhTrangThue FROM tblMAYTINH WHERE MaPhong = N'" + cboPhong.SelectedValue + "'";
            string sql1 = "SELECT COUNT(MaMay) FROM tblMAYTINH WHERE MaPhong = N'" + cboPhong.SelectedValue + "'"; 
           txtDemMay.Text = Function.GetFieldValues(sql1).ToString();
            tblMAYTINH = Class.Function.GetDataToTable(sql);
            dataGridView1.DataSource = tblMAYTINH;
            dataGridView1.Columns[0].HeaderText = "Mã Phòng";
            dataGridView1.Columns[1].HeaderText = "Mã Máy";
            dataGridView1.Columns[2].HeaderText = "Tình Trạng Thuê";
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView1.AllowUserToDeleteRows = false;
            UpdateRowColors();
        }
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cboMaTP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string maTM;
            string maTP;
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaTM.Focus();
                return;
            }
            if (tblDICHVU.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            maTM = dgvDichVu.CurrentRow.Cells["MaTM"].Value.ToString();

            cboMaTM.Text = Function.GetFieldValues("SELECT MaTM FROM tblTHUEMAY WHERE MaTM = N'" + maTM + "'");

            maTP = dgvDichVu.CurrentRow.Cells["MaTP"].Value.ToString();
            cboMaTP.Text = Function.GetFieldValues("SELECT TenTP FROM tblTHUCPHAM WHERE MaTP = N'" + maTP + "'");
            txtQuantity.Text = dgvDichVu.CurrentRow.Cells["Soluong"].Value.ToString();
            txtPrice.Text = dgvDichVu.CurrentRow.Cells["Dongia"].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            cboMaTM.Enabled = true;
            cboMaTM.Focus();

        }

        private void ResetValues()
        {
            cboMaTM.Text = "";
            cboMaTP.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sqlLuuDichVu;
            if (cboMaTM.Text == " ")
            {
                MessageBox.Show("Bạn chưa chọn máy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaTM.Focus();
                return;
            }
            else if (cboMaTP.Text == " ")
            {
                MessageBox.Show("Bạn chưa chọn thực phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaTP.Focus();
                return;
            }
            else if(txtQuantity.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQuantity.Focus();
                return;
            } 
            else if(txtPrice.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập đơn giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPrice.Focus();
                return;
            }    
            sqlLuuDichVu = "INSERT INTO tblDICHVU(MaTM, MaTP, Soluong, Dongia) VALUES(N'" +
cboMaTM.Text + "',N'" + cboMaTP.SelectedValue.ToString() + "',N'" + txtQuantity.Text + "',N'" + txtPrice.Text + "')";
            string sqlUpdateTP = "UPDATE tblTHUCPHAM SET Soluong = Soluong - " + txtQuantity.Text + " WHERE MaTP = N'" + cboMaTP.SelectedValue.ToString() + "'";
            Function.RunSQL(sqlUpdateTP);
            Function.RunSQL(sqlLuuDichVu);
            LoadDataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sqlSuaDichVu;
            if (cboMaTM.Text == " ")
            {
                MessageBox.Show("Bạn chưa chọn máy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaTM.Focus();
                return;
            }
            else if (cboMaTP.Text == " ")
            {
                MessageBox.Show("Bạn chưa chọn thực phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaTP.Focus();
                return;
            }
            else if (txtQuantity.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtQuantity.Focus();
                return;
            }
            else if (txtPrice.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập đơn giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPrice.Focus();
                return;
            }

            sqlSuaDichVu = "Update tblDICHVU SET MaTP = N'" + cboMaTP.SelectedValue.ToString() + "', Soluong =" + txtQuantity.Text + ", Dongia =" + txtPrice.Text + "WHERE MaTM = N'" + cboMaTM.SelectedValue.ToString() + "'";
            Class.Function.RunSQL(sqlSuaDichVu);
            LoadDataGridView();
            ResetValues();
            cboMaTP.Text = "";
            cboMaTM.Text = "";
            btnBoQua.Enabled = false;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string sqlXoaDichVu = "DELETE tblDICHVU WHERE MaTM=N'" + cboMaTM.Text + "'";
                Class.Function.RunSQL(sqlXoaDichVu);
                LoadDataGridView();
                ResetValues();
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            int tabCount = tabRoom.TabCount;
            int currentTabIndex = tabRoom.SelectedIndex;
            int nextTabIndex = (currentTabIndex + 1) % tabCount;
            tabRoom.SelectedIndex = nextTabIndex;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            cboMaTM.Enabled = false;
            btnThem.Enabled = true;
        }
        private void RefreshCboMaTM()
        {
            string sql = "SELECT MaTM FROM tblTHUEMAY";
            SystemTableData dtMaTM = Class.Function.GetDataToTable(sql);

            cboMaTM.DataSource = dtMaTM;
            cboMaTM.DisplayMember = "MaTM";
            cboMaTM.ValueMember = "MaTM";
            cboMaTM.SelectedIndex = -1;
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            RefreshCboMaTM();
            MessageBox.Show("Phần mềm đã được làm mới!");
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*SELECT tblTHUEMAY.MaTM, tblTHUEMAY.MaPhong, tblTHUEMAY.MaMay, tblTHUEMAY.TenKhach, tblTHUEMAY.GioVao, tblTHUEMAY.GioRa,tblDICHVU.MaTP, tblDICHVU.Soluong, tblDICHVU.Dongia, tblTHUEMAY.MaNV FROM tblTHUEMAY
LEFT JOIN tblDICHVU
ON tblTHUEMAY.MaTM = tblDICHVU.MaTM*/
            DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];
            string maTMHD = selectedRow.Cells["MaTM"].Value.ToString();
            string maPhongHD = selectedRow.Cells["MaPhong"].Value.ToString();
            string maMayHD = selectedRow.Cells["MaMay"].Value.ToString();
            string tenKhachHD = selectedRow.Cells["TenKhach"].Value.ToString();
            string gioVaoHD = selectedRow.Cells["GioVao"].Value.ToString();
            string gioRaHD = selectedRow.Cells["GioRa"].Value.ToString();
            string maTPHD = selectedRow.Cells["MaTP"].Value.ToString();
            string soLuongHD = selectedRow.Cells["Soluong"].Value.ToString();
            string donGiaHD = selectedRow.Cells["DonGia"].Value.ToString();
            string tenNV = selectedRow.Cells["TenNV"].Value.ToString();

            txtMaTMHD.Text = maTMHD;
            txtMaPhongHD.Text = maPhongHD;
            txtMaMayHD.Text = maMayHD;
            txtTenKhachHD.Text = tenKhachHD;
            txtGioVaoHD.Text = gioVaoHD;
            txtGioRaHD.Text = gioRaHD;
            DateTime gioVao = Convert.ToDateTime(selectedRow.Cells["GioVao"].Value);
            DateTime gioRa = Convert.ToDateTime(selectedRow.Cells["GioRa"].Value);
            TimeSpan thoiGianSuDung = gioRa - gioVao;
            int gio = thoiGianSuDung.Hours;
            int phut = thoiGianSuDung.Minutes;
            int giay = thoiGianSuDung.Seconds;
            string thoiGianChoi = $"{gio} giờ, {phut} phút, {giay} giây";
            txtTongGioChoiHD.Text = thoiGianChoi.ToString();
            cboMaTPHD.Text = maTPHD.ToString();
            txtSoLuongHD.Text = soLuongHD;
            txtDonGiaHD.Text = donGiaHD;
            double tongSoPhut = thoiGianSuDung.TotalMinutes;
            double thanhTien = Math.Round(tongSoPhut / 60 * 10000, 0);

            txtThanhTienGioChoi.Text = thanhTien.ToString("N0") + " VNĐ";

            double soLuong = string.IsNullOrEmpty(soLuongHD) ? 0 : Convert.ToDouble(soLuongHD);
            double donGia = string.IsNullOrEmpty(donGiaHD) ? 0 : Convert.ToDouble(donGiaHD);
            double tongTienDichVu = Math.Round(soLuong * donGia, 0);
            txtThanhTienDichVu.Text = tongTienDichVu.ToString("N0") + " VNĐ";

            //Tổng tiền hóa đơn
            double thanhTienGioChoi = Convert.ToDouble(txtThanhTienGioChoi.Text.Replace(" VNĐ", "").Trim());
            double thanhTienDichVu = Convert.ToDouble(txtThanhTienDichVu.Text.Replace(" VNĐ", "").Trim());
            double tongTien = Math.Round(thanhTienGioChoi + thanhTienDichVu, 0);
            txtTongTienHoaDon.Text = tongTien.ToString("N0") + " VNĐ";
            //Tổng tiền bằng chữ
            string soTienBangChu;
            string input = txtTongTienHoaDon.Text.Replace(" VNĐ", "");
            string numericString = new string(input.Where(char.IsDigit).ToArray());
            numericString = numericString.Split('.')[0];
            double tongTienHD = Math.Round(thanhTien, 0);
            if (double.TryParse(numericString, out tongTienHD))
            {
                soTienBangChu = Function.ChuyenSoSangChu(tongTienHD.ToString("N0"));
                lblTien.Text = soTienBangChu;
            }
            else
            {
                lblTien.Text = "Không thể chuyển đổi số tiền thành chữ.";
            }


        }


        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void txtMaTMHD_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hàng để xuất ra Excel.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string sqlHoaDonDichVu = "SELECT MaTP, SoLuong, DonGia FROM TBLDICHVU WHERE MaTM = '" + txtMaTMHD.Text + "'";
            SystemTableData dataTable = Function.GetDataToTable(sqlHoaDonDichVu);

            DataGridViewRow selectedRow = dataGridView2.SelectedRows[0];
            string maTMHD = selectedRow.Cells["MaTM"].Value.ToString();
            string maPhongHD = selectedRow.Cells["MaPhong"].Value.ToString();
            string maMayHD = selectedRow.Cells["MaMay"].Value.ToString();
            string tenKhachHD = selectedRow.Cells["TenKhach"].Value.ToString();
            string gioVaoHD = selectedRow.Cells["GioVao"].Value.ToString();
            string gioRaHD = selectedRow.Cells["GioRa"].Value.ToString();
            string maTPHD = selectedRow.Cells["MaTP"].Value.ToString();
            string soLuongHD = selectedRow.Cells["Soluong"].Value.ToString();
            string donGiaHD = selectedRow.Cells["DonGia"].Value.ToString();
            string tenNV = selectedRow.Cells["TenNV"].Value.ToString();
            string tongTienHD = txtTongTienHoaDon.Text;
        
            string tongTienBangChu = lblTien.Text;
            DateTime gioVao = Convert.ToDateTime(selectedRow.Cells["GioVao"].Value);
            DateTime gioRa = Convert.ToDateTime(selectedRow.Cells["GioRa"].Value);
            TimeSpan thoiGianSuDung = gioRa - gioVao;
            int gio = thoiGianSuDung.Hours;
            int phut = thoiGianSuDung.Minutes;
            int giay = thoiGianSuDung.Seconds;
            string thoiGianChoi = $"{gio} giờ, {phut} phút, {giay} giây";
            txtTongGioChoiHD.Text = thoiGianChoi.ToString();
            cboMaTPHD.Text = maTPHD.ToString();
            txtSoLuongHD.Text = soLuongHD;
            txtDonGiaHD.Text = donGiaHD;
            double tongSoPhut = thoiGianSuDung.TotalMinutes;
            double thanhTien = Math.Round(tongSoPhut / 60 * 10000, 0);
            txtThanhTienGioChoi.Text = thanhTien.ToString("N0") + " VNĐ";
            double tongTienHDExcel = Math.Round(thanhTien, 0);
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook;
            COMExcel.Worksheet exSheet;
            COMExcel.Range exRange;

            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];

            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Name = "Times New Roman";
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 1; // Màu vàng
            exRange.Range["A1:A1"].ColumnWidth = 10;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "NÉT NÈN NẸT";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "12 Chùa Bộc - Đống Đa";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: (1900)1000";
            exRange.Range["A3:B3"].Font.Size = 10;


            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Name = "Times New Roman";
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 1; // Màu đen
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN BÁN";

            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:C9"].Font.Name = "Times new roman";
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = maTMHD;
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tenKhachHD;
            exRange.Range["B8:B8"].Value = "Mã phòng:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = maPhongHD;
            exRange.Range["B9:B9"].Value = "Thuê máy:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = maMayHD;


            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên hàng";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Giảm giá";
            exRange.Range["F11:F11"].Value = "Thành tiền";

            int currentRow = 12;

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow dataRow = dataTable.Rows[i];
                string maTP = dataRow["MaTP"].ToString();
                string soLuong = dataRow["SoLuong"].ToString();
                string donGia = dataRow["DonGia"].ToString();

                double thanhTienDV = double.Parse(soLuong) * double.Parse(donGia);
                tongTienHDExcel += thanhTienDV;

                exSheet.Cells[currentRow, 1] = i + 1; // Số thứ tự dịch vụ
                exSheet.Cells[currentRow, 2] = maTP; // Mã dịch vụ
                exSheet.Cells[currentRow, 3] = soLuong; // Số lượng
                exSheet.Cells[currentRow, 4] = donGia; // Đơn giá
                exSheet.Cells[currentRow, 5] = "0"; // Giảm giá (mặc định là 0)
                exSheet.Cells[currentRow, 6] = thanhTienDV; // Thành tiền cho dịch vụ
                currentRow++;
            }



            exSheet.Cells[currentRow, 1] = currentRow - 11;
            exSheet.Cells[currentRow, 2] = "Giờ chơi";
            exSheet.Cells[currentRow, 3] = thoiGianChoi;
            exSheet.Cells[currentRow, 4] = "10,000 VNĐ";
            exSheet.Cells[currentRow, 5] = "0";
            exSheet.Cells[currentRow, 6] = thanhTien;


            int lastRow = 13;
            exRange = exSheet.Cells[lastRow + 1, 5];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[lastRow + 1, 6];
            exRange.Font.Bold = true;
            exRange.Value2 = tongTienHDExcel;

            exRange = exSheet.Cells[lastRow + 2, 1];
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + tongTienBangChu;



            exRange = exSheet.Cells[lastRow + 4, 4];
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = DateTime.Now;
            exRange.Range["A1:C1"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tenNV;

            exSheet.Name = "Hóa đơn nhập";
            exApp.Visible = true;
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sqlSearch = "SELECT tblPHIEUNHAPHANG.MaPN, tblPHIEUNHAPHANG.Soluong, tblPHIEUNHAPHANG.Dongianhap, tblPHIEUNHAPHANG.Ngaynhap, tblTHUCPHAM.TenTP " +
                               "FROM tblPHIEUNHAPHANG " +
                               "LEFT JOIN tblTHUCPHAM ON tblPHIEUNHAPHANG.MaTP = tblTHUCPHAM.MaTP " +
                        "WHERE 1=1";

            List<SqlParameter> parameters = new List<SqlParameter>();
            Console.WriteLine($"Date From: {masTextBoxDateFrom.Text.Trim()}");
            Console.WriteLine($"Date To: {masTextBoxDateTo.Text.Trim()}");
            if (masTextBoxDateFrom.Text.Trim() != "00/00/0000" && masTextBoxDateTo.Text.Trim() != "00/00/0000")
            {
                DateTime dateFrom;
                DateTime dateTo;

                if (DateTime.TryParse(masTextBoxDateFrom.Text.Trim(), out dateFrom) && DateTime.TryParse(masTextBoxDateTo.Text.Trim(), out dateTo))
                {
                    dateFrom = dateFrom.Date;
                    dateTo = dateTo.Date;

                    sqlSearch += " AND CAST(tblPHIEUNHAPHANG.Ngaynhap AS DATE) >= @dateFrom AND CAST(tblPHIEUNHAPHANG.Ngaynhap AS DATE) <= @dateTo";
                    parameters.Add(new SqlParameter("@dateFrom", dateFrom));
                    parameters.Add(new SqlParameter("@dateTo", dateTo));
                }

            }


            SystemTableData dataTable = Function.GetDataToTableRange(sqlSearch, parameters.ToArray());
            dataGridView4.DataSource = dataTable;

            dataGridView4.Columns[0].HeaderText = "Mã phiếu nhập";
            dataGridView4.Columns[1].HeaderText = "Số lượng";
            dataGridView4.Columns[2].HeaderText = "Đơn giá nhập";
            dataGridView4.Columns[3].HeaderText = "Ngày nhập";
            dataGridView4.Columns[4].HeaderText = "Tên thực phẩm";

            dataGridView4.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView4.AllowUserToAddRows = false;
            dataGridView4.AllowUserToDeleteRows = false;

            double totalMoney = 0;
            int totalQuantity = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                int quantity = Convert.ToInt32(row["Soluong"]);
                double price = Convert.ToDouble(row["Dongianhap"]);
                totalQuantity += quantity;
                totalMoney += quantity * price;
            }

            DataRow rowTotal = dataTable.NewRow();
            rowTotal["MaPN"] = "Tổng";
            rowTotal["Soluong"] = totalQuantity;
            rowTotal["Dongianhap"] = totalMoney;
            dataTable.Rows.Add(rowTotal);

            if (cboBaoCaoThuChi.SelectedIndex == 0)
            {
                dataGridView4.Visible = true;
                dataGridView5.Visible = false;
                dataGridView6.Visible = false;
                lblTongTienDichVu.Visible = false;
                lblTongTienThueMay.Visible = false;
                label30.Visible = false;
                label32.Visible = false;
            }
            else if (cboBaoCaoThuChi.SelectedIndex == 1)
            {
                dataGridView4.Visible = false;
                dataGridView5.Visible = true;
                dataGridView6.Visible = false;
                lblTongTienDichVu.Visible = true;
                lblTongTienThueMay.Visible = true;
                label30.Visible = true;
                label32.Visible = true;
            }
            else
            {
                dataGridView4.Visible = false;
                dataGridView5.Visible = false;
                dataGridView6.Visible = true;
                lblTongTienDichVu.Visible = false;
                lblTongTienThueMay.Visible = false;
                label30.Visible = false;
                label32.Visible = false;
            }

            //Tổng thu: 

            string sqlTongThu = "SELECT tblTHUEMAY.MaTM, tblTHUEMAY.MaPhong, tblTHUEMAY.MaMay, tblTHUEMAY.TenKhach, tblTHUEMAY.GioVao, tblTHUEMAY.GioRa, tblDICHVU.MaTP, tblDICHVU.Soluong, tblDICHVU.Dongia, tblTHUEMAY.MaNV " +
                    "FROM tblTHUEMAY " +
                    "LEFT JOIN tblDICHVU ON tblTHUEMAY.MaTM = tblDICHVU.MaTM " +
                        "WHERE 1=1";

            List<SqlParameter> parametersTongThu = new List<SqlParameter>();

            if (masTextBoxDateFrom.Text.Trim() != "00/00/0000" && masTextBoxDateTo.Text.Trim() != "00/00/0000")
            {
                DateTime dateFrom;
                DateTime dateTo;

                if (DateTime.TryParse(masTextBoxDateFrom.Text.Trim(), out dateFrom) && DateTime.TryParse(masTextBoxDateTo.Text.Trim(), out dateTo))
                {
                    dateFrom = dateFrom.Date;
                    dateTo = dateTo.Date;

                    sqlTongThu += " AND CAST(tblTHUEMAY.GioVao AS DATE) >= @dateFrom AND CAST(tblTHUEMAY.GioVao AS DATE) <= @dateTo";
                    parametersTongThu.Add(new SqlParameter("@dateFrom", dateFrom));
                    parametersTongThu.Add(new SqlParameter("@dateTo", dateTo));
                }

            }

            SystemTableData dataTableTongThu = Function.GetDataToTableRange(sqlTongThu, parametersTongThu.ToArray());
            dataGridView5.DataSource = dataTableTongThu;

            dataGridView5.Columns[0].HeaderText = "Mã phiếu thu";
            dataGridView5.Columns[1].HeaderText = "Mã phòng";
            dataGridView5.Columns[2].HeaderText = "Mã máy";
            dataGridView5.Columns[3].HeaderText = "Tên khách";
            dataGridView5.Columns[4].HeaderText = "Giờ vào";
            dataGridView5.Columns[5].HeaderText = "Giờ ra";
            dataGridView5.Columns[6].HeaderText = "Mã thực phẩm";
            dataGridView5.Columns[7].HeaderText = "Số lượng";
            dataGridView5.Columns[8].HeaderText = "Đơn giá";
            dataGridView5.Columns[9].HeaderText = "Mã nhân viên";

            dataGridView5.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView5.AllowUserToAddRows = false;
            dataGridView5.AllowUserToDeleteRows = false;


            double totalTienThueMay = 0;
            double totalTienDichVu = 0;
            double? dongiaDichVu = null;
            int? soLuongThucPham = null;
            foreach (DataRow row in dataTableTongThu.Rows)
            {
                DateTime gioVao = Convert.ToDateTime(row["GioVao"]);
                DateTime gioRa = Convert.ToDateTime(row["GioRa"]);
                if (row["Dongia"] != DBNull.Value)
                {
                    dongiaDichVu = Convert.ToDouble(row["Dongia"]);
                }

                if (row["Soluong"] != DBNull.Value)
                {
                    soLuongThucPham = Convert.ToInt32(row["Soluong"]);
                }
                TimeSpan thoiGianThue = gioRa - gioVao;
                double hours = thoiGianThue.TotalHours;
                double tienThueMay = Math.Ceiling(hours) * 10000; 
                totalTienThueMay += tienThueMay;
                if (soLuongThucPham != null && dongiaDichVu != null)
                {
                    tienThueMay = soLuongThucPham.Value * 10000;
                    totalTienThueMay += Math.Round(tienThueMay, 0);
                    double tienDichVu = soLuongThucPham.Value * dongiaDichVu.Value;
                    totalTienDichVu += Math.Round(tienDichVu, 0);
                }

                if (soLuongThucPham != null)
                {
                    tienThueMay = soLuongThucPham.Value * 10000;
                    totalTienThueMay += Math.Round(tienThueMay, 0);
                }

            }
            lblTongTienThueMay.Text = totalTienThueMay.ToString("N0") + " VNĐ";

            lblTongTienDichVu.Text = totalTienDichVu.ToString("N0")+" VNĐ";



            //Trả lương nhân viên
            string sqlLuong = "SELECT tblTRALUONG.MaLuong, tblNHANVIEN.TenNV, tblTRALUONG.TienLuong, tblTRALUONG.NgayTraLuong " +
                        "FROM tblTRALUONG " +
                        "LEFT JOIN tblNHANVIEN ON tblTRALUONG.MaNV = tblNHANVIEN.MaNV " +
                        "WHERE 1=1";

            List<SqlParameter> parametersSalary = new List<SqlParameter>();

            if (masTextBoxDateFrom.Text.Trim() != "00/00/0000" && masTextBoxDateTo.Text.Trim() != "00/00/0000")
            {
                DateTime dateFrom;
                DateTime dateTo;

                if (DateTime.TryParse(masTextBoxDateFrom.Text.Trim(), out dateFrom) && DateTime.TryParse(masTextBoxDateTo.Text.Trim(), out dateTo))
                {
                    dateFrom = dateFrom.Date;
                    dateTo = dateTo.Date;

                    sqlLuong += " AND CAST(tblTRALUONG.NgayTraLuong AS DATE) >= @dateFrom AND CAST(tblTRALUONG.NgayTraLuong AS DATE) <= @dateTo";
                    parametersSalary.Add(new SqlParameter("@dateFrom", dateFrom));
                    parametersSalary.Add(new SqlParameter("@dateTo", dateTo));
                }
            }

            SystemTableData dataTableSalary = Function.GetDataToTableRange(sqlLuong, parametersSalary.ToArray());

            dataGridView6.DataSource = dataTableSalary;
            dataGridView6.Columns[0].HeaderText = "Mã Lương";
            dataGridView6.Columns[1].HeaderText = "Tên Nhân Viên";
            dataGridView6.Columns[2].HeaderText = "Tiền Lương";
            dataGridView6.Columns[3].HeaderText = "Ngày Trả Lương";

            dataGridView6.EditMode = DataGridViewEditMode.EditProgrammatically;
            dataGridView6.AllowUserToAddRows = false;
            dataGridView6.AllowUserToDeleteRows = false;

            double totalLuong = 0;
            foreach (DataRow row in dataTableSalary.Rows)
            {
                double Luong = Convert.ToDouble(row["TienLuong"]);
                totalLuong += Luong;
            }

            DataRow rowTotalLuong = dataTableSalary.NewRow();
            rowTotalLuong["MaLuong"] = "Tổng";
            rowTotalLuong["TienLuong"] = totalLuong;
            dataTableSalary.Rows.Add(rowTotalLuong);
        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void btnBaoTri_Click(object sender, EventArgs e)
        {
            frmBAOTRI frmBAOTRI = new frmBAOTRI();
            frmBAOTRI.Show();
        }

        private void btnQuanLyMay_Click(object sender, EventArgs e)
        {
            frmQUANLYPHONG frmQUANLYPHONG = new frmQUANLYPHONG();
            frmQUANLYPHONG.Show();
        }

        private void btnNHANVIEN_Click(object sender, EventArgs e)
        {
            frmNHANVIEN frmNHANVIEN = new frmNHANVIEN();
            frmNHANVIEN.Show();
        }

        private void btnNHAPHANG_Click(object sender, EventArgs e)
        {
            PHIEUNHAPHANG phieuNhapHang = new PHIEUNHAPHANG();
            phieuNhapHang.Show();
        }

        private void btnSearchHoaDon_Click(object sender, EventArgs e)
        {
            TIMHOADON timHoaDon = new TIMHOADON();
            timHoaDon.Show();
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            TRALUONG traLuong = new TRALUONG();
            traLuong.Show();
        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void btnHuyHoaDon_Click(object sender, EventArgs e)
        {

        }
    }
}

