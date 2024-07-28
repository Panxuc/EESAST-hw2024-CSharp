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
    public Dictionary<string, Grade> Grades { get; private set; }
    public Student(string name, string id)
    {
        Name = name;

        if (int.TryParse(id, out int idValue))
        {
            ID = idValue;
        }
        else
        {
            throw new ArgumentException("ID must be a string with integer format.");
        }

        Grades = new Dictionary<string, Grade>();
    }


    public void AddGrade(string course, string credit, string score)
    {
        if (int.TryParse(credit, out int creditValue) && int.TryParse(score, out int scoreValue))
        {
            Grades[course] = new Grade(creditValue, scoreValue);
        }
        else
        {
            throw new ArgumentException("credit and score must be valid integer.");
        }
    }

    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var (course, credit, score) in grades)
        {
            AddGrade(course, credit.ToString(), score.ToString());
        }
    }

    public void RemoveGrade(string course)
    {
        if (Grades.ContainsKey(course))
        {
            Grades.Remove(course);
        }
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
        double totalPoints = Grades.Sum(g => g.Value.GradePoint * g.Value.Credit);
        return totalPoints;
    }

    public double GetGPA()
    {
        int totalCredits = GetTotalCredit();
        if (totalCredits == 0)
        {
            return 0.0;
        }
        return GetTotalGradePoint() / totalCredits;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Name: {Name}");
        sb.AppendLine($"ID: {ID}");
        sb.AppendLine("Grades:");
        foreach (var (course, grade) in Grades)
        {
            sb.AppendLine($"{course}: {grade.Credit} credits, {grade.Score} points, {grade.GradePoint} grade points.");
        }
        sb.AppendLine($"Total credits: {GetTotalCredit()}");
        sb.AppendLine($"Total grade points: {GetTotalGradePoint()}");
        sb.AppendLine($"GPA: {GetGPA()}");
        return sb.ToString();
    }

}
