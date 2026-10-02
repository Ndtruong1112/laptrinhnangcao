using System;
using System.Collections.Generic;
using System.Linq;

namespace DemoApp
{
    /// <summary>
    /// Lớp Subject: Id, Name, Semester, Teacher, List&lt;Student&gt;
    /// Các thao tác CRUD + thống kê dùng LINQ
    /// </summary>
    public class Subject
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Semester { get; set; } = "";
        public string Teacher { get; set; } = "";
        public List<Student> Students { get; set; } = new();

        public Subject() { }

        public Subject(string id, string name, string semester, string teacher)
        {
            Id = id;
            Name = name;
            Semester = semester;
            Teacher = teacher;
        }

        // ---------- CRUD cơ bản ----------

        // Thêm 1 SV (chặn trùng StdID)
        public bool AddStudent(Student s)
        {
            if (s == null) return false;
            if (Students.Any(x => x.StdID.Equals(s.StdID, StringComparison.OrdinalIgnoreCase)))
                return false; // trùng mã -> không thêm
            Students.Add(s);
            return true;
        }

        // Thêm nhiều SV cùng lúc
        public void AddStudents(params Student[] list)
        {
            foreach (var s in list) AddStudent(s);
        }

        // Xóa SV theo mã, trả về true nếu xóa được
        public bool RemoveStudentById(string stdID)
        {
            var s = FindStudentById(stdID);
            if (s == null) return false;
            return Students.Remove(s);
        }

        // Cập nhật điểm / tên theo mã
        public bool UpdateStudent(string stdID, string? newName = null, double? newMid = null, double? newFinal = null)
        {
            var s = FindStudentById(stdID);
            if (s == null) return false;
            if (newName != null) s.Name = newName;
            if (newMid.HasValue) s.MidPoint = newMid.Value;
            if (newFinal.HasValue) s.FinalPoint = newFinal.Value;
            return true;
        }

        // ---------- Tìm kiếm bằng LINQ ----------

        // Tìm 1 SV theo mã (FirstOrDefault)
        public Student? FindStudentById(string stdID)
        {
            return Students.FirstOrDefault(x =>
                x.StdID.Equals(stdID, StringComparison.OrdinalIgnoreCase));
        }

        // Tìm SV theo tên (chứa, không phân biệt hoa thường) - Where
        public List<Student> FindStudentsByName(string keyword)
        {
            return Students
                .Where(x => x.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Lọc SV theo khoảng điểm TB - Where
        public List<Student> FindByAverageRange(double min, double max)
        {
            return Students.Where(x => x.Average >= min && x.Average <= max).ToList();
        }

        // ---------- Sắp xếp bằng LINQ ----------

        public List<Student> SortByAverageDescending()
            => Students.OrderByDescending(x => x.Average).ToList();

        public List<Student> SortByName()
            => Students.OrderBy(x => x.Name).ToList();

        public List<Student> SortById()
            => Students.OrderBy(x => x.StdID).ToList();

        // ---------- Lọc / Top dùng LINQ ----------

        public List<Student> GetPassedStudents()   // Đậu
            => Students.Where(x => x.IsPassed).ToList();

        public List<Student> GetFailedStudents()   // Rớt
            => Students.Where(x => !x.IsPassed).ToList();

        public List<Student> GetExcellentStudents() // Giỏi (TB >= 8.5)
            => Students.Where(x => x.Average >= 8.5).ToList();

        // Top N điểm cao nhất - OrderByDescending + Take
        public List<Student> GetTopN(int n)
            => Students.OrderByDescending(x => x.Average).Take(n).ToList();

        // ---------- Thống kê dùng LINQ ----------

        public double GetClassAverage() // điểm TB cả lớp
            => Students.Count == 0 ? 0 : Math.Round(Students.Average(x => x.Average), 2);

        public Student? GetBestStudent() // Max
            => Students.OrderByDescending(x => x.Average).FirstOrDefault();

        public Student? GetWorstStudent() // Min
            => Students.OrderBy(x => x.Average).FirstOrDefault();

        public int CountPassed() => Students.Count(x => x.IsPassed);

        public double PassRate() // tỉ lệ đậu %
            => Students.Count == 0 ? 0 : Math.Round(Students.Count(x => x.IsPassed) * 100.0 / Students.Count, 1);

        // Nhóm SV theo xếp loại - GroupBy
        public Dictionary<string, List<Student>> GroupByGrade()
        {
            return Students
                .GroupBy(x => x.GetGrade())
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        // Đếm số lượng mỗi xếp loại - GroupBy + Count (để in thống kê nhanh)
        public Dictionary<string, int> CountByGrade()
        {
            return Students
                .GroupBy(x => x.GetGrade())
                .ToDictionary(g => g.Key, g => g.Count());
        }

        // ---------- Hiển thị ----------

        public void PrintInfo()
        {
            Console.WriteLine($"Môn: [{Id}] {Name} | HK: {Semester} | GV: {Teacher} | Sĩ số: {Students.Count}");
        }

        public static void PrintList(IEnumerable<Student> list, string title = "Danh sách sinh viên")
        {
            Console.WriteLine($"\n--- {title} ({list.Count()} SV) ---");
            foreach (var s in list)
                Console.WriteLine(s);
        }

        public void PrintAll() => PrintList(Students, $"Toàn bộ lớp {Name}");
    }
}
