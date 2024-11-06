namespace QLBHDongHo.Forms
{
    partial class frmLoaiSanPham
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
            groupBox1 = new GroupBox();
            btnXoa1 = new Button();
            btnSua1 = new Button();
            txtTenLoai = new TextBox();
            btnThoat = new Button();
            btnHuy = new Button();
            btnLuu = new Button();
            btnThem = new Button();
            label1 = new Label();
            groupBox2 = new GroupBox();
            groupBox3 = new GroupBox();
            dataGridView = new DataGridView();
            IDD = new DataGridViewTextBoxColumn();
            TenLoai = new DataGridViewTextBoxColumn();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.BackgroundImage = Properties.Resources.screenshot_17019183911;
            groupBox1.Controls.Add(btnXoa1);
            groupBox1.Controls.Add(btnSua1);
            groupBox1.Controls.Add(txtTenLoai);
            groupBox1.Controls.Add(btnThoat);
            groupBox1.Controls.Add(btnHuy);
            groupBox1.Controls.Add(btnLuu);
            groupBox1.Controls.Add(btnThem);
            groupBox1.Controls.Add(label1);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(3, 26);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1113, 159);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Thông Tin Loại Sản Phẩm";
            // 
            // btnXoa1
            // 
            btnXoa1.FlatStyle = FlatStyle.Flat;
            btnXoa1.Location = new Point(576, 101);
            btnXoa1.Name = "btnXoa1";
            btnXoa1.Size = new Size(78, 37);
            btnXoa1.TabIndex = 9;
            btnXoa1.Text = "Xóa";
            btnXoa1.UseVisualStyleBackColor = true;
            btnXoa1.Click += btnXoa1_Click;
            // 
            // btnSua1
            // 
            btnSua1.FlatStyle = FlatStyle.Flat;
            btnSua1.Location = new Point(492, 101);
            btnSua1.Name = "btnSua1";
            btnSua1.Size = new Size(78, 37);
            btnSua1.TabIndex = 9;
            btnSua1.Text = "Sửa";
            btnSua1.UseVisualStyleBackColor = true;
            btnSua1.Click += btnSua1_Click;
            // 
            // txtTenLoai
            // 
            txtTenLoai.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            txtTenLoai.Location = new Point(313, 46);
            txtTenLoai.Name = "txtTenLoai";
            txtTenLoai.Size = new Size(710, 30);
            txtTenLoai.TabIndex = 8;
            // 
            // btnThoat
            // 
            btnThoat.FlatStyle = FlatStyle.Flat;
            btnThoat.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnThoat.Location = new Point(828, 101);
            btnThoat.Name = "btnThoat";
            btnThoat.Size = new Size(78, 37);
            btnThoat.TabIndex = 7;
            btnThoat.Text = "Thoát";
            btnThoat.UseVisualStyleBackColor = true;
            btnThoat.Click += btnThoat_Click;
            // 
            // btnHuy
            // 
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnHuy.Location = new Point(744, 101);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(78, 37);
            btnHuy.TabIndex = 6;
            btnHuy.Text = "Hủy bỏ";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            // 
            // btnLuu
            // 
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnLuu.ForeColor = SystemColors.HotTrack;
            btnLuu.Location = new Point(660, 101);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(78, 37);
            btnLuu.TabIndex = 5;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = true;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnThem
            // 
            btnThem.FlatStyle = FlatStyle.Flat;
            btnThem.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            btnThem.Location = new Point(408, 101);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(78, 37);
            btnThem.TabIndex = 2;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            label1.Location = new Point(151, 49);
            label1.Name = "label1";
            label1.Size = new Size(167, 23);
            label1.TabIndex = 0;
            label1.Text = "Tên loại sản phẩm:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(groupBox3);
            groupBox2.Controls.Add(groupBox1);
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            groupBox2.Location = new Point(0, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1119, 596);
            groupBox2.TabIndex = 3;
            groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.BackgroundImage = Properties.Resources.screenshot_17019183911;
            groupBox3.Controls.Add(dataGridView);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            groupBox3.Location = new Point(3, 185);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(1113, 408);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "Danh sách loại sản phẩm";
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { IDD, TenLoai });
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.Location = new Point(3, 26);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.ReadOnly = true;
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(1107, 379);
            dataGridView.TabIndex = 0;
            dataGridView.CellContentClick += dataGridView_CellContentClick;
            // 
            // IDD
            // 
            IDD.DataPropertyName = "ID";
            IDD.HeaderText = "ID";
            IDD.MinimumWidth = 6;
            IDD.Name = "IDD";
            IDD.ReadOnly = true;
            // 
            // TenLoai
            // 
            TenLoai.DataPropertyName = "TenLoai";
            TenLoai.HeaderText = "Tên loại sản sản phẩm";
            TenLoai.MinimumWidth = 6;
            TenLoai.Name = "TenLoai";
            TenLoai.ReadOnly = true;
            // 
            // frmLoaiSanPham
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1119, 596);
            Controls.Add(groupBox2);
            Name = "frmLoaiSanPham";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmLoaiSanPham";
            Load += frmLoaiSanPham_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button btnThoat;
        private Button btnHuy;
        private Button btnLuu;
        private Button btnThem;
        private Label label1;
        private GroupBox groupBox2;
        private DataGridView dataGridView;
        private GroupBox groupBox3;
        private TextBox txtTenLoai;
        private Button btnXoa1;
        private Button btnSua1;
        private DataGridViewTextBoxColumn IDD;
        private DataGridViewTextBoxColumn TenLoai;
    }
}