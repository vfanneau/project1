namespace project1;

class Grade : DataPrototype
{
	public Student student {get; set;}
	public Course course {get; set;}
	public int grade {get; set;}
	public string comment {get; set;}

	public Grade(int id, Student student, Course course, int grade, string comment)
	{
		this.id = id;
		this.student = student;
		this.course = course;
		this.grade = grade;
		this.comment = comment;
	}

	public void Display()
    {
        Console.WriteLine("");
        Console.WriteLine("Note : " + grade);
        Console.WriteLine("Appreciation : " + comment);
    }

}
