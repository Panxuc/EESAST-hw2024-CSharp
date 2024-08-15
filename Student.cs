namespace StudentGradeManager;

using System;
using System.Collections.Generic;
using System.Data.Common;
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
    public Dictionary<string, Grade> Grades { get; }//没有set意味着只能从外部访问但是不能直接修改它
                                                    //字典类型,这个grade是另一个类的一个属性
    public Student(string studname, string studid)
    {
        Name = studname;
        if (int.TryParse(studid, out int id))
            ID = id;
        else
            System.Console.WriteLine("wrong ID input.");
        Grades = new Dictionary<string, Grade>();//没有这行代码， Grades  将会是  null ，在后续对  Grades  字典的任何操作（如添加、删除成绩）都会导致  NullReferenceException  异常
    }

    public void AddGrade(string course, string credit, string score)
    {
        if (int.TryParse(credit, out int credit_int) && int.TryParse(score, out int score_int))
        {
            Grades[course] = new Grade(credit_int, score_int);//字典的键是课程名，值是换算成的每门课的绩点
        }
        else
            System.Console.WriteLine("wrong credit or score input.");
    }
    public void AddGrades(List<(string course, int credit, int score)> grades)//接受一个包含元组的列表，元组包含这三个元素
    {
        foreach (var (course, credit, score) in grades)//遍历列表中的每个元组，使用var关键字声明这三个变量
        {
            AddGrade(course, credit.ToString(), score.ToString());//在每个元组里里面使用addgrade方法，另外使用之前把元组里面的int转化成string
        }
    }
    public void RemoveGrade(string course)
    {
        if (Grades.ContainsKey(course))
            Grades.Remove(course);//在那个字典里面去除了那个课程的键，同时也就去掉了键的值，也就是这门课的grade point
        else
            Console.WriteLine("no such course.");
    }
    public void RemoveGrades(List<string> courses)//传入一个叫courses的列表，这个列表的元素只能是字符串
    {
        foreach (string course in courses)
        {
            RemoveGrade(course);
        }
    }
    public int GetTotalCredit()
    {
        int TotalCredit = 0;
        foreach (Grade grade in Grades.Values)//这个values就是字典的值，也就是grade,同时grade还是一个类，这个类里包含Credit和Score
            TotalCredit += grade.Credit;
        return TotalCredit;
    }
    public double GetTotalGradePoint()
    {
        double TotalGradePoint = 0.0;
        foreach (Grade grade in Grades.Values)
            TotalGradePoint += grade.GradePoint * grade.Credit;
        return TotalGradePoint;
    }
    public double GetGPA()
    {
        return GetTotalGradePoint() / GetTotalCredit();
    }
    public override string ToString()
    {
        return $"name:{Name}\nID:{ID}\ntotal credits:{GetTotalCredit()}\ntotal grade points:{GetTotalGradePoint()}\nGPA:{GetGPA()}";
    }

}
