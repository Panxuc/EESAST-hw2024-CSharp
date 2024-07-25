using System.Runtime.InteropServices;

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

public class Student(string name, string id) : IStudent
{
    public string Name { get; set; } = name;

    public int ID { get; set; } = int.Parse(id);

    public Dictionary<string, Grade> Grades { get; } = [];

    public void AddGrade(string course, string credit, string score)
    {
        //若课程已存在，则不添加成绩，并打印提示信息
        if (Grades.ContainsKey(course))
        {
            Console.WriteLine("The course already exists, please try again.");
            return;
        }
        Grades.Add(course, new Grade(int.Parse(credit), int.Parse(score)));
    }

    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        //若课程已存在，则不添加成绩，并打印提示信息
        foreach (var (course, credit, score) in grades)
        {
            if (!Grades.ContainsKey(course)) continue;
            Console.WriteLine("The course already exists, please try again.");
            return;
        }
        foreach (var (course, credit, score) in grades)
        {
            Grades.Add(course, new Grade(credit, score));
        }
    }

    public void RemoveGrade(string course)
    {
        //若课程不存在，则打印提示信息
        if (!Grades.ContainsKey(course))
        {
            Console.WriteLine("The course does not exist, please try again.");
            return;
        }
        Grades.Remove(course);
    }

    public void RemoveGrades(List<string> courses)
    {
        //若课程不存在，则打印提示信息
        if (courses.Any(course => !Grades.ContainsKey(course)))
        {
            Console.WriteLine("The course does not exist, please try again.");
            return;
        }

        foreach (var course in courses)
        {
            Grades.Remove(course);
        }
    }

    public int GetTotalCredit()
    {
        //如果没有课程，则返回0
        return Grades.Values.Sum(g => g.Credit);
    }

    public double GetTotalGradePoint()
    {
        //如果没有课程，则返回0
        return Grades.Values.Sum(g => g.GradePoint * g.Credit);
    }

    public double GetGPA()
    {
        //如果没有课程，则返回0
        if (Grades.Count == 0) return 0;
        return GetTotalGradePoint() / GetTotalCredit();
    }

    public override string ToString()
    {
        var result = $"Name: {Name}\nID: {ID}\n";
        //如果有课程，则遍历所有课程并输出
        if (Grades.Count != 0)
            return Grades.Aggregate(result,
                (current, course) =>
                    current
                    + $"{course.Key}: {course.Value.Credit} credits, {course.Value.Score} points, grade point is {course.Value.GradePoint} \n")
                    + $"Total credits: {GetTotalCredit()}\nTotal grade points: {GetTotalGradePoint()}\nGPA: {GetGPA()}\n";
        //如果没有课程，则输出提示信息
        result += "No grades available.";
        return result;

    }
}
