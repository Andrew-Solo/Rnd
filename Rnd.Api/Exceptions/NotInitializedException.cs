namespace Rnd.Api.Exceptions;

public class NotInitializedException : Exception
{
    public NotInitializedException(string name) : base($"Instance of class {name} not initialized.") { }
}