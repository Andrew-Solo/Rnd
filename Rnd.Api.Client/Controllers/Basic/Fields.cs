using Rnd.Api.Client.Models.Basic.Field;

namespace Rnd.Api.Client.Controllers.Basic;

public class Fields : Controller<FieldModel, FieldFormModel, EmptySelector>
{
    public Fields(HttpClient client, Uri uri, bool suppressEmbedding = false) : base(client, uri, suppressEmbedding) { }

    protected override string Name => nameof(Fields);
}