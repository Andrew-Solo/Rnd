using Rnd.Api.Client.Models.Basic.Game;
using Rnd.Api.Client.Responses;

namespace Rnd.Api.Client.Controllers.Basic;

public class Games : Controller<GameModel, GameFormModel, GamesSelector>
{
    public Games(HttpClient client, Uri uri) : base(client, uri) { }

    protected override string Name => nameof(Games);
    
    public virtual async Task<Response<GameModel>> SelectAsync(Guid id)
    {
        var response = await Client.GetAsync(GetUri(id, nameof(SelectAsync).Replace("Async", "")));
        return await Response<GameModel>.Create(response);
    }
}