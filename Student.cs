namespace project1;

class Student
{
    // ATTRIBUTS ELEVES
    private int _id;
    private string _firstName;
    private string _lastName;
    private string[] _dateBirth;


    // METHODES
    public Student(int id, string firstName, string lastName, string[] dateBirth)
    {
        _id = id;
        _firstName = firstName;
        _lastName = lastName;
        _dateBirth = dateBirth;
    }

    public string [] PassStudentInfo()
    {
        string[] studentInfo = new string[2];
        
        studentInfo = new string[4]
        {
            _id.ToString(),
            _firstName,
            _lastName,
            String.Join("/", _dateBirth)
        };

        return studentInfo;
    }

    // public void AddGrade(string grade, string comment){
    //     string[][] temp = new string[_reportCard.GetLength(0)+1][];
    //     _reportCard.CopyTo(temp, 0);
    //     temp[temp.GetLength(0)] = new string[] 
    //     {
    //         grade,
    //         comment
    //     };
    //     _reportCard = temp;
    //     // UpdateJSON(_id, _reportCard);
    // }
}
