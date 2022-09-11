using Newtonsoft.Json;
using Rnd.Api.Data.Entities;
using Rnd.Api.Helpers;
using Rnd.Api.Localization;

namespace Rnd.Api.Modules.Basic.Parameters;

public class Parameter<T> : IParameter where T : notnull
{
    public Parameter(string name)
    {
        Name = name;
        
        Id = Guid.NewGuid();
    }
    
    public Guid Id { get; }
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

    private Guid CharacterId { get; set; }

    public void Save(Parameter entity)
    {
        if (entity.Id != Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);
        
        entity.Fullname = PathHelper.Combine(Path, Name);
        entity.Type = Type.Name;
        entity.ValueJson = JsonConvert.SerializeObject(Value);
        entity.CharacterId = CharacterId;
    }

    public void Load(Parameter entity)
    {
        if (Id != entity.Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);

        Path = PathHelper.GetPath(entity.Fullname);
        Name = PathHelper.GetName(entity.Fullname);
        Value = JsonConvert.DeserializeObject<T>(entity.ValueJson);
        CharacterId = entity.CharacterId;
    }

    #endregion
}