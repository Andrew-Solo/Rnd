using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Resources;
using Character = Rnd.Api.Modules.RndCore.Characters.Character;

namespace Rnd.Api.Modules.RndCore.Resources;

public class States : IEnumerable<State>, IResourcesProvider
{
    #region StateList

    public States(Character character)
    {
        Body = new State(StateType.Body, character.Attributes.Endurance.PassiveValue);
        Will = new State(StateType.Will, character.Attributes.Determinism.PassiveValue);
        Armor = new State(StateType.Armor, 0);
        Barrier = new State(StateType.Barrier, 0);
        Energy = new State(StateType.Energy, character.Leveling.GetMaxEnergy());
    }
    
    public State Body { get; }
    public State Will { get; }
    public State Armor { get; }
    public State Barrier { get; }
    public State Energy { get; }

    #endregion
    
    #region IEnumerable

    public IEnumerator<State> GetEnumerator()
    {
        return new BasicEnumerator<State>(Body, Will, Armor, Barrier, Energy);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion

    #region IResourcesProvider

    public IEnumerable<IResource> Resources => this;

    #endregion
}