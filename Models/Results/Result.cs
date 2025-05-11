namespace newsletter_form_api.Models.Results
{
    public enum ErrorType
    {
        None,
        Validation,
        NotFound,
        Conflict,
        Failure
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; }
        public bool IsFailure => !IsSuccess;
        public ErrorType ErrorType { get; }

        protected Result(bool isSuccess, string error, ErrorType errorType = ErrorType.None)
        {
            IsSuccess = isSuccess;
            Error = error;
            ErrorType = errorType;
        }

        public static Result Success() => new(true, string.Empty);
        public static Result Failure(string error) => new(false, error, ErrorType.Failure);
        public static Result ValidationError(string error) => new(false, error, ErrorType.Validation);
        public static Result NotFound(string error) => new(false, error, ErrorType.NotFound);
        public static Result Conflict(string error) => new(false, error, ErrorType.Conflict);

        public static Result<T> Success<T>(T value) => Result<T>.Success(value);
        public static Result<T> Failure<T>(string error) => Result<T>.Failure(error);
        public static Result<T> ValidationError<T>(string error) => Result<T>.ValidationError(error);
        public static Result<T> NotFound<T>(string error) => Result<T>.NotFound(error);
        public static Result<T> Conflict<T>(string error) => Result<T>.Conflict(error);
    }

    public class Result<T> : Result
    {
        private readonly T? _value;

        public T Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Cannot access Value on failure result");

        protected internal Result(bool isSuccess, string error, T? value, ErrorType errorType = ErrorType.None)
            : base(isSuccess, error, errorType)
        {
            _value = value;
        }

        public static Result<T> Success(T value) => new(true, string.Empty, value);
        public static new Result<T> Failure(string error) => new(false, error, default, ErrorType.Failure);
        public static new Result<T> ValidationError(string error) => new(false, error, default, ErrorType.Validation);
        public static new Result<T> NotFound(string error) => new(false, error, default, ErrorType.NotFound);
        public static new Result<T> Conflict(string error) => new(false, error, default, ErrorType.Conflict);
    }
}