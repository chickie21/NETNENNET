namespace NETNENNET
{
    partial class frmBAOTRI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtThanhtien = new System.Windows.Forms.TextBox();
            this.txtNguyennhan = new System.Windows.Forms.TextBox();
            this.txtGiaiphap = new System.Windows.Forms.TextBox();
            this.txtMabaotri = new System.Windows.Forms.TextBox();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnBoqua = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dgvBaoTri = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cboMaNBT = new System.Windows.Forms.ComboBox();
            this.cboMamay = new System.Windows.Forms.ComboBox();
            this.mskNgayBT = new System.Windows.Forms.MaskedTextBox();
            this.btnTimkiem = new System.Windows.Forms.Button();
            this.btnHienthi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoTri)).BeginInit();
            this.SuspendLayout();
            // 
            // txtThanhtien
            // 
            this.txtThanhtien.Location = new System.Drawing.Point(258, 275);
            this.txtThanhtien.Name = "txtThanhtien";
            this.txtThanhtien.Size = new System.Drawing.Size(413, 22);
            this.txtThanhtien.TabIndex = 39;
            this.txtThanhtien.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtThanhtien_KeyPress);
            // 
            // txtNguyennhan
            // 
            this.txtNguyennhan.Location = new System.Drawing.Point(258, 195);
            this.txtNguyennhan.Name = "txtNguyennhan";
            this.txtNguyennhan.Size = new System.Drawing.Size(413, 22);
            this.txtNguyennhan.TabIndex = 36;
            // 
            // txtGiaiphap
            // 
            this.txtGiaiphap.Location = new System.Drawing.Point(258, 237);
            this.txtGiaiphap.Name = "txtGiaiphap";
            this.txtGiaiphap.Size = new System.Drawing.Size(413, 22);
            this.txtGiaiphap.TabIndex = 35;
            // 
            // txtMabaotri
            // 
            this.txtMabaotri.Location = new System.Drawing.Point(258, 74);
            this.txtMabaotri.Name = "txtMabaotri";
            this.txtMabaotri.Size = new System.Drawing.Size(413, 22);
            this.txtMabaotri.TabIndex = 34;
            // 
            // btnDong
            // 
            this.btnDong.Location = new System.Drawing.Point(819, 622);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(91, 34);
            this.btnDong.TabIndex = 33;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnBoqua
            // 
            this.btnBoqua.Location = new System.Drawing.Point(678, 622);
            this.btnBoqua.Name = "btnBoqua";
            this.btnBoqua.Size = new System.Drawing.Size(91, 34);
            this.btnBoqua.TabIndex = 32;
            this.btnBoqua.Text = "Bỏ qua";
            this.btnBoqua.UseVisualStyleBackColor = true;
            this.btnBoqua.Click += new System.EventHandler(this.btnBoqua_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(536, 622);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(91, 34);
            this.btnLuu.TabIndex = 31;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(382, 622);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(91, 34);
            this.btnXoa.TabIndex = 30;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(215, 622);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(91, 34);
            this.btnSua.TabIndex = 29;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(73, 622);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(91, 34);
            this.btnThem.TabIndex = 28;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dgvBaoTri
            // 
            this.dgvBaoTri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaoTri.Location = new System.Drawing.Point(144, 374);
            this.dgvBaoTri.Name = "dgvBaoTri";
            this.dgvBaoTri.RowHeadersWidth = 51;
            this.dgvBaoTri.RowTemplate.Height = 24;
            this.dgvBaoTri.Size = new System.Drawing.Size(837, 214);
            this.dgvBaoTri.TabIndex = 27;
            this.dgvBaoTri.Click += new System.EventHandler(this.dgvBaoTri_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(141, 275);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 26;
            this.label7.Text = "Thành tiền:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(141, 237);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 16);
            this.label6.TabIndex = 25;
            this.label6.Text = "Giải pháp:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(141, 195);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 16);
            this.label5.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(141, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 16);
            this.label4.TabIndex = 23;
            this.label4.Text = "Mã máy:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(141, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 16);
            this.label3.TabIndex = 22;
            this.label3.Text = "Mã nhà bảo trì:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 21;
            this.label2.Text = "Mã bảo trì:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(442, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(351, 38);
            this.label1.TabIndex = 20;
            this.label1.Text = "DANH MỤC BẢO TRÌ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(141, 195);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 16);
            this.label8.TabIndex = 40;
            this.label8.Text = "Nguyên nhân:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(141, 324);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 16);
            this.label9.TabIndex = 42;
            this.label9.Text = "Ngày bảo trì:";
            // 
            // cboMaNBT
            // 
            this.cboMaNBT.FormattingEnabled = true;
            this.cboMaNBT.Location = new System.Drawing.Point(258, 115);
            this.cboMaNBT.Name = "cboMaNBT";
            this.cboMaNBT.Size = new System.Drawing.Size(413, 24);
            this.cboMaNBT.TabIndex = 43;
            // 
            // cboMamay
            // 
            this.cboMamay.FormattingEnabled = true;
            this.cboMamay.Location = new System.Drawing.Point(258, 152);
            this.cboMamay.Name = "cboMamay";
            this.cboMamay.Size = new System.Drawing.Size(413, 24);
            this.cboMamay.TabIndex = 44;
            // 
            // mskNgayBT
            // 
            this.mskNgayBT.Location = new System.Drawing.Point(258, 324);
            this.mskNgayBT.Mask = "00/00/0000";
            this.mskNgayBT.Name = "mskNgayBT";
            this.mskNgayBT.Size = new System.Drawing.Size(413, 22);
            this.mskNgayBT.TabIndex = 45;
            this.mskNgayBT.ValidatingType = typeof(System.DateTime);
            // 
            // btnTimkiem
            // 
            this.btnTimkiem.Location = new System.Drawing.Point(953, 621);
            this.btnTimkiem.Name = "btnTimkiem";
            this.btnTimkiem.Size = new System.Drawing.Size(103, 35);
            this.btnTimkiem.TabIndex = 52;
            this.btnTimkiem.Text = "Tìm kiếm";
            this.btnTimkiem.UseVisualStyleBackColor = true;
            this.btnTimkiem.Click += new System.EventHandler(this.btnTimkiem_Click);
            // 
            // btnHienthi
            // 
            this.btnHienthi.Location = new System.Drawing.Point(987, 467);
            this.btnHienthi.Name = "btnHienthi";
            this.btnHienthi.Size = new System.Drawing.Size(103, 35);
            this.btnHienthi.TabIndex = 53;
            this.btnHienthi.Text = "Hiển thị DS";
            this.btnHienthi.UseVisualStyleBackColor = true;
            this.btnHienthi.Click += new System.EventHandler(this.btnHienthi_Click);
            // 
            // frmBAOTRI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 668);
            this.Controls.Add(this.btnHienthi);
            this.Controls.Add(this.btnTimkiem);
            this.Controls.Add(this.mskNgayBT);
            this.Controls.Add(this.cboMamay);
            this.Controls.Add(this.cboMaNBT);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtThanhtien);
            this.Controls.Add(this.txtNguyennhan);
            this.Controls.Add(this.txtGiaiphap);
            this.Controls.Add(this.txtMabaotri);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnBoqua);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvBaoTri);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmBAOTRI";
            this.Text = "frmBAOTRI";
            this.Load += new System.EventHandler(this.frmBAOTRI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaoTri)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtThanhtien;
        private System.Windows.Forms.TextBox txtNguyennhan;
        private System.Windows.Forms.TextBox txtGiaiphap;
        private System.Windows.Forms.TextBox txtMabaotri;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnBoqua;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridView dgvBaoTri;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboMaNBT;
        private System.Windows.Forms.ComboBox cboMamay;
        private System.Windows.Forms.MaskedTextBox mskNgayBT;
        private System.Windows.Forms.Button btnTimkiem;
        private System.Windows.Forms.Button btnHienthi;
    }
}