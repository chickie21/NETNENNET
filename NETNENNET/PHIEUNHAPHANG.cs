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
    public partial class PHIEUNHAPHANG : Form
    {
        public PHIEUNHAPHANG()
        {
            InitializeComponent();
        }

        private void PHIEUNHAPHANG_Load(object sender, EventArgs e)
        {
            Class.Function.Connect();
            txtMaPN.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DataGridView();
            Class.Function.FillCombo("SELECT MaTP, TenTP FROM tblTHUCPHAM", cboMaTP, "MaTP", "TenTP");
            cboMaTP.SelectedIndex = -1;
            ResetValues();
        }
        private void ResetValues()
        {
            txtMaPN.Text = "";
            cboMaTP.Text = "";
            txtSoluong.Text = "0";
            txtDongianhap.Text = "0";
            mskNgaynhap.Text = "";
        }
        DataTable tblPN;
        private void Load_DataGridView()
        {
            string sql;
            sql = "select MaPN, MaTP, Soluong, Dongianhap, Ngaynhap from tblPHIEUNHAPHANG";
            tblPN = Class.Function.GetDataToTable(sql);
            DataGridView.DataSource = tblPN;
            DataGridView.Columns[0].HeaderText = "Mã phiếu nhập";
            DataGridView.Columns[1].HeaderText = "Mã thực phẩm";
            DataGridView.Columns[2].HeaderText = "Số lượng";
            DataGridView.Columns[3].HeaderText = "Đơn giá nhập";
            DataGridView.Columns[4].HeaderText = "Ngày nhập";
            DataGridView.Columns[0].Width = 100;
            DataGridView.Columns[1].Width = 150;
            DataGridView.Columns[2].Width = 100;
            DataGridView.Columns[3].Width = 150;
            DataGridView.Columns[4].Width = 100;
            DataGridView.AllowUserToAddRows = false;
            DataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string ma;
            if (tblPN.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            txtMaPN.Text = DataGridView.CurrentRow.Cells["MaPN"].Value.ToString();
            ma = DataGridView.CurrentRow.Cells["MaTP"].Value.ToString();
            cboMaTP.Text = Class.Function.GetFieldValues("SELECT MaTP FROM tblTHUCPHAM WHERE MaTP = N'" + ma + "'");
            txtSoluong.Text = DataGridView.CurrentRow.Cells["Soluong"].Value.ToString();
            txtDongianhap.Text = DataGridView.CurrentRow.Cells["Dongianhap"].Value.ToString();
            mskNgaynhap.Text = DataGridView.CurrentRow.Cells["Ngaynhap"].Value.ToString();

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaPN.Enabled = true;
            txtMaPN.Focus();

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaPN.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã phiếu", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPN.Focus();
                return;
            }
            if (cboMaTP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã thực phẩm", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaTP.Focus();
                return;
            }
            if (txtSoluong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoluong.Focus();
                return;
            }
            if (txtDongianhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDongianhap.Focus();
                return;
            }
            if (mskNgaynhap.Text == "  /  /")
            {
                MessageBox.Show("Chưa nhập ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaynhap.Focus();
                return;
            }
            if (!Class.Function.IsDate(mskNgaynhap.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaynhap.Text = "";
                mskNgaynhap.Focus();
                return;
            }
            sql = "SELECT MaPN FROM tblPHIEUNHAPHANG WHERE MaPN=N'" + txtMaPN.Text.Trim() + "'";
            if (Class.Function.CheckKey(sql))
            {
                MessageBox.Show("Mã phiếu này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaPN.Focus();
                txtMaPN.Text = "";
                return;
            }
            Class.Function.RunSQL(sql);
            sql = "INSERT INTO tblPHIEUNHAPHANG(MaPN, MaTP, Soluong, Dongianhap, Ngaynhap) VALUES (N'"
                          + txtMaPN.Text.Trim() + "', N'"
                          + cboMaTP.SelectedValue.ToString() + "', "
                          + txtSoluong.Text.Trim() + ", "
                          + txtDongianhap.Text.Trim() + ", N'"
                          + Class.Function.ConvertDateTime(mskNgaynhap.Text) + "')";
            Class.Function.RunSQL(sql);

            sql = "UPDATE tblTHUCPHAM SET Soluong = Soluong + " + txtSoluong.Text.Trim() +
                      " WHERE MaTP = N'" + cboMaTP.SelectedValue.ToString() + "'";
            Class.Function.RunSQL(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaPN.Enabled = false;

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblPN.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaPN.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMaTP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã thực phẩm", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaTP.Focus();
                return;
            }
            if (txtSoluong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoluong.Focus();
                return;
            }
            if (txtDongianhap.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đơn giá", "Thông báo",
                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDongianhap.Focus();
                return;
            }
            if (mskNgaynhap.Text == " / /")
            {
                MessageBox.Show("Bạn phải nhập ngày nhập", "Thông báo",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaynhap.Focus();
                return;
            }
            if (!Class.Function.IsDate(mskNgaynhap.Text))
            {
                MessageBox.Show("Bạn phải nhập lại ngày sinh", "Thông báo",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgaynhap.Text = "";
                mskNgaynhap.Focus();
                return;
            }

            sql = "UPDATE tblPHIEUNHAPHANG SET MaTP=N'" + cboMaTP.SelectedValue.ToString()
                    + "', Soluong=" + txtSoluong.Text + ", Dongianhap=" + txtDongianhap.Text
                    + ", Ngaynhap=N'" + Class.Function.ConvertDateTime(mskNgaynhap.Text)
                    + "' WHERE MaPN=N'" + txtMaPN.Text + "'";

            Class.Function.RunSQL(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblPN.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaPN.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
            MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblPHIEUNHAPHANG WHERE MaPN=N'" + txtMaPN.Text + "'";
                Class.Function.RunSQL(sql);
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
            txtMaPN.Enabled = false;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((txtMaPN.Text == "") && (cboMaTP.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện tìm kiếm!!!", "Yêu cầu ...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM tblPHIEUNHAPHANG WHERE 1=1";
            if (txtMaPN.Text != "")
                sql = sql + " AND MaPN Like N'%" + txtMaPN.Text + "%'";
            if (cboMaTP.Text != "")
                sql = sql + " AND MaTP Like N'%" + cboMaTP.SelectedValue + "%'";
            tblPN = Class.Function.GetDataToTable(sql);
            if (tblPN.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Có " + tblPN.Rows.Count + " bản ghi thỏa mãn điều kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            DataGridView.DataSource = tblPN;
            ResetValues();

        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT MaPN, MaTP, Soluong, Dongianhap, Ngaynhap FROM tblPHIEUNHAPHANG";
            tblPN = Class.Function.GetDataToTable(sql);
            DataGridView.DataSource = tblPN;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
