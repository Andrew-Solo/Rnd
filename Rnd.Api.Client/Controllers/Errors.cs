namespace Rnd.Api.Client.Controllers;

public class Errors : Dictionary<string, string[]>
{
    public Errors() {}
    public Errors(IDictionary<string, string[]> dictionary) : base(dictionary) {}
    
    public override string ToString()
    {
        return String.Join("\n", this.Select(pair => $"{pair.Key}: {String.Join(", ", pair.Value)}"));
    }
}