namespace Rnd.Api.Modules.Basic.Fields;

public abstract class TextField : Field<string>
{
    protected TextField(string path, string name) : base(path, name) { }
    
    public abstract int MaxLength { get; }
}