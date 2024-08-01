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
    public Dictionary<string, Grade> Grades { get; } = new Dictionary<string, Grade>();
    public void AddGrade(string course, int credit, int score)
    {
        Grades[course] = new Grade(credit, score);
    }
    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var (course, credit, score) in grades)
        {
            Grades[course] = new Grade(credit, score);
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
        return Grades.Values.Sum(g => g.Credit);
    }
    public double GetTotalGradePoint()
    {
        return Grades.Values.Sum(g => g.Credit * g.GradePoint);
    }
    public double GetGPA()
    {
        var totalCredits = GetTotalCredit();
        return totalCredits > 0 ? GetTotalGradePoint() / totalCredits : 0.0;
    }
    public override string ToString()
    {
        return $"{Name} (ID: {ID}), GPA: {GetGPA():F2}";
    }  
}
