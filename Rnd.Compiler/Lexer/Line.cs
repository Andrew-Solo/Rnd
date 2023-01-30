namespace Rnd.Compiler.Lexer;

public class Line
{
    private Line(int number, string source)
    {
        Number = number;
        Source = source;
        Lexemes = Lexeme.Parse(source);
    }
    
    public int Number { get; }
    public string Source { get; }
    public List<Lexeme> Lexemes { get; }
    
    #region Parser

    public static List<Line> Parse(string source)
    {
        return source
            .Split(Environment.NewLine)
            .Select((line, index) => new Line(index + 1, line))
            .ToList();
    }

    #endregion
}