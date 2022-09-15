using Rnd.Api.Data;
using Rnd.Api.Localization;

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
        return CreateSimilar(entity).LoadNotNull(entity);
    }
    
    //TODO Нужно сделать эти фактори нормальными такими, наследуемыми и все такое
    protected virtual ICharacter CreateSimilar(Data.Entities.Character entity)
    {
        return entity.Module switch
        {
            nameof(Basic) => CreateBasic(entity),
            nameof(RndCore) => CreateRndCore(entity),
            _ => throw new ArgumentOutOfRangeException(nameof(entity.Module), entity.Module, 
                Lang.Exceptions.IStorableFactory.UnknownType)
        };
    }

    private ICharacter CreateBasic(IEntity entity)
    {
        return new Character(entity);
    }
    
    private ICharacter CreateRndCore(IEntity entity)
    {
        return new RndCore.Characters.Character(entity);
    }
}