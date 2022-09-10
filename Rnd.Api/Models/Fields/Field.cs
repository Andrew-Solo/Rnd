using Newtonsoft.Json;
using Rnd.Api.Data.Entities;
using Rnd.Api.Helpers;

namespace Rnd.Api.Models.Fields;

public abstract class Field<T> : IField where T : notnull
{
    protected Field(Guid id, string group, string name)
    {
        Id = id;
        Path = group;
        Name = name;
    }

    public Guid Id { get; private set; }
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
}