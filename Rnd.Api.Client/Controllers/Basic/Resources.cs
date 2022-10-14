using Rnd.Api.Client.Models.Basic.Resource;

namespace Rnd.Api.Client.Controllers.Basic;

public class Resources : Controller<ResourceModel, ResourceFormModel, EmptySelector>
{
    public Resources(HttpClient client, Uri uri, bool suppressEmbedding = false) : base(client, uri, suppressEmbedding) { }

    protected override string Name => nameof(Resources);
}