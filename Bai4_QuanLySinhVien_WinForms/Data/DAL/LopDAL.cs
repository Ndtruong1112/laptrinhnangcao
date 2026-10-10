using System;
using System.Collections.Generic;
using System.Linq;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    public class LopDAL
    {
        // Dữ liệu dùng chung static để lưu trạng thái xuyên suốt phiên chạy
        private static List<LopHoc> lopHocs = new List<LopHoc>();

        public LopDAL()
        {
            if (lopHocs.Count == 0)
            {
                lopHocs.Add(new LopHoc()
                {
                    MaLop = "CSE0001",
                    TenLop = "Khoa học máy tính 01"
                });
                lopHocs.Add(new LopHoc()
                {
                    MaLop = "CSE0002",
                    TenLop = "Khoa học máy tính 02"
                });
                lopHocs.Add(new LopHoc()
                {
                    MaLop = "CSE0003",
                    TenLop = "Khoa học máy tính 03"
                });
            }
        }

        public List<LopHoc> GetAllLopHoc()
        {
            return lopHocs;
        }

        public LopHoc? GetLopHocById(string maLop)
        {
            return lopHocs.FirstOrDefault(l => l.MaLop.Equals(maLop, StringComparison.OrdinalIgnoreCase));
        }

        public void AddLopHoc(LopHoc lh)
        {
            lopHocs.Add(lh);
        }
    }
}
