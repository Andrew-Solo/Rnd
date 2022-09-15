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
        _attributes = attributes;
        _leveling = leveling;
    }
    
    public ICharacter Character { get; }
    
    public virtual State Body => GetState(StateType.Armor, _attributes.Endurance.PassiveValue);
    public virtual State Will => GetState(StateType.Armor, _attributes.Determinism.PassiveValue);
    public virtual State Armor => GetState(StateType.Armor, 0);
    public virtual State Barrier => GetState(StateType.Armor, 0);
    public virtual State Energy => GetState(StateType.Armor, _leveling.GetMaxEnergy());

    private readonly Attributes _attributes;
    private readonly Leveling _leveling;
    
    private State GetState(StateType type, int max)
    {
        return Character.Resources.FirstOrDefault(r => r.Path == nameof(State) && r.Name == type.ToString()) as State 
               ?? new State(Character, type, max);
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