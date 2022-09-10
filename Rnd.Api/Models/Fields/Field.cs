namespace Rnd.Api.Models.Fields;

public abstract class Field<T> : IField where T : notnull
{
    protected Field(string group, string name)
    {
        Group = group;
        Name = name;
    }

    public string Group { get; }
    public string Name { get; }
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