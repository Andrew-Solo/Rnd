using Rnd.Constants;
using Rnd.Results;
using Rnd.Script.Parser;

namespace Rnd.Script.Compiler.LexemeParsers;

public class AccessParser : LexemeParser<UnitAccess>
{
    public override UnitAccess Parse(Node.Property? property)
    {
        return property?.Value switch
        {
            null => UnitAccess.Public,
            "public" => UnitAccess.Public,
            "protected" => UnitAccess.Protected,
            "private" => UnitAccess.Private,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}