using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Rnd.Script.Parser;

namespace Rnd.Script.Compiler.ValueLexer;

public class ValuePart
{
    private ValuePart(ValuePartType partType, string value, ValuePart? previous)
    {
        PartType = partType;
        Value = value;

        if (previous == null) return;
        
        Previous = previous;
        previous.Next = this;
    }
    
    public int Width => Value.Length;
    
    public ValuePartType PartType { get; }
    public string Value { get; }
    
    [JsonIgnore]
    public ValuePart? Previous { get; }
    
    [JsonIgnore]
    public ValuePart? Next { get; set; }
    
    // private string GetValue()
    // {
    //     return PartType switch
    //     {
    //         Compiler.ValueLexer.ValuePartType.Value => Value.TrimStart('=', ' '),
    //         Compiler.ValueLexer.ValuePartType.Title => Value[1..^1],
    //         Compiler.ValueLexer.ValuePartType.ChildrenType => Value[1..^1],
    //         Compiler.ValueLexer.ValuePartType.ChildrenCustomType => Value[1..^1],
    //         _ => Value
    //     };
    // }
    
    #region Parser

    public static List<ValuePart> Parse(string source)
    {
        var result = new List<ValuePart>();
        
        while (source != String.Empty)
        {
            result.Add(ParseNext(ref source, result.LastOrDefault()));
        }

        return result;
    }

    private static ValuePart ParseNext(ref string source, ValuePart? previous)
    {
        var type = ParseType(source);
        var value = Regex.Match(source, Patterns[type]).Value;
        source = Regex.Replace(source, Patterns[type], "").TrimStart(' ');
        return new ValuePart(type, value, previous);
    }
    
    private static ValuePartType ParseType(string source)
    {
        return Patterns.FirstOrDefault(p => Regex.IsMatch(source, p.Value)).Key;
    }

    public static Dictionary<ValuePartType, string> Patterns => 
        Enum.GetValues<ValuePartType>().Reverse()
            .ToDictionary(type => type, GetPattern);

    public static string GetPattern(ValuePartType partType)
    {
        return "^(?:" + partType switch
        {
            ValuePartType.Unknown => ".",
            ValuePartType.Integer => "-?\\d+",
            ValuePartType.Float => "-?\\d+\\.\\d*",
            ValuePartType.Dice => "-?\\d+d\\w*",
            ValuePartType.Comma => ",",
            ValuePartType.Colon => ":",
            ValuePartType.Identifier => "[A-Z_]\\w*",
            ValuePartType.Boolean => "true|false",
            ValuePartType.None => "none",
            ValuePartType.List => "\\[.*\\]",
            ValuePartType.Object => "\\{.*\\}",
            ValuePartType.Invocation => "\\(.*\\)",
            ValuePartType.String => "\".*\"",
            ValuePartType.Title => "'.*'",
            
            ValuePartType.Script => "[^@A-Z_]*",
            ValuePartType.Reference => "[@A-Z_][A-Za-z_]*(?:\\.[A-Z_][A-Za-z_]*)*",
            _ => throw new ArgumentOutOfRangeException(nameof(partType), partType, "Unknown lexeme type")
        } + ")";
    }

    #endregion
}