using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bai5_QuanLyThuVien.Entities
{
    /// <summary>
    /// Thực thể Sách trong Quản Lý Thư Viện (Code First).
    /// </summary>
    [Table("Sach")]
    public class Sach
    {
        [Key]
        [Column("MaSach", TypeName = "nvarchar(20)")]
        [StringLength(20)]
        public string MaSach { get; set; } = string.Empty;

        [Required]
        [Column("TenSach", TypeName = "nvarchar(150)")]
        [StringLength(150)]
        public string TenSach { get; set; } = string.Empty;

        [Column("TacGia", TypeName = "nvarchar(100)")]
        [StringLength(100)]
        public string TacGia { get; set; } = string.Empty;

        [Column("NamXuatBan")]
        public int NamXuatBan { get; set; } = 2024;

        [Column("GiaTien")]
        public double GiaTien { get; set; } = 0.0;

        [Column("SoLuong")]
        public int SoLuong { get; set; } = 1;

        [Required]
        [Column("MaTheLoai", TypeName = "nvarchar(20)")]
        [StringLength(20)]
        public string MaTheLoai { get; set; } = string.Empty;

        // Khóa ngoại đến bảng TheLoai (N - 1)
        [ForeignKey(nameof(MaTheLoai))]
        public virtual TheLoai? TheLoai { get; set; }

        // Quan hệ 1 - N với Phiếu mượn
        public virtual ICollection<PhieuMuon> PhieuMuons { get; set; } = new HashSet<PhieuMuon>();

        public Sach() { }

        public Sach(string maSach, string tenSach, string tacGia, int namXuatBan, double giaTien, int soLuong, string maTheLoai)
        {
            MaSach = maSach;
            TenSach = tenSach;
            TacGia = tacGia;
            NamXuatBan = namXuatBan;
            GiaTien = giaTien;
            SoLuong = soLuong;
            MaTheLoai = maTheLoai;
        }

        public override string ToString() => TenSach;
    }
}
