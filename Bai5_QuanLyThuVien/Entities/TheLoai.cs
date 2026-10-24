using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bai5_QuanLyThuVien.Entities
{
    /// <summary>
    /// Thực thể Thể loại sách trong Quản Lý Thư Viện (Code First).
    /// </summary>
    [Table("TheLoai")]
    public class TheLoai
    {
        [Key]
        [Column("MaTheLoai", TypeName = "nvarchar(20)")]
        [StringLength(20)]
        public string MaTheLoai { get; set; } = string.Empty;

        [Required]
        [Column("TenTheLoai", TypeName = "nvarchar(100)")]
        [StringLength(100)]
        public string TenTheLoai { get; set; } = string.Empty;

        [Column("MoTa", TypeName = "nvarchar(250)")]
        [StringLength(250)]
        public string? MoTa { get; set; }

        // Quan hệ 1 - N: Một thể loại có nhiều đầu sách
        public virtual ICollection<Sach> Sachs { get; set; } = new HashSet<Sach>();

        public TheLoai() { }

        public TheLoai(string maTheLoai, string tenTheLoai, string? moTa = null)
        {
            MaTheLoai = maTheLoai;
            TenTheLoai = tenTheLoai;
            MoTa = moTa;
        }

        public override string ToString() => TenTheLoai;
    }
}
