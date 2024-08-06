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
    public Dictionary<string, Grade> Grades { get; private set; } = new Dictionary<string, Grade>();

    public Student(string name, int id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        ID = id;
    }

    public Student(string name, string id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        if (int.TryParse(id, out int parsedId))
        {
            ID = parsedId;
        }
        else
        {
            throw new ArgumentException("Invalid ID format.", nameof(id));
        }
    }

    public void AddGrade(string course, string credit, string score)
    {
        if (string.IsNullOrWhiteSpace(course))
            throw new ArgumentException("Course name cannot be null or whitespace.");

        if (!int.TryParse(credit, out int parsedCredit))
            throw new ArgumentException("Credit must be a valid integer.");

        if (!int.TryParse(score, out int parsedScore))
            throw new ArgumentException("Score must be a valid integer.");

        Grades[course] = new Grade(parsedCredit, parsedScore);
    }

    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        if (grades == null)
            throw new ArgumentNullException(nameof(grades));

        foreach (var (course, credit, score) in grades)
        {
            Grades[course] = new Grade(credit, score);
        }
    }

    public void RemoveGrade(string course)
    {
        if (string.IsNullOrWhiteSpace(course))
            throw new ArgumentException("Course name cannot be null or whitespace.");

        Grades.Remove(course);
    }

    public void RemoveGrades(List<string> courses)
    {
        if (courses == null)
            throw new ArgumentNullException(nameof(courses));

        foreach (var course in courses)
        {
            Grades.Remove(course);
        }
    }

    public int GetTotalCredit()
    {
        return Grades.Values.Sum(g => g.Credit);
    }

    public double GetTotalGradePoint()
    {
        return Grades.Values.Sum(g => g.Credit * g.GradePoint);
    }

    public double GetGPA()
    {
        int totalCredit = GetTotalCredit();
        if (totalCredit == 0)
            return 0;

        return GetTotalGradePoint() / totalCredit;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"ID: {ID}");
        sb.AppendLine("Grades:");
        foreach (var (course, grade) in Grades)
        {
            sb.AppendLine($"  Course: {course}, Credit: {grade.Credit}, Score: {grade.Score}, Grade Point: {grade.GradePoint:F2}");
        }
        sb.AppendLine($"Total Credits: {GetTotalCredit()}");
        sb.AppendLine($"GPA: {GetGPA():F2}");
        return sb.ToString();
    }
}
