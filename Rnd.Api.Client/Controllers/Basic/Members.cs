using Rnd.Api.Client.Models.Basic.Member;

namespace Rnd.Api.Client.Controllers.Basic;

public class Members : Controller<MemberModel, MemberInviteModel, MemberEditModel, MembersSelector>
{
    public Members(HttpClient client, Uri path, bool suppressEmbedding = false) : base(client, path, suppressEmbedding) { }

    protected override string Name => nameof(Members);
}