namespace Rnd.Primitives;

public record struct Association(
    Provider Provider,
    string Identifier,
    string? Secret = null
);