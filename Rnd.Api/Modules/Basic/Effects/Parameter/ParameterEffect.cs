using Newtonsoft.Json;
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Helpers;
using Rnd.Api.Localization;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.Basic.Effects.Parameter;

public abstract class ParameterEffect<T> : IParameterEffect where T : notnull
{
    protected ParameterEffect(IEntity entity)
    {
        Id = entity.Id;
        
        ParameterName = null!;
        Modifier = default!;
    }
    
    protected ParameterEffect(IEffect effect, string parameterName, T modifier)
    {
        EffectId = effect.Id;
        ParameterName = parameterName;
        Modifier = modifier;
        
        Id = Guid.NewGuid();
        
        effect.ParameterEffects.Add(this);
    }

    public Guid Id { get; }
    public Guid EffectId { get; private set; }
    public virtual string? ParameterPath { get; protected set; }
    public string ParameterName { get; private set; }
    public T Modifier { get; set; }

    #region IParameterEffect
    
    public Type Type => typeof(T);
    
    object IParameterEffect.Modifier
    {
        get => Modifier;
        set => Modifier = (T) value;
    }

    public abstract TParameter Modify<TParameter>(TParameter parameter) where TParameter : IParameter;

    #endregion
    
    #region IStorable
    
    public IStorable<ParameterEffect> AsStorable => this;

    public ParameterEffect? Save(ParameterEffect? entity, bool upcome = true)
    {
        entity ??= new ParameterEffect {Id = Id};
        if (AsStorable.NotSave(entity)) return null;
        
        entity.ParameterFullname = PathHelper.Combine(ParameterPath, ParameterName);
        entity.Type = Type.Name;
        entity.ModifierJson = JsonConvert.SerializeObject(Modifier);
        entity.EffectId = EffectId;

        return entity;
    }

    public IStorable<ParameterEffect>? Load(ParameterEffect entity, bool upcome = true)
    {
        if (AsStorable.NotLoad(entity)) return null;

        EffectId = entity.EffectId;
        ParameterPath = PathHelper.GetPath(entity.ParameterFullname);
        ParameterName = PathHelper.GetName(entity.ParameterFullname);
        Modifier = JsonConvert.DeserializeObject<T>(entity.ModifierJson) 
                   ?? throw new InvalidOperationException(Lang.Exceptions.JsonNullError);

        return AsStorable;
    }

    #endregion
}