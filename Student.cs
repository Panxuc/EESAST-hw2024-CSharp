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
    public string Name
    {
        get => name;
        public set { name = value; }
    }
    private int id;
    public int ID
    {
        get => id;
        public set { id = value; }
    }
    private Dictionary<string, Grade> grades;
    public Dictionary<string, Grade> Grades
    {
        get => grades;
    }
    public void AddGrade(string course, string credit, string score)
    {
        if(grades.ContainsKey(course))
        {
            Console.WriteLine("Grade of " + course + " exist");
            return;
        }
        Grade grade = new Grade(int.Parse(credit), int.Parse(score));
        grades.Add(course, grade);
    }
    public void AddGrades(List<(string course, int credit, int score)> grds)
    {
        foreach (var grd in grds)
        {
            AddGrade(grd.course,grd.credit, grd.score);
        }
    }
    public void RemoveGrade(string course)
    {
        if (grades.ContainsKey(course))
            grades.Remove(course);
        else Console.WriteLine("Grade of " + course + " not exist");
    }
    public void RemoveGrades(List<string> courses)
    {
        foreach (string course in courses)
            RemoveGrade(course);
    }
    public int GetTotalCredit()
    {
        int sum = 0;
        foreach (var value in grades.Values)
            sum += value.Credit;
        return sum;
    }
    public double GetTotalGradePoint()
    {
        double sum = 0;
        foreach (var value in grades.Values)
            sum += value.GradePoint();
        return sum;
    }
    public double GetGPA()
    {
        double sum = 0;
        foreach (var value in grades.Values)
            sum += value.Credit * value.GradePoint();
        return sum / GetTotalCredit();
    }
    public string ToString()
    {
        var stdent = $"Name:{name}\nID: { id }\nCourse\tCredit\tScore\n";
        foreach (var grade in grades)
        {
            stdent += $"{grade.Key}\t{grade.Value.Credit}\t{grade.Value.Score}\n";
        }
    }
}
