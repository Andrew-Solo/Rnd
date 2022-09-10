namespace Rnd.Api.Logic.Models.Parameters;

public class Parameter<T> : IParameter where T : notnull
{
    public Parameter(Guid id, string group, string name)
    {
        Id = id;
        Path = group;
        Name = name;
    }
    
    public Guid Id { get; }
    public string? Path { get; }
    public string Name { get; }
    public T? Value { get; set; }

    #region IParameter

    public Type Type => typeof(T);

    object? IParameter.Value
    {
        get => Value;
        set => Value = (T?) value;
    }

    #endregion
}