using Rnd.Api.Client.Models.Basic.Game;

namespace Rnd.Api.Client.Controllers.Basic;

public class GamesSelector : Selector<GameModel, GameFormModel>
{
    public GamesSelector(HttpClient client, Uri path, IController<GameModel, GameFormModel> controller) 
        : base(client, path, controller) { }
    
    public Members Members => new(Client, Path, true);
}