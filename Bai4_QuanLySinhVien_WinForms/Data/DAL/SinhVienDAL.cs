using System;
using System.Collections.Generic;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    /// <summary>
    /// Lớp SinhVienDAL kế thừa từ SinhVienDAO để tương thích 100% với cả 2 quy ước: SinhVienDAL và SinhVienDAO.
    /// Sử dụng ADO.NET để truy vấn và thao tác trực tiếp với CSDL SQL Server.
    /// </summary>
    public class SinhVienDAL : SinhVienDAO
    {
        public SinhVienDAL() : base() { }
        public SinhVienDAL(string connectionString) : base(connectionString) { }
        public SinhVienDAL(DataHelper helper) : base(helper) { }
    }
}
