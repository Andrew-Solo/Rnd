namespace Rnd.Result;

public class EmptyResult : Result<object>
{
    protected EmptyResult(Status status, Message? message = null) : base(status, message, null) { }
    
    public new static EmptyResult Empty(Message? message = null)
    {
        return new EmptyResult(Status.Empty, message);
    }
    
    public new static EmptyResult Error(string error)
    {
        return new EmptyResult(Status.Error, new Message(error));
    }
    
    public new static EmptyResult Error(Message errors)
    {
        return new EmptyResult(Status.Error, errors);
    }
}