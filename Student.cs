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
    // 请仅在此处实现接口，不要在此处以外的地方进行任何修改
    // 请尽可能周全地考虑鲁棒性
    // 提交作业时请删除这 3 行注释
    private string name;
    private int id;
    public Student(string nam, string i)
    {
        name = nam;
        id = int.Parse(i);
    }
    string IStudent.Name
    {
        get { return name; }//应该正确
        set { }
    }
    int IStudent.ID
    {
        get { return id;}//应该正确
        set { }
    }
    Dictionary<string, Grade> IStudent.Grades
    {
        get { return new Dictionary<string, Grade>(); }//乱写的奥
    }
    public void AddGrade(string course, string credit, string score)
    {
        Console.WriteLine(course, credit, score);
    }
    void IStudent.AddGrade(string course, string credit, string score)
    {
        Console.WriteLine(course, credit, score);
    }
    void IStudent.AddGrades(List<(string course, int credit, int score)> grades)
    {
        Console.WriteLine("AddGrades");
    }
    void IStudent.RemoveGrade(string course)
    {
        Console.WriteLine("RemoveGrade");
    }
    void IStudent.RemoveGrades(List<string> courses)
    {
        Console.WriteLine("RemoveGrades");
    }
    int IStudent.GetTotalCredit()
    {
        Console.WriteLine("GetTotalCredit");
        return 0;
    }
    double IStudent.GetTotalGradePoint()
    {
        Console.WriteLine("GetTotalGradePoint");
        return 0.0;
    }
    double IStudent.GetGPA()
    {
        Console.WriteLine("GetGPA");
        return 0.0;
    }
    public override string ToString()
    {
        return "Student";
    }
}
