using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data.Context;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    /// <summary>
    /// Tầng Data Access Layer / Data Access Object (DAO) cho thực thể SinhVien.
    /// Sử dụng Entity Framework Core Code First (QLSVDbContext).
    /// YÊU CẦU ĐỀ BÀI: Mọi phương thức ném về ngoại lệ khi gặp lỗi để tầng trên xử lý.
    /// </summary>
    public class SinhVienDAO
    {
        public SinhVienDAO()
        {
        }

        public SinhVienDAO(string? connectionString)
        {
        }

        public SinhVienDAO(DataHelper? helper)
        {
        }

        /// <summary>
        /// Lấy toàn bộ danh sách sinh viên từ CSDL kèm thông tin lớp qua EF Core.
        /// </summary>
        public virtual List<SinhVien> GetAllSinhVien()
        {
            try
            {
                using var db = new QLSVDbContext();
                return db.SinhViens
                         .Include(s => s.LopHoc)
                         .AsNoTracking()
                         .OrderBy(s => s.MaSV)
                         .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO - EF Core] Lỗi khi lấy danh sách sinh viên từ CSDL: {ex.Message}", ex);
            }
        }

        public virtual List<SinhVien> GetAllSinhVien1() => GetAllSinhVien();

        /// <summary>
        /// Lấy thông tin sinh viên theo mã sinh viên qua EF Core.
        /// </summary>
        public virtual SinhVien? GetSinhVienById(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV)) return null;

            try
            {
                using var db = new QLSVDbContext();
                return db.SinhViens
                         .Include(s => s.LopHoc)
                         .AsNoTracking()
                         .FirstOrDefault(s => s.MaSV == maSV.Trim());
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO - EF Core] Lỗi khi tìm sinh viên có mã '{maSV}': {ex.Message}", ex);
            }
        }

        public virtual SinhVien? GetSinhVienByMaSV(string maSV) => GetSinhVienById(maSV);

        /// <summary>
        /// Lấy danh sách sinh viên theo mã lớp qua EF Core.
        /// </summary>
        public virtual List<SinhVien> GetSinhVienByLop(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop) || maLop.Equals("Tất cả lớp", StringComparison.OrdinalIgnoreCase))
            {
                return GetAllSinhVien();
            }

            try
            {
                using var db = new QLSVDbContext();
                return db.SinhViens
                         .Include(s => s.LopHoc)
                         .AsNoTracking()
                         .Where(s => s.MaLop == maLop.Trim())
                         .OrderBy(s => s.MaSV)
                         .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO - EF Core] Lỗi khi lấy sinh viên theo mã lớp '{maLop}': {ex.Message}", ex);
            }
        }

        public virtual List<SinhVien> GetSinhViensByMaLop(string maLop) => GetSinhVienByLop(maLop);

        /// <summary>
        /// Thêm mới sinh viên vào CSDL qua EF Core.
        /// </summary>
        public virtual void AddSinhVien(SinhVien sv)
        {
            if (sv == null) throw new ArgumentNullException(nameof(sv), "Dữ liệu sinh viên không được rỗng!");

            try
            {
                using var db = new QLSVDbContext();
                // Ngắt navigation property để EF Core chỉ ánh xạ theo khóa ngoại MaLop, tránh lỗi tracking
                sv.LopHoc = null;
                db.SinhViens.Add(sv);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO - EF Core] Lỗi khi thêm mới sinh viên '{sv.MaSV}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên trong CSDL qua EF Core.
        /// </summary>
        public virtual bool UpdateSinhVien(SinhVien sv)
        {
            if (sv == null) throw new ArgumentNullException(nameof(sv), "Dữ liệu sinh viên không được rỗng!");

            try
            {
                using var db = new QLSVDbContext();
                var existing = db.SinhViens.Find(sv.MaSV.Trim());
                if (existing == null) return false;

                existing.HoTen = sv.HoTen.Trim();
                existing.NgaySinh = sv.NgaySinh;
                existing.GioiTinh = sv.GioiTinh;
                existing.Email = sv.Email.Trim();
                existing.SoDienThoai = sv.SoDienThoai.Trim();
                existing.Diem = sv.Diem;
                existing.MaLop = sv.MaLop.Trim();
                existing.TrangThai = sv.TrangThai;

                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO - EF Core] Lỗi khi cập nhật sinh viên '{sv.MaSV}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Xóa sinh viên khỏi CSDL qua EF Core.
        /// </summary>
        public virtual bool DeleteSinhVien(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV)) return false;

            try
            {
                using var db = new QLSVDbContext();
                var existing = db.SinhViens.Find(maSV.Trim());
                if (existing == null) return false;

                db.SinhViens.Remove(existing);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO - EF Core] Lỗi khi xóa sinh viên '{maSV}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Tìm kiếm sinh viên từ CSDL theo từ khóa, mã lớp và điểm tối thiểu qua EF Core Linq.
        /// </summary>
        public virtual List<SinhVien> Search(string tuKhoa, string maLop, double diemTu)
        {
            try
            {
                using var db = new QLSVDbContext();
                var query = db.SinhViens
                              .Include(s => s.LopHoc)
                              .AsNoTracking()
                              .AsQueryable();

                if (!string.IsNullOrWhiteSpace(tuKhoa))
                {
                    string kw = tuKhoa.Trim();
                    query = query.Where(s => s.MaSV.Contains(kw) ||
                                             s.HoTen.Contains(kw) ||
                                             s.Email.Contains(kw) ||
                                             s.SoDienThoai.Contains(kw));
                }

                if (!string.IsNullOrWhiteSpace(maLop) && !maLop.Equals("Tất cả lớp", StringComparison.OrdinalIgnoreCase))
                {
                    string ml = maLop.Trim();
                    query = query.Where(s => s.MaLop == ml);
                }

                query = query.Where(s => s.Diem >= diemTu);

                return query.OrderBy(s => s.MaSV).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO - EF Core] Lỗi khi tìm kiếm sinh viên trong CSDL: {ex.Message}", ex);
            }
        }

        // Tương thích với các tên gọi khác
        public List<SinhVien> LayTatCa() => GetAllSinhVien();
        public SinhVien? LayTheoMa(string maSV) => GetSinhVienById(maSV);
        public List<SinhVien> LayTheoLop(string maLop) => GetSinhVienByLop(maLop);
        public bool Them(SinhVien sv) { AddSinhVien(sv); return true; }
        public bool Sua(SinhVien sv) => UpdateSinhVien(sv);
        public bool Xoa(string maSV) => DeleteSinhVien(maSV);
    }
}
