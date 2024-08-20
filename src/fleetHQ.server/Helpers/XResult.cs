namespace FleetHQ.Server.Helpers;

public struct XResult
{
    public XResult
       (bool isSuccess, string? message = default, object? data = default, Error? error = default)
    {
        if (isSuccess && error != null || !isSuccess && error == null)
        {
            throw new ArgumentException("invalid result");
        };

        IsSuccess = isSuccess;
        Message = message;
        Data = data;
        Error = error;
    }


    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }
    public Error? Error { get; set; }

    public static XResult Success<T>(string message, T data)
    => new(isSuccess: true, message: message, data: data);

    public static XResult Success<T>(T data)
     => new(isSuccess: true, data: data);

    public static XResult Success(string message)
    => new(isSuccess: true, message: message);

    public static XResult Failure(List<string> errors) => new(isSuccess: false, error: new()
    {
        Code = 400,
        Messages = [.. errors]
    });

    public static XResult Exception(string exception) => new(isSuccess: false, error: new()
    {
        Code = 500,
        Messages = [exception]
    });
}

public record Error
{
    public int Code { get; set; }
    public List<string> Messages { get; set; } = [];
}

