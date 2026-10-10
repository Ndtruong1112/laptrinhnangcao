# Lập trình nâng cao

Solution gồm các bài thực hành C# trên .NET 8: ứng dụng console, LINQ và Windows Forms.

## Yêu cầu môi trường

- .NET 8 SDK
- Windows để chạy các ứng dụng Windows Forms
- Visual Studio 2022 trở lên hoặc Visual Studio Community 2026

## Danh sách dự án

| Dự án | Loại ứng dụng | Nội dung |
|---|---|---|
| `Bai1` | Console | Nhập ba số nguyên, kiểm tra dữ liệu nhập và tính tổng. |
| `Bai2_QuanLySinhVien` | Console | Quản lý sinh viên theo môn học: thêm, sửa, xóa, tìm kiếm, sắp xếp, lọc và thống kê bằng LINQ. |
| `Bai3_Calculator` | Windows Forms | Máy tính thực hiện các phép cộng, trừ, nhân và chia. |
| `Bai4_QuanLySinhVien_WinForms` | Windows Forms | Quản lý lớp và sinh viên; hiển thị, tìm kiếm, lọc và thao tác với thông tin sinh viên. |

## Mở và chạy

Mở `LapTrinhNangCao.sln` bằng Visual Studio, chọn dự án muốn chạy làm Startup Project rồi nhấn **F5**.

Có thể chạy từng dự án từ thư mục chứa solution bằng .NET CLI:

```powershell
dotnet run --project .\Bai1\Bai1.csproj
dotnet run --project .\Bai2_QuanLySinhVien\Bai2_QuanLySinhVien.csproj
dotnet run --project .\Bai3_Calculator\Bai3_Calculator.csproj
dotnet run --project .\Bai4_QuanLySinhVien_WinForms\Bai4_QuanLySinhVien_WinForms.csproj
```

Build toàn bộ solution:

```powershell
dotnet build .\LapTrinhNangCao.sln
```

## Bài 4 — Quản lý sinh viên

- Danh sách lớp được nạp vào các ComboBox.
- Có thể lọc danh sách sinh viên theo lớp, từ khóa và điểm tối thiểu trên giao diện.
- Nhập mã sinh viên để hiển thị thông tin nếu mã tồn tại; nếu không tồn tại, các trường thông tin còn lại được xóa trống.
- Hỗ trợ thêm, sửa và xóa sinh viên.
- Dữ liệu mẫu hiện được lưu trong danh sách trong bộ nhớ của ứng dụng, chưa dùng cơ sở dữ liệu và không được lưu sau khi đóng ứng dụng.
