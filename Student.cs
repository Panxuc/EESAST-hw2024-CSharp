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

    // 构造函数
    public Student(string name_, string id_)
    {
        Name = name_;
        if (int.TryParse(id_, out int idValue))
        {
            ID = idValue;
        }
        else
        {
            throw new ArgumentException("Invalid id values. It should be an integer.");
        }
    }

    public Dictionary<string, Grade> _grades = [];
    public Dictionary<string, Grade> Grades { get => _grades; }

    /// <summary>
    /// 将一门课程及其学分、成绩等信息添加到 _grades 字典中
    /// </summary>
    /// <param name="course">课程名</param>
    /// <param name="credit">学分</param>
    /// <param name="score">成绩</param>
    /// <exception cref="ArgumentException">credit 或 score 不能转化为 int 类型</exception>
    public void AddGrade(string course, string credit, string score)
    {
        // 将 string credit 与 string score 转化为 int 类型
        if (int.TryParse(credit, out int creditValue) && int.TryParse(score, out int scoreValue))
        {
            _grades[course] = new Grade(creditValue, scoreValue);
        }
        else
        {
            throw new ArgumentException("Invalid credit or score values. They should be integers.");
        }

    }

    /// <summary>
    /// 将多门课程及其学分、成绩等信息添加到 _grades 字典中
    /// </summary>
    /// <param name="grades">记录课程相关信息的列表</param>
    public void AddGrades(List<(string course, int credit, int score)> grades)
    {
        foreach (var grade in grades)
        {
            _grades[grade.course] = new Grade(grade.credit, grade.score);
        }
    }

    /// <summary>
    /// 移除一门指定课程的成绩
    /// </summary>
    /// <param name="course">待移除的课程名</param>
    /// <exception cref="KeyNotFoundException">不存在对应的课程</exception>
    public void RemoveGrade(string course)
    {
        // 检查是否存在对应的课程
        if (!_grades.ContainsKey(course))
        {
            throw new KeyNotFoundException("Course NOT found in student's grades.");
        }
        _grades.Remove(course); // 移除课程对应的键值对
    }

    /// <summary>
    /// 移除多门指定课程的成绩
    /// </summary>
    /// <param name="courses">待移除的课程名组成的列表</param>
    public void RemoveGrades(List<string> courses)
    {
        foreach (var course in courses)
        {
            RemoveGrade(course);
        }
    }


    /// <summary>
    /// 计算总学分
    /// </summary>
    /// <returns>总学分</returns>
    public int GetTotalCredit()
    {
        // 获取 _grades 字典中的所有键值对的 Credit 的值 并计算其总和
        return _grades.Values.Sum(grade => grade.Credit);
    }


    /// <summary>
    /// 计算总的(学分*绩点)
    /// </summary>
    /// <returns>总的(学分*绩点)</returns>
    public double GetTotalGradePoint()
    {
        return _grades.Values.Sum(grade => grade.GradePoint * grade.Credit);
    }


    /// <summary>
    /// 计算GPA
    /// </summary>
    /// <returns>GPA; 若总学分为零，则返回零</returns>
    public double GetGPA()
    {
        var totalCredit = GetTotalCredit();
        if (totalCredit == 0)
        {
            return 0.0;
        }
        return GetTotalGradePoint() / totalCredit;
    }


    /// <summary>
    /// 获取学生信息 重写基类 Object 的 ToString 方法
    /// </summary>
    /// <returns>包含学生信息的字符串 GPA保留2位小数</returns>
    public override string ToString()
    {
        return $"Name: {Name}, ID: {ID}, GPA: {GetGPA():F2}";
    }
}
