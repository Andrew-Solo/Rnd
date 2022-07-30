using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using RnDBot.Data;
using RnDBot.Models.Character;
using RnDBot.Views;

namespace RnDBot.Controllers.Helpers;

public class CharacterDepot
{
    //TODO кеширование
    public CharacterDepot(DataContext db, SocketInteractionContext socket)
    {
        _db = db;
        _socket = socket;
    }

    public IQueryable<DataCharacter> DataCharacters => 
        _db.Characters
            .Where(c => c.UserId == UserId)
            .OrderByDescending(c => c.Selected);

    public async Task<List<AncorniaCharacter>> GetCharactersAsync() => 
        (await DataCharacters.ToListAsync())
        .Select(c => c.Character).ToList();

    public async Task<List<string>> GetCharacterNamesAsync() => (await GetCharactersAsync()).Select(c => c.Name).ToList();

    public async Task<DataCharacter> GetDataCharacterAsync()
    {
        var dataCharacter = await DataCharacters.FirstOrDefaultAsync();

        if (dataCharacter == null) await _socket.Interaction.RespondAsync(
            embed: EmbedView.Error("У вас нет ни одного персонажа. Используйте **/character create**"), 
            ephemeral: true);

        return dataCharacter ?? throw new InvalidOperationException();
    }

    public async Task<AncorniaCharacter> GetCharacterAsync() => (await GetDataCharacterAsync()).Character;

    public async Task AddCharacterAsync(AncorniaCharacter character)
    {
        var dataCharacter = new DataCharacter(UserId, character, DateTime.Now);

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
    private readonly SocketInteractionContext _socket;
    
    private ulong UserId => _socket.User.Id;
}