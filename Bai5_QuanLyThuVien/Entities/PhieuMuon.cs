using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bai5_QuanLyThuVien.Entities
{
    /// <summary>
    /// Thực thể Phiếu mượn sách trong Quản Lý Thư Viện (Code First).
    /// </summary>
    [Table("PhieuMuon")]
    public class PhieuMuon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaPhieu { get; set; }

        [Required]
        [Column("MaDocGia", TypeName = "nvarchar(20)")]
        [StringLength(20)]
        public string MaDocGia { get; set; } = string.Empty;

        [Required]
        [Column("MaSach", TypeName = "nvarchar(20)")]
        [StringLength(20)]
        public string MaSach { get; set; } = string.Empty;

        [Column("NgayMuon", TypeName = "date")]
        public DateTime NgayMuon { get; set; } = DateTime.Now;

        [Column("NgayHenTra", TypeName = "date")]
        public DateTime NgayHenTra { get; set; } = DateTime.Now.AddDays(14);

        [Column("TrangThai", TypeName = "nvarchar(50)")]
        [StringLength(50)]
        public string TrangThai { get; set; } = "Đang mượn";

        // Khóa ngoại đến Độc giả
        [ForeignKey(nameof(MaDocGia))]
        public virtual DocGia? DocGia { get; set; }

        // Khóa ngoại đến Sách
        [ForeignKey(nameof(MaSach))]
        public virtual Sach? Sach { get; set; }

        public PhieuMuon() { }

        public PhieuMuon(string maDocGia, string maSach, DateTime ngayMuon, DateTime ngayHenTra, string trangThai = "Đang mượn")
        {
            MaDocGia = maDocGia;
            MaSach = maSach;
            NgayMuon = ngayMuon;
            NgayHenTra = ngayHenTra;
            TrangThai = trangThai;
        }
    }
}
