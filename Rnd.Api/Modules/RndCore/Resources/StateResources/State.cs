using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Resources;

namespace Rnd.Api.Modules.RndCore.Resources.StateResources;

public class State : StrictResource
{
    public State(ICharacter character, StateType stateType, decimal max, decimal? value = null) : base(character, stateType.ToString())
    {
        Min = 0;
        Max = max;
        Value = value ?? Max;
        StateType = stateType;
    }
    
    public StateType StateType { get; }
    public override string Path => nameof(State);
}