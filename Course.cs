namespace project1;

class Course : DataPrototype, ImyInterface
{
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

	public IEnumerable<DataPrototype> AddNew(int id, string[] name, IEnumerable<DataPrototype> allCourses)
    {
        Course newGuy = new Course(id, name[0]);
        allCourses.Add(newGuy);
        return allCourses;
    }

	public string[] PromptInfo(int id)
    {
        string inputName = "";

        while(inputName == "")
        {
            Console.Write("Intitule de la matiere : ");
            inputName = Console.ReadLine();
            if(inputName == "")
            {
                Console.WriteLine("Erreur : Saisie non reconnue");
            }
        }

        return new string[] {inputName};
    }

	~Course()
	{
		Console.WriteLine("Le cours " + name +" a bien été supprimé");
	}
}
