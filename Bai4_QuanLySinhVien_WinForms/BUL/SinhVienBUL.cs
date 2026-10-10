using System;
using System.Collections.Generic;
using System.Text;
using QuanLySinhVien.Data.DAL;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.BUL
{
    public class SinhVienBUL
    {
        private SinhVienDAL svd;

        public SinhVienBUL()
        {
            svd = new SinhVienDAL();
        }

        public List<SinhVien> GetAllSinhVien()
        {
            return svd.GetAllSinhVien();
        }

        public SinhVien? GetSinhVienById(string maSV)
        {
            return svd.GetSinhVienById(maSV);
        }

        public void AddSinhVien(SinhVien sv)
        {
            if (sv.IsInValid().Count > 0)
            {
                throw new Exception("Sinh viên không hợp lệ");
            }

            var s = svd.GetSinhVienById(sv.MaSV);
            if (s != null)
            {
                throw new Exception("Mã sinh viên đã tồn tại");
            }

            // Thêm sinh viên vào cơ sở dữ liệu
            svd.AddSinhVien(sv);
        }

        public void UpdateSinhVien(SinhVien sv)
        {
            if (sv.IsInValid().Count > 0)
            {
                throw new Exception("Sinh viên không hợp lệ");
            }

            bool ok = svd.UpdateSinhVien(sv);
            if (!ok)
            {
                throw new Exception("Không tìm thấy sinh viên để cập nhật");
            }
        }

        public void DeleteSinhVien(string maSV)
        {
            bool ok = svd.DeleteSinhVien(maSV);
            if (!ok)
            {
                throw new Exception("Không tìm thấy sinh viên để xóa");
            }
        }

        public List<SinhVien> Search(string tuKhoa, string maLop, double diemTu)
        {
            return svd.Search(tuKhoa, maLop, diemTu);
        }
    }
}
