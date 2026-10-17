using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    /// <summary>
    /// Tầng Data Access Layer / Data Access Object (DAO) cho thực thể LopHoc.
    /// Thao tác trực tiếp với CSDL SQL Server bằng ADO.NET qua DataHelper.
    /// YÊU CẦU: Ném ngoại lệ khi có lỗi xảy ra để tầng trên xử lý.
    /// </summary>
    public class LopHocDAO
    {
        protected readonly DataHelper dataHelper;

        public LopHocDAO()
        {
            dataHelper = new DataHelper();
        }

        public LopHocDAO(string connectionString)
        {
            dataHelper = new DataHelper(connectionString);
        }

        public LopHocDAO(DataHelper helper)
        {
            dataHelper = helper;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách lớp học từ CSDL. Ném ngoại lệ nếu truy vấn thất bại.
        /// </summary>
        public virtual List<LopHoc> GetAllLopHoc()
        {
            try
            {
                var dsLop = new List<LopHoc>();
                string sql = "SELECT MaLop, TenLop FROM LopHoc ORDER BY MaLop ASC";
                DataTable dt = dataHelper.GetDataTable(sql);

                foreach (DataRow row in dt.Rows)
                {
                    dsLop.Add(new LopHoc
                    {
                        MaLop = row["MaLop"]?.ToString() ?? string.Empty,
                        TenLop = row["TenLop"]?.ToString() ?? string.Empty
                    });
                }

                return dsLop;
            }
            catch (Exception ex)
            {
                throw new Exception($"[LopHocDAO] Lỗi khi lấy danh sách lớp học từ CSDL: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy thông tin lớp học theo mã lớp. Ném ngoại lệ nếu có lỗi.
        /// </summary>
        public virtual LopHoc? GetLopHocById(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop)) return null;

            try
            {
                string sql = "SELECT MaLop, TenLop FROM LopHoc WHERE MaLop = @MaLop";
                var parameters = new[]
                {
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 20) { Value = maLop.Trim() }
                };

                DataTable dt = dataHelper.GetDataTable(sql, parameters);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    return new LopHoc
                    {
                        MaLop = row["MaLop"]?.ToString() ?? string.Empty,
                        TenLop = row["TenLop"]?.ToString() ?? string.Empty
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"[LopHocDAO] Lỗi khi tìm lớp học với mã '{maLop}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Thêm mới lớp học vào CSDL. Ném ngoại lệ nếu thêm thất bại.
        /// </summary>
        public virtual void AddLopHoc(LopHoc lh)
        {
            if (lh == null) throw new ArgumentNullException(nameof(lh), "Dữ liệu lớp học không được rỗng!");

            try
            {
                string sql = "INSERT INTO LopHoc (MaLop, TenLop) VALUES (@MaLop, @TenLop)";
                var parameters = new[]
                {
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 20) { Value = lh.MaLop.Trim() },
                    new SqlParameter("@TenLop", SqlDbType.NVarChar, 100) { Value = lh.TenLop.Trim() }
                };

                dataHelper.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"[LopHocDAO] Lỗi khi thêm lớp học '{lh.MaLop}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin lớp học trong CSDL. Ném ngoại lệ nếu cập nhật thất bại.
        /// </summary>
        public virtual bool UpdateLopHoc(LopHoc lh)
        {
            if (lh == null) throw new ArgumentNullException(nameof(lh), "Dữ liệu lớp học không được rỗng!");

            try
            {
                string sql = "UPDATE LopHoc SET TenLop = @TenLop WHERE MaLop = @MaLop";
                var parameters = new[]
                {
                    new SqlParameter("@TenLop", SqlDbType.NVarChar, 100) { Value = lh.TenLop.Trim() },
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 20) { Value = lh.MaLop.Trim() }
                };

                int rowsAffected = dataHelper.ExecuteNonQuery(sql, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"[LopHocDAO] Lỗi khi cập nhật lớp học '{lh.MaLop}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Xóa lớp học khỏi CSDL. Ném ngoại lệ nếu xóa thất bại.
        /// </summary>
        public virtual bool DeleteLopHoc(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop)) return false;

            try
            {
                string sql = "DELETE FROM LopHoc WHERE MaLop = @MaLop";
                var parameters = new[]
                {
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 20) { Value = maLop.Trim() }
                };

                int rowsAffected = dataHelper.ExecuteNonQuery(sql, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"[LopHocDAO] Lỗi khi xóa lớp học '{maLop}': {ex.Message}", ex);
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
