using Rnd.Api.Client.Models.Basic.Member;

namespace Rnd.Api.Client.Controllers.Basic;

public class Members : Controller<MemberModel, MemberFormModel, MembersSelector>
{
    public Members(HttpClient client, Uri uri) : base(client, uri) { }

    protected override string Name => nameof(Members);
}