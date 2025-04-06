namespace LMS.Common.Domain;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    public Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidOperationException("ErrorType should be None for success result.");
        }
        if (!isSuccess && error == Error.None)
        {
            throw new InvalidOperationException("ErrorType should not be None for failure result.");
        }
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, Error.None);
    public static Result Failure(Error error) => new(false, error);
}
