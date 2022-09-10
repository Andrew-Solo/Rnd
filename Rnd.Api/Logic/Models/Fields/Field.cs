using Newtonsoft.Json;
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Logic.Helpers;
using Rnd.Api.Logic.Localization;

namespace Rnd.Api.Logic.Models.Fields;

public abstract class Field<T> : IField, IStorable<Field> where T : notnull
{
    protected Field(Guid id, string path, string name)
    {
        Id = id;
        Path = path;
        Name = name;
    }

    public Guid Id { get; }
    public string? Path { get; private set; }
    public string Name { get; private set; }
    public T? Value { get; set; }

    #region IField

    public abstract FieldType Type { get; }

    object? IField.Value
    {
        get => Value;
        set => Value = (T?) value;
    }

    #endregion

    #region IStorable

    public void Save(Field entity)
    {
        if (entity.Id != Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);
        
        entity.Fullname = PathHelper.Combine(Path, Name);
        entity.Type = Type;
        entity.ValueJson = JsonConvert.SerializeObject(Value);
    }

    public void Load(Field entity)
    {
        if (Id != entity.Id) throw new InvalidOperationException(Lang.Exceptions.IStorable.DifferentIds);

        Path = PathHelper.GetPath(entity.Fullname);
        Name = PathHelper.GetName(entity.Fullname);
        Value = JsonConvert.DeserializeObject<T>(entity.ValueJson);
    }

    #endregion
}