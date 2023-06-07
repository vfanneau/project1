namespace project1;

class Student
{
    // ATTRIBUTS ELEVES
    private int _id;
    private string _firstName;
    private string _lastName;
    private int[] _dateBirth = new int[3];
    private string[] _reportCard = new string[3];
    private int _averageAllCourses;

    // PROFILE GENERATING DATA
    private string[] _firstNames = 
    {"Églantine", "Josiane", "Aurèle", "Rémi", "Yoan", "René", "Flore", "Edmond", "Noël", "Léopold", "Théo", "Sasha", "Robin", "Zacharie", "Christèle", "Célia", "Ariel", "Emmanuel", "Martin", "Jean", "Paul", "Martine", "Lucie"};
    private string[] _lastnames = 
    {"Tremblay", "Dupont", "Dubois", "Boucher", "Pelletier", "Faure", "Favre", "Beaulieu", "Planche", "Gagnon", "Roussel", "Masson", "Desrosiers", "Lachance", "Cloutier", "Comtois"};

    // METHODES
    public Student(){}

    public string [] PassStudentInfo(){}

    public void AddGrade(){}
}
