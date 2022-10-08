using Rnd.Api.Client.Models.Basic.Member;

namespace Rnd.Api.Client.Controllers.Basic;

public class Members : Controller<MemberModel, MemberInviteModel, MemberEditModel, MembersSelector>
{
    public Members(HttpClient client, string path, bool suppressEmbedding) : base(client, path, suppressEmbedding) { }

    protected override string Name => nameof(Members);
}