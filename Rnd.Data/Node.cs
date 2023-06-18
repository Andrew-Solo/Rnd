namespace Rnd.Data;

public class Node
{
    public Node(string resource, string name)
    {
        Resource = resource;
        Name = name;
    }

    public string Resource { get; }
    public string Name { get; }
    
    public Guid? Id => Guid.TryParse(Name, out var value) ? value : null;
    public bool IsId => Id != null;

    
    public bool IsAuto => Name is "@me" or "@first";
    public bool IsNone => Name is "@none";
}