using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.RndCore.Characters;

namespace Rnd.Api.Modules.RndCore.Parameters.AttributeParameters;

public class Attributes : IEnumerable<Attribute>, IParametersProvider
{
    #region AttributeList

    public Attributes(ICharacter character)
    {
        Character = character;
    }
    
    public ICharacter Character { get; }

    public virtual Attribute Strength => GetAttribute(AttributeType.Strength);
    public virtual Attribute Endurance => GetAttribute(AttributeType.Endurance);
    public virtual Attribute Dexterity => GetAttribute(AttributeType.Dexterity);
    public virtual Attribute Perception => GetAttribute(AttributeType.Perception);
    public virtual Attribute Intellect => GetAttribute(AttributeType.Intellect);
    public virtual Attribute Wisdom => GetAttribute(AttributeType.Wisdom);
    public virtual Attribute Charisma => GetAttribute(AttributeType.Charisma);
    public virtual Attribute Determinism => GetAttribute(AttributeType.Determinism);

    protected Attribute GetAttribute(AttributeType type, bool isFinal = false)
    {
        var path = PathHelper.Combine(isFinal ? nameof(Final) : null, nameof(Attribute));
        return Character.Parameters.FirstOrDefault(p => p.Path == path && p.Name == type.ToString()) as Attribute 
               ?? (isFinal ? CreateFinal(type) : new Attribute(Character, type));
    }

    private Attribute CreateFinal(AttributeType type)
    {
        return Character.Effects.Aggregate(
            new FinalAttribute(Character, GetAttribute(type)), 
            (attribute, effect) => effect.ModifyParameter(attribute));
    }

    #endregion
    
    public void CreateItems()
    {
        var objects = new object[] { Strength, Endurance, Dexterity, Perception, Intellect, Wisdom, Charisma, Determinism };
    }
    
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