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
    public Dictionary<string, Grade> Grade { get; }
    public void AddGrade(string course, string credit, string score)
    {
        Grade addgrade = new Grade();
        addgrade.credit = credit.ToInt();
        addgrade.score = score.ToInt();
        if (!Grade.ContainsKey(course))
        {
            Grade.Add(course, addgrade);
        }
        else
        {
            Grade[course] = addgrade;
        }
    }
    public void AddGrades(List<(string course,int credit,int score)> grades)
    {
        Grade addgrades = new Grade();
        addgrades.credit = grades.credit;
        addgrades.score = grades.score;
        for(int i=0;i<grades.Count; i++)
        {
            if (!Grade.ContainsKey(grades[i].course))
            {
                Grade.Add(grades[i].course, addgrades);
            }
            else
            {
                Grade[grades[i].course] = addgrades;
            }
        }
    }
    public void RemoveGrade(string course)
    {
        if (Grade.ContainsKey(course))
        {
            Grade.Remove(course);
        }
    }
    public void RemoveGrades(List<string> courses)
    {
        foreach (string course in courses)
        {
            if (Grade.ContainsKey(course))
            {
                Grade.Remove(course);
            }
        }
    }
    public int GetTotalCredit()
    {
        int totalCredit = 0;
        foreach (string course in Grade.Keys)
        {
            totalCredit += Grade[course].credit;
        }
        return totalCredit;
    }
    public double GetTotalGradePoint()
    {
        int totalGradePoint = 0;
        foreach(string course in Grade.Keys)
        {
            totalGradePoint += Grade[course].GradePoint;
        }
        return totalGradePoint;
    }
    public double GetGPA()
    {
        double totalGPA = 0;
        if (GetTotalCredit() > 0)
        {
            foreach(string course in Grade.Keys)
            {
                totalGPA += Grade[course].GradePoint * Grade[course].credit;
            }
        }
        return totalGPA / GetTotalCredit();
    }
    public string ToString()
    {
        string str = "";
        str += "Name: " + Name + "\n";
        str += "ID: " + ID + "\n";
        str += "Grades:\n";
        foreach (string course in Grade.Keys)
        {
            str += "  " + course + ": " + Grade[course].ToString() + "\n";
        }
        str += "Total Credit: " + GetTotalCredit() + "\n";
        str += "Total Grade Point: " + GetTotalGradePoint() + "\n";
        str += "GPA: " + GetGPA();
        return str;
    }
}
