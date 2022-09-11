using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class Domains : IEnumerable<Domain>, IParametersProvider
{
    #region DomainList

    public Domains(
        int? war = null, 
        int? mist = null, 
        int? way = null, 
        int? word = null, 
        int? lore = null, 
        int? work = null, 
        int? art = null)
    {
        War = new Domain(DomainType.War, war);
        Mist = new Domain(DomainType.Mist, mist);
        Way = new Domain(DomainType.Way, way);
        Word = new Domain(DomainType.Word, word);
        Lore = new Domain(DomainType.Lore, lore);
        Work = new Domain(DomainType.Work, work);
        Art = new Domain(DomainType.Art, art);
    }

    public Domain War { get; }
    public Domain Mist { get; }
    public Domain Way { get; }
    public Domain Word { get; }
    public Domain Lore { get; }
    public Domain Work { get; }
    public Domain Art { get; }

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