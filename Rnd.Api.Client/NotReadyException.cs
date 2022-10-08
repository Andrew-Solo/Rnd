namespace Rnd.Api.Client;

public class NotReadyException : Exception
{
    public NotReadyException() : base("Client is not ready") { }
}