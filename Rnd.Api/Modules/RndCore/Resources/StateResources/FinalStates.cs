using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Resources.StateResources;

public class FinalStates : States
{
    public FinalStates(Character character) : base(character, character.Final.Attributes, character.Leveling) { }

    //TODO Modify by all method
    public override State Body => GetState(StateType.Body, Attributes.Endurance.PassiveValue, true);
    public override State Will => GetState(StateType.Will, Attributes.Determinism.PassiveValue, true);
    public override State Armor => GetState(StateType.Armor, 0, true);
    public override State Barrier => GetState(StateType.Barrier, 0, true);
    public override State Energy => GetState(StateType.Energy, Leveling.GetMaxEnergy(), true);
}