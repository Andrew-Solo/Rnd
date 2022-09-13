using Rnd.Api.Data;

namespace Rnd.Api.Modules.Basic.Characters;

public class CharacterFactory : IStorableFactory<Data.Entities.Character>
{
    public static ICharacter Create(Data.Entities.Character entity)
    {
        throw new NotImplementedException();
    }

    public IStorable<Data.Entities.Character> CreateStorable(Data.Entities.Character entity)
    {
        throw new NotImplementedException();
    }
}