﻿using Microsoft.Reporting.WinForms;
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

namespace QLBHDongHo.Reports
{
    public partial class frmThongKeSanPham : Form
    {
        QLBHDongHoDbContext context = new QLBHDongHoDbContext();
        QLBHDongHoDataSet.DanhSachSanPhamDataTable danhSachSanPhamDataTable = new QLBHDongHoDataSet.DanhSachSanPhamDataTable();
        string reportsFolder = Application.StartupPath.Replace("bin\\Debug\\net8.0-windows", "Reports");
        public frmThongKeSanPham()
        {
            InitializeComponent();
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
        private void frmThongKeSanPham_Load(object sender, EventArgs e)
        {
            LayHangSanXuatVaoComboBox();
            LayLoaiSanPhamVaoComboBox();

            var danhSachSanPham = context.SanPham.Select(r => new DanhSachSanPham
            {
                ID = r.ID,
                HangSanXuatID = r.HangSanXuatID,
                TenHangSanXuat = r.HangSanXuat.TenHangSanXuat,
                LoaiSanPhamID = r.LoaiSanPhamID,
                TenLoai = r.LoaiSanPham.TenLoai,
                TenSanPham = r.TenSanPham,
                DonGia = r.DonGia,
                SoLuong = r.SoLuong,
                HinhAnh = r.HinhAnh,
                MoTa = r.MoTa
            }).ToList();

            danhSachSanPhamDataTable.Clear();
            foreach (var row in danhSachSanPham)
            {
                danhSachSanPhamDataTable.AddDanhSachSanPhamRow(row.ID,
                row.HangSanXuatID,
                row.TenHangSanXuat,
                row.LoaiSanPhamID,
                row.TenLoai,
                row.TenSanPham,
                row.DonGia,
                row.SoLuong,
                row.HinhAnh,
                row.MoTa);
            }
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DanhSachSanPham";
            reportDataSource.Value = danhSachSanPhamDataTable;
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(reportDataSource);
            reportViewer.LocalReport.ReportPath = Path.Combine(reportsFolder, "rptThongKeSanPham1.rdlc");

            ReportParameter reportParameter = new ReportParameter("MoTaKetQuaHienThi", "(Tất cả sản phẩm)");
            reportViewer.LocalReport.SetParameters(reportParameter);


            reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer.ZoomMode = ZoomMode.Percent;
            reportViewer.ZoomPercent = 100;
            reportViewer.RefreshReport();



        }



        private void btnLocKetQua_Click(object sender, EventArgs e)
        {
            if (cboHangSanXuat.Text == "" && cboLoaiSanPham.Text == "")
            {

                frmThongKeSanPham_Load(sender, e);
            }
            else
            {
                var danhSachSanPham = context.SanPham.Select(r => new DanhSachSanPham
                {
                    ID = r.ID,
                    HangSanXuatID = r.HangSanXuatID,
                    TenHangSanXuat = r.HangSanXuat.TenHangSanXuat,
                    LoaiSanPhamID = r.LoaiSanPhamID,
                    TenLoai = r.LoaiSanPham.TenLoai,
                    TenSanPham = r.TenSanPham,
                    DonGia = r.DonGia,
                    SoLuong = r.SoLuong,
                    HinhAnh = r.HinhAnh,
                    MoTa = r.MoTa
                });
                string hangSanXuat = null;
                string loaiSanPham = null;
                if (cboHangSanXuat.Text != "")
                {
                    int hangSanXuatID = Convert.ToInt32(cboHangSanXuat.SelectedValue.ToString());
                    hangSanXuat = "Hãng sản xuất: " + cboHangSanXuat.Text;
                    danhSachSanPham = danhSachSanPham.Where(r => r.HangSanXuatID == hangSanXuatID);
                }
                if (cboLoaiSanPham.Text != "")
                {
                    int loaiSanPhamID = Convert.ToInt32(cboLoaiSanPham.SelectedValue.ToString());
                    loaiSanPham += "Phân loại: " + cboLoaiSanPham.Text;
                    danhSachSanPham = danhSachSanPham.Where(r => r.LoaiSanPhamID == loaiSanPhamID);
                }
                danhSachSanPhamDataTable.Clear();
                foreach (var row in danhSachSanPham)
                {
                    danhSachSanPhamDataTable.AddDanhSachSanPhamRow(row.ID,
                    row.HangSanXuatID,
                    row.TenHangSanXuat,
                    row.LoaiSanPhamID,
                    row.TenLoai,
                    row.TenSanPham,
                    row.DonGia,
                    row.SoLuong,
                    row.HinhAnh,
                    row.MoTa);
                }
                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DanhSachSanPham";
                reportDataSource.Value = danhSachSanPhamDataTable;
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(reportDataSource);
                reportViewer.LocalReport.ReportPath = Path.Combine(reportsFolder, "rptThongKeSanPham1.rdlc");
                if (cboHangSanXuat.Text == "" || cboLoaiSanPham.Text == "")
                {
                    ReportParameter reportParameter = new ReportParameter("MoTaKetQuaHienThi", "(" + hangSanXuat + "" + loaiSanPham + ")");
                    reportViewer.LocalReport.SetParameters(reportParameter);
                }
                else
                {
                    ReportParameter reportParameter = new ReportParameter("MoTaKetQuaHienThi", "(" + hangSanXuat + " - " + loaiSanPham + ")");
                    reportViewer.LocalReport.SetParameters(reportParameter);
                }
                reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer.ZoomMode = ZoomMode.Percent;
                reportViewer.ZoomPercent = 100;
                reportViewer.RefreshReport();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}

    

