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
    public Dictionary<string, Grade> Grades { get; } = new();

    public Student(string name, int id)
    {
        this.Name = name;
        this.ID = id;
    }

    public void AddGrade(string course, string credit, string score)
    {
        int creditValue;
        if (!int.TryParse(credit, out creditValue))
        {
            throw new ArgumentException("Invalid credit value. Expected an integer.");
        }
        int scoreValue;
        if (!int.TryParse(score, out scoreValue))
        {
            throw new ArgumentException("Invalid score value. Expected an integer.");
        }
        Grades.Add(course, new Grade(creditValue, scoreValue));
    }

    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var (course, credit, score) in grades)
        {
            Grades.Add(course, new Grade(credit, score));
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
            Grades.Remove(course);
        }
    }

    public int GetTotalCredit()
    {
        return Grades.Values.Sum(grade => grade.Credit);
    }

    public double GetTotalGradePoint()
    {
        return Grades.Values.Sum(grade => grade.GradePoint);
    }

    public double GetGPA()
    {
        int totalCredit = GetTotalCredit();
        if (totalCredit == 0)
        {
            return 0;
        }
        return GetTotalGradePoint() / totalCredit;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"ID: {ID}");
        sb.AppendLine("Grades:");
        foreach (var grade in Grades)
        {
            sb.AppendLine($"{grade.Key}: {grade.Value}");
        }
        sb.AppendLine($"Total Credit: {GetTotalCredit()}");
        sb.AppendLine($"Total Grade Point: {GetTotalGradePoint()}");
        sb.AppendLine($"GPA: {GetGPA()}");
        return sb.ToString();
    }
}
