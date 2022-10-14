using Rnd.Api.Client.Responses;

namespace Rnd.Api.Client.Exceptions;

public class NotSuccessResponseException : InvalidOperationException
{
    public NotSuccessResponseException(Errors? errors) : base(errors?.ToString() ?? DefaultMessage) {}

    public const string DefaultMessage = "API request failed for unknown reason";
}