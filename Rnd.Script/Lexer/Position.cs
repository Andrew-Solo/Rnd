namespace Rnd.Script.Lexer;

public readonly record struct Position(
    int Line,
    int Column,
    int Width
);