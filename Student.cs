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
    public Student(string name, string id)
    {
        this.name = name;
        int num_id = 0;
        int.TryParse(id, out num_id);
        this.id = num_id;
        Grades = new Dictionary<string, Grade>();
    }

    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private int id;

    public int ID
    {
        get { return id; }
        set { id = value >= 0 ? value : 0; }
    }

    public Dictionary<string, Grade> Grades { get; }

    public void AddGrade(string course, string credit, string score)
    {

        string _course = course.Trim();
        string _score_temp = score.Trim();
        string _credit_temp = credit.Trim();

        if (Grades != null && Grades.ContainsKey(_course))
        {
            Console.WriteLine("Course Grade Already Existed!");
            return;
        }


        int _credit = 0;
        int _score = 0;
        int re_credit = 0;
        int re_score = 0;

        if (int.TryParse(_credit_temp, out _credit))
            if (_credit <= 10) // 假定学分上限为10
                re_credit = _credit >= 0 ? _credit : 0; // 注意条件运算符，左侧成立，式值为左侧，否则式值为右侧

        if (int.TryParse(_score_temp, out _score))
            if (_score <= 100)
                re_score = _score >= 0 ? _score : 0;

        Grades.Add(_course, new Grade(re_credit, re_score));
        return;
    }

    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var g in grades)
        {
            if (!Grades.ContainsKey(g.course.Trim()))
            {
                Grades[g.course] = new Grade(g.credit >= 0 ? g.credit : 0, g.score >= 0 ? g.score : 0);
            }
        }
    }

    public void RemoveGrade(string course)
    {
        if (Grades.ContainsKey(course.Trim()))
            Grades.Remove(course);
    }

    public void RemoveGrades(List<string> courses)
    {
        courses.ForEach(course =>
        {
            if (Grades.ContainsKey(course.Trim()))
                Grades.Remove(course);
        });
    }

    public int GetTotalCredit()
    {
        int total_credit = 0;
        foreach (var item in Grades)
        {
            Grade value = item.Value;
            total_credit += value.Credit;
        }
        return total_credit;
    }

    public double GetTotalGradePoint()
    {
        double total_gp = 0;
        foreach (var item in Grades)
        {
            Grade value = item.Value;
            total_gp += value.Credit * value.GradePoint;
        }
        return total_gp;
    }

    public double GetGPA()
    {
        double gpa = 0.00;
        if (GetTotalCredit() > 0)
            gpa = GetTotalGradePoint() / GetTotalCredit();
        return gpa;
    }

    override public string ToString()
    {
        string courselist = "";
        foreach (var item in Grades)
        {
            courselist += item.Key;
            courselist += "\n";
        }
        string result =
            $"\nStudent Name :{this.Name}\n" +
            $"Student ID :{this.ID}\n" +
            $"Course Information Below:\n" +
            $"Course Names:\n{courselist}\n" +
            $"Total Credit = {this.GetTotalCredit()}\n" +
            $"Total Grade Point = {this.GetTotalGradePoint()}\n" +
            $"GPA = {this.GetGPA()}\n";
        return result;
    }
}
