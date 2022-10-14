using System.Net;

namespace Rnd.Api.Client.Controllers;

public class Response<T> where T : class
{
    private Response(T? value = null, ResponseStatus status = ResponseStatus.Valid, Errors? errors = null)
    {
        Value = value;
        Status = status;
        Errors = errors;
    }

    public static async Task<Response<T>> Create(HttpResponseMessage httpResponse)
    {
        switch (httpResponse.StatusCode)
        {
            case HttpStatusCode.NoContent:
                return Empty();
            case HttpStatusCode.Conflict:
            {
                var errors = await httpResponse.Content.ReadAsAsync<Errors>();
                return Overlap(errors ?? new Errors());
            }
            case HttpStatusCode.BadRequest:
            {
                var errors = await httpResponse.Content.ReadAsAsync<Errors>();
                return Error(errors ?? new Errors());
            }
            default:
            {
                var errors = new Errors
                {
                    [nameof(httpResponse.StatusCode)] = new []
                    {
                        httpResponse.StatusCode.ToString(), 
                        httpResponse.ReasonPhrase ?? String.Empty
                    }
                };

                if (!httpResponse.IsSuccessStatusCode) return Unknown(errors);
                
                var value = await httpResponse.Content.ReadAsAsync<T>();
                return Valid(value ?? throw new InvalidOperationException("Value is null"));
            }
        }
    }

    public static Response<T> Error(Errors errors)
    {
        return new Response<T>(null, ResponseStatus.Error, errors);
    }
    
    public static Response<T> Empty()
    {
        return new Response<T>(null, ResponseStatus.Empty);
    }
    
    public static Response<T> Overlap(Errors errors)
    {
        return new Response<T>(null, ResponseStatus.Overlap, errors);
    }
    
    public static Response<T> Valid(T value)
    {
        return new Response<T>(value);
    }
    
    public static Response<T> Unknown(Errors errors)
    {
        return new Response<T>(null, ResponseStatus.Unknown, errors);
    }
    
    public T? Value { get; }
    public ResponseStatus Status { get; }
    public Errors? Errors { get; }

    public bool IsSuccess => Status is ResponseStatus.Empty or ResponseStatus.Valid;
}