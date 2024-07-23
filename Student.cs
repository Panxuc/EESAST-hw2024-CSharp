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

    public void AddGrade(string course, string credit, string score)
    {
        if (Grades.ContainsKey(course))
        {
            throw new ArgumentException("此课程已经存在，请勿重复添加，如需修改请使用RemoveGrade方法");
        }
        int intScore = int.Parse(score);
        int intCredit = int.Parse(credit);
        Grades.Add(course, new Grade(intCredit, intScore));
    }

    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var grade in grades)
        {
            AddGrade(grade.course, grade.credit.ToString(), grade.score.ToString());    //
        }
    }

    public void RemoveGrade(string course)
    {
        if (!Grades.ContainsKey(course))
        {
            throw new ArgumentException("此课程不存在，无法删除");
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
        return Grades.Values.Sum(grade => grade.GradePoint);
    }

    public double GetGPA()
    {
        return GetTotalGradePoint() / GetTotalCredit();
    }

    public override string ToString()
    {
        return $"Name: {Name}, ID: {ID}, GPA: {GetGPA()}";
    }

    public Student(string name, string ID)
    {
        Name = name;
        this.ID = int.Parse(ID);
    }
}