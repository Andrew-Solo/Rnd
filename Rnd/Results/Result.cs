namespace Rnd.Results;

public static class Result
{
    public static Result<T> Success<T>(T value, string header)
    {
        return Success(value, new Message(header));
    }
    
    public static Result<T> Success<T>(T value, Message message)
    {
        return new Result<T>(true, message, value);
    }
    
    public static Result<T> Fail<T>(string header)
    {
        return Fail<T>(new Message(header));
    }
    
    public static Result<T> Fail<T>(Message message)
    {
        return new Result<T>(false, message);
    }
    
    public static Result<T> Validated<T>(ValidationResult result, Func<T> getValue, string header)
    {
        return Validated(result, getValue, new Message(header));
    }
    
    public static Result<T> Validated<T>(ValidationResult result, Func<T> getValue, Message message)
    {
        return result.IsValid
            ? Success(getValue(), message)
            : Fail<T>(result.Message);
    }
    
    public static Result<T> Found<T>(T? value, string successHeader, string failHeader)
    {
        return Found(value, new Message(successHeader), new Message(failHeader));
    }
    
    public static Result<T> Found<T>(T? value, Message successMessage, Message failMessage)
    {
        return value != null 
            ? Success(value, successMessage)
            : Fail<T>(failMessage);
    }
}

public class Result<T>
{
    internal Result(bool isSuccess, Message message, T? value = default)
    {
        IsSuccess = isSuccess;
        Message = message;
        _value = value!;

        _onSuccess = x => x!;
    }

    public bool IsSuccess { get; private set; }
    public bool IsFailed => !IsSuccess;
    public Message Message { get; }
    public T Value => _value ?? throw new NullReferenceException("Value not initialized." + 
                                                                 (IsFailed ? Message.Header : String.Empty));

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