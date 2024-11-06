using ClosedXML.Excel;
using QLBHDongHo.Data;
using QLBHDongHo.Reports;
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
    public partial class frmHoaDon : Form
    {
        QLBHDongHoDbContext context = new QLBHDongHoDbContext(); // Khởi tạo biến ngữ cảnh CSDL
        int id;

        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            List<DanhSachHoaDon> hd = new List<DanhSachHoaDon>();
            hd = context.HoaDon.Select(r => new DanhSachHoaDon
            {

                ID = r.ID,
                NhanVienID = r.NhanVienID,
                HoVaTenNhanVien = r.NhanVien.HoVaTen,
                KhachHangID = r.KhachHangID,
                HoVaTenKhachHang = r.KhachHang.HoVaTen,
                NgayLap = r.NgayLap,
                GhiChuHoaDon = r.GhiChuHoaDon,
                TongTienHoaDon = r.HoaDon_ChiTiet.Sum(r => r.DonGiaBan * r.SoLuongBan),
                XemChiTiet = "Xem chi tiết"
            }).ToList();
            dataGridView.DataSource = hd;
        }

        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            using (frmHoaDon_ChiTiet chiTiet = new frmHoaDon_ChiTiet())
            {
                chiTiet.ShowDialog();
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value.ToString());
            using (frmInHoaDon inHoaDon = new frmInHoaDon(id))
            {
                inHoaDon.ShowDialog();
            }

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value.ToString());
            using (frmHoaDon_ChiTiet chiTiet = new frmHoaDon_ChiTiet(id))
            {
                chiTiet.ShowDialog();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xác nhận xóa hóa đơn này ?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                id = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value.ToString());
                HoaDon sp = context.HoaDon.Find(id);
                if (sp != null)
                {
                    context.HoaDon.Remove(sp);
                }
                context.SaveChanges();
                frmHoaDon_Load(sender, e);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thoát", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                this.Close();
            }
            return;
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Xuất dữ liệu ra tập tin Excel";
            saveFileDialog.Filter = "Tập tin Excel|*.xls;*.xlsx";
            saveFileDialog.FileName = "HoaDon_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //xu ly sheet HD
                    DataTable tableHoaDon = new DataTable();
                    tableHoaDon.Columns.AddRange(new DataColumn[4] {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("NhanVien", typeof(int)),
                        new DataColumn("KhachHang", typeof(int)),
                        new DataColumn("NgayTao", typeof(DateTime))
                    });
                    var hoaDon = context.HoaDon.ToList();
                    if (hoaDon != null)
                    {
                        foreach (var p in hoaDon)
                            tableHoaDon.Rows.Add(p.ID, p.NhanVienID, p.KhachHangID, p.NgayLap);
                    }

                    //xu ly sheet HD_CT
                    DataTable tableHoaDonChiTiet = new DataTable();
                    tableHoaDonChiTiet.Columns.AddRange(new DataColumn[5] {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("HoaDonID", typeof(int)),
                        new DataColumn("SanPhamID", typeof(int)),
                        new DataColumn("SoLuong", typeof(int)),
                        new DataColumn("DonGia", typeof(decimal))
                    });
                    var hoaDonChiTiet = context.HoaDon_ChiTiet.ToList();
                    if (hoaDonChiTiet != null)
                    {
                        foreach (var p in hoaDonChiTiet)
                            tableHoaDonChiTiet.Rows.Add(p.ID, p.HoaDonID, p.SanPhamID, p.SoLuongBan, p.DonGiaBan);
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet1 = wb.Worksheets.Add(tableHoaDon, "HoaDon");
                        sheet1.Columns().AdjustToContents();

                        var sheet2 = wb.Worksheets.Add(tableHoaDonChiTiet, "HoaDon_ChiTiet");
                        sheet2.Columns().AdjustToContents();

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

        private void frmHoaDon_Activated(object sender, EventArgs e)
        {
            frmHoaDon_Load(sender, e);
        }
    }

}

