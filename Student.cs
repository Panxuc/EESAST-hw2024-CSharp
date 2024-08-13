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
    public Student(string name, string id)
    {
        Name = name;
        try
        {
            ID = int.Parse(id);
        }
        catch
        {
            Console.WriteLine("Invalid ID, set id as null");
        }
    }
    public Dictionary<string, Grade> Grades { get; } = new();
    public void AddGrade(string course, string credit, string score)
    {
        int Score, Credit;
        try
        {
            Credit = int.Parse(credit);
        }
        catch
        {
            Console.WriteLine("Invalid credit, set credit as 0");
            Credit = 0;
        }
        try
        {
            Score = int.Parse(score);
        }
        catch
        {
            Console.WriteLine("Invalid score, set score as 0");
            Score = 0;
        }
        var grade = new Grade(Credit, Score);
        Grades.Add(course, grade);
        Console.WriteLine("Grade added");
    }
    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        grades.ForEach(grade => AddGrade(grade.course, grade.credit.ToString(), grade.score.ToString()));
    }
    public void RemoveGrade(string course)
    {
        try
        {
            Grades.Remove(course);
            Console.WriteLine("Course removed");
        }
        catch
        {
            Console.WriteLine("Course not found");
        }
    }
    public void RemoveGrades(List<string> courses)
    {
        courses.ForEach(cs => RemoveGrade(cs));
    }
    public int GetTotalCredit() { return Grades.Values.Sum(grade => grade.Credit); }
    public double GetTotalGradePoint()
    {
        return Grades.Values.Sum(grade => grade.GradePoint * grade.Credit);
    }
    public double GetGPA()
    {
        return GetTotalGradePoint() / GetTotalCredit();
    }
    public string ToString()
    {
        return string.Format("Name: {0}\nID: {1}\nGPA: {2}", Name, ID, GetGPA());
    }
}
