using Newtonsoft.Json;
using Rnd.Compiler.Lexer;

namespace Rnd.Compiler.Parser;

public class Node
{
    public Node(Node? parent = null)
    {
        Parent = parent;
        Parent?.Children.Add(this);
        
        Children = new List<Node>();
        Attributes = new List<Attribute>();
    }
    
    [JsonIgnore]
    public Node? Parent { get; }
    public List<Node> Children { get; }

    public Lexeme? Name { get; set; }
    public Lexeme? Access { get; set; }
    public Lexeme? Type { get; set; }
    public Lexeme? TypePicker { get; set; }
    public Lexeme? CustomType { get; set; }
    public Lexeme? Role { get; set; }
    public Lexeme? Value { get; set; }
    
    public Lexeme? Title { get; set; }
    public Lexeme? Description { get; set; }
    public List<Attribute> Attributes { get; }

    public record struct Attribute(Lexeme Name, Lexeme? Value);
}
