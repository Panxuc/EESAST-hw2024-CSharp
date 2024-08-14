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
    private string name;
    private int id;
    private Dictionary<string, Grade> Grades{ get; };
    private List<(string course, int credit, int score)> grades;
    private List<string> courses;
    public Student(string _name, int _id)
    {
        name = _name;
        id = _id;
        Grades = new Dictionary<string, Grade>();
    }
    public string Name
    {
        get => name;
        private set       
        {
            name = value;        
        }    
    }
    public int ID
    {
        get => id;
        private set       
        {
            id = value;        
        }    
    }
    public void AddGrade(string course, string credit, string score)
    {
        if (int.TryParse(credit, out int cred) && int.TryParse(score, out int scr))
        {
            Grades[course] = new Grade(course, cred, scr);
        }
    }

    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var grade in grades)
        {
            Grades[grade.course] = new Grade(grade.course, grade.credit, grade.score);
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
        if (GetTotalCredit() == 0)
            return 0.0;
        return GetTotalGradePoint() / GetTotalCredit();
    }

    public override string ToString()
    {
        return $"Name: {_name}, ID: {_id}, Grades: {{{string.Join(", ", Grades.Values.Select(grade => $"{grade.Course}: {grade.Score}/{grade.Credit}"))}}}}";
    }
}
