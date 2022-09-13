using Rnd.Api.Data;

namespace Rnd.Api.Modules.Basic.Games;

public class GameFactory : IStorableFactory<Data.Entities.Game>
{
    public static Game Create(Data.Entities.Game entity)
    {
        throw new NotImplementedException();
    }
    
    public IStorable<Data.Entities.Game> CreateStorable(Data.Entities.Game entity)
    {
        throw new NotImplementedException();
    }
}