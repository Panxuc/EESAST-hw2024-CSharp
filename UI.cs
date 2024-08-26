namespace StudentGradeManager;

public class UI
{
    private readonly StudentManager _sm = new();

    private static void Menu()
    {
        Console.WriteLine("STUDENT GRADE MANAGER");
        Console.WriteLine("1. Add student");
        Console.WriteLine("2. Remove student");
        Console.WriteLine("3. Edit student");
        Console.WriteLine("4. Get students by ID");
        Console.WriteLine("5. Get students by name");
        Console.WriteLine("6. Get top students");
        Console.WriteLine("7. Exit");
        Console.Write("Enter your choice: ");
    }

    private void AddStudent()
    {
        string? name, id;
        while (true)
        {
            Console.Write("Enter student name: ");
            name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name)) break;
            Console.WriteLine("The student name cannot be empty. Please try again.");
        }
        while (true)
        {
            Console.Write("Enter student ID: ");
            id = Console.ReadLine();
            if (int.TryParse(id, out _)) break;
            Console.WriteLine("Invalid ID. Please try again.");
        }
        var student = new Student(name, id);
        _sm.AddStudent(student);
        while (true)
        {
            Console.WriteLine("1. Add grade");
            Console.WriteLine("2. Finish");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();
            if (choice == "1")
            {
                string? course, credit, score;
                while (true)
                {
                    Console.Write("Enter course name: ");
                    course = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(course)) break;
                    Console.WriteLine("The course name cannot be empty. Please try again.");
                }
                while (true)
                {
                    Console.Write("Enter credit: ");
                    credit = Console.ReadLine();
                    if (int.TryParse(credit, out var result) && result >= 0) break;
                    Console.WriteLine("Invalid credit. Please try again.");
                }
                while (true)
                {
                    Console.Write("Enter score: ");
                    score = Console.ReadLine();
                    if (int.TryParse(score, out var result) && result is >= 0 and <= 100) break;
                    Console.WriteLine("Invalid score. Please try again.");
                }
                student.AddGrade(course, credit, score);
            }
            else if (choice == "2")
            {
                break;
            }
        }
    }

    private void RemoveStudent()
    {
        string? id;
        while (true)
        {
            Console.Write("Enter student ID: ");
            id = Console.ReadLine();
            if (int.TryParse(id, out _)) break;
            Console.WriteLine("Invalid ID. Please try again.");
        }
        _sm.RemoveStudent(int.Parse(id));
    }

    private void EditStudent()
    {
        string? id;
        do
        {
            Console.Write("Enter student ID: ");
            id = Console.ReadLine();
        } while (int.TryParse(id, out _) == false);
        var student = _sm.GetStudentsByID(int.Parse(id))[0];
        while (true)
        {
            Console.WriteLine("1. Add grade");
            Console.WriteLine("2. Remove grade");
            Console.WriteLine("3. Finish");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();
            if (choice == "1")
            {
                string? course, credit, score;
                while (true)
                {
                    Console.Write("Enter course name: ");
                    course = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(course)) break;
                    Console.WriteLine("The course name cannot be empty. Please try again.");
                }
                while (true)
                {
                    Console.Write("Enter credit: ");
                    credit = Console.ReadLine();
                    if (int.TryParse(credit, out var result) && result >= 0) break;
                    Console.WriteLine("Invalid credit. Please try again.");
                }
                while (true)
                {
                    Console.Write("Enter score: ");
                    score = Console.ReadLine();
                    if (int.TryParse(score, out var result) && result is >= 0 and <= 100) break;
                    Console.WriteLine("Invalid score. Please try again.");
                }
                student.AddGrade(course, credit, score);
            }
            else if (choice == "2")
            {
                string? course;
                while (true)
                {
                    Console.Write("Enter course name: ");
                    course = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(course)) break;
                    Console.WriteLine("The course name cannot be empty. Please try again.");
                }
                student.RemoveGrade(course);
            }
            else if (choice == "3")
            {
                break;
            }
        }
    }

    private void GetStudentsByID()
    {
        string? id;
        while (true)
        {
            Console.Write("Enter student ID: ");
            id = Console.ReadLine();
            if (int.TryParse(id, out _)) break;
            Console.WriteLine("Invalid ID. Please try again.");
        }
        var students = _sm.GetStudentsByID(int.Parse(id));
        foreach (var student in students)
        {
            Console.WriteLine(student.ToString());
        }
    }

    private void GetStudentsByName()
    {
        string? name;
        while (true)
        {
            Console.Write("Enter student name: ");
            name = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(name)) break;
            Console.WriteLine("The student name cannot be empty. Please try again.");
        }
        var students = _sm.GetStudentsByName(name);
        foreach (var student in students)
        {
            Console.WriteLine(student.ToString());
        }
    }

    private void GetTopStudents()
    {
        string? count;
        while (true)
        {
            Console.Write("Enter count: ");
            count = Console.ReadLine();
            if (int.TryParse(count, out var result) && result > 0) break;
            Console.WriteLine("Invalid count. Please try again.");
        }
        var students = _sm.GetTopStudents(int.Parse(count));
        foreach (var student in students)
        {
            Console.WriteLine(student.ToString());
        }
    }
    public void Run()
    {
        while (true)
        {
            Menu();
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddStudent();
                    break;
                case "2":
                    RemoveStudent();
                    break;
                case "3":
                    EditStudent();
                    break;
                case "4":
                    GetStudentsByID();
                    break;
                case "5":
                    GetStudentsByName();
                    break;
                case "6":
                    GetTopStudents();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }
    }
}