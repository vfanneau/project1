﻿namespace project1;

using Newtonsoft.Json;

class Program
{
    static void Main(string[] args)
    {
        string filepath = "";
        Database database;
        if(args.Length == 0)
        {
            filepath = "data.json";
        }
        if(!File.Exists(filepath))
        {
            var jsonFile = File.CreateText(filepath);
            jsonFile.Close();
            database = new Database();
        }
        else
        {
            database = GetFromJson(filepath);
        }
        List<Student> allStudents = new List<Student>();
        List<Course> allCourses = new List<Course>();
        List<Grade> allGrades = new List<Grade>();
        foreach(Student one in database.students)
        {
            allStudents.Add(one);
        }
        foreach(Course one in database.courses)
        {
            allCourses.Add(one);
        }
        foreach(Grade one in database.grades)
        {
            AddGrade(one.id, new string[] {one.student.id.ToString(), one.course.id.ToString(), one.grade.ToString(), one.comment}, allGrades, allStudents, allCourses);
        }
        
        Console.Clear();
        string input;
        Student student;
        Course course;
        string navigation = "main";
        string[] logJournal = new string[0];
        var logFile = File.CreateText("log_file.txt");    // CLEAR HISTORY
        logFile.Close();
        
        Console.WriteLine("Bienvenue sur le campus");
        
        while(true)
        {
            switch(navigation)
            {
            // MENU PRINCIPAL
                case "main":
                    Console.WriteLine("MENU PRINCIPAL");
                    Console.WriteLine("1 --> Eleves");
                    Console.WriteLine("2 --> Cours");
                    Console.WriteLine("3 --> Fermer");
                    Log("Menu principal");
                    input = Console.ReadLine();
                    navigation = MenuMain(input);
                    break;

            // MENU ELEVES
                case "students":
                    Console.WriteLine("MENU ELEVES");
                    Console.WriteLine("1 --> Liste des eleves");
                    Console.WriteLine("2 --> Ajouter un eleve");
                    Console.WriteLine("3 --> Acceder aux informations d'un eleve");
                    Console.WriteLine("4 --> Ajouter une note a un eleve");
                    Console.WriteLine("5 --> Retour au menu principal");
                    Log("Menu eleves");
                    input = Console.ReadLine();
                    navigation = MenuStudents(input);
                    break;

            // MENU COURS
                case "courses":
                    Console.WriteLine("MENU COURS");
                    Console.WriteLine("1 --> Liste des cours");
                    Console.WriteLine("2 --> Ajouter un cours au programme");
                    Console.WriteLine("3 --> Supprimer un cours du programme");
                    Console.WriteLine("4 --> Retour au menu principal");
                    Log("Menu cours");
                    input = Console.ReadLine();
                    navigation = MenuCourses(input);
                    break;

            // LISTE DES ETUDIANTS
                case "students_list":
                    Console.WriteLine("LISTE DES ETUDIANTS");
                    Log("Consultation de la liste des etudiants");
                    
                    if(allStudents.Count != 0)
                    {
                        foreach(Student one in allStudents)
                        {
                            one.Display();
                            Console.WriteLine("");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Liste vide");
                        Console.WriteLine("");
                    }
                    Console.Write("Entree pour revenir au menu :");
                    Console.ReadLine();
                    navigation = "students";
                    break;

            // AJOUTER UN ETUDIANT
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
                    studentInfo = PromptStudentInfo();
                    Log("Ajout d'un nouvel etudiant #" + idStudent + " " + studentInfo[0] + " " + studentInfo[1]);
                    allStudents = AddStudent(idStudent, studentInfo, allStudents);
                    database.students = allStudents;
                    foreach(Student one in database.students)
                    {
                        one.reportCard = new List<Grade>();
                    }
                    GetToJson(filepath, database);
                    foreach(Grade one in allGrades)
                    {
                        allStudents[one.student.id].reportCard.Add(one);
                    }
                    navigation = "students";
                    break;

            // CONSULTER LE DOSSIER D'UN ELEVE
                case "students_studentInfo":
                    Console.WriteLine("CONSULTER LE DOSSIER D'UN ELEVE");
                    Console.Write("Identifiant de l'étudiant a examiner : ");
                    input = Console.ReadLine();
                    if(Int32.TryParse(input, out idStudent))
                    {
                        int index = GetIndexFromId(allStudents, idStudent);
                        if(index != -1)
                        {
                            student = allStudents[index];
                            student.Display();
                            Console.WriteLine("Date de naissance : " + student.dateBirth);
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
                            Log("Consultation du profil étudiant #" + idStudent);
                        }
                        else
                        {
                            Console.WriteLine("Erreur : Identifiant inexistant dans la base de donnees # " + input);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Erreur : Saisie non reconnue");
                    }
                    Console.Write("Entree pour revenir au menu :");
                    Console.ReadLine();
                    navigation = "students";
                    break;

            // AJOUTER UNE NOTE A UN ETUDIANT
                case "students_grade":
                    Console.WriteLine("AJOUTER UNE NOTE A ETUDIANT");
                    string[] gradeInfo = new string[4];
                    int idGrade;
                    if(allGrades.Count == 0)
                    {
                        idGrade = 0;
                    }
                    else
                    {
                        idGrade = allGrades[allGrades.Count-1].id + 1;    // LAST ID + 1
                    }
                    gradeInfo = PromptGradeInfo(allStudents, allCourses);
                    student = allStudents[GetIndexFromId(allStudents, Int32.Parse(gradeInfo[0]))];
                    course = allCourses[GetIndexFromId(allCourses, Int32.Parse(gradeInfo[1]))];
                    Log("Ajout d'une nouvelle note pour l'etudiant " + student.firstName + " " + student.lastName + " en " + course.name + " : " + gradeInfo[2] + "/20 ; " + gradeInfo[3]);
                    allGrades = AddGrade(idGrade, gradeInfo, allGrades, allStudents, allCourses);
                    database.grades = allGrades;
                    database.students = allStudents;
                    foreach(Student one in database.students)
                    {
                        one.reportCard = new List<Grade>();
                    }
                    GetToJson(filepath, database);
                    foreach(Grade one in allGrades)
                    {
                        allStudents[one.student.id].reportCard.Add(one);
                    }
                    navigation = "students";
                    break;

            // LISTE DES COURS
                case "courses_list":
                    Console.WriteLine("LISTE DES COURS");
                    Log("Consultation de la liste des cours");
                    
                    if(allCourses.Count != 0)
                    {
                        foreach(Course one in allCourses)
                        {
                            one.Display();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Liste vide");
                        Console.WriteLine("");
                    }
                    Console.Write("Entree pour revenir au menu :");
                    Console.ReadLine();
                    navigation = "courses";
                    break;

            // AJOUTER UN COURS
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
                    string name = PromptCourseInfo();
                    Log("Ajout d'un nouveau cours #" + idNewCourse + " " + name);
                    allCourses = AddCourse(idNewCourse, name, allCourses);
                    database.courses = allCourses;
                    foreach(Student one in database.students)
                    {
                        one.reportCard = new List<Grade>();
                    }
                    GetToJson(filepath, database);
                    foreach(Grade one in allGrades)
                    {
                        allStudents[one.student.id].reportCard.Add(one);
                    }
                    navigation = "courses";
                    break;

            // AJOUTER UN COURS
                case "courses_remove":
                    Console.WriteLine("SUPPRIMER UN COURS");
                    Console.Write("Identifiant du cours a supprimer : ");
                    input = Console.ReadLine();
                    int idCourse;
                    if(Int32.TryParse(input, out idCourse))
                    {
                        int index = GetIndexFromId(allCourses, idCourse);
                        if(index != -1)
                        {
                            course = allCourses[index];
                            allCourses.Remove(course);
                            int removals= 0;
                            foreach(Student eachStudent in allStudents)
                            {
                                for(int i = eachStudent.reportCard.Count() - 1; i >= 0; i--)
                                {
                                    if(eachStudent.reportCard[i].course == course)
                                    {
                                        allGrades.Remove(eachStudent.reportCard[i]);
                                        eachStudent.reportCard.Remove(eachStudent.reportCard[i]);
                                        removals++;
                                    }
                                }
                            }
                            Console.WriteLine("");
                            Console.WriteLine(removals+ " notes supprimees");
                            Log("Suppression du cours " + course.name + " et de " + removals+ " notes");
                            database.grades = allGrades; 
                            database.courses = allCourses;
                            database.students = allStudents;
                            foreach(Student one in database.students)
                            {
                                one.reportCard = new List<Grade>();
                            }
                            Console.WriteLine(allCourses.Count());
                            GetToJson(filepath, database);
                            foreach(Grade one in allGrades)
                            {
                                allStudents[one.student.id].reportCard.Add(one);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Erreur : Identifiant inexistant dans la base de donnees # " + input);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Erreur : Saisie non reconnue");
                    }
                    Console.ReadLine();
                    navigation = "courses";
                    break;
            }

            Console.Clear();
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

            case "3":
                Log("Sortie du programme par la bonne porte");
                Environment.Exit(0);
                return "fini";

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

            // STUDENT FUNCTIONS

    static string[] PromptStudentInfo()
    {
        string inputFirstName = "";
        string inputLastName = "";
        string inputBirthDay = "0";
        string inputBirthMonth = "0";
        string inputBirthYear = "0";

        while(inputFirstName == "")
        {
            Console.Write("Prenom : ");
            inputFirstName = Console.ReadLine();
            if(inputFirstName == "")
            {
                Console.WriteLine("Erreur : Saisie non reconnue");
            }
        }

        while(inputLastName == "")
        {
            Console.Write("Nom : ");
            inputLastName = Console.ReadLine();
            if(inputLastName == "")
            {
                Console.WriteLine("Erreur : Saisie non reconnue");
            }
        }

        Console.WriteLine("Date de naissance : ");

        while(inputBirthDay == "0")
        {
            Console.Write("Jour : ");
            inputBirthDay = Console.ReadLine();
            int birthDay;
            if(Int32.TryParse(inputBirthDay, out birthDay))
            {
                if(inputBirthDay == "" || birthDay < 1 || birthDay > 31)
                {
                    Console.WriteLine("Erreur : Saisie non reconnue");
                    inputBirthDay = "0";
                }
                else if(birthDay < 10)
                {
                    inputBirthDay = "0" + inputBirthDay;
                }
            }
            else
            {
                Console.WriteLine("Erreur : Saisie non reconnue");
                inputBirthDay = "0";
            }
        }

        while(inputBirthMonth == "0")
        {
            Console.Write("Mois : ");
            int birthMonth;
            inputBirthMonth = Console.ReadLine();
            if(Int32.TryParse(inputBirthMonth, out birthMonth))
            {
                if(inputBirthMonth == "" || birthMonth < 1 || birthMonth > 12)
                {
                    Console.WriteLine("Erreur : Saisie non reconnue");
                    inputBirthMonth = "0";
                }
                else if(birthMonth < 10)
                {
                    inputBirthMonth = "0" + inputBirthMonth;
                }

            }
            else
            {
                Console.WriteLine("Erreur : Saisie non reconnue");
                inputBirthMonth = "0";
            }
        }

        while(inputBirthYear == "0")
        {
            Console.Write("Annee : ");
            inputBirthYear = Console.ReadLine();
            int birthYear;
            if(Int32.TryParse(inputBirthYear, out birthYear))
            {
                if(inputBirthYear == "" || birthYear < 1900 || birthYear > 2022)
                {
                    Console.WriteLine("Erreur : Saisie non reconnue");
                    inputBirthYear = "0";
                }

            }
        }

        string birthDate = inputBirthDay + "/" +  inputBirthMonth + "/" + inputBirthYear;
        string[] studentInfo = {inputFirstName, inputLastName, birthDate};
        return studentInfo;
    }

    static List<Student> AddStudent(int id, string[] studentInfo, List<Student> allStudents)
    {
        Student newGuy = new Student(id, studentInfo[0], studentInfo[1], studentInfo[2]);
        allStudents.Add(newGuy);
        return allStudents;
    }

            // STUDENT FUNCTIONS

    static string PromptCourseInfo()
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

        return inputName;
    }

    static List<Course> AddCourse(int id, string name, List<Course> allCourses)
    {
        Course newGuy = new Course(id, name);
        allCourses.Add(newGuy);
        return allCourses;
    }

            // GRADE FUNCTIONS
    
    static string[] PromptGradeInfo(List<Student> allStudents, List<Course> allCourses)
    {
        string inputIdStudent = "";
        string inputIdCourse = "";
        string inputGradeValue = "";
        string inputGradeComment = "";

        while(inputIdStudent == "")
        {
            Console.Write("Identifiant de l'etudiant a noter : ");
            inputIdStudent = Console.ReadLine();
            int inputIdStudentInt;
            if(Int32.TryParse(inputIdStudent, out inputIdStudentInt))
            {
                int index = GetIndexFromId(allStudents, inputIdStudentInt);
                if(index == -1)
                {
                    Console.WriteLine("Erreur : Identifiant inexistant dans la base de donnees # " + inputIdStudent);
                    inputIdStudent = "";
                }
            }
            else
            {
                    Console.WriteLine("Erreur : Saisie non reconnue");
                    inputIdStudent = "";
            }
        }

        while(inputIdCourse == "")
        {
            Console.Write("Identifiant de la matiere : ");
            inputIdCourse = Console.ReadLine();
            int inputIdCourseInt;
            if(Int32.TryParse(inputIdCourse, out inputIdCourseInt))
            {
                int index = GetIndexFromId(allCourses, inputIdCourseInt);
                if(index == -1)
                {
                    Console.WriteLine("Erreur : Identifiant inexistant dans la base de donnees # " + inputIdCourse);
                    inputIdCourse = "";
                }
            }
            else
            {
                    Console.WriteLine("Erreur : Saisie non reconnue");
                    inputIdStudent = "";
            }
        }

        while(inputGradeValue == "")
        {
            Console.Write("Note sur 20 : ");
            inputGradeValue = Console.ReadLine();
            int gradeValue;
            if(Int32.TryParse(inputGradeValue, out gradeValue))
            {
                if(inputGradeValue == "" || gradeValue < 0 || gradeValue > 20)
                {
                    Console.WriteLine("Erreur : Saisie non reconnue");
                    inputGradeValue = "";
                }
                else if(gradeValue < 10)
                {
                    inputGradeValue = "0" + gradeValue.ToString();
                }
            }
            else
            {
                Console.WriteLine("Erreur : Saisie non reconnue");
                inputGradeValue = "";
            }
        }

        Console.Write("Appreciation (peut etre vide) : ");
        inputGradeComment = Console.ReadLine();

        return new string[] {inputIdStudent, inputIdCourse, inputGradeValue, inputGradeComment};
    }

    static List<Grade> AddGrade(int id, string[] gradeInfo, List<Grade> allGrades, List<Student> allStudents, List<Course> allCourses)
    {
        Student student = allStudents[GetIndexFromId(allStudents, Int32.Parse(gradeInfo[0]))];
        Course course = allCourses[GetIndexFromId(allCourses, Int32.Parse(gradeInfo[1]))];
        Grade newGrade = new Grade(id, student, course, Int32.Parse(gradeInfo[2]), gradeInfo[3]);
        student.reportCard.Add(newGrade);
        allGrades.Add(newGrade);
        return allGrades;
    }

            // JSON FUNCTIONS

    static Database GetFromJson(string filepath)
    {
        Log("Récupération des donnees du fichier " + filepath);
        string jsonText = File.ReadAllText(filepath);
        Database database = JsonConvert.DeserializeObject<Database>(jsonText);
        return database;
    }

    static void GetToJson(string filepath, Database database)
    {
        Log("Ecriture des donnees du fichier " + filepath);
        string jsonText = JsonConvert.SerializeObject(database);
        File.WriteAllText(filepath, jsonText);
    }

}
