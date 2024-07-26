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
    private Dictionary<string, Grade> grades = new Dictionary<string, Grade>();
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
    public Dictionary<string, Grade> Grades
    {
        get => grades;
    }
    public Student(string name, string id)
    {
        this.name = name;
        this.id = int.TryParse(id, out int result) ? result : 0;
    }
    public void AddGrade(string course, string credit, string score)
    {
        int tempcre = int.TryParse(credit, out int rresult) ? rresult : 0;
        int tempsco = int.TryParse(score, out int rrresult) ? rrresult : 0;
        Grade newGrade = new Grade(tempcre, tempsco);
        this.grades.Add(course, newGrade);
    }
    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var grade in grades)
        {
            string courseName = grade.course;
            int courseCredit = grade.credit;
            int courseScore = grade.score;
            Grade newGrade = new Grade(courseCredit, courseScore);
            this.grades.Add(courseName, newGrade);
        }
    }
    public void RemoveGrade(string course)
    {
        this.grades.Remove(course);
    }
    public void RemoveGrades(List<string> courses)
    {
        foreach (var course in courses)
        {
            this.grades.Remove(course);
        }
    }
    public int GetTotalCredit()
    {
        int temp = 0;
        foreach (var kvp in this.grades)
        {
            temp += kvp.Value.Credit;
        }
        return temp;
    }
    public double GetTotalGradePoint()
    {
        double temp = 0;
        foreach (var kvp in this.grades)
        {
            temp += kvp.Value.GradePoint;
        }
        return temp;
    }
    public double GetGPA()
    {
        return this.GetTotalGradePoint() / this.GetTotalCredit();
    }
    public override string ToString()
    {
        string result = $"Student Name : {this.name}, ID : {this.ID}";
        if (this.grades != null && this.grades.Count > 0)
        {
            result += Environment.NewLine;
            result += "Courses    Credits    Scores";
            result += Environment.NewLine;
            foreach (var grade in this.grades)
            {
                result += $"{grade.Key}    {grade.Value.Credit}    {grade.Value.Score}";
                result += Environment.NewLine;
            }
            result += "TotalCredit:";
            result += Convert.ToString(this.GetTotalCredit());
            result += Environment.NewLine;
            result += "TotalGradePoint:";
            result += Convert.ToString(this.GetTotalGradePoint());
            result += Environment.NewLine;
            result += "GPA:";
            result += Convert.ToString(this.GetGPA());
            result += Environment.NewLine;
        }
        return result;
    }
}

}
