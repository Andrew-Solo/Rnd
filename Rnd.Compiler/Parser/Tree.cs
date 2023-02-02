using File = Rnd.Compiler.Lexer.File;

namespace Rnd.Compiler.Parser;

public class Tree
{
    public Tree(
        string filepath, 
        string filename, 
        List<Node> nodes
    )
    {
        Filepath = filepath;
        Filename = filename;
        Nodes = nodes;
    }

    public static Tree Parse(File file)
    {
        var builder = new TreeBuilder();
        return builder.Build(file);
    }
    
    public string Filepath { get; }
    public string Filename { get; }
    public List<Node> Nodes { get; }
}