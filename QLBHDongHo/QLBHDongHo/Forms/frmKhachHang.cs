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

namespace QLBHDongHo.Forms
{
    public partial class frmKhachHang : Form
    {
        QLBHDongHoDbContext context = new QLBHDongHoDbContext(); // Khởi tạo biến ngữ cảnh CSDL
        bool xuLyThem = false; // Kiểm tra có nhấn vào nút Thêm hay không?
        int id;
        public frmKhachHang()
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
            btnThem.Enabled = !giaTri;
            btnSua.Enabled = !giaTri;
            btnXoa.Enabled = !giaTri;
            btnTimKiem.Enabled = !giaTri;
            btnNhap.Enabled = !giaTri;
            btnXuat.Enabled = !giaTri;
        }
        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            List<KhachHang> kh = new List<KhachHang>();
            kh = context.KhachHang.ToList();
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = kh;
            txtHoVaTen.DataBindings.Clear();
            txtHoVaTen.DataBindings.Add("Text", bindingSource, "HoVaTen", false, DataSourceUpdateMode.Never);
            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("Text", bindingSource, "DiaChi", false, DataSourceUpdateMode.Never);
            txtDienThoai.DataBindings.Clear();
            txtDienThoai.DataBindings.Add("Text", bindingSource, "DienThoai", false, DataSourceUpdateMode.Never);

            // Tương tự cho txtDienThoai và txtDiaChi
            dataGridView.DataSource = bindingSource;
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);
            txtHoVaTen.Clear();
            txtDienThoai.Clear();
            txtDiaChi.Clear();

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            xuLyThem = false;
            BatTatChucNang(true);
            id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value.ToString());
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa khách hàng " + txtHoVaTen.Text + "?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value.ToString());
                KhachHang kh = context.KhachHang.Find(id);
                if (kh != null)
                {
                    context.KhachHang.Remove(kh);
                }
                context.SaveChanges();
                frmKhachHang_Load(sender, e);
            }

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoVaTen.Text))
                MessageBox.Show("Vui lòng nhập họ và tên khách hàng?", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (xuLyThem)
                {
                    KhachHang kh = new KhachHang();
                    kh.HoVaTen = txtHoVaTen.Text;
                    kh.DienThoai = txtDienThoai.Text;
                    kh.DiaChi = txtDiaChi.Text;
                    context.KhachHang.Add(kh);
                    context.SaveChanges();
                }
                else
                {
                    KhachHang kh = context.KhachHang.Find(id);
                    if (kh != null)
                    {
                        kh.HoVaTen = txtHoVaTen.Text;
                        kh.DienThoai = txtDienThoai.Text;
                        kh.DiaChi = txtDiaChi.Text;
                        context.KhachHang.Update(kh);
                        context.SaveChanges();
                    }
                }
                frmKhachHang_Load(sender, e);
            }

        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            frmKhachHang_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

         private void btnXuLyTimKiem_Click(object sender, EventArgs e)
         {
            btnTimKiem.Visible = true;
            List<KhachHang> kh = new List<KhachHang>();
            kh = context.KhachHang.Where(r => r.HoVaTen.Contains(txtHoVaTen.Text)
                                        && r.DiaChi.Contains(txtDiaChi.Text)
                                        && r.DienThoai.Contains(txtDienThoai.Text)).ToList();
            dataGridView.DataSource = kh;
        }
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            txtHoVaTen.Enabled = true;
            txtDienThoai.Enabled = true;
            txtDiaChi.Enabled = true;
            txtHoVaTen.Clear();
            txtDienThoai.Clear();
            txtDiaChi.Clear();

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
                                    KhachHang kh = new KhachHang();
                                    kh.HoVaTen = r["HoVaTen"].ToString();
                                    kh.DiaChi = r["DiaChi"].ToString();
                                    kh.DienThoai = r["DienThoai"].ToString();
                                    context.KhachHang.Add(kh);
                                }
                                context.SaveChanges();
                                MessageBox.Show("Đã nhập thành công " + table.Rows.Count + " dòng.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                frmKhachHang_Load(sender, e);
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
            saveFileDialog.FileName = "KhachHang_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable table = new DataTable();
                    table.Columns.AddRange(new DataColumn[4] {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("HoVaTen", typeof(string)),
                        new DataColumn("DiaChi", typeof(string)),
                        new DataColumn("DienThoai", typeof(string))
                    });
                    var khachHang = context.KhachHang.ToList();
                    if (khachHang != null)
                    {
                        foreach (var p in khachHang)
                            table.Rows.Add(p.ID, p.HoVaTen, p.DiaChi, p.DienThoai);
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "KhachHang");
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

    

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                btnSua_Click(sender, e);
            }
        }      
    }
}

