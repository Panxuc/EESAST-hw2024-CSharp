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
    //比较奇怪，程序里没有看到AddGrades和RemoveGrades的使用
    public int GetTotalCredit();
    public double GetTotalGradePoint();
    public double GetGPA();
    public string ToString();
}

public class Student : IStudent
{
    private string name;
    private int id;
    public Dictionary<string, Grade> Grades { get; }
    public Student(string _name, string _id)
    {
        Name = _name;
        ID = ConvertToInt(_id);
        Grades = new Dictionary<string, Grade>();
    }
    public string Name
    {
        get => name;
        set
        {
            name = value;
        }
    }
    public int ID
    {
        get => id;
        set
        {
            id = value;
        }
    }
    private int ConvertToInt(string str)
    {
        int number;
        if (int.TryParse(str, out number))
        {
            return number;
        }
        else
        {
            throw new ArgumentException("ID must be a valid integer.");
        }
    }
    public void AddGrade(string course, string credit, string score)
    {
        if (int.TryParse(credit, out int cred) && int.TryParse(score, out int scr))
        {
            Grades[course] = new Grade(cred, scr);
        }
    }

    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var grade in grades)
        {
            Grades[grade.course] = new Grade(grade.credit, grade.score);
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
        int totalCredits = GetTotalCredit();
        if (totalCredits == 0)
        {
            // 抛出异常，指出不能计算GPA因为总学分为零
            throw new InvalidOperationException("Cannot calculate GPA because total credit is zero.");
        }
        return GetTotalGradePoint() / totalCredits;
    }

    public override string ToString()
    {
        return $"Name: {Name}, ID: {ID}, GPA: {GetGPA()}";
    }
}
