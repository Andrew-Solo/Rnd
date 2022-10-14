using Rnd.Api.Client.Models.Basic.ResourceEffect;

namespace Rnd.Api.Client.Controllers.Basic;

public class ResourceEffects : Controller<ResourceEffectModel, ResourceEffectAddModel, ResourceEffectEditModel, EmptySelector>
{
    public ResourceEffects(HttpClient client, Uri path, bool suppressEmbedding = false) : base(client, path, suppressEmbedding) { }

    protected override string Name => nameof(ResourceEffects);
}