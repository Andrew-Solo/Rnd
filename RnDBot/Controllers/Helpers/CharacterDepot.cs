using Discord;
using Discord.Interactions;
using Microsoft.EntityFrameworkCore;
using RnDBot.Data;
using RnDBot.Models.Character;
using RnDBot.Views;

namespace RnDBot.Controllers.Helpers;

public class CharacterDepot
{
    //TODO кеширование
    public CharacterDepot(DataContext db, SocketInteractionContext socket, IUser? player = null)
    {
        _db = db;
        _socket = socket;
        _player = player;
    }

    public bool IsExecutorGuid => _player != null;
    public ulong ExecutorId => _socket.User.Id;
    public ulong PlayerId => _player?.Id ?? ExecutorId;

    public IQueryable<DataCharacter> DataCharacters => 
        _db.Characters
            .Where(c => c.PlayerId == PlayerId && (!IsExecutorGuid || c.GuidId == ExecutorId))
            .OrderByDescending(c => c.Selected);

    public async Task<List<AncorniaCharacter>> GetCharactersAsync() => 
        (await DataCharacters.ToListAsync())
        .Select(c => c.Character).ToList();

    public async Task<List<string>> GetCharacterNamesAsync() => (await GetCharactersAsync()).Select(c => c.Name).ToList();

    public async Task<DataCharacter> GetDataCharacterAsync()
    {
        var dataCharacter = await DataCharacters.FirstOrDefaultAsync();

        var error = IsExecutorGuid
            ? "У вас нет доступа к персонажу выбранного игрока"
            : "У вас нет ни одного персонажа. Используйте **/character create**";
        
        if (dataCharacter == null) await _socket.Interaction.RespondAsync(
            embed: EmbedView.Error(error), 
            ephemeral: true);

        return dataCharacter ?? throw new InvalidOperationException();
    }

    public async Task<IUser?> GetGuidAsync()
    {
        var dataCharacter = await GetDataCharacterAsync();

        var guidId = dataCharacter.GuidId;

        return guidId == null 
            ? null 
            : _socket.Guild.Users.FirstOrDefault(u => u.Id == guidId);
    }

    public async Task<AncorniaCharacter> GetCharacterAsync() => (await GetDataCharacterAsync()).Character;

    public async Task AddCharacterAsync(AncorniaCharacter character, ulong? guidId)
    {
        var dataCharacter = new DataCharacter(PlayerId, character, DateTime.Now, guidId ?? (IsExecutorGuid ? ExecutorId : null));

        _db.Characters.Add(dataCharacter);
        
        await _db.SaveChangesAsync();
    }
    
    public async Task UpdateCharacterAsync(AncorniaCharacter character)
    {
        var dataCharacter = await GetDataCharacterAsync();

        dataCharacter.Character = character;
        
        await _db.SaveChangesAsync();
    }

    public async Task UpdateGuidAsync(ulong? guid)
    {
        var dataCharacter = await GetDataCharacterAsync();

        dataCharacter.GuidId = guid;
        
        await _db.SaveChangesAsync();
    }

    private readonly IUser? _player;
    private readonly DataContext _db;
    private readonly SocketInteractionContext _socket;
}