using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Fields;

namespace Rnd.Api.Modules.RndCore.Fields;

public class Additional : IEnumerable<IField>, IFieldsProvider
{
    public Additional(ICharacter character)
    {
        Character = character;
    }
    
    public ICharacter Character { get; }

    public ListField Goals => GetListField(nameof(Goals));
    public ListField Outlook => GetListField(nameof(Outlook));
    public ListField Lifepath => GetListField(nameof(Lifepath));
    public ListField Habits => GetListField(nameof(Habits));
    
    private ListField GetListField(string name)
    {
        return Character.Fields.FirstOrDefault(p => p.Path == nameof(Additional) && p.Name == name) as ListField 
               ?? new ListField(Character, name, path: nameof(Additional));
    }
    
    #region IEnumerable

    public IEnumerator<IField> GetEnumerator()
    {
        return new BasicEnumerator<IField>(Goals, Outlook, Lifepath, Habits);
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