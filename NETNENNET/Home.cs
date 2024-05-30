using NETNENNET.Class;
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
using COMExcel = Microsoft.Office.Interop.Excel;

    // ben ref add outlook thi ben nay add excel lsao duoc. nhin ne, ơ rõ ràng là..., à  nhầm thật :<
namespace NETNENNET
{

    public partial class Home : Form
    {
        public string CustomFormat { get; set; }
        DataTable tblMAYTINH;
        DataTable tblDICHVU;
        DataTable HoaDon;

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
            Function.FillCombo("SELECT MaNV, TenNV FROM tblNHANVIEN",
cboNhanVien, "MaNV", "TenNV");
            cboNhanVien.SelectedIndex = -1;

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
            string sqlHoaDon = "SELECT tblTHUEMAY.MaTM, tblTHUEMAY.MaPhong, tblTHUEMAY.MaMay, tblTHUEMAY.TenKhach, tblTHUEMAY.GioVao, tblTHUEMAY.GioRa,tblDICHVU.MaTP, tblDICHVU.Soluong, tblDICHVU.Dongia, tblTHUEMAY.MaNV FROM tblTHUEMAY\r\nLEFT JOIN tblDICHVU\r\nON tblTHUEMAY.MaTM = tblDICHVU.MaTM ";
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
            dataGridView2.Columns[9].HeaderText = "Mã nhân viên";
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.EditMode = DataGridViewEditMode.EditProgrammatically;


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

            DateTime gioVao = DateTime.Now;
            string gioVaoString = gioVao.ToString("yyyy-MM-dd HH:mm:ss");
            string ngayThueString = gioVao.ToString("yyyy-MM-dd");

            // Thêm giá trị mặc định cho GioRa
            DateTime gioRaMacDinh = gioVao.AddMinutes(1); // ví dụ: thời gian mặc định là 1 phút sau giờ vào
            string gioRaMacDinhString = gioRaMacDinh.ToString("yyyy-MM-dd HH:mm:ss");

            string sqlInsert = "INSERT INTO tblTHUEMAY(MaPhong, MaMay, TenKhach, NgayThue, GioVao, GioRa, MaNV) VALUES (N'" + txtMaPhong.Text + "', N'" + txtMaMay.Text + "', N'" + txtTenKhachHang.Text + "', N'" + ngayThueString + "', N'" + gioVaoString + "', N'" + gioRaMacDinhString + "', N'" + cboNhanVien.SelectedValue + "')";
            string sqlUpdate = "UPDATE tblMAYTINH SET TinhTrangThue = N'Đang thuê' WHERE MaMay = N'" + txtMaMay.Text + "'";

            Class.Function.RunSQL(sqlInsert);
            Class.Function.RunSQL(sqlUpdate);

