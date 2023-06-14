namespace project1;

class Course
{
	public int id {get; set;}
	public string name {get; set;}

	public Course(int id, string name)
	{
		this.id = id;
		this.name = name;
	}

	public void Display()
	{
        Console.WriteLine("Identifiant : " + id);
        Console.WriteLine("Matiere : " + name);
        Console.WriteLine("");
	}

	~Course()
	{
		Console.WriteLine("Le cours " + name +" a bien été supprimé");
	}
}
