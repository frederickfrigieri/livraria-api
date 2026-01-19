namespace Domain.Exceptions;

public class DomainException(string code, string message) : Exception(message)
{
    public string CodeError { get; init; } = code;
}
