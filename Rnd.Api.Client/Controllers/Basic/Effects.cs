using Rnd.Api.Client.Models.Basic.Effect;

namespace Rnd.Api.Client.Controllers.Basic;

public class Effects : Controller<EffectModel, EffectAddModel, EffectEditModel, EmptySelector>
{
    public Effects(HttpClient client, string path, bool suppressEmbedding = false) : base(client, path, suppressEmbedding) { }

    protected override string Name => nameof(Effects);
}