using Newtonsoft.Json;
using Rnd.Script.Lexer;

namespace Rnd.Script.Compiler.ValueLexer;

public class Value
{
    private Value(string source)
    {
        Source = source;
        Parts = ValuePart.Parse(source);
    }
    
    private static Value Parse(string source)
    {
        return new Value(source);
    }
    
    public string Source { get; }
    public List<ValuePart> Parts { get; }

    // public bool IsLexemeExist(ValuePartType partType)
    // {
    //     return GetLexeme(partType) != null;
    // }
    //
    // public ValuePart? GetLexeme(ValuePartType partType)
    // {
    //     return GetLexeme(l => l.PartType == partType);
    // }
    //
    // public ValuePart? GetLexeme(Func<ValuePart, bool> predicate)
    // {
    //     return Parts.FirstOrDefault(predicate);
    // }
    //
    // public List<ValuePart> GetLexemes(ValuePartType partType)
    // {
    //     return GetLexemes(l => l.PartType == partType);
    // }
    //
    // public List<ValuePart> GetLexemes(Func<ValuePart, bool> predicate)
    // {
    //     return Parts.Where(predicate).ToList();
    // }
    //
    // public bool CheckPattern(params ValuePartType[] types)
    // {
    //     return !types.Where((t, i) => Parts[i].PartType != t).Any();
    // }
}