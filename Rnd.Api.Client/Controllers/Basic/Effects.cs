using Rnd.Api.Client.Models.Basic.Effect;

namespace Rnd.Api.Client.Controllers.Basic;

public class Effects : Controller<EffectModel, EffectFormModel, EffectsSelector>
{
    public Effects(HttpClient client, Uri uri, bool suppressEmbedding = false) : base(client, uri, suppressEmbedding) { }

    protected override string Name => nameof(Effects);
}