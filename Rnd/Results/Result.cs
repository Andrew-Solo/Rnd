using Newtonsoft.Json;

namespace Rnd.Results;

public abstract class Result
{
    public static Result Empty(string title = "")
    {
        return Ok<dynamic>(null!, new Message(title));
    }
    
    public static Result<T> Ok<T>(T value, string title = "")
    {
        return Ok(value, new Message(title));
    }

    public static Result<T> Ok<T>(T value, Message message)
    {
        return new Result<T>(true, message, value);
    }

    public static Result<object> Fail(string title = "")
    {
        return Fail<dynamic>(new Message(title));
    }
    
    public static Result<object> Fail(Message message)
    {
        return Fail<dynamic>(message);
    }

    public static Result<T> Fail<T>(string title = "")
    {
        return new Result<T>(false, new Message(title));
    }
    
    public static Result<T> Fail<T>(Message message)
    {
        return new Result<T>(false, message);
    }
    
    public static Result<T> Found<T>(T? value, string successTitle = "", string failTitle = "")
    {
        return Found(value, new Message(successTitle), new Message(failTitle));
    }
    
    public static Result<T> Found<T>(T? value, Message successMessage, Message failMessage)
    {
        return value != null 
            ? Ok(value, successMessage)
            : Fail<T>(failMessage).WithStatus(Status.NotFound);
    }

    public abstract bool Success { get; }
    public bool Failed => !Success;
    public abstract Message Message { get; }
    public abstract Status Status { get; protected set; }
    public abstract Func<bool, Message, object?, dynamic> Selector { get; protected set; }
    protected abstract object? Data { get; }

    public Result<T> Cast<T>()
    {
        return new Result<T>(Success, Message, (T?) Data!).WithStatus(Status).WithSelector(Selector);
    }
    
    public dynamic Get()
    {
        return Selector(Success, Message, Data);
    }
    
    public dynamic Get(Func<bool, Message, object?, dynamic> selector)
    {
        return selector(Success, Message, Data);
    }
    
    public override string ToString()
    {
        return JsonConvert.SerializeObject(Get());
    }
}

public sealed class Result<T> : Result
{
    internal Result(bool success, Message message, T? value = default)
    {
        Success = success;
        Message = message;
        Value = value!;
        Selector = SelectDefault;
        Status = Success ? Status.Ok : Status.Error;
    }

    public static dynamic SelectDefault(bool success, Message message, object? data)
    {
        return new {Success = success, Message = message, Data = data};
    }

    public Result<T> WithSelector(Func<bool, Message, object?, dynamic> selector)
    {
        Selector = selector;
        return this;
    }
    
    public Result<T> WithStatus(Status status)
    {
        Status = status;
        return this;
    }

    public override bool Success { get; }
    public override Message Message { get; }
    public override Status Status { get; protected set; }
    public override Func<bool, Message, object?, dynamic> Selector { get; protected set; }
    public T Value { get; }
    protected override object? Data => Value;
}