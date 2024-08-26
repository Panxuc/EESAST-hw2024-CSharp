namespace StudentGradeManager;

using System;
using System.Collections.Generic;

public class StudentManager
{
    private readonly List<IStudent> _students = [];
    public List<IStudent> GetStudentsByID(int studentID)
    {
        return _students.Where(s => s.ID == studentID).ToList();
    }
    public List<IStudent> GetStudentsByName(string studentName)
    {
        return _students.Where(s => s.Name == studentName).ToList();
    }
    public List<IStudent> GetTopStudents(int count)
    {
        return _students.OrderByDescending(s => s.GetGPA()).Take(count).ToList();
    }
    public void AddStudent(IStudent student)
    {
        if (_students.FirstOrDefault(s => s.ID == student.ID) is null)
            _students.Add(student);
        else
            throw new Exception("Student already exists");
    }
    public void RemoveStudent(int studentID)
    {
        var student = _students.FirstOrDefault(s => s.ID == studentID) ?? throw new Exception("Student not found");
        _students.Remove(student);
    }
    public void PrintStudent(int studentID)
    {
        var student = _students.FirstOrDefault(s => s.ID == studentID) ?? throw new Exception("Student not found");
        Console.WriteLine(student.ToString());
    }
}
