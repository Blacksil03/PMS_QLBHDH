using ClosedXML.Excel;
using QLBHDongHo.Data;
using SlugGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QLBHDongHo.Forms
{
    public partial class frmSanPham : Form
    {
        QLBHDongHoDbContext context = new QLBHDongHoDbContext(); // Khởi tạo biến ngữ cảnh CSDL
        bool xuLyThem = false; // Kiểm tra có nhấn vào nút Thêm hay không?
        int id;
        string imagesFolder = Application.StartupPath.Replace("bin\\Debug\\net8.0-windows", "Images");
        string anh ="";
        public frmSanPham()
        {
            InitializeComponent();
        }

        private void BatTatChucNang(bool giaTri)
        {
            btnLuu.Enabled = giaTri;
            btnHuyBo.Enabled = giaTri;
            cboHangSanXuat.Enabled = giaTri;
            cboLoaiSanPham.Enabled = giaTri;
            txtTenSanPham.Enabled = giaTri;
            numSoLuong.Enabled = giaTri;
            numDonGia.Enabled = giaTri;
            txtMoTa.Enabled = giaTri;
            picHinhAnh.Enabled = giaTri;
            btnThem.Enabled = !giaTri;
            btnDoiAnh.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btnNhap.Enabled = !giaTri;
            btnXuat.Enabled = !giaTri;
            btnThemAnh.Enabled = giaTri;
        }
        public void LayLoaiSanPhamVaoComboBox()
        {
            cboLoaiSanPham.DataSource = context.LoaiSanPham.ToList();
            cboLoaiSanPham.ValueMember = "ID";
            cboLoaiSanPham.DisplayMember = "TenLoai";
        }
        public void LayHangSanXuatVaoComboBox()
        {
            cboHangSanXuat.DataSource = context.HangSanXuat.ToList();
            cboHangSanXuat.ValueMember = "ID";
            cboHangSanXuat.DisplayMember = "TenHangSanXuat";
        }
        private void frmSanPham_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            LayLoaiSanPhamVaoComboBox();
            LayHangSanXuatVaoComboBox();
            dataGridView.AutoGenerateColumns = false;
            List<DanhSachSanPham> sp = new List<DanhSachSanPham>();
            sp = context.SanPham.Select(r => new DanhSachSanPham
            {
                ID = r.ID,
                LoaiSanPhamID = r.LoaiSanPhamID,
                TenLoai = r.LoaiSanPham.TenLoai,
                HangSanXuatID = r.HangSanXuatID,
                TenHangSanXuat = r.HangSanXuat.TenHangSanXuat,
                TenSanPham = r.TenSanPham,
                SoLuong = r.SoLuong,
                DonGia = r.DonGia,
                HinhAnh = r.HinhAnh
            }).ToList();
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = sp;
            cboLoaiSanPham.DataBindings.Clear();
            cboLoaiSanPham.DataBindings.Add("SelectedValue", bindingSource, "LoaiSanPhamID", false, DataSourceUpdateMode.Never);
            // Tương tự đối với cboHangSanXuat
            cboHangSanXuat.DataBindings.Clear();
            cboHangSanXuat.DataBindings.Add("SelectedValue", bindingSource, "HangSanXuatID", false, DataSourceUpdateMode.Never);
            txtTenSanPham.DataBindings.Clear();
            txtTenSanPham.DataBindings.Add("Text", bindingSource, "TenSanPham", false, DataSourceUpdateMode.Never);
            // Tương tự đối với txtMoTa
            txtMoTa.DataBindings.Clear();
            txtMoTa.DataBindings.Add("Text", bindingSource, "MoTa", false, DataSourceUpdateMode.Never);
            numSoLuong.DataBindings.Clear();
            numSoLuong.DataBindings.Add("Value", bindingSource, "SoLuong", false, DataSourceUpdateMode.Never);
            // Tương tự đối với numDonGia
            picHinhAnh.DataBindings.Clear();
            Binding hinhAnh = new Binding("ImageLocation", bindingSource, "HinhAnh");
            hinhAnh.Format += (s, e) =>
            {
                e.Value = Path.Combine(imagesFolder, e.Value.ToString());
            };
            picHinhAnh.DataBindings.Add(hinhAnh);
            dataGridView.DataSource = bindingSource;
        }


        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "HinhAnh")
            {
                Image image = Image.FromFile(Path.Combine(imagesFolder, e.Value.ToString()));
                image = new Bitmap(image, 24, 24);
                e.Value = image;
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);
            cboLoaiSanPham.Text = "";
            cboHangSanXuat.Text = "";
            txtTenSanPham.Clear();
            txtMoTa.Clear();
            numSoLuong.Value = 0;
            numDonGia.Value = 0;
            picHinhAnh.Image = null;
            btnThemAnh.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            xuLyThem = false;
            BatTatChucNang(true);
            id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value.ToString());
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cboLoaiSanPham.Text))
                MessageBox.Show("Vui lòng chọn loại sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(cboHangSanXuat.Text))
                MessageBox.Show("Vui lòng chọn hãng sản xuất.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (string.IsNullOrWhiteSpace(txtTenSanPham.Text))
                MessageBox.Show("Vui lòng nhập tên sản phẩm.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (numSoLuong.Value <= 0)
                MessageBox.Show("Số lượng phải lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (numDonGia.Value <= 0)
                MessageBox.Show("Đơn giá sản phẩm phải lớn hơn 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (xuLyThem)
                {
                    SanPham sp = new SanPham();
                    sp.TenSanPham = txtTenSanPham.Text;
                    sp.MoTa = txtMoTa.Text;

                    sp.HangSanXuatID = Convert.ToInt32(cboHangSanXuat.SelectedValue.ToString());
                    sp.LoaiSanPhamID = Convert.ToInt32(cboLoaiSanPham.SelectedValue.ToString());
                    sp.DonGia = Convert.ToInt32(numDonGia.Value);
                    sp.SoLuong = Convert.ToInt32(numSoLuong.Value);
                    if (anh == "")
                    {
                        sp.HinhAnh = "noimage.jpg";
                    }
                    else sp.HinhAnh = anh;
                    context.SanPham.Add(sp);
                    context.SaveChanges();
                }
                else
                {
                    SanPham sp = context.SanPham.Find(id);
                    if (sp != null)
                    
                        sp.TenSanPham = txtTenSanPham.Text;
                        sp.MoTa = txtMoTa.Text;
                        sp.HangSanXuatID = Convert.ToInt32(cboHangSanXuat.SelectedValue.ToString());
                        sp.LoaiSanPhamID = Convert.ToInt32(cboLoaiSanPham.SelectedValue.ToString());
                        sp.DonGia = Convert.ToInt32(numDonGia.Value);
                        sp.SoLuong = Convert.ToInt32(numSoLuong.Value);
                        context.SanPham.Update(sp);
                        context.SaveChanges();
                    
                }
                frmSanPham_Load(sender, e);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa sản phẩm " + txtTenSanPham.Text + "?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value.ToString());
                SanPham sp = context.SanPham.Find(id);
                if (sp != null)
                {
                    context.SanPham.Remove(sp);
                }
                context.SaveChanges();
                frmSanPham_Load(sender, e);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            frmSanPham_Load(sender, e);
        }

        private void btnDoiAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Cập nhật hình ảnh sản phẩm";
            openFileDialog.Filter = "Tập tin hình ảnh|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                string ext = Path.GetExtension(openFileDialog.FileName);
                string fileSavePath = Path.Combine(imagesFolder, fileName.GenerateSlug() + ext);
                File.Copy(openFileDialog.FileName, fileSavePath, true);
                id = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value.ToString());
                SanPham sp = context.SanPham.Find(id);
                sp.HinhAnh = fileName.GenerateSlug() + ext;
                context.SanPham.Update(sp);
                context.SaveChanges();
                frmSanPham_Load(sender, e);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                this.Close();
            }
        }
        private void btnXuLyTimKiem_click(object sender, EventArgs e)
        {
            string selectedHangSanXuat = cboHangSanXuat.Text;
            string selectedLoaiSanPham = cboLoaiSanPham.Text;

            string tenHangSanXuat = cboHangSanXuat.Text.Trim();
            string tenLoaiSanPham = cboLoaiSanPham.Text.Trim();
            string tenSanPham = txtTenSanPham.Text.Trim();

            // Truy vấn dữ liệu dựa trên các điều kiện
            List<SanPham> sp = context.SanPham.ToList();

            if (!string.IsNullOrWhiteSpace(tenHangSanXuat))
            {
                sp = sp.Where(r => r.HangSanXuat.TenHangSanXuat.Contains(tenHangSanXuat)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(tenLoaiSanPham))
            {
                sp = sp.Where(r => r.LoaiSanPham.TenLoai.Contains(tenLoaiSanPham)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(tenSanPham))
            {
                sp = sp.Where(r => r.TenSanPham.Contains(tenSanPham)).ToList();
            }

            // Hiển thị kết quả tìm kiếm trên DataGridView
            dataGridView.DataSource = sp;
            cboHangSanXuat.Text = selectedHangSanXuat;
            cboLoaiSanPham.Text = selectedLoaiSanPham;

        }
        // Xem 2 cái btnTimKiem_   tìm kiếm coi cái nào chạy được
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            cboHangSanXuat.Enabled = true;
            cboLoaiSanPham.Enabled = true;
            txtTenSanPham.Enabled = true;

            cboHangSanXuat.Text = " ";
            cboLoaiSanPham.Text = " ";
            txtTenSanPham.Clear();


            btnXuLyTimKiem.Visible = true;
            btnTimKiemm.Visible = false;
        }




        private void btnNhap_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Nhập dữ liệu từ tập tin Excel";
            openFileDialog.Filter = "Tập tin Excel|*.xls;*.xlsx";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable table = new DataTable();
                    using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                    {
                        IXLWorksheet worksheet = workbook.Worksheet(1);
                        bool firstRow = true;
                        string readRange = "1:1";
                        foreach (IXLRow row in worksheet.RowsUsed())
                        {
                            // Đọc dòng tiêu đề (dòng đầu tiên)
                            if (firstRow)
                            {
                                readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);
                                foreach (IXLCell cell in row.Cells(readRange))
                                    table.Columns.Add(cell.Value.ToString());
                                firstRow = false;
                            }
                            else // Đọc các dòng nội dung (các dòng tiếp theo)
                            {
                                table.Rows.Add();
                                int cellIndex = 0;
                                foreach (IXLCell cell in row.Cells(readRange))
                                {
                                    table.Rows[table.Rows.Count - 1][cellIndex] = cell.Value.ToString();
                                    cellIndex++;
                                }
                            }
                        }
                        if (table.Rows.Count > 0)
                        {
                            foreach (DataRow r in table.Rows)
                            {
                                SanPham sanPham = new SanPham();
                                sanPham.LoaiSanPhamID = Convert.ToInt32(r["LoaiSanPhamID"]);
                                sanPham.HangSanXuatID = Convert.ToInt32(r["HangSanXuatID"]);
                                sanPham.TenSanPham = r["TenSanPham"].ToString();
                                sanPham.SoLuong = Convert.ToInt32(r["SoLuong"]);
                                sanPham.DonGia = Convert.ToInt32(r["DonGia"]);
                                sanPham.MoTa = r["MoTa"].ToString();
                                sanPham.HinhAnh = r["HinhAnh"].ToString();
                                context.SanPham.Add(sanPham);
                            }
                            context.SaveChanges();
                            MessageBox.Show("Đã nhập thành công " + table.Rows.Count + " dòng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            frmSanPham_Load(sender, e);
                        }
                        if (firstRow)
                            MessageBox.Show("Tập tin Excel rỗng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất dữ liệu ra tập tin Excel";
            saveFileDialog.Filter = "Tập tin Excel|*.xls;*.xlsx";
            saveFileDialog.FileName = "SanPham_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable table = new DataTable();
                    table.Columns.AddRange(new DataColumn[8] {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("HangSanXuatID", typeof(int)),
                        new DataColumn("LoaiSanPhamID", typeof(int)),
                        new DataColumn("TenSanPham", typeof(string)),
                        new DataColumn("DonGia", typeof(int)),
                        new DataColumn("SoLuong", typeof(int)),
                        new DataColumn("HinhAnh", typeof(string)),
                        new DataColumn("MoTa", typeof(string))
                });
                    var sanPham = context.SanPham.ToList();
                    if (sanPham != null)
                    {
                        foreach (var p in sanPham)
                            table.Rows.Add(p.ID,
                                p.HangSanXuatID,
                                p.LoaiSanPhamID,
                                p.TenSanPham,
                                p.DonGia,
                                p.SoLuong,
                                p.HinhAnh,
                                p.MoTa);
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "SanPham");
                        sheet.Columns().AdjustToContents();
                        wb.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Đã xuất dữ liệu ra tập tin Excel thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnThemAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn của tệp được chọn và hiển thị nó
                string selectedImagePath = openFileDialog.FileName;
                anh = selectedImagePath;

                // Load ảnh lên PictureBox
                try
                {
                    // Tạo một đối tượng Image từ đường dẫn đã chọn
                    Image image = Image.FromFile(selectedImagePath);

                    // Gán ảnh cho PictureBox
                    picHinhAnh.Image = image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }




}

