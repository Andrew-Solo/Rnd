using FluentValidation.Results;

namespace Rnd.Model;

internal static class Extensions
{
    internal static Message ToMessage(this ValidationResult result, string? header = null)
    {
        return new Message(
            header,
            null,
            result.Errors.ToDictionary(
                error => error.PropertyName,
                error => error.ErrorMessage)
        );
    }
}