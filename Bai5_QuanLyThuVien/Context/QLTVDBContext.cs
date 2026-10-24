using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Bai5_QuanLyThuVien.Entities;

namespace Bai5_QuanLyThuVien.Context
{
    /// <summary>
    /// Lớp DbContext cho Hệ thống Quản Lý Thư Viện (QLTVDBContext).
    /// Ánh xạ các thực thể (TheLoai, Sach, DocGia, PhieuMuon) sang CSDL bằng EF Core Code First.
    /// Hỗ trợ các câu lệnh migration:
    ///   dotnet ef migrations add InitialLibraryDB --context QLTVDBContext
    ///   dotnet ef database update --context QLTVDBContext
    /// </summary>
    public class QLTVDBContext : DbContext
    {
        public virtual DbSet<TheLoai> TheLoais { get; set; } = null!;
        public virtual DbSet<Sach> Sachs { get; set; } = null!;
        public virtual DbSet<DocGia> DocGias { get; set; } = null!;
        public virtual DbSet<PhieuMuon> PhieuMuons { get; set; } = null!;

        public QLTVDBContext()
        {
        }

        public QLTVDBContext(DbContextOptions<QLTVDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string strcon = string.Empty;
                try
                {
                    strcon = ConfigurationManager.ConnectionStrings["QLTV"]?.ConnectionString
                             ?? ConfigurationManager.ConnectionStrings["QLTVDBContext"]?.ConnectionString
                             ?? ConfigurationManager.ConnectionStrings["LibraryDB"]?.ConnectionString
                             ?? string.Empty;
                }
                catch
                {
                    // Bỏ qua lỗi đọc config để dùng fallback
                }

                if (string.IsNullOrWhiteSpace(strcon))
                {
                    strcon = @"Server=(localdb)\MSSQLLocalDB;Database=QLTV;Integrated Security=True;TrustServerCertificate=True";
                }

                optionsBuilder.UseSqlServer(strcon);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Cấu hình bảng TheLoai
            modelBuilder.Entity<TheLoai>(entity =>
            {
                entity.ToTable("TheLoai");
                entity.HasKey(e => e.MaTheLoai);

                entity.Property(e => e.MaTheLoai).HasMaxLength(20).IsRequired();
                entity.Property(e => e.TenTheLoai).HasMaxLength(100).IsRequired();
                entity.Property(e => e.MoTa).HasMaxLength(250);
            });

            // 2. Cấu hình bảng Sach
            modelBuilder.Entity<Sach>(entity =>
            {
                entity.ToTable("Sach");
                entity.HasKey(e => e.MaSach);

                entity.Property(e => e.MaSach).HasMaxLength(20).IsRequired();
                entity.Property(e => e.TenSach).HasMaxLength(150).IsRequired();
                entity.Property(e => e.TacGia).HasMaxLength(100);
                entity.Property(e => e.GiaTien).HasDefaultValue(0.0);
                entity.Property(e => e.SoLuong).HasDefaultValue(1);

                entity.HasOne(d => d.TheLoai)
                      .WithMany(p => p.Sachs)
                      .HasForeignKey(d => d.MaTheLoai)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Sach_TheLoai");
            });

            // 3. Cấu hình bảng DocGia
            modelBuilder.Entity<DocGia>(entity =>
            {
                entity.ToTable("DocGia");
                entity.HasKey(e => e.MaDocGia);

                entity.Property(e => e.MaDocGia).HasMaxLength(20).IsRequired();
                entity.Property(e => e.HoTen).HasMaxLength(100).IsRequired();
                entity.Property(e => e.NgaySinh).HasColumnType("date");
                entity.Property(e => e.SoDienThoai).HasMaxLength(20);
                entity.Property(e => e.Email).HasMaxLength(100);
            });

            // 4. Cấu hình bảng PhieuMuon
            modelBuilder.Entity<PhieuMuon>(entity =>
            {
                entity.ToTable("PhieuMuon");
                entity.HasKey(e => e.MaPhieu);

                entity.Property(e => e.MaPhieu).ValueGeneratedOnAdd();
                entity.Property(e => e.NgayMuon).HasColumnType("date");
                entity.Property(e => e.NgayHenTra).HasColumnType("date");
                entity.Property(e => e.TrangThai).HasMaxLength(50).HasDefaultValue("Đang mượn");

                entity.HasOne(d => d.DocGia)
                      .WithMany(p => p.PhieuMuons)
                      .HasForeignKey(d => d.MaDocGia)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_PhieuMuon_DocGia");

                entity.HasOne(d => d.Sach)
                      .WithMany(p => p.PhieuMuons)
                      .HasForeignKey(d => d.MaSach)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_PhieuMuon_Sach");
            });

            // 5. Data Seeding mẫu ban đầu (Code First)
            modelBuilder.Entity<TheLoai>().HasData(
                new TheLoai { MaTheLoai = "CNTT", TenTheLoai = "Công nghệ thông tin", MoTa = "Sách lập trình, thuật toán, AI, CSDL" },
                new TheLoai { MaTheLoai = "KT", TenTheLoai = "Kinh tế - Tài chính", MoTa = "Quản trị kinh doanh, marketing, tài chính" },
                new TheLoai { MaTheLoai = "VH", TenTheLoai = "Văn học - Nghệ thuật", MoTa = "Tiểu thuyết, truyện ngắn, thơ ca" }
            );

            modelBuilder.Entity<Sach>().HasData(
                new Sach { MaSach = "S001", TenSach = "Lập trình C# nâng cao với .NET", TacGia = "Nguyễn Văn An", NamXuatBan = 2024, GiaTien = 150000, SoLuong = 20, MaTheLoai = "CNTT" },
                new Sach { MaSach = "S002", TenSach = "Thiết kế CSDL với SQL Server & EF Core", TacGia = "Chu Thị Minh Huệ", NamXuatBan = 2023, GiaTien = 180000, SoLuong = 15, MaTheLoai = "CNTT" },
                new Sach { MaSach = "S003", TenSach = "Kinh tế vi mô ứng dụng", TacGia = "Trần Minh Anh", NamXuatBan = 2022, GiaTien = 120000, SoLuong = 10, MaTheLoai = "KT" },
                new Sach { MaSach = "S004", TenSach = "Dế Mèn Phiêu Lưu Ký", TacGia = "Tô Hoài", NamXuatBan = 2021, GiaTien = 75000, SoLuong = 25, MaTheLoai = "VH" }
            );

            modelBuilder.Entity<DocGia>().HasData(
                new DocGia { MaDocGia = "DG001", HoTen = "Lê Hoàng Bình", NgaySinh = new DateTime(2006, 5, 9), SoDienThoai = "0912345678", Email = "binh.lh@vju.ac.vn" },
                new DocGia { MaDocGia = "DG002", HoTen = "Đỗ Thị Hồng", NgaySinh = new DateTime(2006, 11, 30), SoDienThoai = "0987654321", Email = "hong.dt@vju.ac.vn" }
            );

            modelBuilder.Entity<PhieuMuon>().HasData(
                new PhieuMuon { MaPhieu = 1, MaDocGia = "DG001", MaSach = "S001", NgayMuon = new DateTime(2026, 10, 1), NgayHenTra = new DateTime(2026, 10, 15), TrangThai = "Đang mượn" },
                new PhieuMuon { MaPhieu = 2, MaDocGia = "DG002", MaSach = "S002", NgayMuon = new DateTime(2026, 10, 10), NgayHenTra = new DateTime(2026, 10, 24), TrangThai = "Đang mượn" }
            );
        }
    }
}
