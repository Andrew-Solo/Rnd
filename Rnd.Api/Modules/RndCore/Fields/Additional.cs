using System.Collections;
using Rnd.Api.Helpers;
using Rnd.Api.Modules.Basic.Characters;
using Rnd.Api.Modules.Basic.Fields;

namespace Rnd.Api.Modules.RndCore.Fields;

public class Additional : IEnumerable<IField>, IFieldsProvider
{
    public Additional(ICharacter character, List<string>? goals = null, List<string>? outlook = null, List<string>? lifepath = null, List<string>? habits = null)
    {
        Goals = new ListField(character, nameof(Goals), goals, nameof(Additional));
        Outlook = new ListField(character, nameof(Outlook), outlook, nameof(Additional));
        Lifepath = new ListField(character, nameof(Lifepath), lifepath, nameof(Additional));
        Habits = new ListField(character, nameof(Habits), habits, nameof(Additional));
    }
    
    public ListField Goals { get; }
    public ListField Outlook { get; }
    public ListField Lifepath { get; }
    public ListField Habits { get; }
    
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