using Rnd.Result;

namespace Rnd.Core;

internal static class Extensions
{
    internal static Message ToMessage(this FluentValidation.Results.ValidationResult result, string header = "Ошибка валидации")
    {
        return new Message(
            header,
            null,
            result.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    x => x.Key, 
                    x => x.Select(f => f.ErrorMessage).ToList())
        );
    }
}