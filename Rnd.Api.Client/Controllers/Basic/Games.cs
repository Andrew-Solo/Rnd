using Rnd.Api.Client.Models.Basic.Game;

namespace Rnd.Api.Client.Controllers.Basic;

public class Games : Controller<GameModel, GameCreateModel, GameEditModel, GamesSelector>
{
    public Games(HttpClient client, Uri uri, bool suppressEmbedding = false) : base(client, uri, suppressEmbedding) { }

    protected override string Name => nameof(Games);
}