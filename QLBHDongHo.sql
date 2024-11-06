USE QLBHDongHo;

-- Xóa dữ liệu các bảng cũ (Nếu có)
DELETE FROM HoaDon_ChiTiet;
DELETE FROM HoaDon;
DELETE FROM SanPham;
DELETE FROM HangSanXuat;
DELETE FROM LoaiSanPham;
DELETE FROM NhanVien;
DELETE FROM KhachHang;

-- Reset identity
IF EXISTS (SELECT * FROM sys.identity_columns WHERE OBJECT_NAME(OBJECT_ID) = 'HoaDon_ChiTiet' AND last_value IS NOT NULL)
	DBCC CHECKIDENT('HoaDon_ChiTiet', RESEED, 0);

IF EXISTS (SELECT * FROM sys.identity_columns WHERE OBJECT_NAME(OBJECT_ID) = 'HoaDon' AND last_value IS NOT NULL)
	DBCC CHECKIDENT('HoaDon', RESEED, 0);

IF EXISTS (SELECT * FROM sys.identity_columns WHERE OBJECT_NAME(OBJECT_ID) = 'SanPham' AND last_value IS NOT NULL)
	DBCC CHECKIDENT('SanPham', RESEED, 0);

IF EXISTS (SELECT * FROM sys.identity_columns WHERE OBJECT_NAME(OBJECT_ID) = 'HangSanXuat' AND last_value IS NOT NULL)
	DBCC CHECKIDENT('HangSanXuat', RESEED, 0);

IF EXISTS (SELECT * FROM sys.identity_columns WHERE OBJECT_NAME(OBJECT_ID) = 'LoaiSanPham' AND last_value IS NOT NULL)
	DBCC CHECKIDENT('LoaiSanPham', RESEED, 0);

IF EXISTS (SELECT * FROM sys.identity_columns WHERE OBJECT_NAME(OBJECT_ID) = 'NhanVien' AND last_value IS NOT NULL)
	DBCC CHECKIDENT('NhanVien', RESEED, 0);
	
IF EXISTS (SELECT * FROM sys.identity_columns WHERE OBJECT_NAME(OBJECT_ID) = 'KhachHang' AND last_value IS NOT NULL)
	DBCC CHECKIDENT('KhachHang', RESEED, 0);

SET IDENTITY_INSERT HangSanXuat ON
INSERT INTO HangSanXuat(ID, TenHangSanXuat) VALUES
(1, N'CTY CP nông dược Hai'),
(2, N'CTY CP nông dược Nhật Việt'),
(3, N'CTY CP nông dược Việt Nam');
SET IDENTITY_INSERT HangSanXuat OFF

INSERT INTO NhanVien(HoVaTen, DienThoai, DiaChi, TenDangNhap, MatKhau, QuyenHan) VALUES
(N'Lê Quyền Nguyệt', '0123456888', N'Long Xuyên', 'Quanly', '$2a$11$XGOVk9m4HqzXBlwVvhKN/ur8FS/keV9QyuCwikTL67sL0gqNUupMa', 1),
(N'Nguyễn Toàn', '0123456999', N'Châu Thành', 'Nhanvien', '$2a$11$c1syEtekaZ6OJrX77mGoJ.6mu4YuUjUHvKrliW8oOU3M3u7I6zyuy', 0);

INSERT INTO KhachHang(HoVaTen, DienThoai, DiaChi) VALUES
(N'Lê Thị Thúy Ngân', '0123456777', N'Phú Hòa'),
(N'Dư Thị Kim Quyên', '0123456555', N'Chợ Mới'),
(N'Huỳnh Thị Kim Anh', '0123456444', N'Long Xuyên'),
(N'Hồ Ngọc Hà', '0123456333', N'TP.Hồ Chí Minh'),
(N'Lê Dương Bảo Lâm', '012345222', N'Đồng Nai'),
(N'Lý Nhã Kỳ', '0123456494', N'Nha Trang');

SET IDENTITY_INSERT LoaiSanPham ON
INSERT INTO LoaiSanPham(ID, TenLoai) VALUES
(1, N'Thuốc trừ bệnh'),
(2, N'Phân bón'),
(3, N'Giống cây trồng'),
(4, N'Thuốc trừ sâu'),
(5, N'Thuốc diệt cỏ');
SET IDENTITY_INSERT LoaiSanPham OFF

INSERT INTO SanPham(LoaiSanPhamID, HangSanXuatID, TenSanPham, DonGia, SoLuong, HinhAnh) VALUES
(1, 1, N'Thuốc trừ bệnh Endico 5SC', 220000, 100, 'endico.jpg'),
(1, 1, N'Thuốc trừ bệnh Ridozeb 72wp', 200000, 100, 'ridozeb.jpg'),
(2, 1, N'Phân bón lá Tekka', 270000, 100, 'tekka.jpg'),
(3, 2, N'Giống cây cải ngọt', 30000, 100, 'giongcaingot.jpg'),
(4, 2, N'Thuốc trừ sâu RHOLAM 50WP', 100000, 100, 'rholam.jpg'),
(5, 3, N'Thuốc diệt cỏ VINARIUS 500WP', 90000, 100, 'vinarius.jpg');
