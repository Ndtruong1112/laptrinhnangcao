using System;
using System.Collections.Generic;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.BUL
{
    /// <summary>
    /// Tầng Business Logic Layer cho Lớp học (LopBus / LopBUL).
    /// YÊU CẦU ĐỀ BÀI: Mọi phương thức ném về ngoại lệ khi gặp lỗi để tầng View xử lý.
    /// </summary>
    public class LopBus
    {
        private readonly LopHocDAO lopDAO;

        public LopBus()
        {
            lopDAO = new LopHocDAO();
        }

        public LopBus(LopHocDAO dao)
        {
            lopDAO = dao;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách lớp học. Ném ngoại lệ lên tầng View nếu có lỗi.
        /// </summary>
        public List<LopHoc> GetAllLopHoc()
        {
            try
            {
                return lopDAO.GetAllLopHoc();
            }
            catch (Exception ex)
            {
                throw new Exception($"[LopBus] Không thể lấy danh sách lớp học: {ex.Message}", ex);
            }
        }

        public List<LopHoc> LayDanhSachLop() => GetAllLopHoc();

        /// <summary>
        /// Lấy thông tin lớp học theo mã lớp. Ném ngoại lệ nếu có lỗi.
        /// </summary>
        public LopHoc? GetLopHocById(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
            {
                throw new ArgumentException("Mã lớp không được để trống!", nameof(maLop));
            }

            try
            {
                return lopDAO.GetLopHocById(maLop);
            }
            catch (Exception ex)
            {
                throw new Exception($"[LopBus] Lỗi khi tra cứu mã lớp '{maLop}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Thêm lớp học mới. Ném ngoại lệ nếu trùng mã hoặc vi phạm nghiệp vụ.
        /// </summary>
        public void AddLopHoc(LopHoc lh)
        {
            if (lh == null)
            {
                throw new ArgumentNullException(nameof(lh), "Thông tin lớp học không được rỗng!");
            }

            if (string.IsNullOrWhiteSpace(lh.MaLop))
            {
                throw new Exception("Mã lớp học không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(lh.TenLop))
            {
                throw new Exception("Tên lớp học không được để trống!");
            }

            // Kiểm tra nghiệp vụ: Trùng mã lớp
            var existing = lopDAO.GetLopHocById(lh.MaLop);
            if (existing != null)
            {
                throw new Exception($"Mã lớp '{lh.MaLop}' đã tồn tại trong cơ sở dữ liệu!");
            }

            try
            {
                lopDAO.AddLopHoc(lh);
            }
            catch (Exception ex)
            {
                throw new Exception($"[LopBus] Lỗi thêm lớp học vào CSDL: {ex.Message}", ex);
            }
        }

        public bool UpdateLopHoc(LopHoc lh)
        {
            if (lh == null) throw new ArgumentNullException(nameof(lh));
            return lopDAO.UpdateLopHoc(lh);
        }

        public bool DeleteLopHoc(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop)) throw new ArgumentException("Mã lớp không được để trống!");
            return lopDAO.DeleteLopHoc(maLop);
        }
    }

    /// <summary>
    /// Alias LopBUL kế thừa từ LopBus để tương thích với mọi quy ước gọi lớp.
    /// </summary>
    public class LopBUL : LopBus
    {
        public LopBUL() : base() { }
        public LopBUL(LopHocDAO dao) : base(dao) { }
    }
}
