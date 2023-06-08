namespace project1;

class Program
{
    static void Main(string[] args)
    {
        string input;
        string navigation = "main";
        
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
                    switch(input)
                    {
                        case "1":
                            navigation = "students";
                            break;

                        case "2":
                            navigation = "courses";
                            break;

                        default:
                            Console.WriteLine("Erreur : Saisie non reconnue");
                            break;
                            
                    }
                    break;

                case "students":
                    Console.WriteLine("MENU ELEVES");
                    Console.WriteLine("1 --> Liste des eleves");
                    Console.WriteLine("2 --> Ajouter un eleve");
                    Console.WriteLine("3 --> Acceder aux informations d'un eleve");
                    Console.WriteLine("4 --> Ajouter une note a un eleve");
                    Console.WriteLine("5 --> Retour au menu principal");
                    input = Console.ReadLine();
                    switch(input)
                    {
                        case "1":
                            navigation = "students_list";
                            break;

                        case "2":
                            navigation = "students_add";
                            break;

                        case "3":
                            navigation = "students_studentInfo";
                            break;

                        case "4":
                            navigation = "students_grade";
                            break;

                        case "5":
                            navigation = "main";
                            break;

                        default:
                            Console.WriteLine("Erreur : Saisie non reconnue");
                            break;
                            
                    }
                    break;

                case "courses":
                    Console.WriteLine("MENU COURS");
                    Console.WriteLine("1 --> Liste des cours");
                    Console.WriteLine("2 --> Ajouter un cours au programme");
                    Console.WriteLine("3 --> Supprimer un cours du programme");
                    Console.WriteLine("4 --> Retour au menu principal");
                    input = Console.ReadLine();
                    switch(input)
                    {
                        case "1":
                            navigation = "courses_list";
                            break;

                        case "2":
                            navigation = "courses_add";
                            break;

                        case "3":
                            navigation = "courses_remove";
                            break;

                        case "4":
                            navigation = "main";
                            break;

                        default:
                            Console.WriteLine("Erreur : Saisie non reconnue");
                            break;
                            
                    }
                    break;
            }

            Console.WriteLine("");
        }
    }
}
