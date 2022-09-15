using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Resources.StateResources;

public class FinalStates : States
{
    public FinalStates(Character character) : base(character, character.Final.Attributes, character.Leveling) { }

    //TODO Modify by all method
    public override State Body => Character.Effects
        .Aggregate(GetState(StateType.Body, Attributes.Endurance.PassiveValue, true), (attribute, effect) => effect.ModifyResource(attribute));
    public override State Will => Character.Effects
        .Aggregate(GetState(StateType.Will, Attributes.Determinism.PassiveValue, true), (attribute, effect) => effect.ModifyResource(attribute));
    public override State Armor => Character.Effects
        .Aggregate(GetState(StateType.Armor, 0, true), (attribute, effect) => effect.ModifyResource(attribute));
    public override State Barrier => Character.Effects
        .Aggregate(GetState(StateType.Barrier, 0, true), (attribute, effect) => effect.ModifyResource(attribute));
    public override State Energy => Character.Effects
        .Aggregate(GetState(StateType.Energy, Leveling.GetMaxEnergy(), true), (attribute, effect) => effect.ModifyResource(attribute));
}