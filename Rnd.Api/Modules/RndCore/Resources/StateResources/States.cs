using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Resources;
using Rnd.Api.Modules.RndCore.Characters;
using Rnd.Api.Modules.RndCore.Parameters.AttributeParameters;

namespace Rnd.Api.Modules.RndCore.Resources.StateResources;

public class States : IEnumerable<State>, IResourcesProvider
{
    #region StateList

    public States(ICharacter character, Attributes attributes, Leveling leveling)
    {
        Character = character;
        Attributes = attributes;
        Leveling = leveling;
    }
    
    public ICharacter Character { get; }
    protected readonly Attributes Attributes;
    protected readonly Leveling Leveling;
    
    public virtual State Body => GetState(StateType.Body, Attributes.Endurance.PassiveValue);
    public virtual State Will => GetState(StateType.Will, Attributes.Determinism.PassiveValue);
    public virtual State Armor => GetState(StateType.Armor, 0);
    public virtual State Barrier => GetState(StateType.Barrier, 0);
    public virtual State Energy => GetState(StateType.Energy, Leveling.GetMaxEnergy());

    protected State GetState(StateType type, int max, bool isFinal = false)
    {
        var path = PathHelper.Combine(isFinal ? nameof(Final) : null, nameof(State));
        return Character.Resources.FirstOrDefault(r => r.Path == path && r.Name == type.ToString()) as State 
               ?? (isFinal ? new FinalState(Character, GetState(type, max)) : new State(Character, type, max));
    }

    #endregion
    
    public void FillDefaults()
    {
        var objects = new object[] { Body, Will, Armor, Barrier, Energy };
    }
    
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