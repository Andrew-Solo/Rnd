namespace Rnd.Api.Modules.Basic.Fields;

public interface IFieldsProvider
{
    public IEnumerable<IField> Fields { get; }
}