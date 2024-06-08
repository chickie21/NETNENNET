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
    public partial class TRALUONG : Form
    {
        public TRALUONG()
        {
            InitializeComponent();
        }

        private void TRALUONG_Load(object sender, EventArgs e)
        {
            Class.Function.Connect();
            txtMaluong.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DataGridView();
            Class.Function.FillCombo("SELECT MaNV, TenNV FROM tblNHANVIEN", cboMaNV, "MaNV", "TenNV");
            cboMaNV.SelectedIndex = -1;
            ResetValues();
        }
        private void ResetValues()
        {
            txtMaluong.Text = "";
            cboMaNV.Text = "";
            txtTienluong.Text = "0";
            mskNgaytraluong.Text = "";
        }
        DataTable tblL;
        private void Load_DataGridView()
        {
            string sql;
            sql = "select Maluong, MaNV, Tienluong, Ngaytraluong from tblTRALUONG";
            tblL = Class.Function.GetDataToTable(sql);
            DataGridView.DataSource = tblL;
            DataGridView.Columns[0].HeaderText = "Mã lương";
            DataGridView.Columns[1].HeaderText = "Mã nhân viên";
            DataGridView.Columns[2].HeaderText = "Tiền lương";
            DataGridView.Columns[3].HeaderText = "Ngày trả lương";
            DataGridView.Columns[0].Width = 100;
            DataGridView.Columns[1].Width = 100;
            DataGridView.Columns[2].Width = 100;
            DataGridView.Columns[3].Width = 100;
            DataGridView.AllowUserToAddRows = false;
            DataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaluong.Enabled = true;
            txtMaluong.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaluong.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNV.Focus();
                return;
            }
            if (txtTienluong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tiền lương", "Thông báo",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTienluong.Focus();
                return;
            }
            if (mskNgaytraluong.Text == " / /")
            {
                MessageBox.Show("Bạn phải nhập ngày trả lương", "Thông báo",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaytraluong.Focus();
                return;
            }
            if (!Class.Function.IsDate(mskNgaytraluong.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày trả lương", "Thông báo",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaytraluong.Text = "";
                mskNgaytraluong.Focus();
                return;
            }

            sql = "UPDATE tblTRALUONG SET MaNV=N'" + cboMaNV.SelectedValue.ToString()
                    + "', Tienluong=" + txtTienluong.Text
                    + ", Ngaytraluong=N'" + Class.Function.ConvertDateTime(mskNgaytraluong.Text)
                    + "' WHERE Maluong=N'" + txtMaluong.Text + "'";

            Class.Function.RunSQL(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaluong.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblTRALUONG WHERE Maluong=N'" + txtMaluong.Text + "'";
                Class.Function.RunSQL(sql);
                Load_DataGridView();
                ResetValues();
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaluong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã lương", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaluong.Focus();
                return;
            }
            if (cboMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNV.Focus();
                return;
            }
            if (txtTienluong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tiền lượng",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTienluong.Focus();
                return;
            }
            if (mskNgaytraluong.Text == "  /  /")
            {
                MessageBox.Show("Chưa nhập trả lương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaytraluong.Focus();
                return;
            }
            if (!Class.Function.IsDate(mskNgaytraluong.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày trả lương", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaytraluong.Text = "";
                mskNgaytraluong.Focus();
                return;
            }
            sql = "SELECT Maluong FROM tblTRALUONG WHERE Maluong=N'" + txtMaluong.Text.Trim() + "'";
            if (Class.Function.CheckKey(sql))
            {
                MessageBox.Show("Mã lương này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaluong.Focus();
                txtMaluong.Text = "";
                return;
            }
            sql = "INSERT INTO tblTRALUONG(Maluong, MaNV, Tienluong, Ngaytraluong) VALUES(N'"
                    + txtMaluong.Text.Trim() + "', N'"
                    + cboMaNV.SelectedValue.ToString() + "', "
                    + txtTienluong.Text.Trim() + ", N'"
                    + Class.Function.ConvertDateTime(mskNgaytraluong.Text) + "')";
            Class.Function.RunSQL(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaluong.Enabled = false;

        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaluong.Enabled = false;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaluong.Text == "") && (cboMaNV.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM tblTRALUONG WHERE 1=1";
            if (txtMaluong.Text != "")
                sql = sql + " AND Maluong Like N'%" + txtMaluong.Text + "%'";
            if (cboMaNV.Text != "")
                sql = sql + " AND MaNV Like N'%" + cboMaNV.SelectedValue + "%'";
            tblL = Class.Function.GetDataToTable(sql);
            if (tblL.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Có " + tblL.Rows.Count + " bản ghi thỏa mãn đkien", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DataGridView.DataSource = tblL;
            ResetValues();

        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT Maluong, MaNV, Tienluong, Ngaytraluong FROM tblTRALUONG";
            tblL = Class.Function.GetDataToTable(sql);
            DataGridView.DataSource = tblL;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ma;
            if (tblL.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtMaluong.Text = DataGridView.CurrentRow.Cells["Maluong"].Value.ToString();
            ma = DataGridView.CurrentRow.Cells["MaNV"].Value.ToString();
            cboMaNV.Text = Class.Function.GetFieldValues("SELECT MaNV FROM tblNHANVIEN WHERE MaNV = N'" + ma + "'");
            txtTienluong.Text = DataGridView.CurrentRow.Cells["Tienluong"].Value.ToString();
            mskNgaytraluong.Text = DataGridView.CurrentRow.Cells["Ngaytraluong"].Value.ToString();

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = false;
        }
    }

}
