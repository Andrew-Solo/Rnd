namespace Rnd.Script.Lexer;

public class File
{
    private File(string filepath, string filename, string source)
    {
        Filepath = filepath;
        Filename = filename;
        Source = source;
        Lines = Line.Parse(source);
    }

    public string Filepath { get; }
    public string Filename { get; }
    public string Source { get; }
    public List<Line> Lines { get; }
    
    #region Parser

    public static async Task<File> ParseAsync(string filepath, string filename)
    {
        return await ParseAsync(Path.Combine(filepath, filename));
    }
    
    public static async Task<File> ParseAsync(string path)
    {
        return new File(
            Path.GetFullPath(path), 
            Path.GetFileName(path), 
            await System.IO.File.ReadAllTextAsync(path)
        );
    }

    #endregion
}