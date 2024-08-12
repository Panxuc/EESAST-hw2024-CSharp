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
    private Dictionary<string, Grade> grades = new Dictionary<string, Grade>();
    public string Name { get; set; }
    public int ID { get; private set; }
    public Dictionary<string, Grade> Grades => grades;
    public Student(string name, string id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        if (int.TryParse(id, out int idValue))
        {
            ID = idValue;
        }
        else
        {
            throw new ArgumentException("Invalid ID format.");
        }
    }
    public void AddGrade(string course, string credit, string score)
        {
            if (int.TryParse(credit, out int creditValue) && double.TryParse(score, out double scoreValue))
            {
                grades[course] = new Grade(creditValue,(int)scoreValue);
            }
            else
            {
                throw new ArgumentException("Invalid credit or score format.");
            }
        }
    public void AddGrades(List<(string course, int credit, int score)> grades)
        {
            foreach (var grade in grades)
            {
                this.grades[grade.course] = new Grade(grade.credit, grade.score);
            }
        }
    public void RemoveGrade(string course)
        {
            grades.Remove(course);
        }
    public void RemoveGrades(List<string> courses)
        {
            foreach (var course in courses)
            {
                grades.Remove(course);
            }
        }
    public int GetTotalCredit()
        {
            return grades.Values.Sum(g => g.Credit);
        }
    public double GetTotalGradePoint()
        {
            return grades.Sum(g => g.Value.Credit * g.Value.Score);
        }
    public double GetGPA()
        {
            int totalCredit = GetTotalCredit();
            if (totalCredit == 0)
            {
                return 0.0;
            }
            return GetTotalGradePoint() / totalCredit;
        }
        public override string ToString()
        {
            var gradeInfo = string.Join(", ", grades.Select(g => $"{g.Key}: {g.Value.Score} (Credit: {g.Value.Credit})"));
            return $"Student ID: {ID}, Name: {Name}, Grades: [{gradeInfo}]";
        }
    }
