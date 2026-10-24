using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVien.Data.Entity
{
    /// <summary>
    /// Thực thể Lớp học (LopHoc) - Mô hình Code First cho Entity Framework Core.
    /// Đại diện cho bảng [LopHoc] trong cơ sở dữ liệu.
    /// </summary>
    [Table("LopHoc")]
    public class LopHoc
    {
        [Key]
        [Column("MaLop", TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "Mã lớp không được để trống.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Mã lớp phải từ 2 đến 20 ký tự.")]
        public string MaLop { get; set; } = string.Empty;

        [Column("TenLop", TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Tên lớp không được để trống.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên lớp phải từ 3 đến 100 ký tự.")]
        public string TenLop { get; set; } = string.Empty;

        // Navigation Property cho Entity Framework Core: Quan hệ 1 - N (Một lớp có nhiều sinh viên)
        [InverseProperty(nameof(SinhVien.LopHoc))]
        public virtual ICollection<SinhVien> DanhSachSinhVien { get; set; } = new HashSet<SinhVien>();

        public LopHoc() { }

        public LopHoc(string maLop, string tenLop)
        {
            MaLop = maLop;
            TenLop = tenLop;
        }

        // Phương thức kiểm tra tính không hợp lệ theo mẫu bài học: IsInValid().Count > 0
        public List<ValidationResult> IsInValid()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, validateAllProperties: true);
            return results;
        }

        public bool IsValid(out List<string> errors)
        {
            errors = new List<string>();
            var results = IsInValid();
            if (results.Count > 0)
            {
                foreach (var r in results)
                {
                    if (!string.IsNullOrEmpty(r.ErrorMessage))
                    {
                        errors.Add(r.ErrorMessage);
                    }
                }
                return false;
            }
            return true;
        }

        public override string ToString() => TenLop;
    }
}
