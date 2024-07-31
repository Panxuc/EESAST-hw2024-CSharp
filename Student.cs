namespace StudentGradeManager;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IStudent
{
    public string Name { get; set; }
    public int ID { get; set; }
    public Dictionary<string, Grade> Grades { get; }
    public void AddGrade(string course, string credit, string score);
    public void AddGrades(List<(string course, int credit, int score)> grades);
    public void RemoveGrade(string course);
    public void RemoveGrades(List<string> courses);
    public int GetTotalCredit();
    public double GetTotalGradePoint();
    public double GetGPA();
    public string ToString();
}

public class Student : IStudent
{
    public string Name { get; set; }
    public int ID { get; set; }
    public Dictionary<string, object> Grades { get; private set; }
    public Student(string name, int id)
    {
        Name = name;
        ID = id;
        Grades = new Dictionary<string, object>();
    }
    public void AddGrade(string course, string credit, string score)
    {
        if (int.TryParse(credit, out int parsedCredit) && int.TryParse(score, out int parsedScore))
        {
            Grades[course] = new Grade { Credit = parsedCredit, Score = parsedScore };
        }
        else
        {
            throw new ArgumentException("Credit and score must be valid integers");
        }
    }
    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var grade in grades)
        {
            AddGrade(grade.course, grade.credit.ToString(), grade.score.ToString());
        }
    }
    public void RemoveGrade(string course)
    {
        Grades.Remove(course);
    }
    public void RemoveGrades(List<string> courses)
    {
        foreach (var course in courses)
        {
            RemoveGrade(course);
        }
    }
    public int GetTotalCredit()
    {
        return Grades.Values.OfType<Grade>().Sum(g => g.Credit);
    }
    public double GetTotalGradePoint()
    {
        return Grades.Values.OfType<Grade>().Sum(g => g.GradePoint * g.Credit);
    }
    public double GetGPA()
    {
        int totalCredits = GetTotalCredit();
        return totalCredits == 0 ? 0.0 : GetTotalGradePoint() / totalCredits;
    }
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Student Name: {Name}");
        sb.AppendLine($"Student ID: {ID}");
        sb.AppendLine("Grades:");
        foreach (var grade in Grades)
        {
            var g = (Grade)grade.Value;
            sb.AppendLine($"Course: {grade.Key}, Credit: {g.Credit}, Score: {g.Score}, Grade Point: {g.GradePoint:F2}");
        }
        sb.AppendLine($"GPA: {GetGPA():F2}");
        return sb.ToString();
    }
    // 嵌套的 Grade 类
    public class Grade
    {
        public int Credit { get; set; }
        public int Score { get; set; }
        public double GradePoint => CalculateGradePoint(Score);
        private double CalculateGradePoint(int score)
        {
            if (score >= 90) return 4.0;
            if (score >= 85) return 3.6;
            if (score >= 80) return 3.3;
            if (score >= 77) return 3.0;
            if (score >= 73) return 2.6;
            if (score >= 70) return 2.3;
            if (score >= 67) return 2.0;
            if (score >= 63) return 1.6;
            if (score >= 60) return 1.3;
            return 0.0;
        }
    }
}
