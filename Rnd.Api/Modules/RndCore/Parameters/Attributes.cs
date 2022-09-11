using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class Attributes : IEnumerable<Attribute>, IParametersProvider
{
    #region AttributeList

    public Attributes(
        int? strength = null, int? endurance = null, 
        int? dexterity = null, int? perception = null, 
        int? intellect = null, int? wisdom = null, 
        int? charisma = null, int? determinism = null)
    {
        Strength = new Attribute(AttributeType.Strength, strength);
        Endurance = new Attribute(AttributeType.Endurance, endurance);
        Dexterity = new Attribute(AttributeType.Dexterity, dexterity);
        Perception = new Attribute(AttributeType.Perception, perception);
        Intellect = new Attribute(AttributeType.Intellect, intellect);
        Wisdom = new Attribute(AttributeType.Wisdom, wisdom);
        Charisma = new Attribute(AttributeType.Charisma, charisma);
        Determinism = new Attribute(AttributeType.Determinism, determinism);
    }

    public Attribute Strength { get; }
    public Attribute Endurance { get; }
    public Attribute Dexterity { get; }
    public Attribute Perception { get; }
    public Attribute Intellect { get; }
    public Attribute Wisdom { get; }
    public Attribute Charisma { get; }
    public Attribute Determinism { get; }

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