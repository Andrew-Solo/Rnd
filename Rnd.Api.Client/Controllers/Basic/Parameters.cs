using Rnd.Api.Client.Models.Basic.Parameter;

namespace Rnd.Api.Client.Controllers.Basic;

public class Parameters : Controller<ParameterModel, ParameterFormModel, EmptySelector<ParameterModel, ParameterFormModel>>
{
    public Parameters(HttpClient client, Uri uri, bool suppressEmbedding = false) : base(client, uri, suppressEmbedding) { }

    protected override string Name => nameof(Parameters);
}