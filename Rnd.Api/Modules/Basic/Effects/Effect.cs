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
    public string? Path { get; set; }
    public string Name { get; set; }
    public List<IParameterEffect> ParameterEffects { get; }
    public List<IResourceEffect> ResourceEffects { get; }
    
    #region IStorable
    
    private Guid CharacterId { get; set; }

    public void Save(Data.Entities.Effect entity)
    {
        if (entity.Id != Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);

        entity.Fullname = PathHelper.Combine(Path, Name);
        entity.ParameterEffects = ParameterEffects.Select(pe => pe.CreateEntity()).ToList();
        entity.ResourceEffects = ResourceEffects.Select(re => re.CreateEntity()).ToList();
        entity.CharacterId = CharacterId;
    }

    public void Load(Data.Entities.Effect entity)
    {
        if (Id != entity.Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);

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