using Rnd.Results;

namespace Rnd.Core;

internal static class Extensions
{
    internal static Message ToMessage(this FluentValidation.Results.ValidationResult result, string title = "Ошибка валидации")
    {
        return new Message(
            title,
            null,
            result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    x => x.Key, 
                    x => x.Select(f => f.ErrorMessage).ToHashSet()
                )
        );
    }
}