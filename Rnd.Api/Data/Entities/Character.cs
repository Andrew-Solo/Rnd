namespace Rnd.Api.Data.Entities;

public class Character
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? ColorHex { get; set; }
    public virtual List<Parameter> Parameters { get; set; } = new();
    public virtual List<Resource> Resources { get; set; } = new();
    
    
}