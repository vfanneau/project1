namespace project1;

class Student
{
    // ATTRIBUTS ELEVES
    private int _id;
    private string _firstName;
    private string _lastName;
    private string _dateBirth;


    // METHODES
    public Student(int id, string firstName, string lastName, string dateBirth)
    {
        _id = id;
        _firstName = firstName;
        _lastName = lastName;
        _dateBirth = dateBirth;
    }

    public void Display()
    {
        Console.WriteLine("Identifiant : " + _id);
        Console.WriteLine("Prénom : " + _firstName);
        Console.WriteLine("Nom : " + _lastName);
        Console.WriteLine("");
    }
}
