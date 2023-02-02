namespace Control.BLL.Exceptions;

public sealed class InvalidValueException : Exception
{
    public InvalidValueException(string message) : base(message) { }
}