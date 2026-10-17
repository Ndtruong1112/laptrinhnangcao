using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.BUL
{
    /// <summary>
    /// Tầng Business Logic Layer cho Sinh viên (SinhVienBUL).
    /// Tiếp nhận yêu cầu từ Presentation, kiểm tra quy tắc nghiệp vụ,
    /// ném ngoại lệ lên tầng View khi có lỗi và gọi tầng DataAccess (DAO / DAL).
    /// </summary>
    public class SinhVienBUL
    {
        private readonly SinhVienDAO svDAO;

        public SinhVienBUL()
        {
            svDAO = new SinhVienDAO();
        }

        public SinhVienBUL(SinhVienDAO dao)
        {
            svDAO = dao;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách sinh viên từ CSDL. Ném ngoại lệ nếu có lỗi.
        /// </summary>
        public List<SinhVien> GetAllSinhVien()
        {
            try
            {
                return svDAO.GetAllSinhVien();
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienBUL] Lỗi tải danh sách sinh viên: {ex.Message}", ex);
            }
        }

        public List<SinhVien> LayDanhSachSinhVien() => GetAllSinhVien();

        /// <summary>
        /// Lấy thông tin sinh viên theo mã. Ném ngoại lệ nếu có lỗi.
        /// </summary>
        public SinhVien? GetSinhVienById(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
            {
                return null;
            }

            try
            {
                return svDAO.GetSinhVienById(maSV);
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienBUL] Lỗi tìm kiếm sinh viên '{maSV}': {ex.Message}", ex);
            }
        }

        public SinhVien? TimSinhVienTheoMa(string maSV) => GetSinhVienById(maSV);

        /// <summary>
        /// Lấy danh sách sinh viên theo lớp. Ném ngoại lệ nếu có lỗi.
        /// </summary>
        public List<SinhVien> GetSinhVienByLop(string maLop)
        {
            try
            {
                return svDAO.GetSinhVienByLop(maLop);
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienBUL] Lỗi lọc sinh viên theo lớp '{maLop}': {ex.Message}", ex);
            }
        }

        public List<SinhVien> LaySinhVienTheoLop(string maLop) => GetSinhVienByLop(maLop);

        /// <summary>
        /// Thêm mới sinh viên. Kiểm tra nghiệp vụ và ném ngoại lệ nếu không hợp lệ hoặc lỗi CSDL.
        /// </summary>
        public void AddSinhVien(SinhVien sv)
        {
            if (sv == null)
            {
                throw new ArgumentNullException(nameof(sv), "Dữ liệu sinh viên không được rỗng!");
            }

            // 1. Kiểm tra tính hợp lệ dữ liệu (Data Annotations)
            var errors = sv.IsInValid();
            if (errors.Count > 0)
            {
                string msg = string.Join("\n", errors.Select(e => "• " + e.ErrorMessage));
                throw new Exception($"Thông tin sinh viên không hợp lệ:\n{msg}");
            }

            // 2. Kiểm tra nghiệp vụ: Trùng mã sinh viên
            var existing = svDAO.GetSinhVienById(sv.MaSV);
            if (existing != null)
            {
                throw new Exception($"Mã sinh viên '{sv.MaSV}' đã tồn tại trong cơ sở dữ liệu! Vui lòng dùng mã khác.");
            }

            // 3. Gọi tầng DAO ném ngoại lệ nếu có lỗi CSDL
            try
            {
                svDAO.AddSinhVien(sv);
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienBUL] Không thể thêm sinh viên vào CSDL: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên. Ném ngoại lệ nếu không tìm thấy hoặc lỗi CSDL.
        /// </summary>
        public void UpdateSinhVien(SinhVien sv)
        {
            if (sv == null)
            {
                throw new ArgumentNullException(nameof(sv), "Dữ liệu sinh viên không được rỗng!");
            }

            // 1. Kiểm tra tính hợp lệ dữ liệu
            var errors = sv.IsInValid();
            if (errors.Count > 0)
            {
                string msg = string.Join("\n", errors.Select(e => "• " + e.ErrorMessage));
                throw new Exception($"Thông tin sinh viên không hợp lệ:\n{msg}");
            }

            // 2. Kiểm tra tồn tại
            var existing = svDAO.GetSinhVienById(sv.MaSV);
            if (existing == null)
            {
                throw new Exception($"Không tìm thấy sinh viên có mã '{sv.MaSV}' trong CSDL để cập nhật!");
            }

            // 3. Gọi DAO cập nhật
            try
            {
                bool ok = svDAO.UpdateSinhVien(sv);
                if (!ok)
                {
                    throw new Exception($"Không có bản ghi nào được cập nhật cho mã sinh viên '{sv.MaSV}'!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienBUL] Không thể cập nhật sinh viên: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Xóa sinh viên khỏi CSDL. Ném ngoại lệ nếu không tìm thấy hoặc lỗi CSDL.
        /// </summary>
        public void DeleteSinhVien(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
            {
                throw new ArgumentException("Mã sinh viên không được để trống!", nameof(maSV));
            }

            // 1. Kiểm tra tồn tại
            var existing = svDAO.GetSinhVienById(maSV);
            if (existing == null)
            {
                throw new Exception($"Không tìm thấy sinh viên có mã '{maSV}' trong CSDL để xóa!");
            }

            // 2. Gọi DAO xóa
            try
            {
                bool ok = svDAO.DeleteSinhVien(maSV);
                if (!ok)
                {
                    throw new Exception($"Không thể xóa sinh viên '{maSV}' khỏi CSDL!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienBUL] Lỗi khi xóa sinh viên: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Tìm kiếm sinh viên đa tiêu chí từ CSDL. Ném ngoại lệ nếu có lỗi.
        /// </summary>
        public List<SinhVien> Search(string tuKhoa, string maLop, double diemTu)
        {
            try
            {
                return svDAO.Search(tuKhoa, maLop, diemTu);
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienBUL] Lỗi khi tìm kiếm sinh viên: {ex.Message}", ex);
            }
        }
    }
}
