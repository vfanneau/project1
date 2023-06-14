namespace project1;

class Program
{
    static void Main(string[] args)
    {
        Student fakeStudent = new Student(-1, "John", "Doe", "76/54/3210");
        Course fakeCourse = new Course(-1, "tournage de pouces");
        Grade fakeGrade = new Grade(-1, fakeStudent, fakeCourse, 20, "Excellent travail, on a jamais vu élève aussi doué pour cette matière !");
        List<Student> allStudents = new List<Student>();
        List<Course> allCourses = new List<Course>();
        List<Grade> allGrades = new List<Grade>();
        string input;
        string navigation = "main";
        string[] logJournal = new string[0];
        var logFile = File.CreateText("log_file.txt");    // CLEAR HISTORY
        logFile.Close();
        
        Console.WriteLine("Bienvenue sur le campus");
        Console.WriteLine("Version off-line, no initial data, no data storage");
        
        while(true)
        {
            switch(navigation)
            {
                case "main":
                    Console.WriteLine("MENU PRINCIPAL");
                    Console.WriteLine("1 --> Eleves");
                    Console.WriteLine("2 --> Cours");
                    input = Console.ReadLine();
                    navigation = MenuMain(input);
                    break;

                case "students":
                    Console.WriteLine("MENU ELEVES");
                    Console.WriteLine("1 --> Liste des eleves");
                    Console.WriteLine("2 --> Ajouter un eleve");
                    Console.WriteLine("3 --> Acceder aux informations d'un eleve");
                    Console.WriteLine("4 --> Ajouter une note a un eleve");
                    Console.WriteLine("5 --> Retour au menu principal");
                    input = Console.ReadLine();
                    navigation = MenuStudents(input);
                    break;

                case "courses":
                    Console.WriteLine("MENU COURS");
                    Console.WriteLine("1 --> Liste des cours");
                    Console.WriteLine("2 --> Ajouter un cours au programme");
                    Console.WriteLine("3 --> Supprimer un cours du programme");
                    Console.WriteLine("4 --> Retour au menu principal");
                    input = Console.ReadLine();
                    navigation = MenuCourses(input);
                    break;

                case "students_list":
                    Console.WriteLine("LISTE DES ETUDIANTS");
                    Log("Consultation de la liste des etudiants");
                    
                    if(allStudents.Count != 0)
                    {
                        foreach(Student student in allStudents)
                        {
                            student.Display();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Liste vide");
                        Console.WriteLine("");
                    }

                    navigation = "students";
                    break;

                case "students_add":
                    Console.WriteLine("AJOUTER UN ETUDIANT");
                    string[] studentInfo = new string[4];
                    int idStudent;
                    if(allStudents.Count == 0)
                    {
                        idStudent = 0;
                    }
                    else
                    {
                        idStudent = allStudents[allStudents.Count-1].id + 1;    // LAST ID + 1
                    }
                    studentInfo = fakeStudent.PromptInfo(idStudent);
                    Log("Ajout d'un nouvel etudiant #" + idStudent + " " + studentInfo[0] + " " + studentInfo[1]);
                    allStudents = fakeStudent.AddNew(idStudent, studentInfo, allStudents);
                    navigation = "students";
                    break;

                case "students_studentInfo":
                    Console.WriteLine("CONSULTER LE DOSSIER D'UN ELEVE");
                    Console.Write("Identifiant de l'étudiant a examiner : ");
                    input = Console.ReadLine();
                    if(Int32.TryParse(input, out idStudent))
                    {
                        int index = GetIndexFromId(allStudents, idStudent);
                        if(index != -1)
                        {
                            Student student = allStudents[index];
                            student.Display();
                            Log("Consultation du profil étudiant #" + idStudent);
                        }
                        else
                        {
                            Console.WriteLine("Erreur : Identifiant inexistant dans la base de donnees # " + input);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Erreur : Ceci n'est pas un nombre entier" + idStudent);
                    }
                    navigation = "students";
                    break;

                case "courses_list":
                    Console.WriteLine("LISTE DES COURS");
                    Log("Consultation de la liste des cours");
                    
                    if(allCourses.Count != 0)
                    {
                        foreach(Course course in allCourses)
                        {
                            course.Display();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Liste vide");
                        Console.WriteLine("");
                    }

                    navigation = "courses";
                    break;

                case "courses_add":
                    Console.WriteLine("AJOUTER UN COURS");
                    int idNewCourse;
                    if(allCourses.Count == 0)
                    {
                        idNewCourse = 0;
                    }
                    else
                    {
                        idNewCourse = allCourses[allCourses.Count-1].id + 1;    // LAST ID + 1
                    }
                    string[] name = fakeCourse.PromptInfo(idNewCourse);
                    Log("Ajout d'un nouveau cours #" + idNewCourse + " " + name[0]);
                    allCourses = fakeCourse.AddNew(idNewCourse, name, allCourses);
                    navigation = "courses";
                    break;
            }

            Console.WriteLine("");
        }
    }


    static void Log(string message)
    {
        string timestamp = DateTime.Now.ToString();
        string[] newLine = new string[1] {timestamp + " -- " + message};
        File.AppendAllLines("log_file.txt", newLine);
    }

    static int GetIndexFromId(IEnumerable<DataPrototype> list, int idGoal)
    {
        int i = 0;
        foreach(DataPrototype item in list)
        {
            if(item.id == idGoal)
            {return i;}
            else
            {i++;}
        }
        return -1;
    }

            // MENU NAVIGATION FUNCTIONS

    static string MenuMain(string input)
    {
        switch(input)
        {
            case "1":
                return "students";

            case "2":
                return "courses";

            default:
                Console.WriteLine("Erreur : Saisie non reconnue");
                return "main";
        }
    }

    static string MenuStudents(string input)
    {
        switch(input)
        {
            case "1":
                return "students_list";

            case "2":
                return "students_add";

            case "3":
                return "students_studentInfo";

            case "4":
                return "students_grade";

            case "5":
                return "main";

            default:
                Console.WriteLine("Erreur : Saisie non reconnue");
                return "students";
        }
    }

    static string MenuCourses(string input)
    {
        switch(input)
        {
            case "1":
                return "courses_list";

            case "2":
                return "courses_add";

            case "3":
                return "courses_remove";

            case "4":
                return "main";

            default:
                Console.WriteLine("Erreur : Saisie non reconnue");
                return "courses";
        }
    }

}
