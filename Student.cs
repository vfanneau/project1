namespace project1;

class Student
{
    // ATTRIBUTS ELEVES
    public int id {get; set;}
    public string firstName {get; set;}
    public string lastName {get; set;}
    public string dateBirth {get; set;}


    // METHODES
    public Student(int id, string firstName, string lastName, string dateBirth)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.dateBirth = dateBirth;
    }

    public void Display()
    {
        Console.WriteLine("Identifiant : " + id);
        Console.WriteLine("Prénom : " + firstName);
        Console.WriteLine("Nom : " + lastName);
        Console.WriteLine("");
    }
}
