using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Fields;

namespace Rnd.Api.Modules.RndCore.Fields;

public class Backstory : IEnumerable<IField>, IFieldsProvider
{
    public Backstory(ICharacter character)
    {
        Character = character;
    }
    
    public ICharacter Character { get; }

    public ParagraphField Brief => GetParagraphField(nameof(Brief));
    public MediumField Culture => GetMediumField(nameof(Culture));
    public MediumField Society => GetMediumField(nameof(Society));
    public MediumField Traditions => GetMediumField(nameof(Traditions));
    public MediumField Mentor => GetMediumField(nameof(Mentor));
    
    public void FillDefaults()
    {
        var objects = new object[] { Brief, Culture, Society, Traditions, Mentor };
    }
    
    private ParagraphField GetParagraphField(string name)
    {
        return Character.Fields.FirstOrDefault(p => p.Path == nameof(Backstory) && p.Name == name) as ParagraphField 
               ?? new ParagraphField(Character, name, path: nameof(Backstory));
    }
    
    private MediumField GetMediumField(string name)
    {
        return Character.Fields.FirstOrDefault(p => p.Path == nameof(Backstory) && p.Name == name) as MediumField 
               ?? new MediumField(Character, name, path: nameof(Backstory));
    }
    
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