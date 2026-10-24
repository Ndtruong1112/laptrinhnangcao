using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLySinhVien.Data.Entity
{
    /// <summary>
    /// Thực thể Sinh viên (SinhVien) - Mô hình Code First cho Entity Framework Core.
    /// Đại diện cho bảng [SinhVien] trong cơ sở dữ liệu.
    /// </summary>
    [Table("SinhVien")]
    public class SinhVien
    {
        [Key]
        [Column("MaSV", TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "Mã sinh viên bắt buộc phải nhập.")]
        [RegularExpression(@"^SV\d{6}$", ErrorMessage = "Mã sinh viên phải có định dạng SVxxxxxx (ví dụ SV000123).")]
        [StringLength(20)]
        public string MaSV { get; set; } = string.Empty;

        [Column("HoTen", TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Họ và tên bắt buộc phải nhập.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Họ và tên phải có độ dài từ 2 đến 100 ký tự.")]
        public string HoTen { get; set; } = string.Empty;

        [Column("NgaySinh", TypeName = "date")]
        [Required(ErrorMessage = "Ngày sinh không được để trống.")]
        public DateTime NgaySinh { get; set; } = DateTime.Now;

        [Column("GioiTinh", TypeName = "nvarchar(10)")]
        public bool GioiTinh { get; set; } = true; // true: Nam, false: Nữ (chuyển đổi sang nvarchar qua ValueConverter)

        [Column("Email", TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "Email bắt buộc phải nhập.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng hợp lệ (ví dụ: an.nv@vju.ac.vn).")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Column("SoDienThoai", TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "Số điện thoại bắt buộc phải nhập.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải gồm 10 chữ số và bắt đầu bằng số 0.")]
        [StringLength(20)]
        public string SoDienThoai { get; set; } = string.Empty;

        // Alias DienThoai để tương thích ngược với code cũ và không map thêm cột trùng lặp vào DB
        [NotMapped]
        public string DienThoai
        {
            get => SoDienThoai;
            set => SoDienThoai = value;
        }

        [Column("Diem")]
        [Required(ErrorMessage = "Điểm số bắt buộc phải nhập.")]
        [Range(0.0, 10.0, ErrorMessage = "Điểm số phải nằm trong khoảng từ 0.0 đến 10.0.")]
        public double Diem { get; set; } = 0.0;

        [Column("MaLop", TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "Vui lòng chọn lớp học cho sinh viên.")]
        [StringLength(20)]
        public string MaLop { get; set; } = string.Empty;

        [Column("TrangThai", TypeName = "nvarchar(50)")]
        [StringLength(50)]
        public string TrangThai { get; set; } = "Đang học";

        // Navigation Property cho Entity Framework Core: Quan hệ N - 1 (Nhiều sinh viên thuộc 1 lớp học)
        [ForeignKey(nameof(MaLop))]
        [InverseProperty(nameof(Entity.LopHoc.DanhSachSinhVien))]
        public virtual LopHoc? LopHoc { get; set; }

        public SinhVien() { }

        public SinhVien(string maSV, string hoTen, DateTime ngaySinh, bool gioiTinh, string email, string dienThoai, double diem, string maLop, string trangThai = "Đang học")
        {
            MaSV = maSV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            Email = email;
            SoDienThoai = dienThoai;
            DienThoai = dienThoai;
            Diem = diem;
            MaLop = maLop;
            TrangThai = trangThai;
        }

        // Phương thức kiểm tra tính không hợp lệ theo mẫu bài học: IsInValid().Count > 0
        public List<ValidationResult> IsInValid()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, results, validateAllProperties: true);
            return results;
        }

        // Phương thức kiểm tra tính hợp lệ trả về danh sách chuỗi lỗi
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
    }
}
