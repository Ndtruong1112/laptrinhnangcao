using System.Collections.Generic;
using Bai4_QuanLySinhVien_WinForms.Data.DAO;
using Bai4_QuanLySinhVien_WinForms.Data.Entity;

namespace Bai4_QuanLySinhVien_WinForms.BUL
{
    /// <summary>
    /// Tầng Business (BUL / BUS): Thực hiện các nghiệp vụ yêu cầu liên quan đến Sinh viên
    /// </summary>
    public class SinhVienBUL
    {
        private readonly SinhVienDAO _sinhVienDAO = new();
        private readonly LopHocDAO _lopHocDAO = new();

        // 1. Lấy tất cả sinh viên
        public List<SinhVien> LayTatCa()
        {
            return _sinhVienDAO.GetAll();
        }

        // 2. Tìm kiếm sinh viên theo mã
        public SinhVien? TimTheoMa(string maSV)
        {
            return _sinhVienDAO.GetById(maSV);
        }

        // 3. Nghiệp vụ Thêm: Kiểm tra Data Annotations + Kiểm tra trùng mã
        public (bool Success, List<string> Errors) ThemSinhVien(SinhVien sv)
        {
            var errors = new List<string>();

            // Kiểm tra Data Annotations
            if (!sv.IsValid(out errors))
            {
                return (false, errors);
            }

            // Nghiệp vụ: Mã sinh viên không được trùng
            if (_sinhVienDAO.GetById(sv.MaSV) != null)
            {
                errors.Add($"Mã sinh viên '{sv.MaSV}' đã tồn tại trong hệ thống.");
                return (false, errors);
            }

            // Nghiệp vụ: Lớp học phải tồn tại
            var lop = _lopHocDAO.GetById(sv.MaLop);
            if (lop == null)
            {
                errors.Add("Lớp học đã chọn không tồn tại trong hệ thống.");
                return (false, errors);
            }

            bool success = _sinhVienDAO.Add(sv);
            return (success, errors);
        }

        // 4. Nghiệp vụ Sửa: Kiểm tra Data Annotations + cập nhật
        public (bool Success, List<string> Errors) SuaSinhVien(SinhVien sv)
        {
            var errors = new List<string>();

            // Kiểm tra Data Annotations
            if (!sv.IsValid(out errors))
            {
                return (false, errors);
            }

            // Kiểm tra sinh viên có tồn tại không
            var svCu = _sinhVienDAO.GetById(sv.MaSV);
            if (svCu == null)
            {
                errors.Add("Không tìm thấy sinh viên cần cập nhật thông tin.");
                return (false, errors);
            }

            // Kiểm tra lớp học mới có tồn tại không
            if (_lopHocDAO.GetById(sv.MaLop) == null)
            {
                errors.Add("Lớp học đã chọn không hợp lệ.");
                return (false, errors);
            }

            bool success = _sinhVienDAO.Update(sv);
            return (success, errors);
        }

        // 5. Nghiệp vụ Xóa
        public bool XoaSinhVien(string maSV)
        {
            return _sinhVienDAO.Delete(maSV);
        }

        // 6. Nghiệp vụ Tìm kiếm và Lọc
        public List<SinhVien> TimKiemVaLoc(string tuKhoa, string maLop, double diemTu)
        {
            return _sinhVienDAO.Search(tuKhoa, maLop, diemTu);
        }
    }
}
