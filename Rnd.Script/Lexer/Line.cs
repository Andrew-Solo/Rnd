using Newtonsoft.Json;

namespace Rnd.Script.Lexer;

public class Line
{
    private Line(string source, Line? previous)
    {
        Source = source;
        Previous = previous;
        Number = 1 + previous?.Number ?? 0;
        Lexemes = Lexeme.Parse(source, previous?.Lexemes.LastOrDefault(), Number);
        
        if (previous != null) previous.Next = this;
    }
    
    public int Number { get; }
    public string Source { get; }
    public List<Lexeme> Lexemes { get; }
    
    [JsonIgnore]
    public Line? Previous { get; }
    
    [JsonIgnore]
    public Line? Next { get; set; }

    public bool IsLexemeExist(LexemeType type)
    {
        return GetLexeme(type) != null;
    }
    
    public Lexeme? GetLexeme(LexemeType type)
    {
        return GetLexeme(l => l.Type == type);
    }
    
    public Lexeme? GetLexeme(Func<Lexeme, bool> predicate)
    {
        return Lexemes.FirstOrDefault(predicate);
    }
    
    public List<Lexeme> GetLexemes(LexemeType type)
    {
        return GetLexemes(l => l.Type == type);
    }
    
    public List<Lexeme> GetLexemes(Func<Lexeme, bool> predicate)
    {
        return Lexemes.Where(predicate).ToList();
    }
    
    public List<Line> GetTabGroup(int tabulation)
    {
        return GetTabGroup(tabulation, new List<Line>());
    }

    private List<Line> GetTabGroup(int tabulation, List<Line> lines)
    {
        if (!CheckPattern(LexemeType.Tabulation, LexemeType.Newline))
        {
            if (GetLexeme(LexemeType.Tabulation)?.Width != tabulation) return lines;
            lines.Add(this);
        }
        
        return Next?.GetTabGroup(tabulation, lines) ?? lines;
    }
    
    public bool CheckPattern(params LexemeType[] types)
    {
        return !types.Where((t, i) => Lexemes[i].Type != t).Any();
    }
    
    #region Parser

    public static List<Line> Parse(string source)
    {
        var result = new List<Line>();
        
        while (source != String.Empty)
        {
            result.Add(ParseNext(ref source, result.LastOrDefault()));
        }

        return result;
    }
    
    private static Line ParseNext(ref string source, Line? previous)
    {
        var newLine = source.IndexOf(Environment.NewLine, StringComparison.Ordinal) + Environment.NewLine.Length;
        var line = source[..newLine];
        source = source[newLine..];
        return new Line(line, previous);
    }

    #endregion
}