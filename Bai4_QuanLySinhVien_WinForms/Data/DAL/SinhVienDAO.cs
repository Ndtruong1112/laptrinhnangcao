using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.SqlClient;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    /// <summary>
    /// Tầng Data Access Layer / Data Access Object (DAO) cho thực thể SinhVien.
    /// Thao tác trực tiếp với CSDL SQL Server bằng ADO.NET qua DataHelper.
    /// YÊU CẦU ĐỀ BÀI: Mọi phương thức ném về ngoại lệ khi gặp lỗi để tầng trên xử lý.
    /// </summary>
    public class SinhVienDAO
    {
        protected readonly DataHelper dataHelper;

        public SinhVienDAO()
        {
            dataHelper = new DataHelper();
        }

        public SinhVienDAO(string connectionString)
        {
            dataHelper = new DataHelper(connectionString);
        }

        public SinhVienDAO(DataHelper helper)
        {
            dataHelper = helper;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách sinh viên từ CSDL kèm tên lớp. Ném ngoại lệ nếu có lỗi.
        /// </summary>
        public virtual List<SinhVien> GetAllSinhVien()
        {
            try
            {
                var list = new List<SinhVien>();
                string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email, 
                                      sv.SoDienThoai, sv.Diem, sv.MaLop, 
                                      ISNULL(l.TenLop, N'') AS TenLop, sv.TrangThai 
                               FROM SinhVien sv 
                               LEFT JOIN LopHoc l ON sv.MaLop = l.MaLop 
                               ORDER BY sv.MaSV ASC";

                DataTable dt = dataHelper.GetDataTable(sql);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(DocSinhVienTuDataRow(row));
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO] Lỗi khi lấy danh sách sinh viên từ CSDL: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy danh sách sinh viên bằng SqlDataReader (phương thức mở rộng theo mẫu cô giáo).
        /// </summary>
        public virtual List<SinhVien> GetAllSinhVien1()
        {
            try
            {
                var list = new List<SinhVien>();
                string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email, 
                                      sv.SoDienThoai, sv.Diem, sv.MaLop, 
                                      ISNULL(l.TenLop, N'') AS TenLop, sv.TrangThai 
                               FROM SinhVien sv 
                               LEFT JOIN LopHoc l ON sv.MaLop = l.MaLop 
                               ORDER BY sv.MaSV ASC";

                using (SqlDataReader dr = dataHelper.ExecuteReader(sql))
                {
                    while (dr.Read())
                    {
                        list.Add(DocSinhVienTuDataReader(dr));
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO] Lỗi khi đọc sinh viên qua DataReader: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lấy thông tin sinh viên theo mã sinh viên. Ném ngoại lệ nếu có lỗi.
        /// </summary>
        public virtual SinhVien? GetSinhVienById(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV)) return null;

            try
            {
                string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email, 
                                      sv.SoDienThoai, sv.Diem, sv.MaLop, 
                                      ISNULL(l.TenLop, N'') AS TenLop, sv.TrangThai 
                               FROM SinhVien sv 
                               LEFT JOIN LopHoc l ON sv.MaLop = l.MaLop 
                               WHERE sv.MaSV = @MaSV";

                var parameters = new[]
                {
                    new SqlParameter("@MaSV", SqlDbType.NVarChar, 20) { Value = maSV.Trim() }
                };

                DataTable dt = dataHelper.GetDataTable(sql, parameters);
                if (dt.Rows.Count > 0)
                {
                    return DocSinhVienTuDataRow(dt.Rows[0]);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO] Lỗi khi tìm sinh viên có mã '{maSV}': {ex.Message}", ex);
            }
        }

        public virtual SinhVien? GetSinhVienByMaSV(string maSV) => GetSinhVienById(maSV);

        /// <summary>
        /// Lấy danh sách sinh viên theo mã lớp. Ném ngoại lệ nếu có lỗi.
        /// </summary>
        public virtual List<SinhVien> GetSinhVienByLop(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop) || maLop.Equals("Tất cả lớp", StringComparison.OrdinalIgnoreCase))
            {
                return GetAllSinhVien();
            }

            try
            {
                var list = new List<SinhVien>();
                string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email, 
                                      sv.SoDienThoai, sv.Diem, sv.MaLop, 
                                      ISNULL(l.TenLop, N'') AS TenLop, sv.TrangThai 
                               FROM SinhVien sv 
                               LEFT JOIN LopHoc l ON sv.MaLop = l.MaLop 
                               WHERE sv.MaLop = @MaLop 
                               ORDER BY sv.MaSV ASC";

                var parameters = new[]
                {
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 20) { Value = maLop.Trim() }
                };

                DataTable dt = dataHelper.GetDataTable(sql, parameters);
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(DocSinhVienTuDataRow(row));
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO] Lỗi khi lấy sinh viên theo mã lớp '{maLop}': {ex.Message}", ex);
            }
        }

        public virtual List<SinhVien> GetSinhViensByMaLop(string maLop) => GetSinhVienByLop(maLop);

        /// <summary>
        /// Thêm mới sinh viên vào CSDL. Sử dụng câu lệnh Parameterized chuẩn xác an toàn.
        /// Ném ngoại lệ nếu thêm thất bại.
        /// </summary>
        public virtual void AddSinhVien(SinhVien sv)
        {
            if (sv == null) throw new ArgumentNullException(nameof(sv), "Dữ liệu sinh viên không được rỗng!");

            try
            {
                string sql = @"INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, Email, SoDienThoai, Diem, MaLop, TrangThai) 
                               VALUES (@MaSV, @HoTen, @NgaySinh, @GioiTinh, @Email, @SoDienThoai, @Diem, @MaLop, @TrangThai)";

                var parameters = new[]
                {
                    new SqlParameter("@MaSV", SqlDbType.NVarChar, 20) { Value = sv.MaSV.Trim() },
                    new SqlParameter("@HoTen", SqlDbType.NVarChar, 100) { Value = sv.HoTen.Trim() },
                    new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = sv.NgaySinh },
                    new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 10) { Value = sv.GioiTinh ? "Nam" : "Nữ" },
                    new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = sv.Email.Trim() },
                    new SqlParameter("@SoDienThoai", SqlDbType.NVarChar, 20) { Value = sv.SoDienThoai.Trim() },
                    new SqlParameter("@Diem", SqlDbType.Float) { Value = sv.Diem },
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 20) { Value = sv.MaLop.Trim() },
                    new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = sv.TrangThai }
                };

                dataHelper.ExecuteNonQuery(sql, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO] Lỗi khi thêm mới sinh viên '{sv.MaSV}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên trong CSDL.
        /// Khắc phục lỗi sai cú pháp SQL UPDATE của kịch bản cũ.
        /// Ném ngoại lệ nếu cập nhật thất bại.
        /// </summary>
        public virtual bool UpdateSinhVien(SinhVien sv)
        {
            if (sv == null) throw new ArgumentNullException(nameof(sv), "Dữ liệu sinh viên không được rỗng!");

            try
            {
                string sql = @"UPDATE SinhVien 
                               SET HoTen = @HoTen, 
                                   NgaySinh = @NgaySinh, 
                                   GioiTinh = @GioiTinh, 
                                   Email = @Email, 
                                   SoDienThoai = @SoDienThoai, 
                                   Diem = @Diem, 
                                   MaLop = @MaLop, 
                                   TrangThai = @TrangThai 
                               WHERE MaSV = @MaSV";

                var parameters = new[]
                {
                    new SqlParameter("@HoTen", SqlDbType.NVarChar, 100) { Value = sv.HoTen.Trim() },
                    new SqlParameter("@NgaySinh", SqlDbType.Date) { Value = sv.NgaySinh },
                    new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 10) { Value = sv.GioiTinh ? "Nam" : "Nữ" },
                    new SqlParameter("@Email", SqlDbType.NVarChar, 100) { Value = sv.Email.Trim() },
                    new SqlParameter("@SoDienThoai", SqlDbType.NVarChar, 20) { Value = sv.SoDienThoai.Trim() },
                    new SqlParameter("@Diem", SqlDbType.Float) { Value = sv.Diem },
                    new SqlParameter("@MaLop", SqlDbType.NVarChar, 20) { Value = sv.MaLop.Trim() },
                    new SqlParameter("@TrangThai", SqlDbType.NVarChar, 50) { Value = sv.TrangThai },
                    new SqlParameter("@MaSV", SqlDbType.NVarChar, 20) { Value = sv.MaSV.Trim() }
                };

                int rowsAffected = dataHelper.ExecuteNonQuery(sql, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO] Lỗi khi cập nhật sinh viên '{sv.MaSV}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Xóa sinh viên khỏi CSDL theo mã sinh viên.
        /// Khắc phục triệt để lỗi của code bạn học (quên xóa trong CSDL).
        /// Ném ngoại lệ nếu xóa thất bại.
        /// </summary>
        public virtual bool DeleteSinhVien(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV)) return false;

            try
            {
                string sql = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
                var parameters = new[]
                {
                    new SqlParameter("@MaSV", SqlDbType.NVarChar, 20) { Value = maSV.Trim() }
                };

                int rowsAffected = dataHelper.ExecuteNonQuery(sql, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO] Lỗi khi xóa sinh viên '{maSV}': {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Tìm kiếm sinh viên từ CSDL theo từ khóa, mã lớp và điểm tối thiểu.
        /// </summary>
        public virtual List<SinhVien> Search(string tuKhoa, string maLop, double diemTu)
        {
            try
            {
                tuKhoa = tuKhoa?.Trim() ?? string.Empty;
                maLop = maLop?.Trim() ?? string.Empty;

                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@TuKhoa", SqlDbType.NVarChar, 100) { Value = $"%{tuKhoa}%" },
                    new SqlParameter("@DiemTu", SqlDbType.Float) { Value = diemTu }
                };

                string sql = @"SELECT sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.Email, 
                                      sv.SoDienThoai, sv.Diem, sv.MaLop, 
                                      ISNULL(l.TenLop, N'') AS TenLop, sv.TrangThai 
                               FROM SinhVien sv 
                               LEFT JOIN LopHoc l ON sv.MaLop = l.MaLop 
                               WHERE (sv.MaSV LIKE @TuKhoa OR sv.HoTen LIKE @TuKhoa OR sv.Email LIKE @TuKhoa OR sv.SoDienThoai LIKE @TuKhoa)
                                 AND sv.Diem >= @DiemTu";

                if (!string.IsNullOrEmpty(maLop) && !maLop.Equals("Tất cả lớp", StringComparison.OrdinalIgnoreCase))
                {
                    sql += " AND sv.MaLop = @MaLop";
                    parameters.Add(new SqlParameter("@MaLop", SqlDbType.NVarChar, 20) { Value = maLop });
                }

                sql += " ORDER BY sv.MaSV ASC";

                DataTable dt = dataHelper.GetDataTable(sql, parameters.ToArray());
                var list = new List<SinhVien>();
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(DocSinhVienTuDataRow(row));
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception($"[SinhVienDAO] Lỗi khi tìm kiếm sinh viên trong CSDL: {ex.Message}", ex);
            }
        }

        // Tương thích với các tên gọi khác
        public List<SinhVien> LayTatCa() => GetAllSinhVien();
        public SinhVien? LayTheoMa(string maSV) => GetSinhVienById(maSV);
        public List<SinhVien> LayTheoLop(string maLop) => GetSinhVienByLop(maLop);
        public bool Them(SinhVien sv) { AddSinhVien(sv); return true; }
        public bool Sua(SinhVien sv) => UpdateSinhVien(sv);
        public bool Xoa(string maSV) => DeleteSinhVien(maSV);

        #region --- HÀM TIỆN ÍCH CHUYỂN ĐỔI DỮ LIỆU ---

        private static SinhVien DocSinhVienTuDataRow(DataRow row)
        {
            var sv = new SinhVien();
            sv.MaSV = row["MaSV"]?.ToString() ?? string.Empty;
            sv.HoTen = row["HoTen"]?.ToString() ?? string.Empty;

            if (row["NgaySinh"] != DBNull.Value)
            {
                sv.NgaySinh = Convert.ToDateTime(row["NgaySinh"]);
            }

            string gioiTinhStr = row["GioiTinh"]?.ToString() ?? "Nam";
            sv.GioiTinh = gioiTinhStr.Equals("Nam", StringComparison.OrdinalIgnoreCase) || gioiTinhStr == "True" || gioiTinhStr == "1";

            sv.Email = row["Email"]?.ToString() ?? string.Empty;
            sv.SoDienThoai = row["SoDienThoai"]?.ToString() ?? (row.Table.Columns.Contains("DienThoai") ? row["DienThoai"]?.ToString() ?? string.Empty : string.Empty);

            if (row["Diem"] != DBNull.Value)
            {
                sv.Diem = Convert.ToDouble(row["Diem"]);
            }

            sv.MaLop = row["MaLop"]?.ToString() ?? string.Empty;
            sv.TrangThai = row["TrangThai"]?.ToString() ?? "Đang học";

            string tenLop = row.Table.Columns.Contains("TenLop") ? row["TenLop"]?.ToString() ?? string.Empty : string.Empty;
            sv.LopHoc = new LopHoc(sv.MaLop, tenLop);

            return sv;
        }

        private static SinhVien DocSinhVienTuDataReader(SqlDataReader dr)
        {
            var sv = new SinhVien();
            sv.MaSV = dr["MaSV"]?.ToString() ?? string.Empty;
            sv.HoTen = dr["HoTen"]?.ToString() ?? string.Empty;

            if (dr["NgaySinh"] != DBNull.Value)
            {
                sv.NgaySinh = Convert.ToDateTime(dr["NgaySinh"]);
            }

            string gioiTinhStr = dr["GioiTinh"]?.ToString() ?? "Nam";
            sv.GioiTinh = gioiTinhStr.Equals("Nam", StringComparison.OrdinalIgnoreCase) || gioiTinhStr == "True" || gioiTinhStr == "1";

            sv.Email = dr["Email"]?.ToString() ?? string.Empty;
            sv.SoDienThoai = dr["SoDienThoai"]?.ToString() ?? string.Empty;

            if (dr["Diem"] != DBNull.Value)
            {
                sv.Diem = Convert.ToDouble(dr["Diem"]);
            }

            sv.MaLop = dr["MaLop"]?.ToString() ?? string.Empty;
            sv.TrangThai = dr["TrangThai"]?.ToString() ?? "Đang học";

            string tenLop = dr["TenLop"]?.ToString() ?? string.Empty;
            sv.LopHoc = new LopHoc(sv.MaLop, tenLop);

            return sv;
        }

        #endregion
    }
}
