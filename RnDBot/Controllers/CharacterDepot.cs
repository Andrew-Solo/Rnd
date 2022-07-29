using Microsoft.EntityFrameworkCore;
using RnDBot.Data;
using RnDBot.Models.Character;

namespace RnDBot.Controllers;

public class CharacterDepot
{
    //TODO кеширование
    public CharacterDepot(DataContext db, ulong userId)
    {
        _db = db;
        _userId = userId;
    }

    public IQueryable<DataCharacter> DataCharacters => 
        _db.Characters
            .Where(c => c.UserId == _userId)
            .OrderByDescending(c => c.Selected);

    public async Task<List<AncorniaCharacter>> GetCharactersAsync() => 
        (await DataCharacters.ToListAsync())
        .Select(c => c.Character).ToList();

    public async Task<List<string>> GetCharacterNamesAsync() => (await GetCharactersAsync()).Select(c => c.Name).ToList();

    public async Task<DataCharacter> GetDataCharacterAsync() => 
        await DataCharacters.FirstOrDefaultAsync() 
        ?? throw new NotImplementedException();
    
    public async Task<AncorniaCharacter> GetCharacterAsync() => 
        (await GetDataCharacterAsync())?.Character 
        ?? throw new NotImplementedException();

    public async Task AddCharacterAsync(AncorniaCharacter character)
    {
        var dataCharacter = new DataCharacter(_userId, character, DateTime.Now);

        _db.Characters.Add(dataCharacter);
        
        await _db.SaveChangesAsync();
    }
    
    public async Task UpdateCharacterAsync(AncorniaCharacter character)
    {
        var dataCharacter = await GetDataCharacterAsync();

        dataCharacter.Character = character;
        
        await _db.SaveChangesAsync();
    }
    
    private readonly DataContext _db;
    private readonly ulong _userId;
}