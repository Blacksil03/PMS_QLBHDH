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
    public partial class frmLoaiSanPham : Form
    {
        QLBHDongHoDbContext context = new QLBHDongHoDbContext(); // Khởi tạo biến ngữ cảnh CSDL
        bool xuLyThem = false; // Kiểm tra có nhấn vào nút Thêm hay không?
        int id; // Lấy mã loại sản phẩm (dùng cho Sửa và Xóa)
        public frmLoaiSanPham()
        {
            InitializeComponent();
        }


        private void BatTatChucNang(bool giaTri)
        {
            btnLuu.Enabled = giaTri;
            btnHuy.Enabled = giaTri;
            txtTenLoai.Enabled = giaTri;
            btnThem.Enabled = !giaTri;
            btnSua1.Enabled = !giaTri;
            btnXoa1.Enabled = !giaTri;
        }
        private void frmLoaiSanPham_Load(object sender, EventArgs e)
        {
            BatTatChucNang(false);
            List<LoaiSanPham> lsp = new List<LoaiSanPham>();
            lsp = context.LoaiSanPham.ToList();
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = lsp;
            txtTenLoai.DataBindings.Clear();
            txtTenLoai.DataBindings.Add("Text", bindingSource, "TenLoai", false, DataSourceUpdateMode.Never);
            dataGridView.DataSource = bindingSource;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            BatTatChucNang(true);
            txtTenLoai.Clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {


        }


        private void btnXoa_Click(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenLoai.Text))
                MessageBox.Show("Vui lòng nhập tên loại sản phẩm?", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (xuLyThem)
                {
                    LoaiSanPham lsp = new LoaiSanPham();
                    lsp.TenLoai = txtTenLoai.Text;
                    context.LoaiSanPham.Add(lsp);
                    context.SaveChanges();
                }
                else
                {
                    QLBHDongHoDbContext context1 = context;
                    LoaiSanPham lsp = context1.LoaiSanPham.Find(id);
                    if (lsp != null)
                    {
                        lsp.TenLoai = txtTenLoai.Text;
                        context.LoaiSanPham.Update(lsp);
                        context.SaveChanges();
                    }
                }
                frmLoaiSanPham_Load(sender, e);
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

        private void btnHuy_Click(object sender, EventArgs e)
        {

            frmLoaiSanPham_Load(sender, e);
        }

        private void btnSua1_Click(object sender, EventArgs e)
        {
            xuLyThem = false;
            BatTatChucNang(true);
            id = Convert.ToInt32(dataGridView.CurrentRow.Cells["IDD"].Value.ToString());
        }

        private void btnXoa1_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Xác nhận xóa loại sản phẩm?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                id = Convert.ToInt32(dataGridView.CurrentRow.Cells["IDD"].Value.ToString());
                LoaiSanPham hsx = context.LoaiSanPham.Find(id);
                if (hsx != null)
                {
                    context.LoaiSanPham.Remove(hsx);
                }
                context.SaveChanges();
                frmLoaiSanPham_Load(sender, e);
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
