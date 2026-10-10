using System;
using System.Collections.Generic;
using System.Linq;
using QuanLySinhVien.Data.Entity;

namespace QuanLySinhVien.Data.DAL
{
    public class SinhVienDAL
    {
        private static List<SinhVien> sinhViens = new List<SinhVien>();
        private readonly LopDAL _lopDAL = new LopDAL();

        public SinhVienDAL()
        {
            if (sinhViens.Count == 0)
            {
                var dsLop = _lopDAL.GetAllLopHoc();
                var lop1 = dsLop.FirstOrDefault(l => l.MaLop == "CSE0001");
                var lop2 = dsLop.FirstOrDefault(l => l.MaLop == "CSE0002");
                var lop3 = dsLop.FirstOrDefault(l => l.MaLop == "CSE0003");

                var sv1 = new SinhVien("SV000123", "Nguyễn Văn An", new DateTime(2006, 8, 15), true, "an.nv@vju.ac.vn", "0912345678", 8.5, "CSE0001", "Đang học") { LopHoc = lop1 };
                var sv2 = new SinhVien("SV000124", "Trần Minh Anh", new DateTime(2006, 1, 22), false, "anh.tm@vju.ac.vn", "0987654321", 9.0, "CSE0002", "Đang học") { LopHoc = lop2 };
                var sv3 = new SinhVien("SV000125", "Lê Hoàng Bình", new DateTime(2006, 5, 9), true, "binh.lh@vju.ac.vn", "0355556677", 7.4, "CSE0001", "Đang học") { LopHoc = lop1 };
                var sv4 = new SinhVien("SV000126", "Đỗ Thị Hồng", new DateTime(2006, 11, 30), false, "hong.dt@vju.ac.vn", "0777888999", 8.1, "CSE0003", "Đang học") { LopHoc = lop3 };

                sinhViens.AddRange(new[] { sv1, sv2, sv3, sv4 });

                // Gán vào quan hệ 1 - n của LopHoc
                lop1?.DanhSachSinhVien.AddRange(new[] { sv1, sv3 });
                lop2?.DanhSachSinhVien.Add(sv2);
                lop3?.DanhSachSinhVien.Add(sv4);
            }
        }

        public List<SinhVien> GetAllSinhVien()
        {
            return sinhViens;
        }

        public SinhVien? GetSinhVienById(string maSV)
        {
            return sinhViens.FirstOrDefault(s => s.MaSV.Equals(maSV, StringComparison.OrdinalIgnoreCase));
        }

        public void AddSinhVien(SinhVien sv)
        {
            var lop = _lopDAL.GetLopHocById(sv.MaLop);
            if (lop != null)
            {
                sv.LopHoc = lop;
                lop.DanhSachSinhVien.Add(sv);
            }
            sinhViens.Add(sv);
        }

        public bool UpdateSinhVien(SinhVien svMoi)
        {
            var svCu = GetSinhVienById(svMoi.MaSV);
            if (svCu == null) return false;

            if (!svCu.MaLop.Equals(svMoi.MaLop, StringComparison.OrdinalIgnoreCase))
            {
                var lopCu = _lopDAL.GetLopHocById(svCu.MaLop);
                var lopMoi = _lopDAL.GetLopHocById(svMoi.MaLop);

                lopCu?.DanhSachSinhVien.Remove(svCu);
                svCu.MaLop = svMoi.MaLop;
                svCu.LopHoc = lopMoi;
                lopMoi?.DanhSachSinhVien.Add(svCu);
            }

            svCu.HoTen = svMoi.HoTen;
            svCu.NgaySinh = svMoi.NgaySinh;
            svCu.GioiTinh = svMoi.GioiTinh;
            svCu.Email = svMoi.Email;
            svCu.DienThoai = svMoi.DienThoai;
            svCu.Diem = svMoi.Diem;
            svCu.TrangThai = svMoi.TrangThai;
            return true;
        }

        public bool DeleteSinhVien(string maSV)
        {
            var sv = GetSinhVienById(maSV);
            if (sv == null) return false;

            var lop = _lopDAL.GetLopHocById(sv.MaLop);
            lop?.DanhSachSinhVien.Remove(sv);

            return sinhViens.Remove(sv);
        }

        public List<SinhVien> Search(string tuKhoa, string maLop, double diemTu)
        {
            tuKhoa = tuKhoa.Trim().ToLower();

            return sinhViens.Where(sv =>
            {
                bool matchTuKhoa = string.IsNullOrEmpty(tuKhoa) ||
                    sv.MaSV.ToLower().Contains(tuKhoa) ||
                    sv.HoTen.ToLower().Contains(tuKhoa) ||
                    sv.Email.ToLower().Contains(tuKhoa) ||
                    sv.DienThoai.ToLower().Contains(tuKhoa);

                bool matchLop = string.IsNullOrEmpty(maLop) || sv.MaLop.Equals(maLop, StringComparison.OrdinalIgnoreCase);
                bool matchDiem = sv.Diem >= diemTu;

                return matchTuKhoa && matchLop && matchDiem;
            }).ToList();
        }

        public List<SinhVien> GetSinhVienByLop(string maLop)
        {
            if (string.IsNullOrEmpty(maLop)) return GetAllSinhVien();
            return sinhViens.Where(s => s.MaLop.Equals(maLop, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
