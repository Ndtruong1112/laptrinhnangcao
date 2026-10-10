using System;
using System.Collections.Generic;
using System.Linq;
using Bai4_QuanLySinhVien_WinForms.Data.Entity;

namespace Bai4_QuanLySinhVien_WinForms.Data.DAO
{
    /// <summary>
    /// Tầng DataAccess Layer (DAO): Thực hiện các thao tác Thêm, Sửa, Xóa, Lấy về cho Sinh viên
    /// </summary>
    public class SinhVienDAO
    {
        private readonly LopHocDAO _lopHocDAO = new();

        // 1. Lấy tất cả sinh viên
        public List<SinhVien> GetAll()
        {
            return _lopHocDAO.GetAll().SelectMany(l => l.DanhSachSinhVien).ToList();
        }

        // 2. Lấy sinh viên theo mã
        public SinhVien? GetById(string maSV)
        {
            return GetAll().FirstOrDefault(s => s.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));
        }

        // 3. Thêm mới sinh viên
        public bool Add(SinhVien sv)
        {
            var lop = _lopHocDAO.GetById(sv.MaLop);
            if (lop == null) return false;

            sv.LopHoc = lop;
            lop.DanhSachSinhVien.Add(sv);
            return true;
        }

        // 4. Sửa thông tin sinh viên
        public bool Update(SinhVien svMoi)
        {
            var svCu = GetById(svMoi.MaSV);
            if (svCu == null) return false;

            // Nếu thay đổi lớp học -> chuyển sinh viên sang lớp mới
            if (!svCu.MaLop.Equals(svMoi.MaLop, StringComparison.OrdinalIgnoreCase))
            {
                var lopCu = _lopHocDAO.GetById(svCu.MaLop);
                var lopMoi = _lopHocDAO.GetById(svMoi.MaLop);

                if (lopMoi == null) return false;

                lopCu?.DanhSachSinhVien.Remove(svCu);
                svCu.MaLop = svMoi.MaLop;
                svCu.LopHoc = lopMoi;
                lopMoi.DanhSachSinhVien.Add(svCu);
            }

            svCu.HoTen = svMoi.HoTen;
            svCu.NgaySinh = svMoi.NgaySinh;
            svCu.GioiTinh = svMoi.GioiTinh;
            svCu.Email = svMoi.Email;
            svCu.DienThoai = svMoi.DienThoai;
            svCu.Diem = svMoi.Diem;
            svCu.TrangThai = svMoi.TrangThai;

            return true;
        }

        // 5. Xóa sinh viên
        public bool Delete(string maSV)
        {
            var sv = GetById(maSV);
            if (sv == null) return false;

            var lop = _lopHocDAO.GetById(sv.MaLop);
            if (lop != null)
            {
                return lop.DanhSachSinhVien.Remove(sv);
            }
            return false;
        }

        // 6. Lấy về danh sách theo bộ lọc / tìm kiếm
        public List<SinhVien> Search(string tuKhoa, string maLop, double diemTu)
        {
            tuKhoa = tuKhoa.Trim().ToLower();

            return GetAll().Where(sv =>
            {
                bool matchTuKhoa = string.IsNullOrEmpty(tuKhoa) ||
                    sv.MaSV.ToLower().Contains(tuKhoa) ||
                    sv.HoTen.ToLower().Contains(tuKhoa) ||
                    sv.Email.ToLower().Contains(tuKhoa) ||
                    sv.DienThoai.ToLower().Contains(tuKhoa);

                bool matchLop = string.IsNullOrEmpty(maLop) || sv.MaLop.Equals(maLop, StringComparison.OrdinalIgnoreCase);
                bool matchDiem = sv.Diem >= diemTu;

                return matchTuKhoa && matchLop && matchDiem;
            }).ToList();
        }
    }
}
