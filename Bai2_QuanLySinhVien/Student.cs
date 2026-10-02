using System;

namespace DemoApp
{
    /// <summary>
    /// Lớp Student: StdID, Name, MidPoint, FinalPoint
    /// Điểm trung bình = Mid*0.3 + Final*0.7 (có thể đổi thành (Mid+Final)/2 tùy cô)
    /// </summary>
    public class Student
    {
        public string StdID { get; set; } = "";
        public string Name { get; set; } = "";
        public double MidPoint { get; set; }
        public double FinalPoint { get; set; }

        // Điểm trung bình môn (tính theo trọng số 30% giữa kỳ + 70% cuối kỳ)
        public double Average => Math.Round(MidPoint * 0.3 + FinalPoint * 0.7, 2);

        // Đậu nếu TB >= 5.0
        public bool IsPassed => Average >= 5.0;

        public Student() { }

        public Student(string stdID, string name, double midPoint, double finalPoint)
        {
            StdID = stdID;
            Name = name;
            MidPoint = ValidPoint(midPoint);
            FinalPoint = ValidPoint(finalPoint);
        }

        private static double ValidPoint(double p)
        {
            if (p < 0 || p > 10)
                throw new ArgumentOutOfRangeException(nameof(p), "Điểm phải từ 0 đến 10.");
            return p;
        }

        // Xếp loại theo thang 10 của VN
        public string GetGrade()
        {
            if (Average >= 8.5) return "Giỏi";
            if (Average >= 7.0) return "Khá";
            if (Average >= 5.5) return "Trung bình";
            if (Average >= 4.0) return "Yếu";
            return "Kém";
        }

        public override string ToString()
        {
            return $"[{StdID}] {Name,-20} | Giữa kỳ: {MidPoint,4:F1} | Cuối kỳ: {FinalPoint,4:F1} | TB: {Average,4:F2} | {GetGrade(),-10} | {(IsPassed ? "Đậu" : "Rớt")}";
        }
    }
}
