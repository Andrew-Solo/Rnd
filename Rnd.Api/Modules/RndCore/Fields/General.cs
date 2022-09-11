using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Fields;

namespace Rnd.Api.Modules.RndCore.Fields;

public class General : IEnumerable<IField>, IFieldsProvider
{
    public General(string? culture = null, int? age = null, 
        List<string>? ideals = null, List<string>? vices = null, List<string>? traits = null)
    {
        Culture = new ShortField(nameof(General), nameof(Culture), culture);
        Age = new NumberField(nameof(General), nameof(Age), age);
        Ideals = new ListField(nameof(General), nameof(Ideals), ideals);
        Vices = new ListField(nameof(General), nameof(Vices), vices);
        Traits = new ListField(nameof(General), nameof(Traits), traits);
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