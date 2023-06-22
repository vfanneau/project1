namespace project1;

public class Course : DataPrototype
{
	public string name {get; set;}

	public Course(int id, string name)
	{
		this.id = id;
		this.name = name;
	}

	public override void Display()
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
