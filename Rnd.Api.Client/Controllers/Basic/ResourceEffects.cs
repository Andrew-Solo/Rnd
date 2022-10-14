using Rnd.Api.Client.Models.Basic.ResourceEffect;

namespace Rnd.Api.Client.Controllers.Basic;

public class ResourceEffects : Controller<ResourceEffectModel, ResourceEffectFormModel, EmptySelector>
{
    public ResourceEffects(HttpClient client, Uri uri, bool suppressEmbedding = false) : base(client, uri, suppressEmbedding) { }

    protected override string Name => nameof(ResourceEffects);
}