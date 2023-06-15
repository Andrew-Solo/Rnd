
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rnd.Constants;

namespace Rnd.Entities;

public abstract class Model : Entity
{
    protected Model(string name, string path)
    {
        Name = name;
        Path = path;
    }

    [MaxLength(TextSize.Tiny)] 
    public string Name { get; protected set; }
    
    [MaxLength(TextSize.Large)] 
    public string Path { get; protected set; }
    
    [MaxLength(TextSize.Small)] 
    public string? Title { get; protected set; }
    
    [MaxLength(TextSize.Small)]
    public string? Subtitle { get; protected set; }
    
    [MaxLength(TextSize.Medium)]
    public string? Description { get; protected set; }
    
    [MaxLength(TextSize.Paragraph)]
    public string? Icon { get; protected set; }
    
    [Column(TypeName = "json")]
    public HslaColor? Color { get; protected set; }
    
    [Column(TypeName = "json")]
    public HslaColor? Subcolor { get; protected set; }
    
    [MaxLength(TextSize.Large)]
    public string? Thumbnail { get; protected set; }
    
    [MaxLength(TextSize.Large)]
    public string? Image { get; protected set; }
    
    [MaxLength(TextSize.Large)]
    public string? Subimage { get; protected set; }

    [Column(TypeName = "json")]
    public Dictionary<string, string?> Attributes { get; protected set; } = new();
}

public record struct HslaColor(
    short Hue,
    byte Saturation,
    byte Lightness,
    byte Alpha
);