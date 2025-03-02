namespace LMS.Modules.IAM.Domain.Users;

public sealed record Role
{
    public string Name { get; }

    public Role(string name)
    {
        Name = name;
    }

    public static Role Librarian = new("Librarian");
    public static Role RegularPatron = new("Regular Patron");
    public static Role ResearchPatron = new("Research Patron");

}
