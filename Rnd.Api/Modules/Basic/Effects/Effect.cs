﻿using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Effects.Parameter;
using Rnd.Api.Modules.Basic.Effects.Resource;
using ResourceEffect = Rnd.Api.Data.Entities.ResourceEffect;

namespace Rnd.Api.Modules.Basic.Effects;

public class Effect : IEffect
{
    public Effect(IEntity entity)
    {
        Id = entity.Id;
        
        Name = null!;
        
        ParameterEffects = new List<IParameterEffect>();
        ResourceEffects = new List<IResourceEffect>();
    }
    
    public Effect(ICharacter character, string name)
    {
        CharacterId = character.Id;
        Name = name;

        Id = Guid.NewGuid();
        ParameterEffects = new List<IParameterEffect>();
        ResourceEffects = new List<IResourceEffect>();

        character.Effects.Add(this);
    }

    public Guid Id { get; }
    public Guid CharacterId { get; private set; }
    public virtual string? Path { get; set; }
    public string Name { get; set; }
    public List<IParameterEffect> ParameterEffects { get; }
    public List<IResourceEffect> ResourceEffects { get; }
    
    #region IStorable
    
    public IStorable<Data.Entities.Effect> AsStorable => this;

    public Data.Entities.Effect? Save(Data.Entities.Effect? entity, bool upcome = true)
    {
        entity ??= new Data.Entities.Effect {Id = Id};
        if (AsStorable.NotSave(entity)) return null;

        entity.Fullname = PathHelper.Combine(Path, Name);
        entity.ParameterEffects.SaveList(ParameterEffects.Cast<IStorable<Data.Entities.ParameterEffect>>().ToList());
        entity.ResourceEffects.SaveList(ResourceEffects.Cast<IStorable<Data.Entities.ResourceEffect>>().ToList());
        entity.CharacterId = CharacterId;

        return entity;
    }

    public IStorable<Data.Entities.Effect>? Load(Data.Entities.Effect entity, bool upcome = true)
    {
        if (AsStorable.NotLoad(entity)) return null;

        Path = PathHelper.GetPath(entity.Fullname);
        Name = PathHelper.GetName(entity.Fullname);
        ParameterEffects.Cast<IStorable<ParameterEffect>>().ToList().LoadList(entity.ParameterEffects, new ParameterEffectFactory());
        ResourceEffects.Cast<IStorable<ResourceEffect>>().ToList().LoadList(entity.ResourceEffects, new ResourceEffectFactory());
        CharacterId = entity.CharacterId;

        return AsStorable;
    }

    #endregion
}