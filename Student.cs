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
    public Dictionary<string, Grade> Grades { get; private set; } = new();
    public Student(string name, string id)
    {
        Name = name;
        if (int.TryParse(id, out int parsedId))
        {
            ID = parsedId;
        }
        else
        {
            throw new ArgumentException("无效的学生ID。");
        }
    }
    public void AddGrade(string course, string credit, string score)
    {
        if (int.TryParse(credit, out var creditValue) && int.TryParse(score, out var scoreValue))
        {
            Grades[course] = new Grade(creditValue, scoreValue);
        }
        else
        {
            throw new ArgumentException("无效的学分或成绩值。");
        }
    }
    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var (course, credit, score) in grades)
        {
            Grades[course] = new Grade(credit, score);
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
        return Grades.Values.Sum(g => g.Credit);
    }
    public double GetTotalGradePoint()
    {
        return Grades.Values.Sum(g => g.Credit * g.GradePoint);
    }
    public double GetGPA()
    {
        var totalCredit = GetTotalCredit();
        return totalCredit == 0 ? 0 : GetTotalGradePoint() / totalCredit;
    }
    public override string ToString()
    {
        var gradesStr = string.Join(", ", Grades.Select(g => $"{g.Key}: {g.Value.Score}"));
        return $"Name: {Name}, ID: {ID}, Grades: [{gradesStr}], GPA: {GetGPA():F2}";
    }
}
