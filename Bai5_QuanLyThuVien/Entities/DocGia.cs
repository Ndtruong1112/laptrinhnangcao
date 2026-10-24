using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bai5_QuanLyThuVien.Entities
{
    /// <summary>
    /// Thực thể Độc giả trong Quản Lý Thư Viện (Code First).
    /// </summary>
    [Table("DocGia")]
    public class DocGia
    {
        [Key]
        [Column("MaDocGia", TypeName = "nvarchar(20)")]
        [StringLength(20)]
        public string MaDocGia { get; set; } = string.Empty;

        [Required]
        [Column("HoTen", TypeName = "nvarchar(100)")]
        [StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        [Column("NgaySinh", TypeName = "date")]
        public DateTime NgaySinh { get; set; } = new DateTime(2000, 1, 1);

        [Column("SoDienThoai", TypeName = "nvarchar(20)")]
        [StringLength(20)]
        public string SoDienThoai { get; set; } = string.Empty;

        [Column("Email", TypeName = "nvarchar(100)")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        // Quan hệ 1 - N với Phiếu mượn
        public virtual ICollection<PhieuMuon> PhieuMuons { get; set; } = new HashSet<PhieuMuon>();

        public DocGia() { }

        public DocGia(string maDocGia, string hoTen, DateTime ngaySinh, string soDienThoai, string email)
        {
            MaDocGia = maDocGia;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            SoDienThoai = soDienThoai;
            Email = email;
        }

        public override string ToString() => HoTen;
    }
}
