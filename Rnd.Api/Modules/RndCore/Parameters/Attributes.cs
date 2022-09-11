using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class Attributes : IEnumerable<Attribute>, IParametersProvider
{
    public Attributes(
        int? strength = null, int? endurance = null, 
        int? dexterity = null, int? perception = null, 
        int? intellect = null, int? wisdom = null, 
        int? charisma = null, int? determinism = null)
    {
        Strength = new Attribute(AttributeType.Strength) { Value = strength ?? Default };
        Endurance = new Attribute(AttributeType.Endurance) { Value = endurance ?? Default };
        Dexterity = new Attribute(AttributeType.Dexterity) { Value = dexterity ?? Default };
        Perception = new Attribute(AttributeType.Perception) { Value = perception ?? Default };
        Intellect = new Attribute(AttributeType.Intellect) { Value = intellect ?? Default };
        Wisdom = new Attribute(AttributeType.Wisdom) { Value = wisdom ?? Default };
        Charisma = new Attribute(AttributeType.Charisma) { Value = charisma ?? Default };
        Determinism = new Attribute(AttributeType.Determinism) { Value = determinism ?? Default };
    }
    
    public const int Default = 0;

    public Attribute Strength { get; }
    public Attribute Endurance { get; }
    public Attribute Dexterity { get; }
    public Attribute Perception { get; }
    public Attribute Intellect { get; }
    public Attribute Wisdom { get; }
    public Attribute Charisma { get; }
    public Attribute Determinism { get; }
    
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