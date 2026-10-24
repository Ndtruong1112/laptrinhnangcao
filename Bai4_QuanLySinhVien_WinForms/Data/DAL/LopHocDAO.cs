using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data.Context;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    /// <summary>
    /// Tầng Data Access Layer / Data Access Object (DAO) cho thực thể LopHoc.
    /// Sử dụng Entity Framework Core Code First (QLSVDbContext).
    /// YÊU CẦU: Ném ngoại lệ khi có lỗi xảy ra để tầng trên xử lý.
    /// </summary>
    public class LopHocDAO
    {
        public LopHocDAO()
        {
        }

        public LopHocDAO(string? connectionString)
        {
        }

        public LopHocDAO(DataHelper? helper)
        {
        }

        /// <summary>
        /// Lấy toàn bộ danh sách lớp học từ CSDL qua EF Core.
        /// </summary>
        public virtual List<LopHoc> GetAllLopHoc()
        {
            try
            {
                using var db = new QLSVDbContext();
                return db.LopHocs.AsNoTracking().OrderBy(l => l.MaLop).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"[LopHocDAO - EF Core] Lỗi khi lấy danh sách lớp học từ CSDL: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy thông tin lớp học theo mã lớp qua EF Core.
        /// </summary>
        public virtual LopHoc? GetLopHocById(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop)) return null;

            try
            {
                using var db = new QLSVDbContext();
                return db.LopHocs.AsNoTracking().FirstOrDefault(l => l.MaLop == maLop.Trim());
            }
            catch (Exception ex)
            {
                throw new Exception($"[LopHocDAO - EF Core] Lỗi khi tìm lớp học với mã '{maLop}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Thêm mới lớp học vào CSDL qua EF Core.
        /// </summary>
        public virtual void AddLopHoc(LopHoc lh)
        {
            if (lh == null) throw new ArgumentNullException(nameof(lh), "Dữ liệu lớp học không được rỗng!");

            try
            {
                using var db = new QLSVDbContext();
                db.LopHocs.Add(lh);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"[LopHocDAO - EF Core] Lỗi khi thêm lớp học '{lh.MaLop}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin lớp học trong CSDL qua EF Core.
        /// </summary>
        public virtual bool UpdateLopHoc(LopHoc lh)
        {
            if (lh == null) throw new ArgumentNullException(nameof(lh), "Dữ liệu lớp học không được rỗng!");

            try
            {
                using var db = new QLSVDbContext();
                var existing = db.LopHocs.Find(lh.MaLop);
                if (existing == null) return false;

                existing.TenLop = lh.TenLop;
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"[LopHocDAO - EF Core] Lỗi khi cập nhật lớp học '{lh.MaLop}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Xóa lớp học khỏi CSDL qua EF Core.
        /// </summary>
        public virtual bool DeleteLopHoc(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop)) return false;

            try
            {
                using var db = new QLSVDbContext();
                var existing = db.LopHocs.Find(maLop.Trim());
                if (existing == null) return false;

                db.LopHocs.Remove(existing);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"[LopHocDAO - EF Core] Lỗi khi xóa lớp học '{maLop}': {ex.Message}", ex);
            }
        }

        // Tương thích với các tên hàm khác nhau
        public List<LopHoc> LayTatCa() => GetAllLopHoc();
        public LopHoc? LayTheoMa(string maLop) => GetLopHocById(maLop);
        public bool Them(LopHoc lh) { AddLopHoc(lh); return true; }
        public bool Sua(LopHoc lh) => UpdateLopHoc(lh);
        public bool Xoa(string maLop) => DeleteLopHoc(maLop);
    }
}
