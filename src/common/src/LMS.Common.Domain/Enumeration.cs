using System.Reflection;
namespace LMS.Common.Domain;

public abstract record Enumeration
{
    public int Id { get; }
    public string Name { get; }

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration => typeof(T)
        .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
        .Select(field => field.GetValue(null))
        .Cast<T>();

    public static T FromId<T>(int id) where T : Enumeration
    {
        T? valueFound = GetAll<T>().FirstOrDefault(t => t.Id == id);

        if (valueFound is null)
        {
            throw new InvalidOperationException($"Value of Id {id} for type {typeof(T)} is invalid.");
        }
        return valueFound;
    }

    public static T FromName<T>(string name) where T : Enumeration
    {
        T? valueFound = GetAll<T>().FirstOrDefault(t => t.Name.Equals(name, StringComparison.Ordinal));

        if (valueFound is null)
        {
            throw new InvalidOperationException($"Value of Name {name} for type {typeof(T)} is invalid.");
        }
        return valueFound;
    }
}
