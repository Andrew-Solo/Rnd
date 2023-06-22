namespace Rnd.Data;

public class Path
{
    public Path(string value)
    {
        Value = value;
    }
    
    public string Value { get; private set; }
    public Guid? Id => Guid.TryParse(Value, out var value) ? value : null;
    public bool IsId => Id != null;

    public bool IsAuto => Value is "@me" or "@first";
    public bool IsNone => Value is "@none" or "@guest";
    public bool IsEmpty => string.IsNullOrWhiteSpace(Value);

    public void Set(string value)
    {
        Value = value;
    }
}