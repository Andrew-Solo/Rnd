using Rnd.Api.Data;

namespace Rnd.Api.Modules.Basic.Games;

public class GameFactory : IStorableFactory<Data.Entities.Game>
{
    public static Game Create(Data.Entities.Game entity)
    {
        var factory = new GameFactory();
        return (Game) factory.CreateStorable(entity);
    }
    
    public IStorable<Data.Entities.Game> CreateStorable(Data.Entities.Game entity)
    {
        var result = new Game(entity);
        result.Load(entity);
        return result;
    }
}