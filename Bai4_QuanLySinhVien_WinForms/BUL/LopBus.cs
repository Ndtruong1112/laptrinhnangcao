using System;
using System.Collections.Generic;
using System.Text;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.BUL
{
    public class LopBus
    {
        private LopDAL lopDAL;

        public LopBus()
        {
            lopDAL = new LopDAL();
        }

        public List<LopHoc> GetAllLopHoc()
        {
            return lopDAL.GetAllLopHoc();
        }

        public void AddLopHoc(LopHoc lh)
        {
            lopDAL.AddLopHoc(lh);
        }
    }
}
