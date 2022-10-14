using Rnd.Api.Client.Models.Basic.Field;

namespace Rnd.Api.Client.Controllers.Basic;

public class Fields : Controller<FieldModel, FieldAddModel, FieldEditModel, EmptySelector>
{
    public Fields(HttpClient client, Uri path, bool suppressEmbedding = false) : base(client, path, suppressEmbedding) { }

    protected override string Name => nameof(Fields);
}