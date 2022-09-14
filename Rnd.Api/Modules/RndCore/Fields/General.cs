﻿using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Fields;

namespace Rnd.Api.Modules.RndCore.Fields;

public class General : IEnumerable<IField>, IFieldsProvider
{
    public General(ICharacter character, string? culture = null, int? age = null, 
        List<string>? ideals = null, List<string>? vices = null, List<string>? traits = null)
    {
        Culture = new ShortField(character, nameof(Culture), value: culture, path: nameof(General));
        Age = new NumberField(character, nameof(Age), age, nameof(General));
        Ideals = new ListField(character, nameof(Ideals), ideals, nameof(General));
        Vices = new ListField(character, nameof(Vices), vices, nameof(General));
        Traits = new ListField(character, nameof(Traits), traits, nameof(General));
    }

    public ShortField Culture { get; }
    public NumberField Age { get; }
    public ListField Ideals { get; }
    public ListField Vices { get; }
    public ListField Traits { get; }
    
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