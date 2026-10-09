using System;
using System.Collections.Generic;
using Bai4_QuanLySinhVien_WinForms.Models;

namespace Bai4_QuanLySinhVien_WinForms.Data
{
    public static class DuLieuMau
    {
        public static List<LopHoc> LayDanhSachLopHoc()
        {
            var dsLop = new List<LopHoc>
            {
                new LopHoc("KTPM01", "Kỹ thuật phần mềm 01"),
                new LopHoc("TTNT01", "Trí tuệ nhân tạo 01"),
                new LopHoc("KHDL01", "Khoa học dữ liệu 01")
            };

            var dsSV = new List<SinhVien>
            {
                new SinhVien(
                    "SV000123",
                    "Nguyễn Văn An",
                    new DateTime(2006, 8, 15),
                    true,
                    "an.nv@vju.ac.vn",
                    "0912345678",
                    8.5,
                    "KTPM01",
                    "Đang học"
                ),
                new SinhVien(
                    "SV000124",
                    "Trần Minh Anh",
                    new DateTime(2006, 1, 22),
                    false,
                    "anh.tm@vju.ac.vn",
                    "0987654321",
                    9.0,
                    "TTNT01",
                    "Đang học"
                ),
                new SinhVien(
                    "SV000125",
                    "Lê Hoàng Bình",
                    new DateTime(2006, 5, 9),
                    true,
                    "binh.lh@vju.ac.vn",
                    "0355556677",
                    7.4,
                    "KTPM01",
                    "Đang học"
                ),
                new SinhVien(
                    "SV000126",
                    "Đỗ Thị Hồng",
                    new DateTime(2006, 11, 30),
                    false,
                    "hong.dt@vju.ac.vn",
                    "0777888999",
                    8.1,
                    "KHDL01",
                    "Đang học"
                )
            };

            // Thiết lập quan hệ 1 - n giữa LopHoc và SinhVien
            foreach (var lop in dsLop)
            {
                foreach (var sv in dsSV)
                {
                    if (sv.MaLop == lop.MaLop)
                    {
                        sv.LopHoc = lop;
                        lop.DanhSachSinhVien.Add(sv);
                    }
                }
            }

            return dsLop;
        }
    }
}
