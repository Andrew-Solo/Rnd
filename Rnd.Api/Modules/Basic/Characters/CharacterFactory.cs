using Rnd.Api.Data;

namespace Rnd.Api.Modules.Basic.Characters;

public class CharacterFactory : IStorableFactory<Data.Entities.Character>
{
    public static ICharacter Create(Data.Entities.Character entity)
    {
        var factory = new CharacterFactory();
        return (ICharacter) factory.CreateStorable(entity);
    }

    public IStorable<Data.Entities.Character> CreateStorable(Data.Entities.Character entity)
    {
        var result = new Character(entity);
        result.Load(entity);
        return result;
    }
}