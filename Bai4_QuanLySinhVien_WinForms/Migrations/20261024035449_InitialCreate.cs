using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bai4_QuanLySinhVien_WinForms.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LopHoc",
                columns: table => new
                {
                    MaLop = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TenLop = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LopHoc", x => x.MaLop);
                });

            migrationBuilder.CreateTable(
                name: "SinhVien",
                columns: table => new
                {
                    MaSV = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "date", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Diem = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    MaLop = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Đang học")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SinhVien", x => x.MaSV);
                    table.ForeignKey(
                        name: "FK_SinhVien_LopHoc",
                        column: x => x.MaLop,
                        principalTable: "LopHoc",
                        principalColumn: "MaLop",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "LopHoc",
                columns: new[] { "MaLop", "TenLop" },
                values: new object[,]
                {
                    { "CSE0001", "Khoa học máy tính 01" },
                    { "CSE0002", "Khoa học máy tính 02" },
                    { "CSE0003", "Khoa học máy tính 03" },
                    { "KTPM01", "Kỹ thuật phần mềm 01" },
                    { "TTNT01", "Trí tuệ nhân tạo 01" }
                });

            migrationBuilder.InsertData(
                table: "SinhVien",
                columns: new[] { "MaSV", "Diem", "Email", "GioiTinh", "HoTen", "MaLop", "NgaySinh", "SoDienThoai", "TrangThai" },
                values: new object[,]
                {
                    { "SV000123", 8.5, "an.nv@vju.ac.vn", "Nam", "Nguyễn Văn An", "CSE0001", new DateTime(2006, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "0912345678", "Đang học" },
                    { "SV000124", 9.0, "anh.tm@vju.ac.vn", "Nữ", "Trần Minh Anh", "CSE0002", new DateTime(2006, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "0987654321", "Đang học" },
                    { "SV000125", 7.4000000000000004, "binh.lh@vju.ac.vn", "Nam", "Lê Hoàng Bình", "CSE0001", new DateTime(2006, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "0355556677", "Đang học" },
                    { "SV000126", 8.0999999999999996, "hong.dt@vju.ac.vn", "Nữ", "Đỗ Thị Hồng", "CSE0003", new DateTime(2006, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "0777888999", "Đang học" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SinhVien_MaLop",
                table: "SinhVien",
                column: "MaLop");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SinhVien");

            migrationBuilder.DropTable(
                name: "LopHoc");
        }
    }
}
