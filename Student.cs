namespace StudentGradeManager;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

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
    public string Name { get; set; } = "NULL";
    public int ID { get; set; } = 0;
    public Student(string name, string id)
    {
        Name = name;
        if (!int.TryParse(id, out int iid))
        {
            Console.WriteLine("Invalid score");
        }
        else ID = iid;
    }
    public Dictionary<string, Grade> Grades { get; set; } = new Dictionary<string, Grade>();
    public void AddGrade(string course, string credit, string score)
    {
        if (!int.TryParse(credit, out int icredit))
        {
            Console.WriteLine("Invalid credit");
        }
        if (!int.TryParse(score, out int iscore))
        {
            Console.WriteLine("Invalid score");
        }
        Grade grade = new Grade(icredit, iscore);
        Grades.Add(course, grade);
    }
    private List<(string, int, int)> grs = new List<(string, int, int)>();
    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var grade in grades)
        {
            grs.Add(grade);
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
            foreach (var gr in grs)
            {
                if (gr.Item1 == course)
                {
                    grs.Remove(gr);
                }
            }
        }
    }
    public int GetTotalCredit()
    {
        int totalCredit = 0;
        foreach (var grade in Grades)
        {
            totalCredit += grade.Value.Credit;
        }
        return totalCredit;
    }
    public double GetTotalGradePoint()
    {
        double totalGradePoint = 0;
        foreach (var grade in Grades)
        {
            totalGradePoint += grade.Value.Credit * grade.Value.GradePoint;
        }
        return totalGradePoint;
    }
    public double GetGPA()
    {
        if (GetTotalCredit() > 0)
            return GetTotalGradePoint() / GetTotalCredit();
        else return 0;
    }
    public override string ToString()
    {
        string str = "\nID:" + ID.ToString() + " Name:" + Name.ToString() + " GPA:" + GetGPA().ToString("F2") + "\n";
        foreach (var grade in Grades)
        {
            str += "course: " + grade.Key.ToString() + " credit:" + grade.Value.Credit + " score:" + grade.Value.Score + " gradepoint:" + grade.Value.GradePoint + "\n";
        }
        return str;
    }
}