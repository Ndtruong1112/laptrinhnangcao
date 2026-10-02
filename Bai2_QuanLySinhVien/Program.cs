using System;
using System.Linq;

namespace DemoApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 1. Tạo môn học
            var subject = new Subject("CS201", "Lập trình nâng cao", "HK1-2026", "Cô Lan");
            subject.PrintInfo();

            // 2. Thêm SV
            subject.AddStudents(
                new Student("SV001", "Nguyễn Văn An", 8.0, 9.0),
                new Student("SV002", "Trần Thị Bình", 6.5, 7.0),
                new Student("SV003", "Lê Văn Cường", 4.0, 3.5),
                new Student("SV004", "Phạm Thị Dung", 9.0, 9.5),
                new Student("SV005", "Hoàng Văn Em", 5.0, 6.0)
            );
            // Thử thêm trùng mã (sẽ bị từ chối)
            bool ok = subject.AddStudent(new Student("SV001", "Trùng mã", 10, 10));
            Console.WriteLine($"\nThêm trùng SV001: {(ok ? "OK" : "Bị chặn (đúng)")}");

            subject.PrintAll();

            // 3. Tìm kiếm (LINQ: FirstOrDefault, Where)
            Console.WriteLine("\n========== TÌM KIẾM (LINQ) ==========");
            var found = subject.FindStudentById("SV002");
            Console.WriteLine("Tìm mã SV002: " + (found?.ToString() ?? "Không thấy"));

            Subject.PrintList(subject.FindStudentsByName("an"), "Tìm tên chứa 'an' (Where + Contains)");

            Subject.PrintList(subject.FindByAverageRange(7, 10), "Điểm TB từ 7-10 (Where)");

            // 4. Sắp xếp (LINQ: OrderBy)
            Console.WriteLine("\n========== SẮP XẾP (LINQ) ==========");
            Subject.PrintList(subject.SortByAverageDescending(), "Sắp xếp TB giảm dần (OrderByDescending)");
            Subject.PrintList(subject.SortByName(), "Sắp xếp theo tên A-Z (OrderBy)");

            // 5. Lọc Top / Đậu-Rớt (LINQ: Where, Take)
            Console.WriteLine("\n========== LỌC (LINQ) ==========");
            Subject.PrintList(subject.GetPassedStudents(), "SV Đậu (Where IsPassed)");
            Subject.PrintList(subject.GetFailedStudents(), "SV Rớt (Where !IsPassed)");
            Subject.PrintList(subject.GetTopN(3), "Top 3 điểm cao nhất (OrderByDescending + Take)");
            Subject.PrintList(subject.GetExcellentStudents(), "SV Giỏi TB>=8.5 (Where)");

            // 6. Thống kê (LINQ: Average, Max/Min, Count, GroupBy)
            Console.WriteLine("\n========== THỐNG KÊ (LINQ) ==========");
            Console.WriteLine($"Điểm TB cả lớp (Average): {subject.GetClassAverage()}");
            Console.WriteLine($"Nhất lớp (Max): {subject.GetBestStudent()}");
            Console.WriteLine($"Thấp nhất (Min): {subject.GetWorstStudent()}");
            Console.WriteLine($"Số SV đậu (Count): {subject.CountPassed()}/{subject.Students.Count} — Tỉ lệ: {subject.PassRate()}%");

            Console.WriteLine("\nNhóm theo xếp loại (GroupBy):");
            foreach (var kv in subject.CountByGrade())
                Console.WriteLine($"  {kv.Key}: {kv.Value} SV");

            // Ví dụ LINQ query-syntax (cách viết thứ 2, cô hay hỏi)
            var query = from s in subject.Students
                        where s.Average >= 5
                        orderby s.Average descending
                        select s;
            Subject.PrintList(query.ToList(), "Query-syntax: SV đậu sắp giảm dần");

            // 7. Cập nhật + Xóa
            Console.WriteLine("\n========== CẬP NHẬT / XÓA ==========");
            subject.UpdateStudent("SV003", newMid: 6.0, newFinal: 7.0);
            Console.WriteLine("Đã cập nhật điểm SV003: " + subject.FindStudentById("SV003"));
            subject.RemoveStudentById("SV005");
            subject.PrintAll();
        }
    }
}
