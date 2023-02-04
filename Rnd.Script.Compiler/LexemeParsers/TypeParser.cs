using Rnd.Constants;
using Rnd.Script.Parser;

namespace Rnd.Script.Compiler.LexemeParsers;

public class TypeParser : LexemeParser<UnitType>
{
    public override UnitType Parse(Node.Property? property)
    {
        return property?.Value switch
        {
            null => UnitType.Object,
            "obj" => UnitType.Object,
            "str" => UnitType.String,
            "int" => UnitType.Integer,
            "float" => UnitType.Float,
            "dice" => UnitType.Dice,
            "bool" => UnitType.Boolean,
            "list" => UnitType.List,
            "ref" => UnitType.Reference,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}