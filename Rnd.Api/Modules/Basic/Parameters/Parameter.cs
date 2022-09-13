using Newtonsoft.Json;
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Helpers;

namespace Rnd.Api.Modules.Basic.Parameters;

public class Parameter<T> : IParameter where T : notnull
{
    public Parameter(string name)
    {
        Name = name;
        
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; }
    public Guid CharacterId { get; set; }
    public virtual string? Path { get; protected set; }
    public string Name { get; private set; }
    public T? Value { get; set; }

    #region IParameter

    public Type Type => typeof(T);

    object? IParameter.Value
    {
        get => Value;
        set => Value = (T?) value;
    }

    #endregion
    
    #region IStorable

    public IStorable<Parameter> AsStorable => this;
    
    public Parameter? Save(Parameter? entity)
    {
        entity ??= new Parameter {Id = Id};
        if (AsStorable.NotSave(entity)) return null;
        
        entity.Fullname = PathHelper.Combine(Path, Name);
        entity.Type = Type.Name;
        entity.ValueJson = JsonConvert.SerializeObject(Value);
        entity.CharacterId = CharacterId;

        return entity;
    }

    public IStorable<Parameter>? Load(Parameter entity)
    {
        if (AsStorable.NotLoad(entity)) return null;

        Path = PathHelper.GetPath(entity.Fullname);
        Name = PathHelper.GetName(entity.Fullname);
        Value = JsonConvert.DeserializeObject<T>(entity.ValueJson);
        CharacterId = entity.CharacterId;

        return AsStorable;
    }

    #endregion
}