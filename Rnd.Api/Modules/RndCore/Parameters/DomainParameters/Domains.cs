using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;
using Rnd.Api.Modules.RndCore.Characters;
using Path = Rnd.Api.Helpers.Path;

namespace Rnd.Api.Modules.RndCore.Parameters.DomainParameters;

public class Domains : IEnumerable<Domain>, IParametersProvider
{
    #region DomainList

    public Domains(ICharacter character)
    {
        Character = character;
    }
    
    public ICharacter Character { get; }

    public virtual Domain War => GetDomain(DomainType.War);
    public virtual Domain Mist => GetDomain(DomainType.Mist);
    public virtual Domain Way => GetDomain(DomainType.Way);
    public virtual Domain Word => GetDomain(DomainType.Word);
    public virtual Domain Lore => GetDomain(DomainType.Lore);
    public virtual Domain Work => GetDomain(DomainType.Work);
    public virtual Domain Art => GetDomain(DomainType.Art);
    
    protected Domain GetDomain(DomainType type, bool isFinal = false)
    {
        var path = Path.Combine(isFinal ? nameof(Final) : null, nameof(Domain));
        return Character.Parameters.FirstOrDefault(p => p.Path == path && p.Name == type.ToString()) as Domain 
               ?? (isFinal ? CreateFinal(type) : new Domain(Character, type));
    }
    
    private Domain CreateFinal(DomainType type)
    {
        return Character.Effects.Aggregate(
            new FinalDomain(Character, GetDomain(type)), 
            (domain, effect) => effect.ModifyParameter(domain));
    }

    #endregion
    
    public void CreateItems()
    {
        var objects = new object[] { War, Mist, Way, Word, Lore, Work, Art };
    }
    
    #region IEnumerable

    public IEnumerator<Domain> GetEnumerator()
    {
        return new BasicEnumerator<Domain>(War, Mist, Way, Word, Lore, Work, Art);
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