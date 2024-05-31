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
    public partial class frmQUANLYMAY : Form
    {
        public frmQUANLYMAY()
        {
            InitializeComponent();
        }

        DataTable tblQLM;
        private void frmQUANLYMAY_Load(object sender, EventArgs e)
        {
            Class.Function.Connect();
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            Load_DataGridView();
            Function.FillCombo("SELECT Maphong, Tenphong FROM tblphong",cboMaphong, "Maphong", "Tenphong");
            cboMaphong.SelectedIndex = 0;
            Function.FillCombo("SELECT Maocung, Tenocung FROM tblocung", cboMaocung, "Maocung", "Tenocung");
            cboMaocung.SelectedIndex = 0;
            Function.FillCombo("SELECT MaRAM, TenRAM FROM tblRAM", cboMaram, "Maram", "Tenram");
            cboMaram.SelectedIndex = 0;
            Function.FillCombo("SELECT Machip, Tenchip FROM tblchip", cboMachip, "Machip", "Tenchip");
            cboMachip.SelectedIndex = 0;
            Function.FillCombo("SELECT Mamanhinh, Tenmanhinh FROM tblmanhinh", cboMamanhinh, "Mamanhinh", "Tenmanhinh");
            cboMamanhinh.SelectedIndex = 0;
            Function.FillCombo("SELECT Macomanhinh, Tencomanhinh FROM tblcomanhinh", cboComanhinh, "Macomanhinh", "Tencomanhinh");
            cboComanhinh.SelectedIndex = 0;
            Function.FillCombo("SELECT Machuot, Tenchuot FROM tblchuot", cboMachuot, "Machuot", "Tenchuot");
            cboMachuot.SelectedIndex = 0;
            Function.FillCombo("SELECT Mabanphim, Tenbanphim FROM tblbanphim", cboMabanphim, "Mabanphim", "Tenbaphim");
            cboMabanphim.SelectedIndex = 0;
            Function.FillCombo("SELECT Maloa, Tenloa FROM tblloa", cboMaloa, "Maloa", "Tenloa");
            cboMaloa.SelectedIndex = 0;
            ResetValues();
        }
        private void ResetValues()
        {
            cboMaphong.Text = "";
            txtMamay.Text = "";
            cboMaocung.Text = "";
            cboMamanhinh.Text = "";
            cboComanhinh.Text = "";
            cboMaram.Text = "";
            cboMachip.Text = "";
            cboMachuot.Text = "";
            cboMabanphim.Text = "";
            cboMaloa.Text = "";
            txtTinhtrang.Text = "";
            txtTrangthai.Text = "";
            txtGhichu.Text = "";
        }
        private void Load_DataGridView()
        {   
            
            string sql;
            sql = "SELECT Maphong, mamay, maocung, maram, machip, mamanhinh, macomanhinh, mabanphim, machuot, maloa, tinhtrang, tinhtrangthue, ghichu FROM tblMaytinh";
            tblQLM = Function.GetDataToTable(sql);
            dgvQuanlymay.DataSource = tblQLM;
            dgvQuanlymay.Columns[0].HeaderText = "Mã phòng";
            dgvQuanlymay.Columns[1].HeaderText = "Mã máy";
            dgvQuanlymay.Columns[2].HeaderText = "Mã ổ cứng";
            dgvQuanlymay.Columns[3].HeaderText = "Mã RAM";
            dgvQuanlymay.Columns[4].HeaderText = "Mã chip";
            dgvQuanlymay.Columns[5].HeaderText = "Mã màn hình";
            dgvQuanlymay.Columns[6].HeaderText = "Mã cỡ màn hình";
            dgvQuanlymay.Columns[7].HeaderText = "Mã bàn phím";
            dgvQuanlymay.Columns[8].HeaderText = "Mã chuột";
            dgvQuanlymay.Columns[9].HeaderText = "Mã loa";
            dgvQuanlymay.Columns[10].HeaderText = "Tình trạng";
            dgvQuanlymay.Columns[11].HeaderText = "Trạng thái thuê";
            dgvQuanlymay.Columns[12].HeaderText = "Ghi chú";
            dgvQuanlymay.AllowUserToAddRows = false;
            dgvQuanlymay.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvQuanlymay_Click(object sender, EventArgs e)
        {
            string ma;
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboMaphong.Focus();
                return;
            }
            if (tblQLM.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            txtMamay.Text = dgvQuanlymay.CurrentRow.Cells["mamay"].Value.ToString();
            ma = dgvQuanlymay.CurrentRow.Cells["maphong"].Value.ToString();
            cboMaphong.Text = Function.GetFieldValues("SELECT Tenphong FROM tblphong WHERE Maphong = N'" + ma + "'");

            ma = dgvQuanlymay.CurrentRow.Cells["maocung"].Value.ToString();
            cboMaocung.Text = Function.GetFieldValues1("select tenocung from tblocung where maocung = N'"+ma+"'");

            ma = dgvQuanlymay.CurrentRow.Cells["maram"].Value.ToString();
            cboMaram.Text = Function.GetFieldValues("select tenRAM from tblRAM where maram = N'" + ma + "'");

            ma = dgvQuanlymay.CurrentRow.Cells["machip"].Value.ToString();
            cboMachip.Text = Function.GetFieldValues("select tenchip from tblchip where machip = N'" + ma + "'");

            ma = dgvQuanlymay.CurrentRow.Cells["mamanhinh"].Value.ToString();
            cboMamanhinh.Text = Function.GetFieldValues("select tenmanhinh from tblmanhinh where mamanhinh = N'" + ma + "'");

            ma = dgvQuanlymay.CurrentRow.Cells["macomanhinh"].Value.ToString();
            cboComanhinh.Text = Function.GetFieldValues("select tencomanhinh from tblcomanhinh where macomanhinh = N'" + ma + "'");

            ma = dgvQuanlymay.CurrentRow.Cells["machuot"].Value.ToString();
            cboMachuot.Text = Function.GetFieldValues("select tenchuot from tblchuot where machuot = N'" + ma + "'");

            ma = dgvQuanlymay.CurrentRow.Cells["mabanphim"].Value.ToString();
            cboMabanphim.Text = Function.GetFieldValues("select tenbanphim from tblbanphim where mabanphim = N'" + ma + "'");

            ma = dgvQuanlymay.CurrentRow.Cells["maloa"].Value.ToString();
            cboMaloa.Text = Function.GetFieldValues("select tenloa from tblloa where maloa = N'" + ma + "'");

            txtTinhtrang.Text = dgvQuanlymay.CurrentRow.Cells["tinhtrang"].Value.ToString();
            txtTrangthai.Text = dgvQuanlymay.CurrentRow.Cells["tinhtrangthue"].Value.ToString();
            txtGhichu.Text = dgvQuanlymay.CurrentRow.Cells["ghichu"].Value.ToString();
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
            cboMaphong.Enabled = true;
            txtMamay.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (cboMaphong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn phòng!", "Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                cboMaphong.Focus();
                return;
            }
            if (txtMamay.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã máy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMamay.Focus();
                return;
            }
            if (cboMaocung.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn Ổ cứng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaocung.Focus();
                return;
            }
            if (cboMaram.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn RAM!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaram.Focus();
                return;
            }
            if (cboMachip.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn chip!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMachip.Focus();
                return;
            }
            if (cboMamanhinh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn màn hình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMamanhinh.Focus();
                return;
            }
            if (cboComanhinh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn cỡ màn hình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboComanhinh.Focus();
                return;
            }
            if (cboMachuot.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn chuột!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMachuot.Focus();
                return;
            }
            if (cboMabanphim.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn bàn phím!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMabanphim.Focus();
                return;
            }
            if (cboMaloa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn loa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaloa.Focus();
                return;
            }
            if (cboMaphong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaphong.Focus();
                return;
            }
            if (txtTinhtrang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tình trạng máy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTinhtrang.Focus();
                return;

            }
            if (txtTrangthai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập trạng thái thuê máy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTrangthai.Focus();
                return;
            }


            sql = "SELECT mamay FROM tblmaytinh WHERE Mamay=N'" + txtMamay.Text.Trim() + "'";
            if (Function.CheckKey(sql))
            {
                MessageBox.Show("Mã máy này đã có, bạn phải nhập mã khác", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMamay.Focus();
                txtMamay.Text = "";
                return;
            }
            sql = "update tblphong set somay=somay+1 where maphong=N'"+cboMaphong.SelectedValue.ToString()+"'";
            Function.RunSQL(sql);

            sql = "INSERT INTO tblMaytinh(Maphong, mamay, maocung, maram, machip, mamanhinh, macomanhinh, mabanphim, machuot, maloa, tinhtrang, tinhtrangthue, ghichu) VALUES(N'" + cboMaphong.SelectedValue.ToString() +
                    "',N'" + txtMamay.Text.Trim() + "',N'" + cboMaocung.SelectedValue.ToString() + "',N'" + cboMaram.SelectedValue.ToString() +
                      "',N'" + cboMachip.SelectedValue.ToString() + "',N'" + cboMamanhinh.SelectedValue.ToString() +
                        "',N'" + cboComanhinh.SelectedValue.ToString() +
                        "',N'"+ cboMabanphim.SelectedValue.ToString() +
                        "',N'" + cboMachuot.SelectedValue.ToString() +
                        "',N'"+cboMaloa.SelectedValue.ToString()+
                        "',N'"+txtTinhtrang.Text.Trim()+
                        "',N'"+txtTrangthai.Text.Trim()+
                        "',N'"+txtGhichu.Text.Trim() +"')";
            Function.RunSQL(sql);
            Load_DataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            cboMaphong.Enabled = false;
        }

        private void txtTinhtrang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == '0' ) ||(e.KeyChar == '1') || (Convert.ToInt32(e.KeyChar) == 8) || (Convert.ToInt32(e.KeyChar) == 13))
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblQLM.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cboMaphong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaphong.Focus();
                return;
            }
            if (txtMamay.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã máy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMamay.Focus();
                return;
            }
            if (cboMaocung.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn Ổ cứng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaocung.Focus();
                return;
            }
            if (cboMaram.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn RAM!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaram.Focus();
                return;
            }
            if (cboMachip.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn chip!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMachip.Focus();
                return;
            }
            if (cboMamanhinh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn màn hình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMamanhinh.Focus();
                return;
            }
            if (cboComanhinh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn cỡ màn hình!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboComanhinh.Focus();
                return;
            }
            if (cboMachuot.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn chuột!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMachuot.Focus();
                return;
            }
            if (cboMabanphim.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn bàn phím!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMabanphim.Focus();
                return;
            }
            if (cboMaloa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn loa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaloa.Focus();
                return;
            }
            if (cboMaphong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaphong.Focus();
                return;
            }
            if (txtTinhtrang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tình trạng máy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTinhtrang.Focus();
                return;

            }
            if (txtTrangthai.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập trạng thái thuê máy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTrangthai.Focus();
                return;
            }

            sql = "UPDATE tblMaytinh SET maphong=N'" +cboMaphong.SelectedValue.ToString()+
                "',Maocung=N'" + cboMaocung.SelectedValue.ToString() +
                "',Maram=N'" + cboMaram.SelectedValue.ToString() +
                "',Machip=N'" + cboMachip.SelectedValue.ToString() +
                "',Mamanhinh=N'" + cboMamanhinh.SelectedValue.ToString() +
                "',Macomanhinh=N'" + cboComanhinh.SelectedValue.ToString() +
                "',Machuot=N'" + cboMachuot.SelectedValue.ToString() +
                "',Mabanphim=N'" + cboMabanphim.SelectedValue.ToString() +
                "',Maloa=N'" + cboMaloa.SelectedValue.ToString() +
                "',Tinhtrang=N'" + txtTinhtrang.Text +
                "',Tinhtrangthue=N'" + txtTrangthai.Text.Trim().ToString() +
                "',Ghichu=N'" + txtGhichu.Text.Trim().ToString() + "' where Mamay=N'" + txtMamay.Text + "'";
            Function.RunSQL(sql);
            Load_DataGridView();
            ResetValues();
            btnBoqua.Enabled = false;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblQLM.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            if (cboMaphong.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblMaytinh WHERE Mamay=N'" + txtMamay.Text + "'";
                Function.RunSqlDel(sql);
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
            cboMaphong.Enabled = false;

        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string sql;
            if ((cboMaphong.Text == "") && (txtMamay.Text == "") && (cboMaocung.Text =="") && (cboMaram.Text == "") && (cboMachip.Text == "") && (cboMamanhinh.Text == "") && (cboComanhinh.Text == "") && (cboMachuot.Text == "") && (cboMabanphim.Text == "") && (cboMaloa.Text == "") && (txtTinhtrang.Text == "") && (txtTrangthai.Text == ""))
            {
                MessageBox.Show("Hãy nhập/chọn một điều kiện tìm kiếm!!!", "Yêu cầu ...",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            sql = "SELECT * FROM tblmaytinh WHERE 1=1";
            if (cboMaphong.Text != "")
                sql = sql + " AND maphong Like N'%" + cboMaphong.SelectedValue + "%'";
            if (txtMamay.Text != "")
                sql = sql + " AND mamay Like N'%" + txtMamay.Text + "%'";
            if (cboMaocung.Text != "")
                sql = sql + " AND maocung Like N'%" + cboMaocung.SelectedValue + "%'";
            if (cboMaram.Text != "")
                sql = sql + " AND maram Like N'%" + cboMaram.SelectedValue + "%'";
            if (cboMachip.Text != "")
                sql = sql + " AND machip Like N'%" + cboMachip.SelectedValue + "%'";
            if (cboMamanhinh.Text != "")
                sql = sql + " AND mamanhinh Like N'%" + cboMamanhinh.SelectedValue + "%'";
            if (cboComanhinh.Text != "")
                sql = sql + " AND macomanhinh Like N'%" + cboComanhinh.SelectedValue + "%'";
            if (cboMachuot.Text != "")
                sql = sql + " AND machuot Like N'%" + cboMachuot.SelectedValue + "%'";
            if (cboMabanphim.Text != "")
                sql = sql + " AND mabanphim Like N'%" + cboMabanphim.SelectedValue + "%'";
            if (cboMaloa.Text != "")
                sql = sql + " AND maloa Like N'%" + cboMaloa.SelectedValue + "%'";
            if (txtTinhtrang.Text != "")
                sql = sql + " AND tinhtrang Like N'%" + txtTinhtrang.Text + "%'";
            if (txtTrangthai.Text != "")
                sql = sql + " AND tinhtrangthue Like N'%" + txtTrangthai.Text + "%'";

            tblQLM = Function.GetDataToTable(sql);
            if (tblQLM.Rows.Count == 0)
                MessageBox.Show("Không có bản ghi thỏa mãn điều kiện!!!", "Thông báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("Có " + tblQLM.Rows.Count + " bản ghi thỏa mãn điều kiện!!!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            dgvQuanlymay.DataSource = tblQLM;
            ResetValues();

        }

        private void btnHienthi_Click(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT * FROM tblmaytinh";
            tblQLM = Function.GetDataToTable(sql);
            dgvQuanlymay.DataSource = tblQLM;

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTrangthai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (e.KeyChar == '-') ||
               (e.KeyChar == '.'))
                e.Handled = true;
            else
                e.Handled = false;
        }
    }
}
