namespace Rnd.Compiler.Lexer;

public readonly record struct Position(
    int Line,
    int Column,
    int Width
);