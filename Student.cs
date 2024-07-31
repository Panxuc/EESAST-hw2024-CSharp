namespace StudentGradeManager;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
{public string Name { get; set; }
    public int ID { get; set; }
    public Dictionary<string, Grade> Grades { get; private set; }

    public Student(string name, int id)
    {
        Name = name;
        ID = id;
        Grades = new Dictionary<string, Grade>();
    }

    public void AddGrade(string course, int credit, int score)
    {
        if (Grades.ContainsKey(course))
        {
            Grades[course] = new Grade { Credit = credit, Score = score };
        }
        else
        {
            Grades.Add(course, new Grade { Credit = credit, Score = score });
        }
    }

    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var (course, credit, score) in grades)
        {
            AddGrade(course, credit, score);
        }
    }

    public void RemoveGrade(string course)
    {
        if (Grades.ContainsKey(course))
        {
            Grades.Remove(course);
        }
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
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Student Name: {Name}");
        sb.AppendLine($"Student ID: {ID}");
        sb.AppendLine("Grades:");
        foreach (var (course, grade) in Grades)
        {
            sb.AppendLine($"  Course: {course}, Credit: {grade.Credit}, Score: {grade.Score}, Grade Point: {grade.GradePoint}");
        }
        sb.AppendLine($"Total GPA: {GetGPA():F2}");
        return sb.ToString();
    }
}
