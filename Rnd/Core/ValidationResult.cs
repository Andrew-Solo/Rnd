using Rnd.Result;

namespace Rnd.Core;

public readonly record struct ValidationResult(bool IsValid, Message Errors);