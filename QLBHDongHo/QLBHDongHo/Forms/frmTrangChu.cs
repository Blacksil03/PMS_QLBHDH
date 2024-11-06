using QLBHDongHo.Data;
using QLBHDongHo.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BC = BCrypt.Net.BCrypt;
namespace QLBHDongHo.Forms
{
    public partial class frmTrangChu : Form
    {
        private Form currentFormChinh;
        public frmTrangChu()
        {
            InitializeComponent();
        }
        private void OpenChinhFrom(Form ChinhFrom)
        {
            if (currentFormChinh != null)
            {
                currentFormChinh.Close();
            }

            currentFormChinh = ChinhFrom;
            ChinhFrom.TopLevel = false;
            ChinhFrom.FormBorderStyle = FormBorderStyle.None;
            ChinhFrom.Dock = DockStyle.Fill;
            panelChinh.Controls.Add(ChinhFrom);
            panelChinh.Tag = ChinhFrom;
            ChinhFrom.BringToFront();
            ChinhFrom.Show();
        }

        QLBHDongHoDbContext context = new QLBHDongHoDbContext(); // Khởi tạo biến ngữ cảnh CSDL
        frmLoaiSanPham loaiSanPham = null;
        frmHangSanXuat hangSanXuat = null;
        frmSanPham sanPham = null;
        frmKhachHang khachHang = null;
        frmNhanVien nhanVien = null;
        frmHoaDon hoaDon = null;
        frmDangNhap dangNhap = null;
        frmThongKeSanPham thongKeSanPham = null;
        frmThongKeDoanhThu thongKeDoanhThu = null;
        string hoVaTenNhanVien = "";


        private void DangNhap()
        {
        LamLai:
            if (dangNhap == null || dangNhap.IsDisposed)
                dangNhap = new frmDangNhap();
            if (dangNhap.ShowDialog() == DialogResult.OK)
            {
                string tenDangNhap = dangNhap.txtTK.Text; ;
                string matKhau = dangNhap.txtMK.Text;
                if (tenDangNhap.Trim() == "")
                {
                    MessageBox.Show("Tên đăng nhập không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dangNhap.txtTK.Focus();
                    goto LamLai;
                }
                else if (matKhau.Trim() == "")
                {
                    MessageBox.Show("Mật khẩu không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dangNhap.txtMK.Focus();
                    goto LamLai;
                }
                else
                {
                    var nhanVien = context.NhanVien.Where(r => r.TenDangNhap == tenDangNhap).SingleOrDefault();
                    if (nhanVien == null)
                    {
                        MessageBox.Show("Tên đăng nhập không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dangNhap.txtTK.Focus();
                        goto LamLai;
                    }
                    else
                    {
                        if (BC.Verify(matKhau, nhanVien.MatKhau))
                        {
                            hoVaTenNhanVien = nhanVien.HoVaTen;
                            if (nhanVien.QuyenHan == true)
                                QuyenQuanLy();
                            else if (nhanVien.QuyenHan == false)
                                QuyenNhanVien();
                            else
                                ChuaDangNhap();
                        }
                        else
                        {
                            MessageBox.Show("Mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dangNhap.txtMK.Focus();
                            goto LamLai;
                        }
                    }
                }
            }
        }
        public void ChuaDangNhap()
        {
            // Sáng đăng nhập
            btnDangNhap.Enabled = true;
            // Mờ tất cả
            btnDangXuat.Enabled = false;
            btnDoiMatKhau.Enabled = false;
            btnLoaiSanPham.Enabled = false;
            btnHangSanXuat.Enabled = false;
            btnSanPham.Enabled = false;
            btnKhachHang.Enabled = false;
            btnNhanVien.Enabled = false;
            btnHoaDon.Enabled = false;
            btnHoaDonChiTiet.Enabled = false;
            btnThongKeSanPham.Enabled = false;
            btnThongKeSanPham.Enabled = false;
            // Hiển thị thông tin trên thanh trạng thái
            txtTrangThai1.Text = "Chưa đăng nhập.";


        }



        public void QuyenQuanLy()
        {
            // Mờ đăng nhập
            btnDangNhap.Enabled = true;
            // Mờ các chức năng quản lý không được phép
            // Sáng đăng xuất và các chức năng quản lý được phép
            btnDangXuat.Enabled = true;
            btnDoiMatKhau.Enabled = true;
            btnLoaiSanPham.Enabled = true;
            btnHangSanXuat.Enabled = true;
            btnSanPham.Enabled = true;
            btnKhachHang.Enabled = true;
            btnNhanVien.Enabled = true;
            btnHoaDon.Enabled = true;
            btnHoaDonChiTiet.Enabled = true;
            btnThongKeDoanhThu.Enabled = true;
            btnThongKeSanPham.Enabled = true;
            // Hiển thị thông tin trên thanh trạng thái
            txtTrangThai1.Text = "Quản lý: " + hoVaTenNhanVien;
        }


        public void QuyenNhanVien()
        {
            // Mờ đăng nhập
            btnDangNhap.Enabled = true;
            // Mờ các chức năng nhân viên không được phép
            btnLoaiSanPham.Enabled = false;
            btnHangSanXuat.Enabled = false;
            btnSanPham.Enabled = false;
            btnNhanVien.Enabled = false;
            // Sáng đăng xuất và các chức năng nhân viên được phép
            btnDangXuat.Enabled = true;
            btnDoiMatKhau.Enabled = true;
            btnKhachHang.Enabled = true;
            btnHoaDon.Enabled = true;
            btnHoaDonChiTiet.Enabled = true;
            btnThongKeDoanhThu.Enabled = true;
            btnThongKeSanPham.Enabled = true;
            // Hiển thị thông tin trên thanh trạng thái
            txtTrangThai1.Text = "Nhân viên: " + hoVaTenNhanVien;
        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            ChuaDangNhap();
            DangNhap();
        }

       

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangNhap();
            this.Show();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát chương trình không?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                {
                    Application.Exit();
                }
                return;
            }
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new frmKhachHang());
            txtHienThi1.Text = btnKhachHang.Text;
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new frmNhanVien());
            txtHienThi1.Text = btnNhanVien.Text;
        }

        private void btnLoaiSanPham_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new frmLoaiSanPham());
            txtHienThi1.Text = btnLoaiSanPham.Text;
        }

        private void btnHangSanXuat_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new frmHangSanXuat());
            txtHienThi1.Text = btnHangSanXuat.Text;
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new frmSanPham());
            txtHienThi1.Text = btnSanPham.Text;
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new frmHoaDon());
            txtHienThi1.Text = btnHoaDon.Text;
        }

        private void btnHoaDonChiTiet_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new frmHoaDon_ChiTiet());
            txtHienThi1.Text = btnHoaDonChiTiet.Text;
        }

        private void btnThongKeSanPham_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new frmThongKeSanPham());
            txtHienThi1.Text = btnThongKeSanPham.Text;
        }

        private void btnThongKeDoanhThu_Click(object sender, EventArgs e)
        {
            OpenChinhFrom(new frmThongKeDoanhThu());
            txtHienThi1.Text = btnThongKeDoanhThu.Text;
        }
    }
}
