namespace FleetHQ.Server.Helpers;

public interface IXResult
{
    public bool IsSuccess { get; set; }
}

public struct Success<T> : IXResult
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
}

public struct Error : IXResult
{
    public bool IsSuccess { get; set; }
    public int Code { get; set; }
    public List<string> Messages { get; set; }
}

public struct XResult
{
    public static Success<T> Ok<T>(T data, string message)
    {
        return new Success<T>()
        {
            IsSuccess = true,
            Data = data,
            Message = message
        };
    }
    public static Success<T> Ok<T>(T data)
    {
        return new Success<T>()
        {
            IsSuccess = true,
            Data = data,
        };
    }

    public static Success<T> Ok<T>(string message)
    {
        return new Success<T>()
        {
            IsSuccess = true,
            Message = message
        };
    }

    public static Error Fail(List<string> messages)
    {
        return new Error()
        {
            IsSuccess = false,
            Code = 400,
            Messages = messages
        };
    }

    public static Error Exception(string message)
    {
        return new Error()
        {
            IsSuccess = false,
            Code = 500,
            Messages = [message]
        };
    }
}