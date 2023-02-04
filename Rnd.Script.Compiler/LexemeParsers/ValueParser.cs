using Rnd.Script.Parser;

namespace Rnd.Script.Compiler.LexemeParsers;

public class ValueParser : LexemeParser<string>
{
    public override string Parse(Node.Property? property)
    {
        return property?.Value switch
        {
            null => "None",
            _ => property.Value.Value
        };
    }
}