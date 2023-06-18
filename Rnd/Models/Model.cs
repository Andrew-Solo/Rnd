
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Rnd.Constants;
using Rnd.Data;
using Rnd.Primitives;
using Rnd.Results;

namespace Rnd.Models;

[Index(nameof(Path), IsUnique = true)]
public abstract class Model
{
    public Guid Id { get; protected set; } = Guid.NewGuid();

    [MaxLength(TextSize.Large)] 
    public string Path { get; protected set; } = null!;

    [MaxLength(TextSize.Tiny)] 
    public string Name { get; protected set; } = null!;

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
    public Dictionary<string, string> Attributes { get; protected set; } = new();
    
    public DateTimeOffset Created { get; protected set; } = Time.Now;
    public DateTimeOffset Viewed { get; protected set; } = Time.Now;
    public DateTimeOffset? Updated { get; protected set; }
    
    protected virtual void FillData(ModelData data)
    {
        if (data.Id != null) Id = data.Id.Value;
        Name = data.Name?.ToLower() ?? Id.ToString("N").ToLower();
        Path = string.IsNullOrWhiteSpace(data.Path) ? Name : $"{data.Path}/{Name}".ToLower();
        Title = data.Title;
        Subtitle = data.Subtitle;
        Description = data.Description;
        Icon = data.Icon;
        Color = data.Color;
        Subcolor = data.Subcolor;
        Thumbnail = data.Thumbnail;
        Image = data.Image;
        Subimage = data.Subimage;
        Attributes.Merge(data.Attributes);
    }
    
    public virtual ExpandoObject Get()
    {
        Viewed = Time.Now;
        
        dynamic view = new ExpandoObject();
        
        view.id = Id;
        view.path = Path;
        view.name = Name;
        view.title = Title!;
        view.subtitle = Subtitle!;
        view.description = Description!;
        view.icon = Icon!;
        view.color = Color?.ToArray()!;
        view.subcolor = Subcolor?.ToArray()!;
        view.thumbnail = Thumbnail!;
        view.image = Image!;
        view.subimage = Subimage!;
        view.attributes = Attributes;
        view.created = Created;
        view.viewed = Viewed;
        view.updated = Updated!;
        
        return view;
    }
    
    public virtual Result<Model> Update(ModelData data)
    {
        return Result.Ok(this);
    }
    
    public virtual Result<Model> Delete()
    {
        return Result.Ok(this);
    }
    
    public static dynamic SelectView(bool success, Message message, object? data)
    {
        return new {Success = success, Message = message, Data = (data as Model)?.Get()};
    }
    
    public static dynamic SelectListView(bool success, Message message, object? data)
    {
        return new
        {
            Success = success, 
            Message = message, 
            Data = (data as IEnumerable<Model>)?.Select(model => model.Get())
        };
    }
}
