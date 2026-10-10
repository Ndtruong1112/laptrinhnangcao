using System.Collections.Generic;
using Bai4_QuanLySinhVien_WinForms.Data.DAO;
using Bai4_QuanLySinhVien_WinForms.Data.Entity;

namespace Bai4_QuanLySinhVien_WinForms.BUL
{
    /// <summary>
    /// Tầng Business (BUL / BUS): Thực hiện các nghiệp vụ liên quan đến Lớp học
    /// </summary>
    public class LopHocBUL
    {
        private readonly LopHocDAO _lopHocDAO = new();

        public List<LopHoc> LayDanhSachLop()
        {
            return _lopHocDAO.GetAll();
        }

        public LopHoc? LayLopTheoMa(string maLop)
        {
            return _lopHocDAO.GetById(maLop);
        }

        public (bool Success, List<string> Errors) ThemLopHoc(LopHoc lop)
        {
            var errors = new List<string>();

            // 1. Kiểm tra tính hợp lệ bằng Data Annotations
            if (!lop.IsValid(out errors))
            {
                return (false, errors);
            }

            // 2. Nghiệp vụ: Mã lớp không được trùng
            if (_lopHocDAO.GetById(lop.MaLop) != null)
            {
                errors.Add($"Mã lớp '{lop.MaLop}' đã tồn tại trong hệ thống.");
                return (false, errors);
            }

            bool success = _lopHocDAO.Add(lop);
            return (success, errors);
        }
    }
}
