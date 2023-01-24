namespace Rnd.Result;

public class Result<T>
{
    protected Result(Status status, Message? message = null, T? value = default)
    {
        Status = status;
        Message = message ?? new Message();
        Value = value!;
    }

    public static Result<T> Empty(Message? message = null)
    {
        return new Result<T>(Status.Empty, message);
    }
    
    public static Result<T> Ok(T value, Message? message)
    {
        return new Result<T>(Status.Ok, message, value);
    }
    
    public static Result<T> Ok(T value, string? message = null)
    {
        return Ok(value, message != null ? new Message(message) : null);
    }
    
    public static Result<T> Error(Message errors)
    {
        return new Result<T>(Status.Error, errors);
    }
    
    public static Result<T> Error(string message)
    {
        return new Result<T>(Status.Error, new Message(message));
    }
    
    public T Value { get; }
    public Message Message { get; }
    
    public Status Status { get; }
        
    public bool IsSuccess => Status != Status.Error;
    public bool IsFailed => Status == Status.Error;
}