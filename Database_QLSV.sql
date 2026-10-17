-- ========================================================
-- KỊCH BẢN TẠO CƠ SỞ DỮ LIỆU: QLSV
-- Bài tập ADO.NET Quản lý Sinh viên - GV: Chu Thị Minh Huệ
-- ========================================================

IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'QLSV')
BEGIN
    CREATE DATABASE QLSV;
END
GO

USE QLSV;
GO

-- 1. Bảng LopHoc (Danh mục lớp học)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LopHoc')
BEGIN
    CREATE TABLE LopHoc (
        MaLop NVARCHAR(20) PRIMARY KEY,
        TenLop NVARCHAR(100) NOT NULL
    );
END
GO

-- 2. Bảng SinhVien (Thông tin chi tiết sinh viên)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'SinhVien')
BEGIN
    CREATE TABLE SinhVien (
        MaSV NVARCHAR(20) PRIMARY KEY,
        HoTen NVARCHAR(100) NOT NULL,
        NgaySinh DATE NOT NULL,
        GioiTinh NVARCHAR(10) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        SoDienThoai NVARCHAR(20) NOT NULL,
        Diem FLOAT NOT NULL DEFAULT 0.0,
        MaLop NVARCHAR(20) NOT NULL,
        TrangThai NVARCHAR(50) NOT NULL DEFAULT N'Đang học',
        CONSTRAINT FK_SinhVien_LopHoc FOREIGN KEY (MaLop) 
            REFERENCES LopHoc(MaLop) ON UPDATE CASCADE ON DELETE NO ACTION
    );
END
GO

-- 3. Nạp dữ liệu mẫu ban đầu cho bảng LopHoc
IF NOT EXISTS (SELECT 1 FROM LopHoc)
BEGIN
    INSERT INTO LopHoc (MaLop, TenLop) VALUES 
    (N'CSE0001', N'Khoa học máy tính 01'),
    (N'CSE0002', N'Khoa học máy tính 02'),
    (N'CSE0003', N'Khoa học máy tính 03'),
    (N'KTPM01', N'Kỹ thuật phần mềm 01'),
    (N'TTNT01', N'Trí tuệ nhân tạo 01');
END
GO

-- 4. Nạp dữ liệu mẫu ban đầu cho bảng SinhVien
IF NOT EXISTS (SELECT 1 FROM SinhVien)
BEGIN
    INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Email, SoDienThoai, Diem, MaLop, TrangThai) VALUES
    (N'SV000123', N'Nguyễn Văn An', '2006-08-15', N'Nam', N'an.nv@vju.ac.vn', N'0912345678', 8.5, N'CSE0001', N'Đang học'),
    (N'SV000124', N'Trần Minh Anh', '2006-01-22', N'Nữ', N'anh.tm@vju.ac.vn', N'0987654321', 9.0, N'CSE0002', N'Đang học'),
    (N'SV000125', N'Lê Hoàng Bình', '2006-05-09', N'Nam', N'binh.lh@vju.ac.vn', N'0355556677', 7.4, N'CSE0001', N'Đang học'),
    (N'SV000126', N'Đỗ Thị Hồng', '2006-11-30', N'Nữ', N'hong.dt@vju.ac.vn', N'0777888999', 8.1, N'CSE0003', N'Đang học');
END
GO
