namespace StudentGradeManager;

public class Grade(int credit, int score)
{
    public int Credit { get; set; } = credit;
    public int Score { get; set; } = score;
    public double GradePoint => Score switch
    {
        >= 90 => 4.0,
        >= 85 => 3.6,
        >= 80 => 3.3,
        >= 77 => 3.0,
        >= 73 => 2.6,
        >= 70 => 2.3,
        >= 67 => 2.0,
        >= 63 => 1.6,
        >= 60 => 1.3,
        _ => 0.0
    };
}
