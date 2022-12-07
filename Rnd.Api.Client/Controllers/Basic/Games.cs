using Rnd.Api.Client.Models.Basic.Game;

namespace Rnd.Api.Client.Controllers.Basic;

public class Games : Controller<GameModel, GameFormModel, GamesSelector>
{
    public Games(HttpClient client, Uri uri) : base(client, uri) { }

    protected override string Name => nameof(Games);
}