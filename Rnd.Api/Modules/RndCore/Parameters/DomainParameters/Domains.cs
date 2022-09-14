using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters.DomainParameters;

public class Domains : IEnumerable<Domain>, IParametersProvider
{
    #region DomainList

    public Domains(ICharacter character,
        int? war = null, 
        int? mist = null, 
        int? way = null, 
        int? word = null, 
        int? lore = null, 
        int? work = null, 
        int? art = null)
    {
        War = new Domain(character, DomainType.War, war);
        Mist = new Domain(character, DomainType.Mist, mist);
        Way = new Domain(character, DomainType.Way, way);
        Word = new Domain(character, DomainType.Word, word);
        Lore = new Domain(character, DomainType.Lore, lore);
        Work = new Domain(character, DomainType.Work, work);
        Art = new Domain(character, DomainType.Art, art);
    }

    public virtual Domain War { get; }
    public virtual Domain Mist { get; }
    public virtual Domain Way { get; }
    public virtual Domain Word { get; }
    public virtual Domain Lore { get; }
    public virtual Domain Work { get; }
    public virtual Domain Art { get; }

    #endregion
    
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