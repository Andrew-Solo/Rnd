using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;

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
    
    private Domain GetDomain(DomainType type)
    {
        return Character.Parameters.FirstOrDefault(p => p.Path == nameof(Domain) && p.Name == type.ToString()) as Domain 
               ?? new Domain(Character, type);
    }

    #endregion
    
    public void FillDefaults()
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