using Rnd.Api.Client.Models.Basic.Member;

namespace Rnd.Api.Client.Controllers.Basic;

public class MembersSelector : Selector<MemberModel, MemberFormModel>
{
    public MembersSelector(HttpClient client, Uri path, IController<MemberModel, MemberFormModel> controller) 
        : base(client, path, controller) { }
}