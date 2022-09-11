namespace Rnd.Api.Modules.Basic.Parameters;

public interface IParametersProvider
{
    public IEnumerable<IParameter> Parameters { get; }
}