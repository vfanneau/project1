namespace project1;

public class Menu
{
    public string title {get;}
    public List<string> choices {get;}

    public Menu(string title, List<string> choices)
    {
        this.title = title;
        this.choices = choices;
    }
}