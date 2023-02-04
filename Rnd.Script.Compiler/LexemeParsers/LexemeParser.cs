using Rnd.Script.Parser;

namespace Rnd.Script.Compiler.LexemeParsers;

public static class LexemeParser
{
    public static AccessParser Access => new();
    public static TypeParser Type => new();
    public static RoleParser Role => new();
    public static ValueParser Value => new();
}

public abstract class LexemeParser<T>
{
    public abstract T Parse(Node.Property? property);
}