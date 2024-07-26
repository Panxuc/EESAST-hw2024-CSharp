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
        if (int.TryParse(id, out var result_id))
            ID = result_id;
        Grades = new Dictionary<string, Grade>();
    }

    public void AddGrade(string course, string credit, string score)
    {
        if (int.TryParse(credit, out var result_credit)
         && int.TryParse(score, out var result_score))
        {
            Grades.Add(course, new Grade(result_credit, result_score));
        }
        else throw new ArgumentException("Invalid credit or score.");
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
        return Grades.Values.Sum(grade => grade.GradePoint * grade.Credit);
    }

    public double GetGPA()
    {
        return GetTotalGradePoint() / GetTotalCredit();
    }

    public override string ToString()
    {
        return $"Name: {Name}, ID: {ID}, GPA: {GetGPA()}";
    }

}
