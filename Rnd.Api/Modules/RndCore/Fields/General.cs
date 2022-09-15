using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Fields;

namespace Rnd.Api.Modules.RndCore.Fields;

public class General : IEnumerable<IField>, IFieldsProvider
{
    public General(ICharacter character)
    {
        Character = character;
    }
    
    public ICharacter Character { get; }

    public ShortField Culture => GetShortField(nameof(Culture));
    public NumberField Age => GetNumberField(nameof(Age));
    public ListField Ideals => GetListField(nameof(Ideals));
    public ListField Vices => GetListField(nameof(Vices));
    public ListField Traits => GetListField(nameof(Traits));
    
    public void CreateItems()
    {
        var objects = new object[] { Culture, Age, Ideals, Vices, Traits };
    }
    
    private ShortField GetShortField(string name)
    {
        return Character.Fields.FirstOrDefault(p => p.Path == nameof(General) && p.Name == name) as ShortField 
               ?? new ShortField(Character, name, path: nameof(General));
    }
    
    private NumberField GetNumberField(string name)
    {
        return Character.Fields.FirstOrDefault(p => p.Path == nameof(General) && p.Name == name) as NumberField 
               ?? new NumberField(Character, name, path: nameof(General));
    }
    
    private ListField GetListField(string name)
    {
        return Character.Fields.FirstOrDefault(p => p.Path == nameof(General) && p.Name == name) as ListField 
               ?? new ListField(Character, name, path: nameof(General));
    }
    
    #region IEnumerable

    public IEnumerator<IField> GetEnumerator()
    {
        return new BasicEnumerator<IField>(Culture, Age, Ideals, Vices, Traits);
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