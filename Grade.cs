namespace project1;

class Grade
{
	private int _idStudent;
	private int _idCourse;
	private int _grade;
	private string _comment;

	public Grade(int idStudent, int idCourse, int grade, string comment)
	{
		_idStudent = idStudent;
		_idCourse = idCourse;
		_grade = grade;
		_comment = comment;
	}

}
