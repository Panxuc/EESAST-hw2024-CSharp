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
    public string Name
    {
        get =>name;
        set
        {
            name=value;
        }
    }
    private int id;
    public int ID
    {
        get =>ID;
        set
        {
            id=value;
        }
    }
    private Dictionary<string,Grade> gradesdict=new Dictionary<string,Grade>();
    public Dictionary<string,Grade> Grades
    {
        get =>gradesdict;
    }
    public void AddGrade(string course, string credit, string score)
    {
        if(gradesdict.ContainsKey(course))
        {
            Console.WriteLine("该课程成绩已存在！");
        }
        else
        {
            gradesdict.Add(course,new Grade(int.Parse(credit),int.Parse(score)));
        }
    }
    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach((string course,int credit,int score) in grades)
        {
            if(gradesdict.ContainsKey(course))
            {
                Console.WriteLine("该课程成绩已存在！");
            }
            else
            {
                gradesdict.Add(course,new Grade(credit,score));
            }
        }
    }
    public void RemoveGrade(string course)
    {
        gradesdict.Remove(course);
    }
    public void RemoveGrades(List<string> courses)
    {
        foreach(string course in courses)
        {
            gradesdict.Remove(course);
        }
    }
    public int GetTotalCredit()
    {
        int sum=0;
        foreach(var grade in gradesdict)
        {
            sum+=grade.Value.Credit;
        }
        return sum;
    }
    public double GetTotalGradePoint()
    {
        double sum=0;
        foreach(var grade in gradesdict)
        {
            sum+=grade.Value.GradePoint;
        }
        return sum;
    }
    public double GetGPA()
    {
        if(GetTotalCredit()==0) return 0;
        return GetTotalGradePoint()/GetTotalCredit();
    }
    public override string ToString()
    {
        return $"NAME:{Name},ID:{ID},GPA:{GetGPA()}";
    }
    public Student(string Name,string ID)
    {
        name=Name;
        id=int.Parse(ID);
    }
}
