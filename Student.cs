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
    
    public void AddGrade(string course, string credit, string score)
    {
        if (int.TryParse(credit, out int credit_int) && int.TryParse(score, out int score_int))
        {
            Grades[course] = new Grade(credit_int, score_int);
        }
        else
            System.Console.WriteLine("wrong input for credit or score.");
    }
    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach(var (course, credit, score) in grades)
        {
            AddGrade(course, credit.ToString(), score.ToString());
        }
    }
    public void RemoveGrade(string course)
    {
        if (Grades.ContainsKey(course))
            Grades.Remove(course);
        else
            System.Console.Writeline("no such course.");
    }
    public void RemoveGrades(List<string> courses)
    {
        foreach (string course in courses)
        {
            RemoveGrade(course);
        }

    }
    public int GetTotalCredit()
    {
        int TotalCredit = 0;
        foreach(Grade grade in Grades.Values)
            TotalCredit += grade.Credit;
        return TotalCredit;
    }
    public double GetTotalGradePoint()
    {
        double TotalGradePoint = 0;
        foreach (Grade grade in Grades.Values)
            TotalGradePoint += grade.GradePoint * grade.Credit;
        return TotalGradePoint;
    }
    public double GetGPA()
    {
        return GetTotalGradePoint() / GetTotalCredit();
    }
    public string ToString()
    {
        return $"name:{Name}\nID:{ID}\ntotal credits:{GetTotalCredit()}\ntotal grade points:{GetTotalGradePoint()}\nGPA:{GetGPA()}";
    }
    public Student(string studname, string studid)
    {
        Name = studname;
        if (int.TryParse(studid, out int id))
            ID = id;
        else
            System.Console.Writeline("wrong input for ID.");
        Grades = new Dictionary<string, Grade>();
    }
}