            LoadDataGridView();
            UpdateRowColors();
            MessageBox.Show("Mở máy thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void txtNgayThue_TextChanged(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "SELECT MaNV, TenNV FROM tblNHANVIEN";
            Function.FillCombo(sql, cboNhanVien, "MaNV", "TenNV");
            cboNhanVien.SelectedIndex = 0;
        }
        private void btnTatMay_Click(object sender, EventArgs e)
        {
            if (txtMaMay.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn máy tính nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string maMay = txtMaMay.Text;

            string sqlGetMaTM = "SELECT MaTM FROM tblTHUEMAY WHERE MaMay = N'" + maMay + "'";
            string maTM = Function.GetFieldValues(sqlGetMaTM);

            DateTime gioRa = DateTime.Now;
            string gioRaString = gioRa.ToString("yyyy-MM-dd HH:mm:ss");

            // Cập nhật thời gian ra cho máy
            string sqlUpdateThueMay = "UPDATE tblTHUEMAY SET GioRa = N'" + gioRaString + "' WHERE MaMay = N'" + txtMaMay.Text + "'";
            Class.Function.RunSQL(sqlUpdateThueMay);

            // Lấy thời gian vào từ cơ sở dữ liệu
            DateTime gioVao = DateTime.Parse(Function.GetFieldValues("SELECT GioVao FROM tblTHUEMAY WHERE MaMay = N'" + txtMaMay.Text + "'"));
            TimeSpan thoiGianSuDung = gioRa - gioVao;


            //Lấy thông tin dịch vụ máy đó đã sử dụng

            int gio = thoiGianSuDung.Hours;
            int phut = thoiGianSuDung.Minutes;
            int giay = thoiGianSuDung.Seconds;


            double giaThue = (gio * 60 + phut + giay / 60.0) * 10000 / 60.0;
            string sqlDichVu = "SELECT MaTP, Soluong, Dongia FROM tblDICHVU WHERE MaTM = N'" + maTM + "'";
            DataTable dtDichVu = Class.Function.GetDataToTable(sqlDichVu);

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
            } if (tblDICHVU.Rows.Count == 0)
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

        private void ResetValues ()
        {
            cboMaTM.Text="";
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
            } else if (cboMaTP.Text == " ")
            {
                MessageBox.Show("Bạn chưa chọn thực phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaTP.Focus();
                return;
            } 
            sqlLuuDichVu = "INSERT INTO tblDICHVU(MaTM, MaTP, Soluong, Dongia) VALUES(N'" +
cboMaTM.Text + "',N'" + cboMaTP.SelectedValue.ToString() + "',N'" + txtQuantity.Text + "',N'" + txtPrice.Text +"')";
            Class.Function.RunSQL(sqlLuuDichVu);
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

            sqlSuaDichVu = "Update tblDICHVU SET MaTM = N'" + cboMaTM.SelectedValue.ToString() + "', MaTP = N'" + cboMaTP.SelectedValue.ToString() + "', Soluong =" + txtQuantity.Text + ", Dongia =" + txtPrice.Text;
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
        }
        private void RefreshCboMaTM()
        {
            string sql = "SELECT MaTM FROM tblTHUEMAY"; 
            DataTable dtMaTM = Class.Function.GetDataToTable(sql);

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
            string maNV = selectedRow.Cells["MaNV"].Value.ToString();

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

            txtThanhTienGioChoi.Text = thanhTien.ToString() + " VNĐ";

            double soLuong = string.IsNullOrEmpty(soLuongHD) ? 0 : Convert.ToDouble(soLuongHD);
            double donGia = string.IsNullOrEmpty(donGiaHD) ? 0 : Convert.ToDouble(donGiaHD);
            double tongTienDichVu = Math.Round(soLuong * donGia, 0);
            txtThanhTienDichVu.Text = tongTienDichVu.ToString() + " VNĐ";

            //Tổng tiền hóa đơn
            double thanhTienGioChoi = Convert.ToDouble(txtThanhTienGioChoi.Text.Replace(" VNĐ", "").Trim());
            double thanhTienDichVu = Convert.ToDouble(txtThanhTienDichVu.Text.Replace(" VNĐ", "").Trim());
            double tongTien = Math.Round(thanhTienGioChoi + thanhTienDichVu, 0);
            txtTongTienHoaDon.Text = tongTien.ToString() + " VNĐ";
            //Tổng tiền bằng chữ
            string soTienBangChu;
            string input = txtTongTienHoaDon.Text.Replace(" VNĐ", "");
            string numericString = new string(input.Where(char.IsDigit).ToArray()); 
            numericString = numericString.Split('.')[0];
            double tongTienHD = Math.Round(thanhTien, 0);
            if (double.TryParse(numericString, out tongTienHD))
            {
                soTienBangChu = Function.ChuyenSoSangChu(tongTienHD.ToString());
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
            DataTable dataTable = Function.GetDataToTable(sqlHoaDonDichVu);
 
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
            string maNV = selectedRow.Cells["MaNV"].Value.ToString();
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
            txtThanhTienGioChoi.Text = thanhTien.ToString() + " VNĐ";
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
            exRange.Range["A1:B3"].Font.ColorIndex = 6; // Màu vàng
            exRange.Range["A1:A1"].ColumnWidth = 10;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Net chị Thanh";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Hà Đông - Hà Nội";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: (1900)1000";
            exRange.Range["A3:B3"].Font.Size = 10;


            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Name = "Times New Roman";
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; // Màu đỏ
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
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = maPhongHD;
            exRange.Range["B9:B9"].Value = "Điện thoại:";
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
            exSheet.Cells[currentRow, 4] = "10000";
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
            exRange.Range["A6:C6"].Value = maNV;

            exSheet.Name = "Hóa đơn nhập";
            exApp.Visible = true;
        }
    }
}

// Trước khi xuất excel thì nhớ chọn cho nó hiện lên các textbox đã nhớ, rồi hẵng xuất :<, phải xử lý hơi nông dân 1 tí để nó chạy đc đã :<
// cái hóa đơn 10, có hai thực phẩm nhá
// bạn viết cho tôi 1 câu sql truy vấn đi, truy vấn 1 khách hàng sử dụng bao nhiêu dịch vụ đi
//Xong từ cái ý sẽ lấy ra đc
//viet vao dau
//ban đầu đang veeieets ntn
//Bắt bbuoojc phải  Trước khi xuất excel thì nhớ chọn cho nó hiện lên các textbox đã nhớ, rồi hẵng xuất :<, phải xử lý hơi nông dân 1 tí để nó chạy đc đã :<
//Nhóeeeeeee
 // máy móc cũng cần chữa lành á :))
 //Chứ sap =)), cx có trái tym mà, cũng biết tan vỡ mà 💔💔💔
 // nhin ne, khong vao duoc cái window security =))) Win lỏd ruii, khả năng win custom nên bị tắt ahhahaha
 //Tắt đi khởi động lại là ok bạn ạ
 // that khong, thử đi, tắt máy đi mở lại xemmmmmm
