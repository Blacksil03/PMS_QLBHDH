﻿using ClosedXML.Excel;
using QLBHDongHo.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BC = BCrypt.Net.BCrypt;
namespace QLBHDongHo.Forms
{
    public partial class frmNhanVien : Form
    {
        QLBHDongHoDbContext context = new QLBHDongHoDbContext(); // Khởi tạo biến ngữ cảnh CSDL
        bool xuLyThem = false; // Kiểm tra có nhấn vào nút Thêm hay không?
        int id;
        public frmNhanVien()
        {
            InitializeComponent();
        }
        private void BatTatChucNang(bool giaTri)
        {
            btnLuu.Enabled = giaTri;
            btnHuyBo.Enabled = giaTri;
            txtHoVaTen.Enabled = giaTri;
            txtDienThoai.Enabled = giaTri;
            txtDiaChi.Enabled = giaTri;
            txtTenDangNhap.Enabled = giaTri;
            txtMatKhau.Enabled = giaTri;
            cboQuyenHan.Enabled = giaTri;
            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btnTimKiem.Enabled = !giaTri;
            btnNhap.Enabled = !giaTri;
            btnXuat.Enabled = !giaTri;
        }
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            dataGridView.AutoGenerateColumns = false;
            List<NhanVien> nv = new List<NhanVien>();
            nv = context.NhanVien.ToList();
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = nv;
            txtHoVaTen.DataBindings.Clear();
            txtHoVaTen.DataBindings.Add("Text", bindingSource, "HoVaTen", false, DataSourceUpdateMode.Never);
            txtDienThoai.DataBindings.Clear();
            txtDienThoai.DataBindings.Add("Text", bindingSource, "DienThoai", false, DataSourceUpdateMode.Never);
            txtTenDangNhap.DataBindings.Clear();
            txtTenDangNhap.DataBindings.Add("Text", bindingSource, "TenDangNhap", false, DataSourceUpdateMode.Never);
            txtMatKhau.DataBindings.Clear();
            txtMatKhau.DataBindings.Add("Text", bindingSource, "MatKhau", false, DataSourceUpdateMode.Never);
            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("Text", bindingSource, "DiaChi", false, DataSourceUpdateMode.Never);
            // Tương tự đối với txtDienThoai, txtDiaChi, txtTenDangNhap
            cboQuyenHan.DataBindings.Clear();
            cboQuyenHan.DataBindings.Add("SelectedIndex", bindingSource, "QuyenHan", false, DataSourceUpdateMode.Never);
            dataGridView.DataSource = bindingSource;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);
            txtHoVaTen.Clear();
            txtDienThoai.Clear();
            txtDiaChi.Clear();
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            cboQuyenHan.Text = "";

        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            xuLyThem = false;
            BatTatChucNang(true);
            id = Convert.ToInt32(dataGridView.CurrentRow.Cells["IDD"].Value.ToString());
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoVaTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên nhân viên?", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTenDangNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập?", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cboQuyenHan.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn quyền hạn cho nhân viên?", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool quyenHan = (cboQuyenHan.SelectedItem as string) == "Quản lý"; // Giả sử "Quản lý" là true, "Nhân viên" là false



