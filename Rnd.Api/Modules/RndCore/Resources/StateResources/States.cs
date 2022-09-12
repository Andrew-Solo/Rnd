using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Resources;
using Rnd.Api.Modules.RndCore.Characters;
using Rnd.Api.Modules.RndCore.Parameters.AttributeParameters;

namespace Rnd.Api.Modules.RndCore.Resources.StateResources;

public class States : IEnumerable<State>, IResourcesProvider
{
    #region StateList

    public States(Attributes attributes, Leveling leveling)
    {
        Body = new State(StateType.Body, attributes.Endurance.PassiveValue);
        Will = new State(StateType.Will, attributes.Determinism.PassiveValue);
        Armor = new State(StateType.Armor, 0);
        Barrier = new State(StateType.Barrier, 0);
        Energy = new State(StateType.Energy, leveling.GetMaxEnergy());
    }
    
    public virtual State Body { get; }
    public virtual State Will { get; }
    public virtual State Armor { get; }
    public virtual State Barrier { get; }
    public virtual State Energy { get; }

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