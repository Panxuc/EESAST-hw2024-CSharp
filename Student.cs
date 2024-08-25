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
    private string name;
    public string Name{
        get=>name;
        set{
            name=value;
        }
    }
    private int id;
    public int ID{
        get=>id;
        set{
            id=value;
        }
    }
    public Student(string?n,string?i){
        if(n!=null&&i!=null){
            name=n;
            id=Convert.ToInt32(i);
            grade=new Dictionary<string, Grade>();
        }
    }
    private Dictionary<string,Grade> grade;
    public Dictionary<string,Grade> Grades{
        get=>grade;
        private set{
            grade=value;
        }
    }
    public void AddGrade(string course, string credit, string score){
        Grades.Add(course,new Grade(Convert.ToInt32(credit),Convert.ToInt32(score)));
    }
    public void AddGrades(List<(string course, int credit, int score)> grades){
        foreach(var i in grades){
            Grades.Add(i.course,new Grade(i.credit,i.score));
        }
    }
    public void RemoveGrade(string course){
        Grades.Remove(course);
    }
    public void RemoveGrades(List<string> courses){
        foreach(var i in courses){
            RemoveGrade(i);
        }
    }
    public int GetTotalCredit(){
        int total_credit=0;
        foreach(var i in grade){
            total_credit+=i.Value.Credit;
        }
        return total_credit;
    }
    public double GetTotalGradePoint(){
        double total_gradepoint=0;
        foreach(var i in grade){
            total_gradepoint+=i.Value.GradePoint*i.Value.Credit;
        }
        return total_gradepoint;
    }
    public double GetGPA()=>GetTotalGradePoint()/GetTotalCredit();
    override public string ToString()=>$"Name:{name}\nID:{id}\nGPA:{GetGPA()}\n";
}
