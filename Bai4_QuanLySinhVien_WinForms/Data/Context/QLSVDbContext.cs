using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.Context
{
    /// <summary>
    /// Lớp DbContext quản lý mô hình Entity Framework Core Code First.
    /// Ánh xạ các thực thể (LopHoc, SinhVien) sang các bảng trong CSDL SQL Server.
    /// </summary>
    public class QLSVDbContext : DbContext
    {
        public virtual DbSet<LopHoc> LopHocs { get; set; } = null!;
        public virtual DbSet<SinhVien> SinhViens { get; set; } = null!;

        public QLSVDbContext()
        {
        }

        public QLSVDbContext(DbContextOptions<QLSVDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Đọc chuỗi kết nối từ App.config hoặc dùng mặc định
                string strcon = string.Empty;
                try
                {
                    strcon = ConfigurationManager.ConnectionStrings["QLSV"]?.ConnectionString
                             ?? ConfigurationManager.ConnectionStrings["studentDataBase"]?.ConnectionString
                             ?? string.Empty;
                }
                catch
                {
                    // Bỏ qua lỗi đọc cấu hình để dùng chuỗi fallback
                }

                if (string.IsNullOrWhiteSpace(strcon))
                {
                    strcon = @"Data Source=.;Initial Catalog=QLSV;Integrated Security=True;TrustServerCertificate=True";
                }

                optionsBuilder.UseSqlServer(strcon);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Cấu hình thực thể LopHoc
            modelBuilder.Entity<LopHoc>(entity =>
            {
                entity.ToTable("LopHoc");
                entity.HasKey(e => e.MaLop);

                entity.Property(e => e.MaLop)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.TenLop)
                      .HasMaxLength(100)
                      .IsRequired();
            });

            // 2. Cấu hình thực thể SinhVien và quan hệ 1 - N với LopHoc
            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.ToTable("SinhVien");
                entity.HasKey(e => e.MaSV);

                entity.Property(e => e.MaSV)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.HoTen)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.NgaySinh)
                      .HasColumnType("date")
                      .IsRequired();

                // Chuyển đổi thuộc tính bool GioiTinh sang chuỗi "Nam"/"Nữ" trong CSDL
                entity.Property(e => e.GioiTinh)
                      .HasConversion(
                          v => v ? "Nam" : "Nữ",
                          v => v.Equals("Nam", StringComparison.OrdinalIgnoreCase) || v == "True" || v == "1"
                      )
                      .HasMaxLength(10)
                      .IsRequired();

                entity.Property(e => e.Email)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.SoDienThoai)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.Diem)
                      .HasDefaultValue(0.0);

                entity.Property(e => e.MaLop)
                      .HasMaxLength(20)
                      .IsRequired();

                entity.Property(e => e.TrangThai)
                      .HasMaxLength(50)
                      .HasDefaultValue("Đang học");

                // Cấu hình mối quan hệ 1 - N: Một LopHoc có nhiều SinhVien
                entity.HasOne(d => d.LopHoc)
                      .WithMany(p => p.DanhSachSinhVien)
                      .HasForeignKey(d => d.MaLop)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_SinhVien_LopHoc");
            });

            // 3. Khởi tạo dữ liệu mẫu ban đầu (Data Seeding theo chuẩn Code First)
            modelBuilder.Entity<LopHoc>().HasData(
                new LopHoc { MaLop = "CSE0001", TenLop = "Khoa học máy tính 01" },
                new LopHoc { MaLop = "CSE0002", TenLop = "Khoa học máy tính 02" },
                new LopHoc { MaLop = "CSE0003", TenLop = "Khoa học máy tính 03" },
                new LopHoc { MaLop = "KTPM01", TenLop = "Kỹ thuật phần mềm 01" },
                new LopHoc { MaLop = "TTNT01", TenLop = "Trí tuệ nhân tạo 01" }
            );

            modelBuilder.Entity<SinhVien>().HasData(
                new SinhVien { MaSV = "SV000123", HoTen = "Nguyễn Văn An", NgaySinh = new DateTime(2006, 8, 15), GioiTinh = true, Email = "an.nv@vju.ac.vn", SoDienThoai = "0912345678", Diem = 8.5, MaLop = "CSE0001", TrangThai = "Đang học" },
                new SinhVien { MaSV = "SV000124", HoTen = "Trần Minh Anh", NgaySinh = new DateTime(2006, 1, 22), GioiTinh = false, Email = "anh.tm@vju.ac.vn", SoDienThoai = "0987654321", Diem = 9.0, MaLop = "CSE0002", TrangThai = "Đang học" },
                new SinhVien { MaSV = "SV000125", HoTen = "Lê Hoàng Bình", NgaySinh = new DateTime(2006, 5, 9), GioiTinh = true, Email = "binh.lh@vju.ac.vn", SoDienThoai = "0355556677", Diem = 7.4, MaLop = "CSE0001", TrangThai = "Đang học" },
                new SinhVien { MaSV = "SV000126", HoTen = "Đỗ Thị Hồng", NgaySinh = new DateTime(2006, 11, 30), GioiTinh = false, Email = "hong.dt@vju.ac.vn", SoDienThoai = "0777888999", Diem = 8.1, MaLop = "CSE0003", TrangThai = "Đang học" }
            );
        }

        /// <summary>
        /// Phương thức hỗ trợ tạo tự động CSDL và các bảng theo Code First nếu chưa tồn tại
        /// </summary>
        public static void EnsureDatabaseCreated()
        {
            try
            {
                using var context = new QLSVDbContext();
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                throw new Exception($"[EF Core Code First] Lỗi khi khởi tạo ánh xạ CSDL: {ex.Message}", ex);
            }
        }
    }
}
