namespace project1;

public class Student : DataPrototype
{
    public string firstName {get; set;}
    public string lastName {get; set;}
    public string dateBirth {get; set;}
    public List<Grade> reportCard {get; set;}


    public Student(int id, string firstName, string lastName, string dateBirth)
    {
        this.id = id;
        this.firstName = firstName;
        this.lastName = lastName;
        this.dateBirth = dateBirth;
        this.reportCard = new List<Grade>();

    }

    public override void Display()
    {
        Console.WriteLine("Identifiant : " + id);
        Console.WriteLine("Prénom : " + firstName);
        Console.WriteLine("Nom : " + lastName);
    }
}
