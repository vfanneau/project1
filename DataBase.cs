namespace project1;

public class Database
{
    public List<Student> students {get; set;}
    public List<Course> courses {get; set;}
    public List<Grade> grades {get; set;}

    public Database()
    {
        students = new List<Student>();
        courses = new List<Course>();
        grades = new List<Grade>();
    }
}