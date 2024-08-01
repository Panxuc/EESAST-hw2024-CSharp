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
    public void AddGrade(string course, int credit, int score)
    {
        if (Grades.ContainsKey(course))
            throw new Exception("Grade for this course already exists.");
        Grades[course] = new Grade(credit, score);
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
        if (!Grades.ContainsKey(course))
            throw new Exception("Grade for this course does not exist.");
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
        return Grades.Values.Sum(grade => grade.Credit);
    }
    public double GetTotalGradePoint()
    {
        return Grades.Values.Sum(grade => grade.Credit * grade.GradePoint);
    }
    public double GetGPA()
    {
        int totalCredits = GetTotalCredit();
        return totalCredits == 0 ? 0 : GetTotalGradePoint() / totalCredits;
    }
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Student ID: {ID}, Name: {Name}");
        sb.AppendLine("Grades:");
        foreach (var (course, grade) in Grades)
        {
            sb.AppendLine($"Course: {course}, Credit: {grade.Credit}, Score: {grade.Score}, Grade Point: {grade.GradePoint}");
        }
        sb.AppendLine($"Total Credits: {GetTotalCredit()}, GPA: {GetGPA():F2}");
        return sb.ToString();
    }
}
