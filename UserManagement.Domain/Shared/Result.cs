namespace UserManagement.Domain.Shared;

public class Result<T>
{
    public bool IsSuccess { get; }
    public bool IsError => !IsSuccess;
    public T? Data { get; }
    public string Message { get; }
    
    private Result(bool isSuccess, T? data, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
        Data = data;
    }

    public static Result<T> Success(T data, string message) => new (true, data, message);
    public static Result<T> Error(string message) => new (false, default, message);

}