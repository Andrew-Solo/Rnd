using Rnd.Api.Helpers;
using Rnd.Api.Localization;

namespace Rnd.Api.Modules.Basic.Resources;

public class Resource : IResource
{
    public Resource(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; }
    public virtual string? Path { get; protected set; }
    public string Name { get; private set; }
    
    public decimal Default => 0;
    public decimal Value { get; set; }
    public decimal? Min { get; set; }
    public decimal? Max { get; set; }
    
    #region IStorable

    private Guid CharacterId { get; set; }

    public void Save(Data.Entities.Resource entity)
    {
        if (entity.Id != Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);
        
        entity.Fullname = PathHelper.Combine(Path, Name);
        entity.Value = Value;
        entity.Min = Min;
        entity.Max = Max;
        entity.CharacterId = CharacterId;
    }

    public void Load(Data.Entities.Resource entity)
    {
        if (Id != entity.Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);

        Path = PathHelper.GetPath(entity.Fullname);
        Name = PathHelper.GetName(entity.Fullname);
        Value = entity.Value;
        Min = entity.Min;
        Max = entity.Max;
        CharacterId = entity.CharacterId;
    }

    #endregion
}