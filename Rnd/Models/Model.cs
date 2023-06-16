
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Rnd.Constants;
using Rnd.Results;

namespace Rnd.Models;

[Index(nameof(Name), IsUnique = true)]
[Index(nameof(Path), IsUnique = true)]
public abstract class Model
{
    protected Model(string name, string path)
    {
        Name = name;
        Path = path;
    }
    
    public Guid Id { get; protected set; } = Guid.NewGuid();

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
    
    public DateTimeOffset Created { get; protected set; } = Time.Now;
    public DateTimeOffset Viewed { get; protected set; } = Time.Now;
    public DateTimeOffset? Updated { get; protected set; }
    
    public virtual dynamic View()
    {
        return this;
    }
    
    public virtual Result<Model> Update(ExpandoObject data)
    {
        return Result.Ok(this);
    }
    
    public virtual Result<Model> Delete()
    {
        return Result.Ok(this);
    }
    
    public static dynamic SelectView(bool success, Message message, object? data)
    {
        return new {Success = success, Message = message, Data = (data as Model)?.View()};
    }
    
    //TODO ???
    public static dynamic SelectListView(bool success, Message message, object? data)
    {
        return new {Success = success, Message = message, Data = (data as IEnumerable<Model>)?.Select(model => model.View())};
    }
}

public record struct HslaColor(
    short Hue,
    byte Saturation,
    byte Lightness,
    byte Alpha
);