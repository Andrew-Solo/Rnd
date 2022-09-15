using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Resources.StateResources;

public class FinalState : State
{
    public FinalState(ICharacter character, State original) : base(character, original.StateType, original.Max, original.Value)
    {
        
    }

    public override string Path => Helpers.Path.Combine(nameof(Final), base.Path);
    public override bool Virtual => true;
}