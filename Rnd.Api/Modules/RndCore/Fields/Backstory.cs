using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Fields;

namespace Rnd.Api.Modules.RndCore.Fields;

public class Backstory : IEnumerable<IField>, IFieldsProvider
{
    public Backstory(ICharacter character, string? brief = null, string? culture = null, string? society = null, string? traditions = null, string? mentor = null)
    {
        Brief = new ParagraphField(character, nameof(Brief), value: brief, path: nameof(Backstory));
        Culture = new MediumField(character, nameof(Culture), value: culture, path: nameof(Backstory));
        Society = new MediumField(character, nameof(Society), value: society, path: nameof(Backstory));
        Traditions = new MediumField(character, nameof(Traditions), value: traditions, path: nameof(Backstory));
        Mentor = new MediumField(character, nameof(Mentor), value: mentor, path: nameof(Backstory));
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