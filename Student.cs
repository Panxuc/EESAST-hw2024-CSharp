namespace StudentGradeManager;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    // 请仅在此处实现接口，不要在此处以外的地方进行任何修改
    // 请尽可能周全地考虑鲁棒性
    // 提交作业时请删除这 3 行注释
    public Student(string name, string id)
    {
        Name = name;
        ID = Convert.ToInt32(id);
    }
    public string Name { get; set; } = "";
    public int ID { get; set; }
    public Dictionary<string, Grade> Grades { get; } = [];
    public void AddGrade(string course, string credit, string score)
    {
        if (Convert.ToInt32(score) < 0 || Convert.ToInt32(score) > 100)
        {
            Console.WriteLine("Invalid score. Please try again.");//虽然写了这一条但是好像在 AddGrade 被调用之前代码就检查了分数合法性
            return;
        }
        Grades.Add(course, new Grade(Convert.ToInt32(credit), Convert.ToInt32(score)));
    }

    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        for (int i = 0; i < grades.Count; i++)
        {
            Grades.Add(grades[i].course, new Grade(grades[i].credit, grades[i].score));
        }
    }

    public double GetGPA()
    {
        double GPA = 0;
        double sum = 0;
        foreach (var grade in Grades)
        {
            GPA += grade.Value.GradePoint * grade.Value.Credit;
            sum += grade.Value.Credit;

        }
        return GPA / sum;
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
            totalGradePoint += grade.Value.GradePoint * grade.Value.Credit;
        }
        return totalGradePoint;
    }

    public void RemoveGrade(string course)
    {
        if (Grades.Remove(course) == false) Console.WriteLine("Cannot find the course. Please try again.");
    }

    public void RemoveGrades(List<string> courses)
    {

        foreach (var course in courses)
        {
            if (Grades.Remove(course) == false)
            {
                Console.WriteLine("Cannot find the course. Please try again.");
                break;
            }
        }
    }
    public override string ToString()
    {
        string result = $"Name: {Name}, ID: {ID}, GPA: {GetGPA():F2}, Total credit: {GetTotalCredit()}, Total grade point: {GetTotalGradePoint()}";
        return result;
    }
}
