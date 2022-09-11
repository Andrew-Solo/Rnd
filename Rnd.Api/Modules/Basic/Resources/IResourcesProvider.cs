namespace Rnd.Api.Modules.Basic.Resources;

public interface IResourcesProvider
{
    public IEnumerable<IResource> Resources { get; }
}