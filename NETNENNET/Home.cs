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

namespace NETNENNET
{

    public partial class Home : Form
    {
        public string CustomFormat { get; set; }
        DataTable tblMAYTINH;
        DataTable tblDICHVU;

        public Home()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Class.Function.Connect();
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

            DateTime gioRa = DateTime.Now;
            string gioRaString = gioRa.ToString("yyyy-MM-dd HH:mm:ss");

            // Cập nhật thời gian ra cho máy
            string sqlUpdateThueMay = "UPDATE tblTHUEMAY SET GioRa = N'" + gioRaString + "' WHERE MaMay = N'" + txtMaMay.Text + "'";
            Class.Function.RunSQL(sqlUpdateThueMay);

            // Lấy thời gian vào từ cơ sở dữ liệu
            DateTime gioVao = DateTime.Parse(Function.GetFieldValues("SELECT GioVao FROM tblTHUEMAY WHERE MaMay = N'" + txtMaMay.Text + "'"));
            TimeSpan thoiGianSuDung = gioRa - gioVao;

            int gio = thoiGianSuDung.Hours;
            int phut = thoiGianSuDung.Minutes;
            int giay = thoiGianSuDung.Seconds;

            double giaThue = (gio * 60 + phut + giay / 60.0) * 10000 / 60.0;

            MessageBox.Show("Thời gian sử dụng: " + gio + " giờ, " + phut + " phút, " + giay + " giây\n" +
                            "Giá thuê: " + giaThue.ToString("N2") + " VNĐ", "Kết quả tính toán", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Cập nhật trạng thái máy tính
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
    }
}


