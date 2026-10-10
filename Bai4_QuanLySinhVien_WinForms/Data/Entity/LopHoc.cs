using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bai4_QuanLySinhVien_WinForms.Data.Entity
{
    public class LopHoc
    {
        [Required(ErrorMessage = "Mã lớp không được để trống.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Mã lớp phải từ 2 đến 20 ký tự.")]
        public string MaLop { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên lớp không được để trống.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên lớp phải từ 3 đến 100 ký tự.")]
        public string TenLop { get; set; } = string.Empty;

        // Quan hệ 1 - n: Một lớp học có nhiều sinh viên
        public List<SinhVien> DanhSachSinhVien { get; set; } = new();

        public LopHoc() { }

        public LopHoc(string maLop, string tenLop)
        {
            MaLop = maLop;
            TenLop = tenLop;
        }

        // Phương thức kiểm tra hợp lệ của đối tượng dùng Data Annotations
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

        public override string ToString() => TenLop;
    }
}
