using Rnd.Results;

namespace Rnd.Core;

internal static class Extensions
{
    internal static Message ToMessage(this FluentValidation.Results.ValidationResult result, string? header = "Ошибка валидации")
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