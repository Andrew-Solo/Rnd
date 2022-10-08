using Rnd.Api.Client.Models.Basic.Game;

namespace Rnd.Api.Client.Controllers.Basic;

public class Games : Controller<GameModel, GameCreateModel, GameEditModel, GamesSelector>
{
    public Games(HttpClient client, string path) : base(client, path) { }

    protected override string Name => nameof(Games);
}