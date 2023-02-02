using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Rnd.Compiler.Lexer;

public class Lexeme
{
    private Lexeme(LexemeType type, string value, Lexeme? previous, int line)
    {
        Type = type;
        Value = value;

        if (previous == null) return;
        
        Column = previous.Type == LexemeType.Newline ? 0 : previous.Column + previous.Width;
        Previous = previous;
        Line = line;
        previous.Next = this;
    }
    
    public int Line { get; }
    public int Column { get; }
    public int Width => Value.Length;
    
    public LexemeType Type { get; }
    public string Value { get; }
    
    [JsonIgnore]
    public Lexeme? Previous { get; }
    
    [JsonIgnore]
    public Lexeme? Next { get; set; }
    
    #region Parser

    public static List<Lexeme> Parse(string source, Lexeme? previous, int line)
    {
        var result = new List<Lexeme>();
        
        while (source != String.Empty)
        {
            result.Add(ParseNext(ref source, result.LastOrDefault() ?? previous, line));
        }

        return result;
    }

    private static Lexeme ParseNext(ref string source, Lexeme? previous, int line)
    {
        var type = ParseType(source);
        var value = Regex.Match(source, Patterns[type]).Value;
        source = Regex.Replace(source, Patterns[type], "").TrimStart(' ');
        return new Lexeme(type, value, previous, line);
    }
    
    private static LexemeType ParseType(string source)
    {
        return Patterns.FirstOrDefault(p => Regex.IsMatch(source, p.Value)).Key;
    }

    public static Dictionary<LexemeType, string> Patterns => 
        Enum.GetValues<LexemeType>().Reverse()
            .ToDictionary(type => type, GetPattern);

    public static string GetPattern(LexemeType type)
    {
        return "^(?:" + type switch
        {
            LexemeType.Unknown => ".",
            LexemeType.Newline => Environment.NewLine,
            LexemeType.Value => $"=[^{Environment.NewLine}]*",
            LexemeType.Declaration => ":",
            LexemeType.Title => "'.*'",
            LexemeType.TypePicker => "<[A-Za-z_]\\w*>",
            LexemeType.Identifier => "[A-Z_]\\w*",
            LexemeType.Attribute => "[a-z]\\w*",
            LexemeType.Role => "var|const|exp|func|type|module",
            LexemeType.Type => "obj|str|int|float|bool|list|ref",
            LexemeType.Accessor => "public|private|protected",
            LexemeType.Tabulation => "(?:  )+",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown lexeme type")
        } + ")";
    }

    #endregion
}