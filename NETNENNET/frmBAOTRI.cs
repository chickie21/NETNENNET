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
    public partial class frmBAOTRI : Form
    {
        public frmBAOTRI()
        {
            InitializeComponent();
        }

        DataTable tblbt;
        private void frmBAOTRI_Load(object sender, EventArgs e)
        {
            Function.Connect();
            txtMabaotri.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DataGridView();
            Function.FillCombo("SELECT Manbt, Tennbt FROM tblnhabaotri", cboMaNBT, "Manbt", "Tennbt");
            cboMaNBT.SelectedIndex = -1;
            Function.FillCombo2("SELECT Mamay FROM tblmaytinh", cboMamay, "Mamay");
            cboMaNBT.SelectedIndex = -1;
            ResetValues();

        }
        private void ResetValues()
        {
            txtMabaotri.Text = "";
            cboMaNBT.Text = "";
            cboMamay.Text = "";
            txtNguyennhan.Text = "";
            txtGiaiphap.Text = "";
            txtThanhtien.Text = "";
            mskNgayBT.Text = "  /  /";
        }

        private void Load_DataGridView()
        {
            string sql;
            sql = "select mabaotri, manbt, mamay, nguyennhan, giaiphap, thanhtien, ngaybt from tblBaotri";
            tblbt = Class.Function.GetDataToTable(sql);
            dgvBaoTri.DataSource = tblbt;
            dgvBaoTri.Columns[0].HeaderText = "Mã bảo trì";
            dgvBaoTri.Columns[1].HeaderText = "Mã nhà bảo trì";
            dgvBaoTri.Columns[2].HeaderText = "Mã máy";
            dgvBaoTri.Columns[3].HeaderText = "Nguyên nhân";
            dgvBaoTri.Columns[4].HeaderText = "Giải pháp";
            dgvBaoTri.Columns[5].HeaderText = "Thành tiền";
            dgvBaoTri.Columns[6].HeaderText = "Ngày bảo trì";

            dgvBaoTri.AllowUserToAddRows = false;
            dgvBaoTri.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvBaoTri_Click(object sender, EventArgs e)
        {
            txtMabaotri.Enabled = false;
            string ma;
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMabaotri.Focus();
                return;
            }

            if (tblbt.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMabaotri.Text = dgvBaoTri.CurrentRow.Cells["Mabaotri"].Value.ToString();
            ma = dgvBaoTri.CurrentRow.Cells["manbt"].Value.ToString();
            cboMaNBT.Text = Function.GetFieldValues("SELECT Tennbt FROM tblnhabaotri WHERE Manbt = N'" + ma + "'");
            ma = dgvBaoTri.CurrentRow.Cells["mamay"].Value.ToString();
            cboMamay.Text = Function.GetFieldValues1("SELECT mamay FROM tblmaytinh WHERE Mamay = N'" + ma + "'");
            txtNguyennhan.Text = dgvBaoTri.CurrentRow.Cells["nguyennhan"].Value.ToString();
            txtGiaiphap.Text = dgvBaoTri.CurrentRow.Cells["giaiphap"].Value.ToString();
            txtThanhtien.Text = dgvBaoTri.CurrentRow.Cells["thanhtien"].Value.ToString();
            mskNgayBT.Text = dgvBaoTri.CurrentRow.Cells["Ngaybt"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMabaotri.Enabled = true;
            txtMabaotri.Text = "";
            cboMaNBT.Text = "";
            cboMamay.Text = "";
            txtNguyennhan.Text = "";
            txtGiaiphap.Text = "";
            txtThanhtien.Text = "";
            mskNgayBT.Text = "  /  /";
            btnLuu.Enabled = true;
            btnBoqua.Enabled = true;
            btnDong.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMabaotri.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã bảo trì", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMabaotri.Focus();
                return;
            }
            if (cboMaNBT.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã NBT", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNBT.Focus();
                return;
            }
            if (cboMamay.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã máy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMamay.Focus();
                return;
            }
            if (mskNgayBT.Text == "  /  /")
            {
                MessageBox.Show("Bạn phải nhập ngày bảo trì", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgayBT.Focus();
                return;
            }
            if (!Function.IsDate(mskNgayBT.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgayBT.Text = "";
                mskNgayBT.Focus();
                return;
            }
            sql = "SELECT Mabaotri FROM tblbaotri WHERE Mabaotri=N'" + txtMabaotri.Text.Trim() + "'";
            if (Function.CheckKey(sql))
            {
                MessageBox.Show("Mã bảo trì này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMabaotri.Focus();
                txtMabaotri.Text = "";
                return;
            }
            sql = "INSERT INTO tblbaotri(Mabaotri,manbt,mamay,nguyennhan, giaiphap, thanhtien, ngayBT) VALUES(N'" + txtMabaotri.Text.Trim() +
                "', N'" + cboMaNBT.SelectedValue.ToString() +
                "', N'" + cboMamay.SelectedValue.ToString() +
                "', N'" + txtNguyennhan.Text.Trim() +
                "', N'" + txtGiaiphap.Text.Trim() +
                "', '" + txtThanhtien.Text +
                "', '" + Function.ConvertDateTime(mskNgayBT.Text) + "')";
            Function.RunSQL(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMabaotri.Enabled = false;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblbt.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMabaotri.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMaNBT.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn mã nhà BT", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNBT.Focus();
                return;
            }
            if (cboMamay.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn mã máy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMamay.Focus();
                return;
            }
            if (txtNguyennhan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập nguyên nhân", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNguyennhan.Focus();
                return;
            }
            if (txtGiaiphap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giải pháp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaiphap.Focus();
                return;
            }
            if (txtThanhtien.Text == "0")
            {
                MessageBox.Show("Bạn phải nhập thành tiền", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtThanhtien.Focus();
                return;
            }
            sql = "UPDATE tblBaotri SET  Manbt=N'" + cboMaNBT.SelectedValue.ToString() +
                "',Mamay=N'" + cboMamay.SelectedValue.ToString() +
                "',Nguyennhan='" + txtNguyennhan.Text.Trim().ToString() +
                "',Giaiphap=N'" + txtGiaiphap.Text.Trim().ToString() + "' WHERE Mabaotri=N'" + txtMabaotri.Text + "'";
            Function.RunSQL(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblbt.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMabaotri.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblbaotri WHERE Mabaotri=N'" + txtMabaotri.Text + "'";
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
            txtMabaotri.Enabled = false;
            cboMaNBT.Text = "";
            cboMamay.Text = "";
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtThanhtien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (e.KeyChar == '-') ||
                (e.KeyChar == '.') || (Convert.ToInt32(e.KeyChar) == 8) || (Convert.ToInt32(e.KeyChar) == 13))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            txtMabaotri.Enabled = true;
            string sql;
            if ((txtMabaotri.Text == "") && (cboMaNBT.Text == "") && (cboMamay.Text == "") && (txtNguyennhan.Text == "") && (txtGiaiphap.Text == "") && (txtThanhtien.Text == "") && (mskNgayBT.Text == "  /  /"))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM tblbaotri WHERE 1=1";
            if (txtMabaotri.Text != "")
                sql = sql + " AND mabaotri Like N'%" + txtMabaotri.Text + "%'";
            if (cboMaNBT.Text != "")
                sql = sql + " AND manbt Like N'%" + cboMaNBT.SelectedValue + "%'";
            if (cboMamay.Text != "")
                sql = sql + " AND mamay Like N'%" + cboMamay.SelectedValue + "%'";
            if (txtNguyennhan.Text != "")
                sql = sql + " AND nguyennhan Like N'%" + txtNguyennhan.Text + "%'";
            if (txtGiaiphap.Text != "")
                sql = sql + " AND giaiphap Like N'%" + txtGiaiphap.Text + "%'";
            if (txtThanhtien.Text != "")
                sql = sql + " AND thanhtien Like N'%" + txtThanhtien.Text + "%'";
            if (mskNgayBT.Text != "  /  /")
                sql = sql + " AND ngayBT Like N'%" + mskNgayBT.Text + "%'";

            tblbt = Function.GetDataToTable(sql);
            if (tblbt.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Có " + tblbt.Rows.Count + " bản ghi thỏa mãn điều kiện!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            dgvBaoTri.DataSource = tblbt;
            ResetValues();
        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT * FROM tblbaotri";
            tblbt = Function.GetDataToTable(sql);
            dgvBaoTri.DataSource = tblbt;
        }

        private void btnSearchBaoTri_Click(object sender, EventArgs e)
        {

        }
    }
}