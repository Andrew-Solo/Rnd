using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Fields;

public abstract class TextField : Field<string>
{
    protected TextField(ICharacter character, string name, string? path = null) : base(character, name, path) { }
    
    public abstract int MaxLength { get; }
}