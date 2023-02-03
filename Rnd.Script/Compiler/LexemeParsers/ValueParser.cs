using Rnd.Constants;
using Rnd.Results;
using Rnd.Script.Parser;

namespace Rnd.Script.Compiler.LexemeParsers;

public class ValueParser : LexemeParser<dynamic?>
{
    public override dynamic? Parse(Node.Property? property)
    {
        return property?.Value switch
        {
            null => null,
            _ => property.Value.Value
        };
    }
}