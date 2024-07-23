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
    private double totalGradePoint;
    private double GPA;
    private Dictionary<string, Grade> grades = new Dictionary<string, Grade>();
    private List<(string, int, int)> courseList = new List<(string, int, int)>();
    public Student(string name, string id)
    {
        this.name = name;
        this.id = int.TryParse(id, out int result) ? result : 0;
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    public int ID
    {
        get { return id; }
        set { id = value; }
    }
    public Dictionary<string, Grade> Grades
    {
        get
        {
            foreach (var item in courseList)
            {
                grades.Add(item.Item1, new Grade(item.Item2, item.Item3));
            }
            return grades;
        }
    }
    public void AddGrade(string course, string credit, string score)
    {
        int credit_ = int.TryParse(credit, out int creditNum) ? creditNum : 0;
        int score_ = int.TryParse(score, out int scoreNum) ? scoreNum : 0;
        courseList.Add((course, credit_, score_));
        grades.Add(course, new Grade(credit_, score_));
    }
    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var item in grades)
        {
            courseList.Add((item.course, item.credit, item.score));
            this.grades.Add(item.course, new Grade(item.credit, item.score));
        }
    }
    public void RemoveGrade(string course)
    {
        courseList.RemoveAll(x => x.Item1 == course);
        grades.Remove(course);
    }
    public void RemoveGrades(List<string> courses)
    {
        foreach (var course in courses)
        {
            courseList.RemoveAll(x => x.Item1 == course);
            grades.Remove(course);
        }
    }
    public int GetTotalCredit()
    {
        int totalCredit = 0;
        foreach (var item in courseList)
        {
            totalCredit += item.Item2;
        }
        return totalCredit;
    }
    public double GetTotalGradePoint()
    {
        double totalGradePoint = 0;
        for (int i = 0; i < grades.Count; ++i)
        {
            string key = courseList[i].Item1;
            totalGradePoint += grades[courseList[i].Item1].GradePoint * courseList[i].Item2;
        }
        return totalGradePoint;
    }
    public double GetGPA()
    {
        totalGradePoint = GetTotalGradePoint();
        GPA = totalGradePoint / GetTotalCredit();
        return GPA;
    }
    public override string ToString()
    {
        return $"Name: {name}, ID: {id}, GPA:{GetGPA()}";
    }
}
