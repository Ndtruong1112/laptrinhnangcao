using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bai4_QuanLySinhVien_WinForms.Models
{
    public class SinhVien
    {
        [Required(ErrorMessage = "Mã sinh viên bắt buộc phải nhập.")]
        [RegularExpression(@"^SV\d{6}$", ErrorMessage = "Mã sinh viên phải có định dạng SVxxxxxx (ví dụ SV000123).")]
        public string MaSV { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ và tên bắt buộc phải nhập.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Họ và tên phải có độ dài từ 2 đến 50 ký tự.")]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày sinh không được để trống.")]
        public DateTime NgaySinh { get; set; } = DateTime.Now;

        public bool GioiTinh { get; set; } = true; // true: Nam, false: Nữ

        [Required(ErrorMessage = "Email bắt buộc phải nhập.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng hợp lệ (ví dụ: an.nv@vju.ac.vn).")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại bắt buộc phải nhập.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải gồm 10 chữ số và bắt đầu bằng số 0.")]
        public string DienThoai { get; set; } = string.Empty;

        [Required(ErrorMessage = "Điểm số bắt buộc phải nhập.")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm số phải nằm trong khoảng từ 0.0 đến 10.0.")]
        public double Diem { get; set; } = 0.0;

        [Required(ErrorMessage = "Vui lòng chọn lớp học cho sinh viên.")]
        public string MaLop { get; set; } = string.Empty;

        public string TrangThai { get; set; } = "Đang học";

        // Tham chiếu quan hệ n - 1 đến LopHoc
        public LopHoc? LopHoc { get; set; }

        public SinhVien() { }

        public SinhVien(string maSV, string hoTen, DateTime ngaySinh, bool gioiTinh, string email, string dienThoai, double diem, string maLop, string trangThai = "Đang học")
        {
            MaSV = maSV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            Email = email;
            DienThoai = dienThoai;
            Diem = diem;
            MaLop = maLop;
            TrangThai = trangThai;
        }

        // Phương thức kiểm tra tính hợp lệ của đối tượng theo Data Annotations
        public bool IsValid(out List<string> errors)
        {
            errors = new List<string>();
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(this, context, results, validateAllProperties: true);
            if (!isValid)
            {
                foreach (var r in results)
                {
                    if (!string.IsNullOrEmpty(r.ErrorMessage))
                    {
                        errors.Add(r.ErrorMessage);
                    }
                }
            }
            return isValid;
        }
    }
}
