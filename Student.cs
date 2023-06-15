namespace project1;

class Student : DataPrototype
{
    // ATTRIBUTS ELEVES
    public string firstName {get; set;}
    public string lastName {get; set;}
    public string dateBirth {get; set;}
    public List<Grade> reportCard {get; set;}


    // METHODES
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
        Console.WriteLine("");
        int sum = 0;
        int i = 0;
        foreach(Grade grade in student.reportCard)
        {
            grade.Display();
            sum += grade.grade;
            i++;
        }
        if(i > 0)
        {
            Console.WriteLine("");
            Console.WriteLine("Moyenne generale : " + (double)sum / i + "/20");
        }
    }
}
