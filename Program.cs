﻿namespace project1;

class Program
{
    static void Main(string[] args)
    {
        string input;
        Student[] allStudents = new Student[0];
        Course[] allCourses = new Course[0];
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
                    
                    if(allStudents.Length != 0)
                    {
                        foreach(Student student in allStudents)
                        {
                            student.Display();
                        }
                    }

                    navigation = "students";
                    break;

                case "students_add":
                    Console.WriteLine("AJOUTER UN ETUDIANT");
                    string[] studentInfo = new string[4];
                    int id = allStudents.Length;
                    studentInfo = PromptStudentInfo(id);
                    Log("Ajout d'un nouvel etudiant #" + id + " " + studentInfo[0] + " " + studentInfo[1]);
                    allStudents = AddStudent(id, studentInfo, allStudents);
                    navigation = "students";
                    break;

                case "courses_list":
                    Console.WriteLine("LISTE DES COURS");
                    Log("Consultation de la liste des cours");
                    
                    if(allCourses.Length != 0)
                    {
                        foreach(Course course in allCourses)
                        {
                            course.Display();
                        }
                    }

                    navigation = "courses";
                    break;

                case "courses_add":
                    Console.WriteLine("AJOUTER UN COURS");
                    id = allCourses.Length;
                    string name = PromptCourseInfo(id);
                    Log("Ajout d'un nouvel cours #" + id + " " + name);
                    allCourses = AddCourse(id, name, allCourses);
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

            // STUDENT FUNCTIONS

    static string[] PromptStudentInfo(int id)
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

    static string PromptCourseInfo(int id)
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

    static Student[] AddStudent(int id, string[] studentInfo, Student[] allStudentsOld)
    {
        Student newGuy = new Student(id, studentInfo[0], studentInfo[1], studentInfo[2]);
        Student[] allStudentsNew = new Student[id + 1];
        allStudentsOld.CopyTo(allStudentsNew, 0);
        allStudentsNew[id] = newGuy;
        return allStudentsNew;
    }

    static Course[] AddCourse(int id, string name, Course[] allCoursesOld)
    {
        Course newGuy = new Course(id, name);
        Course[] allCoursesNew = new Course[id + 1];
        allCoursesOld.CopyTo(allCoursesNew, 0);
        allCoursesNew[id] = newGuy;
        return allCoursesNew;
    }

}
