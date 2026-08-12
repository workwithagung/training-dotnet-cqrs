namespace UserManagement.Domain.Shared;

public record Error(string Code, string Message, ErrorType Type)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.General);
    public static Error General(string code, string message) => new(code, message, ErrorType.General);
    public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);
    public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);
    public static Error Validation(string code, string message) => new(code, message, ErrorType.Validation);
}

public record ValidationError<T>(string Code, string Message, T Errors): Error(Code, Message, ErrorType.Validation);