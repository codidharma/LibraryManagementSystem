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

    public static Result<T> FromId<T>(int id) where T : Enumeration
    {
        T? valueFound = GetAll<T>().FirstOrDefault(t => t.Id == id);

        if (valueFound is null)
        {
            Error error = Error.NotFound("Common.NotFound", $"Value of Id {id} for type {typeof(T)} was not found.");
            return Result.Failure<T>(error);
        }
        return Result.Success(valueFound);
    }

    public static Result<T> FromName<T>(string name) where T : Enumeration
    {
        T? valueFound = GetAll<T>().FirstOrDefault(t => t.Name.Equals(name, StringComparison.Ordinal));

        if (valueFound is null)
        {
            Error error = Error.NotFound("Common.NotFound", $"Value of Name {name} for type {typeof(T)} was not found.");
            return Result.Failure<T>(error);
        }
        return Result.Success(valueFound);
    }
}
