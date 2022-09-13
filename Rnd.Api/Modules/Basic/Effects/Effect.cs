using Rnd.Api.Data;
using Rnd.Api.Helpers;
using Rnd.Api.Localization;
using Rnd.Api.Modules.Basic.Effects.Parameter;
using Rnd.Api.Modules.Basic.Effects.Resource;

namespace Rnd.Api.Modules.Basic.Effects;

public class Effect : IEffect
{
    public Effect(string name)
    {
        Name = name;

        Id = Guid.NewGuid();
        ParameterEffects = new List<IParameterEffect>();
        ResourceEffects = new List<IResourceEffect>();
    }

    public Guid Id { get; }
    public Guid CharacterId { get; set; }
    public virtual string? Path { get; set; }
    public string Name { get; set; }
    public List<IParameterEffect> ParameterEffects { get; }
    public List<IResourceEffect> ResourceEffects { get; }
    
    #region IStorable
    
    public IStorable<Data.Entities.Effect> AsStorable => this;

    public Data.Entities.Effect? Save(Data.Entities.Effect? entity)
    {
        entity ??= new Data.Entities.Effect {Id = Id};
        if (AsStorable.NotSave(entity)) return null;

        entity.Fullname = PathHelper.Combine(Path, Name);
        entity.ParameterEffects.SaveList(ParameterEffects.Cast<IStorable<Data.Entities.ParameterEffect>>().ToList());
        entity.ResourceEffects.SaveList(ResourceEffects.Cast<IStorable<Data.Entities.ResourceEffect>>().ToList());
        entity.CharacterId = CharacterId;

        return entity;
    }

    public void Load(Data.Entities.Effect entity)
    {
        if (AsStorable.NotLoad(entity)) return;

        Path = PathHelper.GetPath(entity.Fullname);
        Name = PathHelper.GetName(entity.Fullname);
        
        ParameterEffects.Clear();
        ParameterEffects.AddRange(entity.ParameterEffects.Select(ParameterEffectFactory.ByEntity));
        
        ResourceEffects.Clear();
        ResourceEffects.AddRange(entity.ResourceEffects.Select(ResourceEffectFactory.ByEntity));
        
        CharacterId = entity.CharacterId;
    }

    #endregion
}