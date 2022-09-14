using Newtonsoft.Json;
using Rnd.Api.Data;
using Rnd.Api.Data.Entities;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Fields;

public abstract class Field<T> : IField where T : notnull
{
    protected Field(IEntity entity)
    {
        Id = entity.Id;
        
        Name = null!;
    }
    
    protected Field(ICharacter character, string name, string? path = null)
    {
        CharacterId = character.Id;
        Path = path;
        Name = name;
        
        Id = Guid.NewGuid();
        
        character.Fields.Add(this);
    }

    public Guid Id { get; }
    public Guid CharacterId { get; private set; }
    public virtual string? Path { get; private set; }
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

    public Field? Save(Field? entity, bool upcome = true)
    {
        entity ??= new Field {Id = Id};
        if (AsStorable.NotSave(entity)) return null;
        
        entity.Fullname = PathHelper.Combine(Path, Name);
        entity.Type = Type;
        entity.ValueJson = JsonConvert.SerializeObject(Value);
        entity.CharacterId = CharacterId;

        return entity;
    }

    public IStorable<Field>? Load(Field entity, bool upcome = true)
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