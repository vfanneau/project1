namespace project1;

class Course
{
	private string _id;
	private string _name;

	public Course(string id, string name)
	{
		_id = id;
		_name = name;
	}

	~Course()
	{
		Console.WriteLine("Le cours " + _name +" a bien été supprimé");
	}
}
