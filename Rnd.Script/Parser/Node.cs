using Newtonsoft.Json;
using Rnd.Script.Lexer;

namespace Rnd.Script.Parser;

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

    public Property? Name { get; set; }
    public Property? Access { get; set; }
    public Property? Type { get; set; }
    public Property? TypePicker { get; set; }
    public Property? CustomType { get; set; }
    public Property? Role { get; set; }
    public Property? Value { get; set; }
    
    public Property? Title { get; set; }
    public Property? Description { get; set; }
    public List<Attribute> Attributes { get; }

    public record struct Property(string Value, Position Position);
    public record struct Attribute(Property Name, Property? Value);
}
