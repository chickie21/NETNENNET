using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NETNENNET.Class;

namespace NETNENNET
{
    public partial class frmNHANVIEN : Form
    {
        public frmNHANVIEN()
        {
            InitializeComponent();
        }
        DataTable tblNV;
        private void frmNHANVIEN_Load(object sender, EventArgs e)
        {
            Function.Connect();
            txtManhanvien.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DataGridView();

        }
        private void Load_DataGridView()
        {
            string sql;
            sql = "select Manv, tennv, namsinh, gioitinh, diachi, dienthoai from tblnhanvien";
            tblNV = Class.Function.GetDataToTable(sql);
            dgvNhanVien.DataSource = tblNV;
            dgvNhanVien.Columns[0].HeaderText = "Mã nhân viên";
            dgvNhanVien.Columns[1].HeaderText = "Tên nhân viên";
            dgvNhanVien.Columns[2].HeaderText = "Năm sinh";
            dgvNhanVien.Columns[3].HeaderText = "Giới tính";
            dgvNhanVien.Columns[4].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns[5].HeaderText = "Điện thoại";
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvNhanVien_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtManhanvien.Focus();
                return;
            }
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtManhanvien.Text = dgvNhanVien.CurrentRow.Cells["manv"].Value.ToString();
            txtTennhanvien.Text = dgvNhanVien.CurrentRow.Cells["Tennv"].Value.ToString();
            txtDiachi.Text = dgvNhanVien.CurrentRow.Cells["Diachi"].Value.ToString();
            txtDienthoai.Text = dgvNhanVien.CurrentRow.Cells["Dienthoai"].Value.ToString();
            txtNamsinh.Text = dgvNhanVien.CurrentRow.Cells["Namsinh"].Value.ToString();
            txtGioitinh.Text = dgvNhanVien.CurrentRow.Cells["gioitinh"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtManhanvien.Enabled = true;
            txtManhanvien.Focus();

        }
        private void ResetValues()
        {
            txtManhanvien.Text = "";
            txtTennhanvien.Text = "";
            txtGioitinh.Text = "";
            txtDiachi.Text = "";
            txtNamsinh.Text = "";
            txtDienthoai.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtManhanvien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtManhanvien.Focus();
                return;
            }
            if (txtTennhanvien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTennhanvien.Focus();
                return;
            }
            if (txtDiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiachi.Focus();
                return;
            }
            if (txtDienthoai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienthoai.Focus();
                return;
            }
            if (txtNamsinh.Text == "")
            {
                MessageBox.Show("Bạn phải nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamsinh.Focus();
                return;
            }
            //if (!Function.IsDate(txtNamsinh.Text))
            //{
            //    MessageBox.Show("Bạn phải nhập lại năm sinh", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtNamsinh.Text = "";
            //    txtNamsinh.Focus();
            //    return;
            //}
            if (txtGioitinh.Text == "")
            {
                MessageBox.Show("Bạn phải nhập giới tính", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGioitinh.Focus();
                return;
            }
            sql = "SELECT Manv FROM tblNhanvien WHERE Manv=N'" + txtManhanvien.Text.Trim() + "'";
            if (Function.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtManhanvien.Focus();
                txtManhanvien.Text = "";
                return;
            }
            sql = "INSERT INTO tblNhanvien(Manv,Tennv,Gioitinh, Diachi, Dienthoai, Namsinh) VALUES(N'" + txtManhanvien.Text.Trim() + "', N'" + txtTennhanvien.Text.Trim() + "', N'" + txtGioitinh.Text.Trim() + "', N'" + txtDiachi.Text.Trim() + "', '" + txtDienthoai.Text + "', '" + txtNamsinh.Text/*Function.ConvertDateTime(txtNamsinh.Text)*/ + "')";
            Function.RunSQL(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtManhanvien.Enabled = false;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtManhanvien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTennhanvien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTennhanvien.Focus();
                return;
            }
            if (txtDiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiachi.Focus();
                return;
            }
            if (txtDienthoai.Text == "")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienthoai.Focus();
                return;
            }
            if (txtNamsinh.Text == "")
            {
                MessageBox.Show("Bạn phải nhập năm sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNamsinh.Focus();
                return;
            }
            //            if (!Functions.IsDate(mskNgaysinh.Text))
            //            {
            //                MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo",
            //MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                mskNgaysinh.Text = "";
            //                mskNgaysinh.Focus();
            //                return;
            //            }
            //            if (chkGioitinh.Checked == true)
            //                gt = "Nam";
            //            else
            //                gt = "Nữ";
            sql = "UPDATE tblNhanvien SET Tennv=N'" +
                    txtTennhanvien.Text.Trim().ToString() +
                    "',Diachi=N'" + txtDiachi.Text.Trim().ToString() +
                    "',Dienthoai='" + txtDienthoai.Text + "',Gioitinh=N'" + txtGioitinh.Text.Trim().ToString() +
                    "',Namsinh='" + txtNamsinh.Text +
                    "' WHERE MaNV=N'" + txtManhanvien.Text + "'";
            Function.RunSQL(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtManhanvien.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblNhanvien WHERE Manv=N'" + txtManhanvien.Text + "'";
                Function.RunSQL(sql);
                Load_DataGridView();
                ResetValues();
            }

        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtManhanvien.Enabled = false;

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNamsinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (e.KeyChar == '-') ||
                (e.KeyChar == '.') || (Convert.ToInt32(e.KeyChar) == 8) || (Convert.ToInt32(e.KeyChar) == 13))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtDienthoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (e.KeyChar == '-') ||
                (e.KeyChar == '.') || (Convert.ToInt32(e.KeyChar) == 8) || (Convert.ToInt32(e.KeyChar) == 13))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtTennhanvien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (e.KeyChar == '-') ||
               (e.KeyChar == '.'))
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void txtGioitinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (e.KeyChar == '-') ||
               (e.KeyChar == '.'))
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            txtManhanvien.Enabled = true;
            string sql;
            if ((txtManhanvien.Text == "") && (txtTennhanvien.Text == "") && (txtGioitinh.Text == "") && (txtDiachi.Text == "") && (txtDienthoai.Text == "") && (txtNamsinh.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM tblNhanvien WHERE 1=1";
            if (txtManhanvien.Text != "")
                sql = sql + " AND maNV Like N'%" + txtManhanvien.Text + "%'";
            if (txtTennhanvien.Text != "")
                sql = sql + " AND tennv Like N'%" + txtTennhanvien.Text + "%'";
            if (txtNamsinh.Text != "")
                sql = sql + " AND namsinh Like N'%" + txtNamsinh.Text + "%'";
            if (txtDiachi.Text != "")
                sql = sql + " AND diachi Like N'%" + txtDiachi.Text + "%'";
            if (txtDienthoai.Text != "")
                sql = sql + " AND dienthoai Like N'%" + txtDienthoai.Text + "%'";
            if (txtGioitinh.Text != "")
                sql = sql + " AND gioitinh Like N'%" + txtGioitinh.Text + "%'";

            tblNV = Function.GetDataToTable(sql);
            if (tblNV.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Có " + tblNV.Rows.Count + " bản ghi thỏa mãn điều kiện!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            dgvNhanVien.DataSource = tblNV;
            ResetValues();
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT * FROM tblnhanvien";
            tblNV = Function.GetDataToTable(sql);
            dgvNhanVien.DataSource = tblNV;
        }
    }
}