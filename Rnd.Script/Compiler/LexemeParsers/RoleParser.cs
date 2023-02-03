using Rnd.Constants;
using Rnd.Results;
using Rnd.Script.Parser;

namespace Rnd.Script.Compiler.LexemeParsers;

public class RoleParser : LexemeParser<UnitRole>
{
    public override UnitRole Parse(Node.Property? property)
    {
        return property?.Value switch
        {
            null => UnitRole.Variable,
            "var" => UnitRole.Variable,
            "const" => UnitRole.Constant,
            "exp" => UnitRole.Expression,
            "func" => UnitRole.Function,
            "type" => UnitRole.Type,
            "module" => UnitRole.Module,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}