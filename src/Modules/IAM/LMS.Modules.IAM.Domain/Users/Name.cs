namespace LMS.Modules.IAM.Domain.Users;

public sealed record Name
{
    public string Value { get; private set; }
    public Name(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
        {
            string errorMessage = $"Parameters {nameof(firstName)} and {nameof(lastName)} should have non empty or whitespace values.";
            throw new InvalidNameException(errorMessage);
        }
        Value = firstName + " " + lastName;
    }
}
