namespace Application.Exceptions;

public class ApplicationException(string code, string message) : Exception(message)
{
    public string CodeError { get; init; } = code;
}
