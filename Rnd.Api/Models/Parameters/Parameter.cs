namespace Rnd.Api.Models.Parameters;

public class Parameter<T> : IParameter where T : notnull
{
    public Parameter(string group, string name)
    {
        Group = group;
        Name = name;
    }
    
    public string Group { get; }
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