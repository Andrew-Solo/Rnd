using Rnd.Api.Modules.Basic.Effects;

namespace Rnd.Api.Modules.RndCore.Effects;

public class Custom : Effect
{
    public Custom(string name) : base(name)
    { }

    public override string Path => nameof(Custom);
}