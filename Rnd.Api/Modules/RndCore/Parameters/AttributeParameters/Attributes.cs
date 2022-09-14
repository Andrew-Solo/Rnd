using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters.AttributeParameters;

public class Attributes : IEnumerable<Attribute>, IParametersProvider
{
    #region AttributeList

    public Attributes(ICharacter character,
        int? strength = null, int? endurance = null, 
        int? dexterity = null, int? perception = null, 
        int? intellect = null, int? wisdom = null, 
        int? charisma = null, int? determinism = null)
    {
        Strength = new Attribute(character, AttributeType.Strength, strength);
        Endurance = new Attribute(character, AttributeType.Endurance, endurance);
        Dexterity = new Attribute(character, AttributeType.Dexterity, dexterity);
        Perception = new Attribute(character, AttributeType.Perception, perception);
        Intellect = new Attribute(character, AttributeType.Intellect, intellect);
        Wisdom = new Attribute(character, AttributeType.Wisdom, wisdom);
        Charisma = new Attribute(character, AttributeType.Charisma, charisma);
        Determinism = new Attribute(character, AttributeType.Determinism, determinism);
    }

    public virtual Attribute Strength { get; }
    public virtual Attribute Endurance { get; }
    public virtual Attribute Dexterity { get; }
    public virtual Attribute Perception { get; }
    public virtual Attribute Intellect { get; }
    public virtual Attribute Wisdom { get; }
    public virtual Attribute Charisma { get; }
    public virtual Attribute Determinism { get; }

    #endregion
    
    #region IEnumerable

    public IEnumerator<Attribute> GetEnumerator()
    {
        return new BasicEnumerator<Attribute>(Strength, Endurance, Dexterity, Perception, Intellect, Wisdom, Charisma, Determinism);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion

    #region IParametersProvider

    public IEnumerable<IParameter> Parameters => this;

    #endregion
}