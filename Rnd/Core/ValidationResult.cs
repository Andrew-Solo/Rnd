using Rnd.Results;

namespace Rnd.Core;

public readonly record struct ValidationResult(bool IsValid, Message Errors);