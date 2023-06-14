namespace project1;

class Grade : DataPrototype
{
	public int idStudent {get; set;}
	public int idCourse {get; set;}
	public int grade {get; set;}
	public string comment {get; set;}

	public Grade(int id, int idStudent, int idCourse, int grade, string comment)
	{
		this.id = id;
		this.idStudent = idStudent;
		this.idCourse = idCourse;
		this.grade = grade;
		this.comment = comment;
	}

}
