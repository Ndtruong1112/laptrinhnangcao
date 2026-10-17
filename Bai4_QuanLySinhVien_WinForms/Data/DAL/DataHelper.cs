using System;
using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;

namespace QuanLySinhVien.Data.DAL
{
    /// <summary>
    /// Lớp tiện ích kết nối và thao tác với CSDL SQL Server bằng ADO.NET
    /// Tuân thủ yêu cầu: Ném ngoại lệ khi xảy ra lỗi để tầng trên xử lý.
    /// </summary>
    public class DataHelper
    {
        private SqlConnection con;
        private string connectionString;

        public SqlConnection Connection => con;
        public string ConnectionString => connectionString;

        public DataHelper()
        {
            // 1. Thử đọc chuỗi kết nối từ App.config
            string strcon = string.Empty;
            try
            {
                strcon = ConfigurationManager.ConnectionStrings["QLSV"]?.ConnectionString
                         ?? ConfigurationManager.ConnectionStrings["studentDataBase"]?.ConnectionString
                         ?? string.Empty;
            }
            catch
            {
                // Bỏ qua lỗi đọc config để dùng chuỗi mặc định bên dưới
            }

            // 2. Nếu không tìm thấy trong App.config, dùng chuỗi kết nối mặc định
            if (string.IsNullOrWhiteSpace(strcon))
            {
                strcon = @"Data Source=.;Initial Catalog=QLSV;Integrated Security=True;TrustServerCertificate=True";
            }

            this.connectionString = strcon;
            this.con = new SqlConnection(connectionString);
        }

        public DataHelper(string customConnectionString)
        {
            this.connectionString = customConnectionString;
            this.con = new SqlConnection(connectionString);
        }

        /// <summary>
        /// Mở kết nối tới CSDL. Ném ngoại lệ nếu kết nối thất bại.
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi không thể kết nối tới cơ sở dữ liệu: {ex.Message}", ex);
            }
        }

        public string MoKetNoi()
        {
            try
            {
                OpenConnection();
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Đóng kết nối CSDL an toàn.
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            catch
            {
                // Bỏ qua lỗi khi đóng kết nối
            }
        }

        public void DongKetNoi() => CloseConnection();

        /// <summary>
        /// Thực thi các câu lệnh INSERT, UPDATE, DELETE. Ném ngoại lệ nếu có lỗi.
        /// </summary>
        public int ExecuteNonQuery(string sqlQuery, SqlParameter[]? parameters = null)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi thực thi câu lệnh SQL (ExecuteNonQuery): {ex.Message}", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void ExcuteNonQuery(string sqlQuery) => ExecuteNonQuery(sqlQuery);

        /// <summary>
        /// Thực thi câu lệnh SELECT và trả về SqlDataReader.
        /// Sử dụng CommandBehavior.CloseConnection để tự động đóng kết nối khi DataReader đóng.
        /// Ném ngoại lệ nếu xảy ra lỗi.
        /// </summary>
        public SqlDataReader ExecuteReader(string sqlSelect, SqlParameter[]? parameters = null)
        {
            try
            {
                OpenConnection();
                SqlCommand cmd = new SqlCommand(sqlSelect, con);
                if (parameters != null && parameters.Length > 0)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw new Exception($"Lỗi truy vấn dữ liệu từ CSDL (ExecuteReader): {ex.Message}", ex);
            }
        }

        public SqlDataReader ExcuteReader(string sqlSelect) => ExecuteReader(sqlSelect);

        /// <summary>
        /// Lấy dữ liệu SELECT và đổ vào một DataTable thông qua SqlDataAdapter.
        /// Ném ngoại lệ nếu xảy ra lỗi.
        /// </summary>
        public DataTable GetDataTable(string sqlSelect, SqlParameter[]? parameters = null)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sqlSelect, con))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi lấy dữ liệu DataTable từ CSDL: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Thực thi truy vấn trả về một giá trị duy nhất (ExecuteScalar).
        /// Ném ngoại lệ nếu xảy ra lỗi.
        /// </summary>
        public object? ExecuteScalar(string sqlSelect, SqlParameter[]? parameters = null)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(sqlSelect, con))
                {
                    if (parameters != null && parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi thực thi ExecuteScalar: {ex.Message}", ex);
            }
            finally
            {
                CloseConnection();
            }
        }

        public object? ExcuteScalar(string sqlSelect) => ExecuteScalar(sqlSelect);
    }
}
