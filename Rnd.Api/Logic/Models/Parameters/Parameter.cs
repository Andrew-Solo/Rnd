using Newtonsoft.Json;
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Logic.Helpers;
using Rnd.Api.Logic.Localization;

namespace Rnd.Api.Logic.Models.Parameters;

public class Parameter<T> : IParameter, IStorable<Parameter> where T : notnull
{
    public Parameter(Guid id, string group, string name)
    {
        Id = id;
        Path = group;
        Name = name;
    }
    
    public Guid Id { get; }
    public string? Path { get; private set; }
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

    public void Save(Parameter entity)
    {
        if (entity.Id != Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);
        
        entity.Fullname = PathHelper.Combine(Path, Name);
        entity.Type = Type.Name;
        entity.ValueJson = JsonConvert.SerializeObject(Value);
    }

    public void Load(Parameter entity)
    {
        if (Id != entity.Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);

        Path = PathHelper.GetPath(entity.Fullname);
        Name = PathHelper.GetName(entity.Fullname);
        Value = JsonConvert.DeserializeObject<T>(entity.ValueJson);
    }

    #endregion
}