            if (xuLyThem)
            {
                if (string.IsNullOrWhiteSpace(txtMatKhau.Text))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu?", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                NhanVien nv = new NhanVien();
                nv.HoVaTen = txtHoVaTen.Text;
                nv.DienThoai = txtDienThoai.Text;
                nv.DiaChi = txtDiaChi.Text;
                nv.TenDangNhap = txtTenDangNhap.Text;
                nv.MatKhau = BC.HashPassword(txtMatKhau.Text); // Mã hóa mật khẩu
                nv.QuyenHan = quyenHan;

                context.NhanVien.Add(nv);
                context.SaveChanges();

                MessageBox.Show("Thêm nhân viên thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmNhanVien_Load(sender, e); // Tải lại dữ liệu sau khi thêm mới
            }

            else
                {
                    NhanVien nv = context.NhanVien.Find(id);
                    if (nv != null)
                    {
                        nv.HoVaTen = txtHoVaTen.Text;
                        nv.DienThoai = txtDienThoai.Text;
                        nv.DiaChi = txtDiaChi.Text;
                        nv.TenDangNhap = txtTenDangNhap.Text;
                        nv.QuyenHan = quyenHan;
                        context.NhanVien.Update(nv);
                        if (string.IsNullOrEmpty(txtMatKhau.Text))
                            context.Entry(nv).Property(x => x.MatKhau).IsModified = false; // Giữ nguyên mật khẩu cũ
                        else
                            nv.MatKhau = BC.HashPassword(txtMatKhau.Text); // Cập nhật mật khẩu mới
                        context.SaveChanges();
                    }
                }
                // Sau khi lưu, làm mới lại dữ liệu trên DataGridView
                frmNhanVien_Load(sender, e);
            }
        

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa nhân viên " + txtHoVaTen.Text + "?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                id = Convert.ToInt32(dataGridView.CurrentRow.Cells["IDD"].Value.ToString());
                NhanVien nv = context.NhanVien.Find(id);
                if (nv != null)
                {
                    context.NhanVien.Remove(nv);
                }
                context.SaveChanges();
                frmNhanVien_Load(sender, e);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            frmNhanVien_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                this.Close();
            }
        }

        
        private void btnXuLyTimKiem_Click(object sender, EventArgs e)
        {
            btnTimKiem.Visible = true;
            List<NhanVien> nv = new List<NhanVien>();
            nv = context.NhanVien.Where(r => r.HoVaTen.Contains(txtHoVaTen.Text)
                                             && r.DiaChi.Contains(txtDiaChi.Text)
                                             && r.TenDangNhap.Contains(txtTenDangNhap.Text)
                                             && r.DienThoai.Contains(txtDienThoai.Text)).ToList();
            dataGridView.DataSource = nv;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            txtHoVaTen.Enabled = true;
            txtDienThoai.Enabled = true;
            txtDiaChi.Enabled = true;
            txtTenDangNhap.Enabled = true;
            txtHoVaTen.Clear();
            txtDienThoai.Clear();
            txtDiaChi.Clear();
            txtTenDangNhap.Clear();

            btnXuLyTimKiem.Visible = true;
            btnTimKiem.Visible = false;
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
                                    NhanVien nv = new NhanVien();
                                    nv.HoVaTen = r["HoVaTen"].ToString();
                                    nv.DienThoai = r["DienThoai"].ToString();
                                    nv.DiaChi = r["DiaChi"].ToString();
                                    nv.TenDangNhap = r["TenDangNhap"].ToString();
                                    nv.MatKhau = r["MatKhau"].ToString();
                                    nv.QuyenHan = Convert.ToBoolean(r["QuyenHan"].ToString());

                                    context.NhanVien.Add(nv);
                                }
                                context.SaveChanges();
                                MessageBox.Show("Đã nhập thành công " + table.Rows.Count + " dòng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //frmLoaiSanPham_Load(sender, e);
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
            saveFileDialog.FileName = "NhanVien_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable table = new DataTable();
                    table.Columns.AddRange(new DataColumn[7] {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("HoVaTen", typeof(string)),
                        new DataColumn("DienThoai", typeof(string)),
                        new DataColumn("DiaChi", typeof(string)),
                        new DataColumn("TenDangNhap", typeof(string)),
                        new DataColumn("MatKhau", typeof(string)),
                        new DataColumn("QuyenHan", typeof(bool))
});
                    var nhanVien = context.NhanVien.ToList();
                    if (nhanVien != null)
                    {
                        foreach (var p in nhanVien)
                            table.Rows.Add(p.ID, p.HoVaTen, p.DienThoai, p.DiaChi, p.TenDangNhap, p.MatKhau, p.QuyenHan);
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "NhanVien");
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
    }

}

     
   
