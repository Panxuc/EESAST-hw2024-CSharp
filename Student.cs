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
    public Dictionary<string, Grade> Grades { get; }

    public Student(string name, string id)
    {
        Name = name;
        ID = int.Parse(id);
        Grades = new Dictionary<string, Grade>();
    }

    public void AddGrade(string course, string credit, string score)
    {
        Grades.Add(course, new Grade(int.Parse(credit), int.Parse(score)));
    }

    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var grade in grades)
        {
            Grades.Add(grade.course, new Grade(grade.credit, grade.score));
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
        return Grades.Values.Sum(grade => grade.Credit * grade.Score);
    }

    public double GetGPA()
    {
        if (GetTotalCredit() == 0)
        {
            return 0;
        }
        else return GetTotalGradePoint() / GetTotalCredit();
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
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
