namespace project1;

class Student
{
    // ATTRIBUTS ELEVES
    private string _id;
    private string _firstName;
    private string _lastName;
    private string[] _dateBirth;
    private string[][] _reportCard;


    // METHODES
    public Student(string id, string firstName, string lastName, string[] dateBirth)
    {
        _id = id;
        _firstName = firstName;
        _lastName = lastName;
        _dateBirth = dateBirth;
        _reportCard = new string[0][];
    }

    public string [][][] PassStudentInfo()
    {
        string[][][] studentInfo = new string[2][][];
        
        studentInfo[0] = new string[4][]    // INFOS PERSO
        {
            new string[] {_id.ToString()},
            new string[] {_firstName},
            new string[] {_lastName},
            _dateBirth
        };
        
        studentInfo[1] = _reportCard;   // NOTES & APPRECIATIONS

        return studentInfo;
    }

    public void AddGrade(string grade, string comment){
        string[][] temp = new string[_reportCard.GetLength(0)+1][];
        _reportCard.CopyTo(temp, 0);
        temp[temp.GetLength(0)] = new string[] 
        {
            grade,
            comment
        };
        _reportCard = temp;
        // UpdateJSON(_id, _reportCard);
    }
}
