using Discord;
using Discord.Interactions;
using Discord.WebSocket;
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
        
        if (!IsExecutorGuide || IsUserValidGuide()) return;
        
        _socket.Interaction.RespondAsync(
            embed: EmbedView.Error($"Чтобы использовать функции ведущего, нужно обладать ролью `{GuidRole}`"),
            ephemeral: true).Start();

        throw new InvalidOperationException();
    }

    public const string GuidRole = "RnDGuid";

    public bool IsExecutorGuide => _player != null;
    public ulong ExecutorId => _socket.User.Id;
    public ulong PlayerId => _player?.Id ?? ExecutorId;

    public IQueryable<DataCharacter> DataCharacters => 
        _db.Characters
            .Where(c => c.PlayerId == PlayerId && (!IsExecutorGuide || c.GuideId == ExecutorId))
            .OrderByDescending(c => c.Selected);
    
    public IQueryable<DataCharacter> GuidedCharacters => 
        _db.Characters
            .Where(c => c.PlayerId != ExecutorId && c.GuideId == ExecutorId)
            .OrderByDescending(c => c.Selected);

    public async Task<List<AncorniaCharacter>> GetCharactersAsync() => 
        (await DataCharacters.ToListAsync())
        .Select(c => c.Character).ToList();
    
    public async Task<List<DataCharacter>> GetGuidedDataCharactersAsync() => await GuidedCharacters.ToListAsync();

    public async Task<List<string>> GetCharacterNamesAsync() => (await GetCharactersAsync()).Select(c => c.Name).ToList();

    public async Task<DataCharacter> GetDataCharacterAsync()
    {
        var dataCharacter = await DataCharacters.FirstOrDefaultAsync();

        var error = IsExecutorGuide
            ? "У вас нет доступа к персонажу выбранного игрока"
            : "У вас нет ни одного персонажа. Используйте **/character create**";
        
        if (dataCharacter == null) await _socket.Interaction.RespondAsync(
            embed: EmbedView.Error(error), 
            ephemeral: true);

        return dataCharacter ?? throw new InvalidOperationException();
    }

    public async Task<IUser?> GetGuideAsync()
    {
        var dataCharacter = await GetDataCharacterAsync();

        var guidId = dataCharacter.GuideId;

        return guidId == null 
            ? null 
            : _socket.Guild.Users.FirstOrDefault(u => u.Id == guidId);
    }

    public async Task<AncorniaCharacter> GetCharacterAsync() => (await GetDataCharacterAsync()).Character;

    public async Task AddCharacterAsync(AncorniaCharacter character, ulong? guidId)
    {
        var dataCharacter = new DataCharacter(PlayerId, _player?.Username ?? _socket.User.Username, character, 
            DateTime.Now, guidId ?? (IsExecutorGuide ? ExecutorId : null));

        _db.Characters.Add(dataCharacter);
        
        await _db.SaveChangesAsync();
    }
    
    public async Task UpdateCharacterAsync(AncorniaCharacter character)
    {
        var dataCharacter = await GetDataCharacterAsync();

        dataCharacter.Character = character;
        
        await _db.SaveChangesAsync();
    }

    public async Task UpdateGuideAsync(ulong? guid)
    {
        var dataCharacter = await GetDataCharacterAsync();

        dataCharacter.GuideId = guid;
        
        await _db.SaveChangesAsync();
    }

    public bool IsUserValidGuide()
    {
        var guildUser = (SocketGuildUser) _socket.User;

        return guildUser.Roles.Select(r => r.Name).Contains(GuidRole);
    }

    private readonly IUser? _player;
    private readonly DataContext _db;
    private readonly SocketInteractionContext _socket;
}