using Rnd.Api.Modules.Basic.Characters;

namespace Rnd.Api.Modules.Basic.Resources;

public class StrictResource : Resource
{
    public StrictResource(ICharacter character, string name) : base(character, name) { }

    public new decimal Min
    {
        get => base.Min.GetValueOrDefault();
        set => base.Min = value;
    }

    public new decimal Max
    {
        get => base.Max.GetValueOrDefault();
        set => base.Max = value;
    }
}