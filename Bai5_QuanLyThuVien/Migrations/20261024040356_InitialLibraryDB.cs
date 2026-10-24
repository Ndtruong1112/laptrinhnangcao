using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bai5_QuanLyThuVien.Migrations
{
    /// <inheritdoc />
    public partial class InitialLibraryDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DocGia",
                columns: table => new
                {
                    MaDocGia = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocGia", x => x.MaDocGia);
                });

            migrationBuilder.CreateTable(
                name: "TheLoai",
                columns: table => new
                {
                    MaTheLoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenTheLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheLoai", x => x.MaTheLoai);
                });

            migrationBuilder.CreateTable(
                name: "Sach",
                columns: table => new
                {
                    MaSach = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenSach = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TacGia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NamXuatBan = table.Column<int>(type: "int", nullable: false),
                    GiaTien = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    MaTheLoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sach", x => x.MaSach);
                    table.ForeignKey(
                        name: "FK_Sach_TheLoai",
                        column: x => x.MaTheLoai,
                        principalTable: "TheLoai",
                        principalColumn: "MaTheLoai",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhieuMuon",
                columns: table => new
                {
                    MaPhieu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaDocGia = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MaSach = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NgayMuon = table.Column<DateTime>(type: "date", nullable: false),
                    NgayHenTra = table.Column<DateTime>(type: "date", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Đang mượn")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuMuon", x => x.MaPhieu);
                    table.ForeignKey(
                        name: "FK_PhieuMuon_DocGia",
                        column: x => x.MaDocGia,
                        principalTable: "DocGia",
                        principalColumn: "MaDocGia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuMuon_Sach",
                        column: x => x.MaSach,
                        principalTable: "Sach",
                        principalColumn: "MaSach",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DocGia",
                columns: new[] { "MaDocGia", "Email", "HoTen", "NgaySinh", "SoDienThoai" },
                values: new object[,]
                {
                    { "DG001", "binh.lh@vju.ac.vn", "Lê Hoàng Bình", new DateTime(2006, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "0912345678" },
                    { "DG002", "hong.dt@vju.ac.vn", "Đỗ Thị Hồng", new DateTime(2006, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "TheLoai",
                columns: new[] { "MaTheLoai", "MoTa", "TenTheLoai" },
                values: new object[,]
                {
                    { "CNTT", "Sách lập trình, thuật toán, AI, CSDL", "Công nghệ thông tin" },
                    { "KT", "Quản trị kinh doanh, marketing, tài chính", "Kinh tế - Tài chính" },
                    { "VH", "Tiểu thuyết, truyện ngắn, thơ ca", "Văn học - Nghệ thuật" }
                });

            migrationBuilder.InsertData(
                table: "Sach",
                columns: new[] { "MaSach", "GiaTien", "MaTheLoai", "NamXuatBan", "SoLuong", "TacGia", "TenSach" },
                values: new object[,]
                {
                    { "S001", 150000.0, "CNTT", 2024, 20, "Nguyễn Văn An", "Lập trình C# nâng cao với .NET" },
                    { "S002", 180000.0, "CNTT", 2023, 15, "Chu Thị Minh Huệ", "Thiết kế CSDL với SQL Server & EF Core" },
                    { "S003", 120000.0, "KT", 2022, 10, "Trần Minh Anh", "Kinh tế vi mô ứng dụng" },
                    { "S004", 75000.0, "VH", 2021, 25, "Tô Hoài", "Dế Mèn Phiêu Lưu Ký" }
                });

            migrationBuilder.InsertData(
                table: "PhieuMuon",
                columns: new[] { "MaPhieu", "MaDocGia", "MaSach", "NgayHenTra", "NgayMuon", "TrangThai" },
                values: new object[,]
                {
                    { 1, "DG001", "S001", new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang mượn" },
                    { 2, "DG002", "S002", new DateTime(2026, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Đang mượn" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhieuMuon_MaDocGia",
                table: "PhieuMuon",
                column: "MaDocGia");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuMuon_MaSach",
                table: "PhieuMuon",
                column: "MaSach");

            migrationBuilder.CreateIndex(
                name: "IX_Sach_MaTheLoai",
                table: "Sach",
                column: "MaTheLoai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhieuMuon");

            migrationBuilder.DropTable(
                name: "DocGia");

            migrationBuilder.DropTable(
                name: "Sach");

            migrationBuilder.DropTable(
                name: "TheLoai");
        }
    }
}
