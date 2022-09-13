using Newtonsoft.Json;
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Helpers;
using Rnd.Api.Localization;

namespace Rnd.Api.Modules.Basic.Fields;

public abstract class Field<T> : IField where T : notnull
{
    protected Field(string path, string name)
    {
        Path = path;
        Name = name;
        
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public Guid CharacterId { get; set; }
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
    
    public IStorable<Field> AsStorable => this;

    public Field? Save(Field? entity)
    {
        entity ??= new Field {Id = Id};
        if (AsStorable.NotSave(entity)) return null;
        
        entity.Fullname = PathHelper.Combine(Path, Name);
        entity.Type = Type;
        entity.ValueJson = JsonConvert.SerializeObject(Value);
        entity.CharacterId = CharacterId;

        return entity;
    }

    public void Load(Field entity)
    {
        if (AsStorable.NotLoad(entity)) return;

        Path = PathHelper.GetPath(entity.Fullname);
        Name = PathHelper.GetName(entity.Fullname);
        Value = JsonConvert.DeserializeObject<T>(entity.ValueJson);
        CharacterId = entity.CharacterId;
    }

    #endregion
}