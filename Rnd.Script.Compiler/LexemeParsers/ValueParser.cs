using Rnd.Script.Parser;

namespace Rnd.Script.Compiler.LexemeParsers;

public class ValueParser : LexemeParser<string>
{
    public override string Parse(Node.Property? property)
    {
        var value = property?.Value;

        if (value == null) return "None";
        
        if (value.StartsWith('"') && value.EndsWith('"')) return value[1..^1];
        
        return value;
    }
}