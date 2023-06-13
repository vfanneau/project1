namespace project1;

class Course
{
	private int _id;
	private string _name;

	public Course(int id, string name)
	{
		_id = id;
		_name = name;
	}

	public void Display()
	{
        Console.WriteLine("Identifiant : " + _id);
        Console.WriteLine("Matiere : " + _name);
        Console.WriteLine("");
	}

	~Course()
	{
		Console.WriteLine("Le cours " + _name +" a bien été supprimé");
	}
}
