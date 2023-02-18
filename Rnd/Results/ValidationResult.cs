namespace Rnd.Results;

public record struct ValidationResult(
    bool IsValid,
    Message Message
);