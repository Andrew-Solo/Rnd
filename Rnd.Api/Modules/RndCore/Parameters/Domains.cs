using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Parameters;

namespace Rnd.Api.Modules.RndCore.Parameters;

public class Domains : IEnumerable<Domain>, IParametersProvider
{
    public Domains(
        int? war = null, 
        int? mist = null, 
        int? way = null, 
        int? word = null, 
        int? lore = null, 
        int? work = null, 
        int? art = null)
    {
        War = new Domain(DomainType.War) { Value = war ?? Default };
        Mist = new Domain(DomainType.Mist) { Value = mist ?? Default };
        Way = new Domain(DomainType.Way) { Value = way ?? Default };
        Word = new Domain(DomainType.Word) { Value = word ?? Default };
        Lore = new Domain(DomainType.Lore) { Value = lore ?? Default };
        Work = new Domain(DomainType.Work) { Value = work ?? Default };
        Art = new Domain(DomainType.Art) { Value = art ?? Default };
    }

    public const int Default = 4;

    public Domain War { get; }
    public Domain Mist { get; }
    public Domain Way { get; }
    public Domain Word { get; }
    public Domain Lore { get; }
    public Domain Work { get; }
    public Domain Art { get; }
    
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