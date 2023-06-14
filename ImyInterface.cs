namespace project1;

public interface ImyInterface
{
    void Display();

    string[] PromptInfo(int id);

    IEnumerable<DataPrototype> AddNew(int id, string[] info, IEnumerable<DataPrototype> allItems);
}