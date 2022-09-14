using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Fields;

namespace Rnd.Api.Modules.RndCore.Fields;

public class Backstory : IEnumerable<IField>, IFieldsProvider
{
    public Backstory(ICharacter character, string? brief = null, string? culture = null, string? society = null, string? traditions = null, string? mentor = null)
    {
        Brief = new ParagraphField(character, nameof(Backstory), nameof(Brief), brief);
        Culture = new MediumField(character, nameof(Backstory), nameof(Culture), culture);
        Society = new MediumField(character, nameof(Backstory), nameof(Society), society);
        Traditions = new MediumField(character, nameof(Backstory), nameof(Traditions), traditions);
        Mentor = new MediumField(character, nameof(Backstory), nameof(Mentor), mentor);
    }
    
    public ParagraphField Brief { get; }
    public MediumField Culture { get; }
    public MediumField Society { get; }
    public MediumField Traditions { get; }
    public MediumField Mentor { get; }
    
    #region IEnumerable

    public IEnumerator<IField> GetEnumerator()
    {
        return new BasicEnumerator<IField>(Brief, Culture, Society, Traditions, Mentor);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    #endregion

    #region IParametersProvider

    public IEnumerable<IField> Fields => this;

    #endregion
}