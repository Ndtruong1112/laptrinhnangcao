using System;
using System.Collections.Generic;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    /// <summary>
    /// Lớp LopDAL kế thừa từ LopHocDAO để đảm bảo tương thích 100% với cả 2 quy ước đặt tên: LopDAL và LopHocDAO.
    /// </summary>
    public class LopDAL : LopHocDAO
    {
        public LopDAL() : base() { }
        public LopDAL(string connectionString) : base(connectionString) { }
        public LopDAL(DataHelper helper) : base(helper) { }
    }
}
