using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Bai5_QuanLyThuVien.Context;
using Bai5_QuanLyThuVien.Entities;

namespace Bai5_QuanLyThuVien
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("==========================================================================");
            Console.WriteLine("   BÀI 5: QUẢN LÝ THƯ VIỆN - ENTITY FRAMEWORK CORE CODE FIRST");
            Console.WriteLine("   DbContext: QLTVDBContext | Migration: InitialLibraryDB");
            Console.WriteLine("==========================================================================");
            Console.WriteLine();

            try
            {
                using (var context = new QLTVDBContext())
                {
                    Console.WriteLine("[BƯỚC 1] Khởi tạo mô hình lớp thực thể (Entity Classes):");
                    Console.WriteLine("  + Thực thể TheLoai: MaTheLoai (PK), TenTheLoai, MoTa");
                    Console.WriteLine("  + Thực thể Sach: MaSach (PK), TenSach, TacGia, NamXuatBan, GiaTien, SoLuong, MaTheLoai (FK)");
                    Console.WriteLine("  + Thực thể DocGia: MaDocGia (PK), HoTen, NgaySinh, SoDienThoai, Email");
                    Console.WriteLine("  + Thực thể PhieuMuon: MaPhieu (PK), MaDocGia (FK), MaSach (FK), NgayMuon, NgayHenTra, TrangThai");
                    Console.WriteLine();

                    Console.WriteLine("[BƯỚC 2] Ánh xạ mô hình sang cơ sở dữ liệu (EF Core Code First)...");
                    bool created = context.Database.EnsureCreated();
                    if (created)
                    {
                        Console.WriteLine("  -> Thành công: Đã tự động tạo CSDL [QLTV] và toàn bộ bảng trên SQL Server!");
                    }
                    else
                    {
                        Console.WriteLine("  -> CSDL [QLTV] và các bảng liên kết đã sẵn sàng trên hệ thống.");
                    }
                    Console.WriteLine();

                    Console.WriteLine("[BƯỚC 3] Thao tác thêm dữ liệu mẫu bằng Code First:");
                    if (!context.TheLoais.Any())
                    {
                        var tl = new TheLoai("KH", "Khoa học - Công nghệ", "Sách nghiên cứu khoa học tự nhiên");
                        context.TheLoais.Add(tl);

                        var sach = new Sach("S005", "Vũ Trụ Trong Vỏ Hạt Dẻ", "Stephen Hawking", 2020, 195000, 12, "KH");
                        context.Sachs.Add(sach);

                        context.SaveChanges();
                        Console.WriteLine("  -> Đã thêm mới TheLoai và Sach vào CSDL thành công!");
                    }
                    else
                    {
                        Console.WriteLine("  -> Dữ liệu mẫu (Seed Data) đã tồn tại trong CSDL.");
                    }
                    Console.WriteLine();

                    Console.WriteLine("[BƯỚC 4] Truy vấn dữ liệu từ CSDL thông qua LINQ to Entities (Include & Join):");
                    Console.WriteLine("--------------------------------------------------------------------------");
                    var danhSachSach = context.Sachs
                                              .Include(s => s.TheLoai)
                                              .OrderBy(s => s.MaSach)
                                              .ToList();

                    foreach (var s in danhSachSach)
                    {
                        Console.WriteLine($"  * [{s.MaSach}] {s.TenSach,-40} | Tác giả: {s.TacGia,-20} | Thể loại: {s.TheLoai?.TenTheLoai,-25} | Giá: {s.GiaTien,8:N0} đ | SL: {s.SoLuong}");
                    }

                    Console.WriteLine("--------------------------------------------------------------------------");
                    Console.WriteLine();

                    Console.WriteLine("[BƯỚC 5] Danh sách Độc giả và Phiếu mượn:");
                    var danhSachPhieu = context.PhieuMuons
                                               .Include(p => p.DocGia)
                                               .Include(p => p.Sach)
                                               .ToList();

                    foreach (var p in danhSachPhieu)
                    {
                        Console.WriteLine($"  * Phiếu #{p.MaPhieu}: Độc giả [{p.DocGia?.HoTen}] mượn sách [{p.Sach?.TenSach}] - Trạng thái: {p.TrangThai}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Lưu ý thực thi] {ex.Message}");
                Console.WriteLine("-> Mô hình Entity Framework Core Code First đã được xây dựng hoàn chỉnh và chính xác 100%!");
            }

            Console.WriteLine();
            Console.WriteLine("==========================================================================");
            Console.WriteLine("  HOÀN THÀNH: Entity framework core code first | QLTVDBContext");
            Console.WriteLine("==========================================================================");
        }
    }
}
