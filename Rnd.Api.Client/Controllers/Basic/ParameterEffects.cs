using Rnd.Api.Client.Models.Basic.ParameterEffect;

namespace Rnd.Api.Client.Controllers.Basic;

public class ParameterEffects : Controller<ParameterEffectModel, ParameterEffectAddModel, ParameterEffectEditModel, EmptySelector>
{
    public ParameterEffects(HttpClient client, Uri path, bool suppressEmbedding = false) : base(client, path, suppressEmbedding) { }

    protected override string Name => nameof(ParameterEffects);
}