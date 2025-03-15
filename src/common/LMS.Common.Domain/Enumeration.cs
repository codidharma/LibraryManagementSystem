using System.Reflection;
namespace LMS.Common.Domain;

public abstract class Enumeration : IComparable
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

    public int CompareTo(object? obj)
    {
        ArgumentNullException.ThrowIfNull($"Argument {nameof(obj)} can not be null.");

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return Id.CompareTo(((Enumeration)obj).Id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

    }

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    {
        if (obj is not Enumeration otherValue)
        {
            return false;
        }

        bool doesTypeMatch = GetType().Equals(obj.GetType());
        bool doesIdMatch = Id.Equals(otherValue.Id);

        return doesTypeMatch && doesIdMatch;

    }

    public override int GetHashCode() => Id.GetHashCode();


    public static bool operator ==(Enumeration left, Enumeration right) => left.Equals(right);

    public static bool operator !=(Enumeration left, Enumeration right) => !(left == right);

    public static bool operator <(Enumeration left, Enumeration right) => left.Id < right.Id;

    public static bool operator <=(Enumeration left, Enumeration right) => left.Id <= right.Id;

    public static bool operator >(Enumeration left, Enumeration right) => !(left < right);

    public static bool operator >=(Enumeration left, Enumeration right) => !(left <= right);
}
