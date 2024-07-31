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
 ublic string Name { get; set; }
    public int ID { get; set; }
    public Dictionary<string, Grade> Grades { get; private set; }

    public Student(string name, int id)
    {
        Name = name;
        ID = id;
        Grades = new Dictionary<string, Grade>();
    }

    public void AddGrade(string course, string credit, string score)
    {
        if (int.TryParse(credit, out int parsedCredit) && int.TryParse(score, out int parsedScore))
        {
            AddGrade(course, parsedCredit, parsedScore);
        }
        else
        {
            throw new ArgumentException("Credit and score must be valid integers");
        }
    }

    public void AddGrade(string course, int credit, int score)
    {
        if (string.IsNullOrEmpty(course))
        {
            throw new ArgumentException("Course name cannot be null or empty");
        }

        if (credit <= 0)
        {
            throw new ArgumentException("Credit must be a positive number");
        }

        if (score < 0 || score > 100)
        {
            throw new ArgumentException("Score must be between 0 and 100");
        }

        Grades[course] = new Grade { Credit = credit, Score = score };
    }

    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        if (grades == null || grades.Count == 0)
        {
            throw new ArgumentException("Grades list cannot be null or empty");
        }

        foreach (var grade in grades)
        {
            AddGrade(grade.course, grade.credit, grade.score);
        }
    }

    public void RemoveGrade(string course)
    {
        if (string.IsNullOrEmpty(course))
        {
            throw new ArgumentException("Course name cannot be null or empty");
        }

        Grades.Remove(course);
    }

    public void RemoveGrades(List<string> courses)
    {
        if (courses == null || courses.Count == 0)
        {
            throw new ArgumentException("Courses list cannot be null or empty");
        }

        foreach (var course in courses)
        {
            RemoveGrade(course);
        }
    }

    public int GetTotalCredit()
    {
        return Grades.Values.Sum(g => g.Credit);
    }

    public double GetTotalGradePoint()
    {
        return Grades.Values.Sum(g => g.GradePoint * g.Credit);
    }

    public double GetGPA()
    {
        int totalCredit = GetTotalCredit();
        return totalCredit > 0 ? GetTotalGradePoint() / totalCredit : 0.0;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Student Name: {Name}");
        sb.AppendLine($"Student ID: {ID}");
        sb.AppendLine("Grades:");

        foreach (var kvp in Grades)
        {
            sb.AppendLine($"  Course: {kvp.Key}, Credit: {kvp.Value.Credit}, Score: {kvp.Value.Score}, Grade Point: {kvp.Value.GradePoint:F2}");
        }

        sb.AppendLine($"Total GPA: {GetGPA():F2}");
        return sb.ToString();
    }
    public class Grade
    {
        public int Credit { get; set; }
        public int Score { get; set; }
        public double GradePoint => CalculateGradePoint(Score);

        private double CalculateGradePoint(int score)
        {
            if (score >= 90) return 4.0;
            if (score >= 80) return 3.0;
            if (score >= 70) return 2.0;
            if (score >= 60) return 1.0;
            return 0.0;
        }
    }
}
