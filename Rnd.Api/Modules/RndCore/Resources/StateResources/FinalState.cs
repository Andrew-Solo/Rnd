using Rnd.Api.Helpers;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Resources.StateResources;

public class FinalState : State
{
    public FinalState(State original) : base(original.StateType, original.Value)
    {
        
    }

    public override string Path => PathHelper.Combine(nameof(Final), base.Path);
}