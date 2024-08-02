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

public class Grade
{
    public string Course { get; }
    public int Credit { get; }
    public int Score { get; }

    public Grade(string course, int credit, int score)
    {
        Course = course;
        Credit = credit;
        Score = score;
    }
}

public class Student : IStudent
{
    public string Name { get; set; }
    public int ID { get; set; }
    public Dictionary<string, Grade> Grades { get; private set; }

    public Student()
    {
        Grades = new Dictionary<string, Grade>();
    }

    public void AddGrade(string course, string credit, string score)
    {
        if (Grades.ContainsKey(course))
        {
            throw new ArgumentException($"学生已存在课程 '{course}' 的成绩。");
        }
        Grades[course] = new Grade(course, credit, score);
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
        {
            throw new ArgumentException($"学生没有课程 '{course}' 的成绩。");
        }
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
        return Grades.Values.Sum(grade => grade.Credit * grade.Score);
    }

    public double GetGPA()
    {
        if (GetTotalCredit() == 0) return 0;
        return GetTotalGradePoint() / GetTotalCredit();
    }

    public override string ToString()
    {
        return $"学生姓名：{Name}, 学号：{ID}, GPA：{GetGPA():F2}";
    }
}


