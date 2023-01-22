using System.ComponentModel;
using System.Dynamic;

namespace Rnd.Result;

public class Result<T> where T : class
{
    protected Result(Status status, Message? message = null, T? value = null)
    {
        Status = status;
        Message = message ?? new Message();
        Value = value;
        
        _onSuccess = x => x;
    }

    public static Result<T> Ok(T value, string? message = null)
    {
        return new Result<T>(Status.Ok, new Message(message), value);
    }
    
    public static Result<T> Invalid(Message errors)
    {
        return new Result<T>(Status.Invalid, errors);
    }
    
    public static Result<T> NotFound(string? message = null)
    {
        return new Result<T>(Status.NotFound, new Message(message));
    }
    
    public T? Value { get; }
    public Message Message { get; }
    
    public Status Status { get; }
        
    public bool IsSuccess => Status == Status.Ok;
    public bool IsFailed => Status != Status.Ok;

    public Result<T> OnSuccess(Func<T, object> func)
    {
        _onSuccess = func;
        return this;
    }

    public ExpandoObject Get()
    {
        dynamic result = new ExpandoObject();

        if (IsSuccess) result.Value = _onSuccess(Value!);
        if (Message.Header != null) result.Message.Header = Message.Header;
        if (Message.General.Count > 0) result.Message.General = Message.General;
        if (Message.Properties.Count > 0) result.Message.Properties = Message.Properties;

        return result;
    }

    private Func<T, object> _onSuccess;
}