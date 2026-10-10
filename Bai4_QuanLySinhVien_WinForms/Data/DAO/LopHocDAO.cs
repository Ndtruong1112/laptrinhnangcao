using System;
using System.Collections.Generic;
using System.Linq;
using Bai4_QuanLySinhVien_WinForms.Data.Entity;

namespace Bai4_QuanLySinhVien_WinForms.Data.DAO
{
    /// <summary>
    /// Tầng DataAccess Layer (DAO): Thực hiện các thao tác với nguồn dữ liệu Lớp học
    /// </summary>
    public class LopHocDAO
    {
        // Nguồn dữ liệu dùng chung (In-Memory Database mô phỏng)
        private static readonly List<LopHoc> _danhSachLop = DuLieuMau.LayDanhSachLopHoc();

        public List<LopHoc> GetAll()
        {
            return _danhSachLop.ToList();
        }

        public LopHoc? GetById(string maLop)
        {
            return _danhSachLop.FirstOrDefault(l => l.MaLop.Equals(maLop, StringComparison.OrdinalIgnoreCase));
        }

        public bool Add(LopHoc lop)
        {
            if (GetById(lop.MaLop) != null) return false;
            _danhSachLop.Add(lop);
            return true;
        }

        public bool Update(LopHoc lop)
        {
            var existing = GetById(lop.MaLop);
            if (existing == null) return false;
            existing.TenLop = lop.TenLop;
            return true;
        }

        public bool Delete(string maLop)
        {
            var existing = GetById(maLop);
            if (existing == null) return false;
            return _danhSachLop.Remove(existing);
        }
    }
}
