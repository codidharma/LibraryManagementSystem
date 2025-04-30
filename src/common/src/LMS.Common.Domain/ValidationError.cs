namespace LMS.Common.Domain;

public sealed record ValidationError : Error
{
    public ValidationError(IEnumerable<Error> errors) : base(
        "Generic.Validation",
        "One or more validation error occured.",
        ErrorType.Validation)
    {
        Errors = errors;
    }
    public IEnumerable<Error> Errors { get; }

    public static ValidationError FromResults(IEnumerable<Result> results)
    {
        IEnumerable<Error> errors = results.Where(r => r.IsFailure).Select(er => er.Error).ToList();
        return new ValidationError(errors);
    }
}
