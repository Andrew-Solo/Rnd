namespace Rnd.Api.Client.Exceptions;

public class NotReadyException : Exception
{
    public NotReadyException() : base("Client is not ready") { }
}