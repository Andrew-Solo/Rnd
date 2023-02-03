namespace Rnd.Script.Parser;

public readonly record struct Position(
    int Line,
    int Column,
    int Width
);