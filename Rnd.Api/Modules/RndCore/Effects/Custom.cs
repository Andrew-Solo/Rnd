using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Effects;

namespace Rnd.Api.Modules.RndCore.Effects;

public class Custom : Effect
{
    public Custom(ICharacter character, string name) : base(character, name)
    { }

    public override string Path => nameof(Custom);
}