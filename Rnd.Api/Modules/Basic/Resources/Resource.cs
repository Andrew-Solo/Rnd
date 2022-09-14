using Rnd.Api.Data;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Resources;

public class Resource : IResource
{
    public Resource(ICharacter character, string name)
    {
        CharacterId = character.Id;
        Name = name;
        
        Id = Guid.NewGuid();
        
        character.Resources.Add(this);
    }

    public Guid Id { get; }
    public Guid CharacterId { get; private set; }
    public virtual string? Path { get; protected set; }
    public string Name { get; private set; }
    
    public decimal Value { get; set; }
    public virtual decimal? Min { get; set; }
    public virtual decimal? Max { get; set; }
    
    #region IStorable

    public IStorable<Data.Entities.Resource> AsStorable => this;
    
    public Data.Entities.Resource? Save(Data.Entities.Resource? entity, bool upcome = true)
    {
        entity ??= new Data.Entities.Resource {Id = Id};
        if (AsStorable.NotSave(entity)) return null;
        
        entity.Fullname = PathHelper.Combine(Path, Name);
        entity.Value = Value;
        entity.Min = Min;
        entity.Max = Max;
        entity.CharacterId = CharacterId;

        return entity;
    }

    public IStorable<Data.Entities.Resource>? Load(Data.Entities.Resource entity)
    {
        if (AsStorable.NotLoad(entity)) return null;

        Path = PathHelper.GetPath(entity.Fullname);
        Name = PathHelper.GetName(entity.Fullname);
        Value = entity.Value;
        Min = entity.Min;
        Max = entity.Max;
        CharacterId = entity.CharacterId;

        return AsStorable;
    }

    #endregion
}