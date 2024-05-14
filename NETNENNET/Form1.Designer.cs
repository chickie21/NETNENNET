namespace NETNENNET
{
    partial class Form1
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
            this.tabRoom = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTatMay = new System.Windows.Forms.PictureBox();
            this.btnKhoaMay = new System.Windows.Forms.PictureBox();
            this.btnMoMay = new System.Windows.Forms.PictureBox();
            this.tabRoom.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTatMay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnKhoaMay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoMay)).BeginInit();
            this.SuspendLayout();
            // 
            // tabRoom
            // 
            this.tabRoom.Controls.Add(this.tabPage1);
            this.tabRoom.Controls.Add(this.tabPage2);
            this.tabRoom.Controls.Add(this.tabPage3);
            this.tabRoom.Location = new System.Drawing.Point(107, 0);
            this.tabRoom.Name = "tabRoom";
            this.tabRoom.SelectedIndex = 0;
            this.tabRoom.Size = new System.Drawing.Size(690, 390);
            this.tabRoom.TabIndex = 1;
            this.tabRoom.SelectedIndexChanged += new System.EventHandler(this.tabRoom_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.btnTatMay);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.btnKhoaMay);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.btnMoMay);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(682, 361);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(682, 361);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(682, 361);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 120);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(670, 238);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(25, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Mở máy";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.UseMnemonic = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(105, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 19);
            this.label2.TabIndex = 5;
            this.label2.Text = "Khóa máy";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.UseMnemonic = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(205, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 19);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tắt máy";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.UseMnemonic = false;
            // 
            // btnTatMay
            // 
            this.btnTatMay.Image = global::NETNENNET.Properties.Resources.start_red;
            this.btnTatMay.Location = new System.Drawing.Point(217, 18);
            this.btnTatMay.Name = "btnTatMay";
            this.btnTatMay.Size = new System.Drawing.Size(55, 53);
            this.btnTatMay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnTatMay.TabIndex = 6;
            this.btnTatMay.TabStop = false;
            this.btnTatMay.Click += new System.EventHandler(this.btnTatMay_Click);
            // 
            // btnKhoaMay
            // 
            this.btnKhoaMay.Image = global::NETNENNET.Properties.Resources._lock;
            this.btnKhoaMay.Location = new System.Drawing.Point(120, 18);
            this.btnKhoaMay.Name = "btnKhoaMay";
            this.btnKhoaMay.Size = new System.Drawing.Size(55, 53);
            this.btnKhoaMay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnKhoaMay.TabIndex = 4;
            this.btnKhoaMay.TabStop = false;
            this.btnKhoaMay.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // btnMoMay
            // 
            this.btnMoMay.Image = global::NETNENNET.Properties.Resources.power_button_black;
            this.btnMoMay.Location = new System.Drawing.Point(25, 18);
            this.btnMoMay.Name = "btnMoMay";
            this.btnMoMay.Size = new System.Drawing.Size(55, 53);
            this.btnMoMay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnMoMay.TabIndex = 1;
            this.btnMoMay.TabStop = false;
            this.btnMoMay.Click += new System.EventHandler(this.btnMoMay_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabRoom);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabRoom.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTatMay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnKhoaMay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMoMay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabRoom;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox btnMoMay;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox btnKhoaMay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox btnTatMay;
    }
}

