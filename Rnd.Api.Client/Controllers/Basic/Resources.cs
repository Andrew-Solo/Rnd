using Rnd.Api.Client.Models.Basic.Resource;

namespace Rnd.Api.Client.Controllers.Basic;

public class Resources : Controller<ResourceModel, ResourceAddModel, ResourceEditModel, EmptySelector>
{
    public Resources(HttpClient client, string path, bool suppressEmbedding = false) : base(client, path, suppressEmbedding) { }

    protected override string Name => nameof(Resources);
}