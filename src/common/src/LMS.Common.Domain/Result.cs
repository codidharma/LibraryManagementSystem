using System.Diagnostics.CodeAnalysis;

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

    public static Result<T> Success<T>(T value)
        => new(value, true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result<T> Failure<T>(Error error)
        => new(default, false, error);
}

public class Result<T> : Result
{
    private readonly T? _value;

    public Result(T? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    [NotNull]
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The property Value cannot be access when IsFailure is true.");
}
