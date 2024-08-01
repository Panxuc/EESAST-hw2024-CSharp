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
        Name = name;
        ID = id;
    }
    public void AddGrade(string course, string credit, string score)
    {
        if (!int.TryParse(credit, out int creditValue))
        {
            throw new Exception("Invalid credit value");
        }
        if (!int.TryParse(score, out int scoreValue))
        {
            throw new Exception("Invalid score value");
        }
        if (!Grades.ContainsKey(course))
        {
            Grades[course] = new Grade(creditValue, scoreValue);
        }
        else
        {
            throw new Exception("Grade for this course already exists");
        }
    }
    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var (course, credit, score) in grades)
        {
            AddGrade(course, credit.ToString(), score.ToString());
        }
    }
    public void RemoveGrade(string course)
    {
        if (Grades.ContainsKey(course))
        {
            Grades.Remove(course);
        }
        else
        {
            throw new Exception("Grade for this course does not exist");
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
        int totalCredits = GetTotalCredit();
        return totalCredits == 0 ? 0 : GetTotalGradePoint() / totalCredits;
    }
    public override string ToString()
    {
        var gradesString = string.Join(", ", Grades.Select(g => $"{g.Key}: {g.Value.Score}"));
        return $"ID: {ID}, Name: {Name}, GPA: {GetGPA():F2}, Grades: [{gradesString}]";
    }
}
