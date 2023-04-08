namespace Rnd.Results;

public static class Result
{
    public static Result<T> Success<T>(T value, string title)
    {
        return Success(value, new Message(title));
    }
    
    public static Result<T> Success<T>(T value, Message message)
    {
        return new Result<T>(true, message, Status.Ok, value);
    }
    
    public static Result<T> Fail<T>(string title, Status status = Status.Error)
    {
        return Fail<T>(new Message(title), status);
    }
    
    public static Result<T> Fail<T>(Message message, Status status = Status.Error)
    {
        return new Result<T>(false, message, status);
    }
    
    public static Result<T> Validated<T>(ValidationResult result, Func<T> getValue, string title)
    {
        return Validated(result, getValue, new Message(title));
    }
    
    public static Result<T> Validated<T>(ValidationResult result, Func<T> getValue, Message message)
    {
        return result.IsValid
            ? Success(getValue(), message)
            : Fail<T>(result.Message, Status.BadRequest);
    }
    
    public static Result<T> Found<T>(T? value, string successTitle, string failTitle)
    {
        return Found(value, new Message(successTitle), new Message(failTitle));
    }
    
    public static Result<T> Found<T>(T? value, Message successMessage, Message failMessage)
    {
        return value != null 
            ? Success(value, successMessage)
            : Fail<T>(failMessage, Status.NotFound);
    }
}

public class Result<T>
{
    internal Result(bool isSuccess, Message message, Status status, T? value = default)
    {
        IsSuccess = isSuccess;
        Message = message;
        Status = status;
        _value = value!;

        _onSuccess = x => x!;
    }

    public bool IsSuccess { get; private set; }
    public bool IsFailed => !IsSuccess;
    public Status Status { get; set; }
    public Message Message { get; }
    public T Value => _value ?? throw new NullReferenceException("Value not initialized." + 
                                                                 (IsFailed ? Message.Title : String.Empty));

    public Result<T> OnSuccess(Func<T, dynamic> onSuccess)
    {
        _onSuccess = onSuccess;
        return this;
    }
    
    public Result<T> ToFail(Action<Message>? changeMessage = null)
    {
        IsSuccess = false;
        _value = default;
        changeMessage?.Invoke(Message);
        return this;
    }

    public Result<T> Update(Func<T, Result<T>> getResult)
    {
        return Update(getResult(Value));
    }
    
    public Result<T> Update(Result<T> result)
    {
        switch (IsSuccess, result.IsSuccess)
        {
            case (true, true):
                _value = result.Value;
                Message.Update(result.Message);
                break;
            case (true, false):
                ToFail(m => m.Update(result.Message));
                break;
            case (false, false):
                Message.Update(result.Message);
                break;
            case (false, true):
                throw new InvalidOperationException("Cannot update failed result to success");
        }
            
        return this;
    }

    public dynamic Get()
    {
        return IsSuccess 
            ? _onSuccess(Value) 
            : Message;
    }
    
    private Func<T, dynamic> _onSuccess;
    private T? _value;
}