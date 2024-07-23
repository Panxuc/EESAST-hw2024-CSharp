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
    public Student(string name, string i)
    {
        Name = name;
        ID = int.Parse(i);
        Grades = new Dictionary<string, Grade>();
    }
    public string Name { get; set; }
    public int ID { get; set; }
    public Dictionary<string, Grade> Grades { get; }
    public void AddGrade(string course, string credit, string score)
    {
        Grades.Add(course, new Grade(int.Parse(credit), int.Parse(score)));
    }
    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach ((string course, int credit, int score) grade in grades)
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
        foreach (string course in courses)
        {
            Grades.Remove(course);
        }
    }
    public int GetTotalCredit()
    {
        int totalCredit = 0;
        foreach (Grade grade in Grades.Values)
        {
            totalCredit += grade.Credit;
            return totalCredit;
        }
        return 0;
    }
    public double GetTotalGradePoint()
    {
        double gradePoint = 0.0;
        foreach (Grade grade in Grades.Values)
        {
            gradePoint += grade.GradePoint * grade.Credit;
            return gradePoint;
        }
        return 0.0;
    }
    public double GetGPA()
    {
        return GetTotalGradePoint() / GetTotalCredit();
    }
    public override string ToString()
    {
        return $"姓名:{Name} 学号:{ID}";
    }
}
