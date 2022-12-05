namespace Rnd.Api.Data.Entities;

public class ParameterDefinition
{
    public Guid Id { get; set; }

    public virtual CharacterDefinition Source { get; set; } = null!;
    
    public string Path { get; set; } = null!;
    public string Name { get; set; } = null!;
    public virtual Type Type { get; set; } = null!;
    public string Default { get; set; } = null!;
    
    public bool Const { get; set; }
    public bool Nullable { get; set; }
    public bool Hidden { get; set; }
    public bool Disabled { get; set; }

    public Dictionary<string, string> Attributes { get; set; } = new();
    
    #region Navigation
    
    public Guid SourceId { get; set; }
    public virtual List<ParameterDefinition> Definitions { get; set; } = new();
    public virtual List<Type> Types { get; set; } = new();

    #endregion
}