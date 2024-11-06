namespace QLBHDongHo.Forms
{
    partial class frmDangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangNhap));
            grbDangNhap = new GroupBox();
            chbHienMK = new CheckBox();
            txtMK = new TextBox();
            txtTK = new TextBox();
            label2 = new Label();
            label1 = new Label();
            btnDangNhap = new Button();
            btnThoat = new Button();
            label3 = new Label();
            pictureBox3 = new PictureBox();
            grbDangNhap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // grbDangNhap
            // 
            grbDangNhap.BackColor = Color.White;
            grbDangNhap.Controls.Add(chbHienMK);
            grbDangNhap.Controls.Add(txtMK);
            grbDangNhap.Controls.Add(txtTK);
            grbDangNhap.Controls.Add(label2);
            grbDangNhap.Controls.Add(label1);
            grbDangNhap.FlatStyle = FlatStyle.System;
            grbDangNhap.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            grbDangNhap.ForeColor = Color.FromArgb(64, 0, 0);
            grbDangNhap.Location = new Point(326, 97);
            grbDangNhap.Name = "grbDangNhap";
            grbDangNhap.Size = new Size(344, 187);
            grbDangNhap.TabIndex = 0;
            grbDangNhap.TabStop = false;
            grbDangNhap.Text = "Đăng nhập";
            // 
            // chbHienMK
            // 
            chbHienMK.AutoSize = true;
            chbHienMK.BackColor = Color.WhiteSmoke;
            chbHienMK.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            chbHienMK.ForeColor = Color.FromArgb(64, 0, 0);
            chbHienMK.Location = new Point(6, 143);
            chbHienMK.Name = "chbHienMK";
            chbHienMK.Size = new Size(182, 27);
            chbHienMK.TabIndex = 4;
            chbHienMK.Text = "Hiển thị mật khẩu";
            chbHienMK.UseVisualStyleBackColor = false;
            chbHienMK.CheckedChanged += chbHienMK_CheckedChanged;
            // 
            // txtMK
            // 
            txtMK.BackColor = Color.White;
            txtMK.Font = new Font("Times New Roman", 10.8F, FontStyle.Bold);
            txtMK.ForeColor = Color.DarkGreen;
            txtMK.Location = new Point(6, 109);
            txtMK.Name = "txtMK";
            txtMK.PasswordChar = '*';
            txtMK.Size = new Size(324, 28);
            txtMK.TabIndex = 3;
            // 
            // txtTK
            // 
            txtTK.BackColor = Color.White;
            txtTK.Font = new Font("Times New Roman", 10.8F, FontStyle.Bold);
            txtTK.ForeColor = Color.DarkGreen;
            txtTK.Location = new Point(6, 51);
            txtTK.Name = "txtTK";
            txtTK.Size = new Size(324, 28);
            txtTK.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.WhiteSmoke;
            label2.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(64, 0, 0);
            label2.Location = new Point(6, 83);
            label2.Name = "label2";
            label2.Size = new Size(98, 23);
            label2.TabIndex = 1;
            label2.Text = "Mật khẩu:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.WhiteSmoke;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(64, 0, 0);
            label1.Location = new Point(6, 28);
            label1.Name = "label1";
            label1.Size = new Size(101, 23);
            label1.TabIndex = 0;
            label1.Text = "Tài khoản:";
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.White;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnDangNhap.ForeColor = Color.FromArgb(64, 0, 0);
            btnDangNhap.Location = new Point(376, 290);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(114, 33);
            btnDangNhap.TabIndex = 1;
            btnDangNhap.Text = "Đăng nhập";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // btnThoat
            // 
            btnThoat.BackColor = Color.White;
            btnThoat.FlatStyle = FlatStyle.Flat;
            btnThoat.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnThoat.ForeColor = Color.FromArgb(64, 0, 0);
            btnThoat.Location = new Point(508, 290);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(94, 33);
            btnThoat.TabIndex = 2;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = false;
            btnThoat.Click += btnThoat_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.WhiteSmoke;
            label3.Font = new Font("Times New Roman", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(64, 0, 0);
            label3.Location = new Point(360, 39);
            label3.Name = "label3";
            label3.Size = new Size(276, 32);
            label3.TabIndex = 7;
            label3.Text = "Đăng Nhập Hệ Thống";
            // 
            // pictureBox3
            // 
            pictureBox3.Dock = DockStyle.Left;
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(0, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(320, 349);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 8;
            pictureBox3.TabStop = false;
            // 
            // frmDangNhap
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            BackgroundImage = Properties.Resources.screenshot_17019183911;
            ClientSize = new Size(686, 349);
            Controls.Add(pictureBox3);
            Controls.Add(label3);
            Controls.Add(btnThoat);
            Controls.Add(btnDangNhap);
            Controls.Add(grbDangNhap);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "frmDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmDangNhap";
            grbDangNhap.ResumeLayout(false);
            grbDangNhap.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grbDangNhap;
        private Label label2;
        private Label label1;
        private CheckBox chbHienMK;
        private Button btnDangNhap;
        private Button btnThoat;
        private Label label3;
        public TextBox txtMK;
        public TextBox txtTK;
        private PictureBox pictureBox3;
    }
}