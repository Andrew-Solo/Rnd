using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Fields;

public abstract class TextField : Field<string>
{
    protected TextField(ICharacter character, string path, string name) : base(character, path, name) { }
    
    public abstract int MaxLength { get; }
}