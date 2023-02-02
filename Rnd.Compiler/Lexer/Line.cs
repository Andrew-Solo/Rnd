using Newtonsoft.Json;

namespace Rnd.Compiler.Lexer;

public class Line
{
    private Line(string source, Line? previous)
    {
        Source = source;
        Previous = previous;
        Number = previous?.Number ?? 0 + 1;
        Lexemes = Lexeme.Parse(source, previous?.Lexemes.LastOrDefault());
        
        if (previous != null) previous.Next = this;
    }
    
    public int Number { get; }
    public string Source { get; }
    public List<Lexeme> Lexemes { get; }
    
    [JsonIgnore]
    public Line? Previous { get; }
    
    [JsonIgnore]
    public Line? Next { get; set; }
    
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